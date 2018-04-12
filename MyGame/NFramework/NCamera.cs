using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
                centre = new Vector2(RectangleToFollow.X + (RectangleToFollow.Width / 2) - (RoomWidth / 2), RectangleToFollow.Y + (RectangleToFollow.Height / 2) - (RoomHeight / 2));
                transform = Matrix.CreateScale(new Vector3(1, 1, 0)) *
                    Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0));
            }
        }

        public static void Camera_CreateViewport(Viewport viewport, int Width, int Height, int CameraID = 0)
        {
            _camera = new Camera(viewport, Width, Height);
        }
        public static void Camera_Use(ref SpriteBatch spriteBatch, int CameraID = 0)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack,
            BlendState.NonPremultiplied,
            null, null, null, null,
            _camera.transform);
        }
        public static void Camera_Bound(Rectangle RectangleToFollow, int CameraID = 0)
        {
            _camera.Update(RectangleToFollow);
        }
    }
}
