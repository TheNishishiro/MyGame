using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.UI;
using NFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GridElements
{
    class baseAddition : baseEntity, ITileAddition
    {
        protected Texture2D texture;
        protected float layerDepth = Settings.tileAdditionTopLayer;
        public bool Walkable { get; set; }
        public bool IsClickable = false;

        public virtual void Draw(ref SpriteBatch sb)
        {
            NDrawing.Draw(ref sb, texture, Position, color, layerDepth);
            if(IsClickable)
                Update(ref sb);
        }

        public virtual void Update(ref SpriteBatch sb)
        {
        }
    }
}
