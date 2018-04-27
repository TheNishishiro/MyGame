using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Items;
using MyGame.Items.ItemTypes;
using MyGame.UI;
using MyGame.UI.Controls;
using NFramework;
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

        public Bag(IItems item, Vector2 Position)
        {
            layerDepth = Settings.tileAdditionBottomLayer;
            IsClickable = true;
            ButtonRename = "Pick up";
            this.item = item;
            this.texture = item.GetTexture();
            this.Position = Position;
            bounds = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
            action = () => PickUpItem();
        }

        public override ITileAddition CreateCopy(Vector2 position)
        {
            bounds = new Rectangle((int)position.X, (int)position.Y, 32, 32);
            return new Bag(item, position);
        }

        private void PickUpItem()
        {
            if (NAction.Get_Distance_Between_Points(Position, Settings._player.GetPosition()) < Settings.GridSize * 2)
            {
                Settings._player.Inventory.Add(item);
                MainUI.FL.Add(new UI.Controls.FadingLabel("Picked up item", Position, Color.White, 0));
                RemoveAdditionFromGrid();
            }
            else
            {
                quitMenu();
                MainUI.FL.Add(new FadingLabel("Too far.", Position, Color.Red));
            }

        }
    }
}
