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
                    AddAddition(new Rock(Game1._Content.Load<Texture2D>("rock"), (int)position.X, (int)position.Y));
                else if (rnd == 1)
                    AddAddition(new Tree(Game1._Content.Load<Texture2D>("tree"), (int)position.X, (int)position.Y));
                else if (rnd == 2)
                    AddAddition(new Tree2(Game1._Content.Load<Texture2D>("tree2"), (int)position.X, (int)position.Y));
                else if (rnd == 3)
                    AddAddition(new GrassBatch(Game1._Content.Load<Texture2D>("grassBatch"), (int)position.X, (int)position.Y));
                Walkable = addition.Walkable;
                
            }
            
        }

        public void AddAddition(ITileAddition addition)
        {
            this.addition = addition;
        }
    }
}
