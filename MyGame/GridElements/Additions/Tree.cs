using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Creatures;
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
    class Tree : baseAddition
    {

        public Tree(Texture2D texture, int posX, int posY)
        {
            hp = 7;
            Position = new Microsoft.Xna.Framework.Vector2(posX, posY);
            this.texture = texture;
            bounds = new Microsoft.Xna.Framework.Rectangle(posX, posY, texture.Width, texture.Height);
            Walkable = false;
            IsClickable = true;
            FL = new List<FadingLabel>();
            cooldown = 60;
            resource = "wood";
            amount = 1;
        }
    }
}
