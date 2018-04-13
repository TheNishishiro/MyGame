using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.UI;
using MyGame.UI.Controls;
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
            hp = 15;
            Position = new Microsoft.Xna.Framework.Vector2(posX, posY);
            this.texture = texture;
            bounds = new Microsoft.Xna.Framework.Rectangle(posX, posY, texture.Width, texture.Height);
            Walkable = false;
            IsClickable = true;
            FL = new List<FadingLabel>();
            cooldown = 60;
        }

        public override void Draw(ref SpriteBatch sb)
        {
            NDrawing.Draw(ref sb, texture, new Vector2(Position.X, Position.Y-32), Color.White, layerDepth + 0.001f);
            if (IsClickable)
                base.Update(ref sb, "wood", 1);
        }

    }
}
