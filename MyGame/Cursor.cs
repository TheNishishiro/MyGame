﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class Cursor
    {
        private Rectangle textureRec;
        public Rectangle bounds;
        public Texture2D texture;

        public Cursor(Texture2D texture)
        {
            this.texture = texture;
            bounds = new Rectangle(0, 0, 5, 5);
            textureRec = new Rectangle(0, 0, texture.Width, texture.Height); 
        }

        public void Update()
        {
            bounds.X = (Mouse.GetState().X - Game1.graphics.PreferredBackBufferWidth / 2) + (int)Settings._player.Position.X + 16;
            bounds.Y = (Mouse.GetState().Y - Game1.graphics.PreferredBackBufferHeight / 2) + (int)Settings._player.Position.Y - 48;
            textureRec.X = bounds.X;
            textureRec.Y = bounds.Y;
           
        }

        public void Draw(ref SpriteBatch sb)
        {
            //   sb.Draw(texture, textureRec, Color.White);
            NDrawing.Draw(ref sb, texture, textureRec, Color.White, Settings.UILayer + 0.001f);
        }
    }
}