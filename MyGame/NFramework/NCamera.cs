using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFramework
{
    class NCamera
    {
        static Camera _camera;
        static AdvencedCamera _AdvencedCamera;
        public static int CameraXOffset = -150;
        public static int CameraYOffset = 0;

        public class Camera
        {
            public Matrix transform;
            Viewport view;
            Vector2 centre;
            int RoomWidth, RoomHeight;

            public Camera(Viewport newView, int newWidth, int newHeight)
            {
                view = newView; RoomWidth = newWidth; RoomHeight = newHeight;
            }

            public void Update(Rectangle RectangleToFollow)
            {
                centre = new Vector2(RectangleToFollow.X + (RectangleToFollow.Width / 2) - (RoomWidth / 2) + CameraXOffset, RectangleToFollow.Y+64 + (RectangleToFollow.Height / 2) - (RoomHeight / 2) + CameraYOffset);
                transform = Matrix.CreateScale(new Vector3(1, 1, 0)) *
                    Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0));
            }
        }

        public static void Camera_CreateViewport(Viewport viewport, int Width, int Height)
        {
            _camera = new Camera(viewport, Width, Height);
        }
        public static void Camera_Use(ref SpriteBatch spriteBatch, SpriteSortMode srm)
        {
            spriteBatch.Begin(srm,
            BlendState.NonPremultiplied,
            null, null, null, null,
            _camera.transform);
        }
        public static void Camera_Bound(Rectangle RectangleToFollow)
        {
            _camera.Update(RectangleToFollow);
        }


        class AdvencedCamera
        {
            public Matrix transform;
            Vector2 centre;
            Viewport viewport;

            public float zoom = 1;
            public float rotation = 0;

            public AdvencedCamera(Viewport newView)
            {
                viewport = newView;
            }

            public void UpdateCamera(Vector2 position)
            {
                centre = position;
                transform = Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0)) *
                    Matrix.CreateRotationZ(rotation) *
                    Matrix.CreateScale(new Vector3(zoom, zoom, 0)) *
                    Matrix.CreateTranslation(new Vector3(viewport.Width / 2, viewport.Height / 2, 0));
            }
        }

        public static void AdvencedCamera_Create(Viewport viewport)
        {
            _AdvencedCamera = new AdvencedCamera(viewport);
        }
        public static void AdvencedCamera_Use(SpriteBatch spriteBatch, SpriteSortMode srm)
        {
            spriteBatch.Begin(srm,
            BlendState.NonPremultiplied,
            null, null, null, null,
            _AdvencedCamera.transform);
        }
        public static bool IsKeyDown(Keys Button)
        {
            if (Keyboard.GetState().IsKeyDown(Button))
                return true;
            return false;
        }
        public static void AdvencedCamera_Controls(Vector2 position, Keys RotateButtonLeft, Keys RotateButtonRight, Keys ZoomInButton, Keys ZoomOutButton, float RotateVelocity = 0.01f, float ZoomVelocity = 0.01f, float ZoomInLimit = 0, float zoomOutLimit = 0)
        {
            _AdvencedCamera.UpdateCamera(position);
            if (IsKeyDown(RotateButtonLeft))
                _AdvencedCamera.rotation -= RotateVelocity;
            if (IsKeyDown(RotateButtonRight))
                _AdvencedCamera.rotation += RotateVelocity;

            if (IsKeyDown(ZoomInButton) && (ZoomInLimit == 0 || (_AdvencedCamera.zoom < ZoomInLimit)))
                _AdvencedCamera.zoom += ZoomVelocity;
            if (IsKeyDown(ZoomOutButton) && _AdvencedCamera.zoom > 0.1f && (zoomOutLimit == 0 || (_AdvencedCamera.zoom > zoomOutLimit)))
                _AdvencedCamera.zoom -= ZoomVelocity;


        }
        public static void AdvencedCameta_Reset(bool ResetRotation, bool ResetZoom, int CameraID = 0)
        {
            if (ResetRotation == true)
                _AdvencedCamera.rotation = 0;
            if (ResetZoom == true)
                _AdvencedCamera.zoom = 1;
        }
    }
}
