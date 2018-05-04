using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GridElements.Specials
{
    class AnyAddition : Addition
    {
        public AnyAddition(Texture2D texture, Vector2 Position, int Rarity = 1000, string biom = Names.BiomGrass,
            bool walkable = false, bool clickable = false, string harvestID = null, bool CreatesFloatingText = false, bool IsTimeLimited = false, bool IsOnTop = false,
            string ButtonRename = null, int? HP = null, int? UseCooldown = null, string resource = null, int? amount = null, Action action = null)
        {
            this.biom = biom;
            this.texture = texture;
            this.Rarity = Rarity;
            this.harvestID = harvestID;
            this.Position = new Vector2(Position.X, Position.Y);
            bounds = new Rectangle((int)Position.X, (int)Position.Y, 32, 32);
            Walkable = walkable;
            IsClickable = clickable;
            this.CreatesFloatingText = CreatesFloatingText;
            this.IsOnTop = IsOnTop;
            if (IsOnTop)
                layerDepth = Settings.tileAdditionTopLayer;
            else
                layerDepth = Settings.tileAdditionBottomLayer;
            if (CreatesFloatingText)
                FL = new List<FadingLabel>();
            hp = HP;
            this.resource = resource;
            this.amount = amount;
            cooldown = UseCooldown;
            this.ButtonRename = ButtonRename;
            this.action = action;
            if (action != null)
            {
                Console.WriteLine(action.Method.Module);
            }
            this.IsTimeLimited = IsTimeLimited;

            try
            {
                if (texture.Height > Settings.GridSize || texture.Width > Settings.GridSize)
                    layerDepth += 0.001f;
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Couldn't load addition texture");
            }
        }

        protected override void SetButtons()
        {
            if(harvestID != null)
                DPL.AddButton("Harvest", () => harvest());
            if (ButtonRename != null)
                DPL.AddButton(ButtonRename, () => FightToggle());
            DPL.AddButton("Quit", () => quitMenu());
        }

        private void harvest()
        {
            if(Textures.ItemTemplates.ContainsKey(harvestID))
                Settings._player.Inventory.Add(Textures.ItemTemplates[harvestID]);
            harvestID = null;
            quitMenu();
        }
    }
}
