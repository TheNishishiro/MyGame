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
        public static int CreatureLimit = 100;
        public const int RenderDistance = 20;
        public const int WorldSizeBlocks = 1000;
        public const int WorldSizePixels = WorldSizeBlocks * 32;

        public static Random rnd = new Random();

        public static Cursor cursor;
        public static SpriteFont font;
        public static Grid grid;
        public static Player _player;

        public static int TextButtonScaling = 9;

        public static bool TestRenderBounds(Vector2 entity, Vector2 player, int RenderDistance = RenderDistance)
        {
            return entity.X >= player.X - RenderDistance * 32 &&
                    entity.X <= player.X + RenderDistance * 32 &&
                    entity.Y >= player.Y - RenderDistance * 32 &&
                    entity.Y <= player.Y + RenderDistance * 32;
        }

    }

    class Textures
    {
        public static Texture2D RockTexture;
        public static Texture2D TreeTexture;
        public static Texture2D Tree2Texture;
        public static Texture2D GrassBatchTexture;
        public static Texture2D BloodStainTexture;
        public static Texture2D WolfTexture;
        public static Texture2D RatTexture;
        public static Texture2D Button;

        public static void LoadTextures()
        {
            RockTexture = Game1._Content.Load<Texture2D>("rock");
            TreeTexture = Game1._Content.Load<Texture2D>("tree");
            Tree2Texture = Game1._Content.Load<Texture2D>("tree2");
            GrassBatchTexture = Game1._Content.Load<Texture2D>("grassBatch");
            BloodStainTexture = Game1._Content.Load<Texture2D>("BloodStain");
            WolfTexture = Game1._Content.Load<Texture2D>("wolf");
            RatTexture = Game1._Content.Load<Texture2D>("rat");
            Button = Game1._Content.Load<Texture2D>("button");
        }
    }
}
