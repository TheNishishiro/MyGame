using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Items.ItemTypes
{
    class Scroll : baseItems
    {
        string spellID;

        public Scroll(Texture2D texture, string spellID, string name, string type, string description)
        {
            Init();
            this.texture = texture;
            this.name = name;
            Description = description;
            Type = type;
            this.spellID = spellID;
            SkillType = spellID; // I don't want to create new variable just for it
        }

        public override IItems CreateCopy()
        {
            return new Scroll(texture, SkillType, name, Type, Description);
        }

        protected override void SetButtons()
        {
            DPL.AddButton("Information", () => ShowInfo());

            DPL.AddButton("Assign to hotkey 1", () => AssignHotkey(0));
            DPL.AddButton("Assign to hotkey 2", () => AssignHotkey(1));
            DPL.AddButton("Assign to hotkey 3", () => AssignHotkey(2));
            DPL.AddButton("Assign to hotkey 4", () => AssignHotkey(3));
            DPL.AddButton("Assign to hotkey 5", () => AssignHotkey(4));
            DPL.AddButton("Assign to hotkey 6", () => AssignHotkey(5));
            DPL.AddButton("Assign to hotkey 7", () => AssignHotkey(6));
            DPL.AddButton("Assign to hotkey 8", () => AssignHotkey(7));
            DPL.AddButton("Assign to hotkey 9", () => AssignHotkey(8));
            DPL.AddButton("Assign to hotkey 10", () => AssignHotkey(9));
            DPL.AddButton("Assign to hotkey 11", () => AssignHotkey(10));
            DPL.AddButton("Assign to hotkey 12", () => AssignHotkey(11));

            DPL.AddButton("Exit", () => quitMenu());
        }

        private void AssignHotkey(int id)
        {
            Settings._player.Spells[id] = Textures.SpellTemplates[spellID].CreateCopy();
            quitMenu();
        }
    }
}
