using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Items.ItemTypes
{
    class Consumable : baseItems
    {
        public Consumable(string ID, Texture2D texture, string name, string type, string description, Dictionary<string, int> Attribiutes)
        {
            Init();
            this.ID = ID;
            this.texture = texture;
            this.name = name;
            Description = description;
            this.Attribiutes = Attribiutes;
            Type = type;

        }

        public override IItems CreateCopy()
        {
            return new Consumable(ID, texture, name, Type, Description, Attribiutes);
        }

        protected override void SetButtons()
        {
            if (!InCrafting && !IsCraftingResult)
            {
                DPL.AddButton("Consume", () => Use());
                DPL.AddButton("Drop", () => Drop());
                DPL.AddButton("Information", () => ShowInfo());
            }
            else if (InCrafting)
                DPL.AddButton("Take out", () => TakeFromCrafting());
            else if (IsCraftingResult)
                DPL.AddButton("Take out", () => TakeFromCraftingResult());

            DPL.AddButton("Exit", () => quitMenu());
        }

        public override void Use()
        {
            foreach(KeyValuePair<string,int> entry in Attribiutes)
                if(Settings._player.baseStats.ContainsKey(entry.Key))
                    Settings._player.baseStats[entry.Key] += entry.Value;

            Settings._player.Inventory.Remove(this);
        }
    }
}
