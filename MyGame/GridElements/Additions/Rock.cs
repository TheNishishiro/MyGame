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
    class Rock : baseAddition
    {

        public Rock(Texture2D texture, int posX, int posY)
        {
            hp = 15;
            Position = new Vector2(posX, posY);
            this.texture = texture;
            bounds = new Rectangle(posX, posY, texture.Width, texture.Height);
            Walkable = false;
            IsClickable = true;
            FL = new List<FadingLabel>();
            cooldown = 60;
            resource = "stone";
            amount = 1;
        }

        protected override void SetButtons()
        {
            base.SetButtons();
            DPL.RenameElement(0, "Mine");
        }
    }
}
