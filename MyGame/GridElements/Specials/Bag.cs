using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Items;
using MyGame.Items.ItemTypes;
using MyGame.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GridElements.Specials
{
    class Bag : Addition
    {
        IItems item;

        public Bag(IItems item, Vector2 Position) : base(null,new Vector2(0,0),true,true,false,false, false, "Pick up", null, null, null, null, null)
        {
            this.item = item;
            this.texture = item.GetTexture();
            this.Position = Position;
            bounds = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
            action = () => PickUpItem();
        }

        private void PickUpItem()
        {
            Settings._player.Inventory.Add(item);
            MainUI.FL.Add(new UI.Controls.FadingLabel("Picked up item", Position, Color.White, 0));
            RemoveAdditionFromGrid();
        }
    }
}
