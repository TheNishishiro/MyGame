using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.GridElements;
using MyGame.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class Settings
    {
        public const float
            entityLayer = 0.5f,
            tileLayer = 0.05f,
            UILayer = 0.9f,
            tileAdditionTopLayer = 0.65f,
            tileAdditionBottomLayer = 0.4f;

        public const int GridSize = 32;
        public static int RenderDistance = 20;
        public const int WorldSizeBlocks = 1000;
        public const int WorldSizePixels = WorldSizeBlocks * 32;

        public static Random rnd = new Random();

        public static Cursor cursor;
        public static SpriteFont font;
        public static Grid grid;
        public static Player _player;

        public static bool TestRenderBounds(Vector2 entity, Vector2 player)
        {
            return entity.X >= player.X - RenderDistance * 32 &&
                    entity.X <= player.X + RenderDistance * 32 &&
                    entity.Y >= player.Y - RenderDistance * 32 &&
                    entity.Y <= player.Y + RenderDistance * 32;
        }
    }
}
