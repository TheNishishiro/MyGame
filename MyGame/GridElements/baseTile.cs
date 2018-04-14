using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.GridElements.Additions;
using NFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GridElements
{
    class baseTile : ITile
    {
        protected Texture2D texture;
        protected Vector2 position;
        public bool Walkable { get; set; }
        ITileAddition addition = null;

        public void Draw(ref SpriteBatch sb)
        {
            NDrawing.Draw(ref sb, texture, position, Color.White, Settings.tileLayer);
        }

        public void DrawAddition(ref SpriteBatch sb)
        {
            if (addition != null)
                addition.Draw(ref sb);
        }

        public void GenerateAddition()
        {
            if (Settings.rnd.Next(10) == 3)
            {
                int rnd = Settings.rnd.Next(4);
                if (rnd == 0)
                    AddAddition(new Rock(Textures.RockTexture, (int)position.X, (int)position.Y));
                else if (rnd == 1)
                    AddAddition(new Tree(Textures.TreeTexture, (int)position.X, (int)position.Y));
                else if (rnd == 2)
                    AddAddition(new Tree2(Textures.Tree2Texture, (int)position.X, (int)position.Y));
                else if (rnd == 3)
                    AddAddition(new GrassBatch(Textures.GrassBatchTexture, (int)position.X, (int)position.Y));
                Walkable = addition.Walkable;
            }
            
        }

        public void AddAddition(ITileAddition addition)
        {
            this.addition = addition;
        }
    }
}
