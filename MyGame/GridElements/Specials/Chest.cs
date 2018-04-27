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
    class Chest : Addition
    {
        int Gold;

        public Chest(Vector2 Position, Texture2D texture, Dictionary<string, int> lootChances, string buttonMessage, int Rarity, int Gold)
        {
            this.layerDepth = Settings.tileAdditionTopLayer;
            this.Gold = Gold;
            this.Rarity = Rarity;
            this.Position = Position;
            this.lootChances = lootChances;
            ButtonRename = buttonMessage;
            this.texture = texture;
            IsClickable = true;
            ButtonRename = "Open";
            bounds = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
            action = () => PickUpItem();
        }

        public override ITileAddition CreateCopy(Vector2 position)
        {
            bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            return new Chest(position, texture, lootChances, ButtonRename, Rarity, Gold);
        }

        private void PickUpItem()
        {
            if (NAction.Get_Distance_Between_Points(Position, Settings._player.GetPosition()) < Settings.GridSize * 2)
            {

                AddGold(Gold);

                foreach (KeyValuePair<string, int> entry in lootChances)
                {
                    if (Settings.rnd.Next(10000) <= entry.Value + (Settings._player.Stats[Names.Luck] - 1) * 10)
                    {
                        Settings.grid.map[(int)(Position.X / Settings.GridSize), (int)(Position.Y / Settings.GridSize)].AddAddition(
                            new Bag(
                            Textures.ItemTemplates[entry.Key].CreateCopy()
                            , new Vector2(Position.X, Position.Y)
                            ));
                    }
                }
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
