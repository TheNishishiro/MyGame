using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.GridElements.Specials;
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
        List<ITileAddition> addition = null;

        public void Draw(ref SpriteBatch sb)
        {
            NDrawing.Draw(ref sb, texture, position, Color.White, Settings.tileLayer);
        }

        public void DrawAddition(ref SpriteBatch sb)
        {
            if (addition != null)
            {
                for (int i = 0; i < addition.Count; i++)
                {
                    if (i < addition.Count)
                        addition[i].Draw(ref sb);
                }
            }
        }

        public void GenerateAddition()
        {
            if (addition == null)
                addition = new List<ITileAddition>();
            if (Settings.rnd.Next(10) == 3)
            {
                AddAddition(AdditionFactory.CreateAddition(position));
                Walkable = addition[addition.Count - 1].Walkable;
            }
        }

        public void SetAddition(ITileAddition addition)
        {
            this.addition = new List<ITileAddition>();
            Walkable = addition.Walkable;
            this.addition.Add(addition);
        }

        public void AddAddition(ITileAddition addition)
        {
            this.addition.Add(addition);
        }

        public void RemoveAddition(ITileAddition addition)
        {
            this.addition.Remove(addition);
        }
    }
}
