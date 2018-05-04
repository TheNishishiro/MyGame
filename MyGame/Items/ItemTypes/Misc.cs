using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Items.ItemTypes
{
    class Misc : baseItems
    {
        public Misc(string ID, Texture2D texture, string name, string type, string description)
        {
            Init();
            this.ID = ID;
            this.texture = texture;
            this.name = name;
            Description = description;
            Type = type;
        }

        public override IItems CreateCopy()
        {
            return new Misc(ID, texture, name, Type, Description);
        }

        protected override void SetButtons()
        {
            if (!InCrafting && !IsCraftingResult)
            {
                DPL.AddButton("Information", () => ShowInfo());
                DPL.AddButton("Drop", () => Drop());
                DPL.AddButton("Discard", () => Discard());
            }
            else if (InCrafting)
                DPL.AddButton("Take out", () => TakeFromCrafting());
            else if (IsCraftingResult)
                DPL.AddButton("Take out", () => TakeFromCraftingResult());

            DPL.AddButton("Exit", () => quitMenu());
        }
    }
}
