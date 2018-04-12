using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GridElements.Additions
{
    class Tree2 : baseAddition
    {
        public Tree2(Texture2D texture, int posX, int posY)
        {
            Position = new Microsoft.Xna.Framework.Vector2(posX, posY);
            this.texture = texture;
            Walkable = false;
        }

        public override void Draw(ref SpriteBatch sb)
        {
            NDrawing.Draw(ref sb, texture, new Vector2(Position.X, Position.Y-32), Color.White, layerDepth + 0.001f);
        }
    }
}
