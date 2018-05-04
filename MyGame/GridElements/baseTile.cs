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
                float j = 0;
                for (int i = 0; i < addition.Count; i++)
                {
                    if (i < addition.Count)
                    {
                        addition[i].Draw(ref sb, j);
                        j += 0.00001f;
                    }
                }
            }
        }

        public Texture2D GetTexture()
        {
            return texture;
        }

        public void GenerateAddition(string biom)
        {
            if (addition == null)
                addition = new List<ITileAddition>();
            if (Settings.rnd.Next(10) == 3)
            {
                if (biom == "")
                {
                    AddAddition(AdditionFactory.CreateAddition(position));
                    if (addition[addition.Count - 1] != null)
                        Walkable = addition[addition.Count - 1].Walkable;
                    else
                        addition.RemoveAt(addition.Count - 1);
                }
                else
                {
                    AddAddition(AdditionFactory.CreateAddition(position, biom));
                    if (addition[addition.Count - 1] != null)
                        Walkable = addition[addition.Count - 1].Walkable;
                    else
                        addition.RemoveAt(addition.Count - 1);
                }
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
            if (this.addition == null)
                this.addition = new List<ITileAddition>();
            this.addition.Add(addition);
        }

        public void RemoveAddition(ITileAddition addition)
        {
            this.addition.Remove(addition);
        }
        public void RemoveAdditions()
        {
            addition = new List<ITileAddition>();
        }

    }
}
