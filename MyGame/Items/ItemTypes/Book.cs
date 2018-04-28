using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Items.ItemTypes
{
    class Book : baseItems
    {
        public Book(Texture2D texture, string name, string type, string description)
        {
            Init();
            this.texture = texture;
            this.name = name;
            Description = description;
            Type = type;
        }

        public override IItems CreateCopy()
        {
            return new Book(texture, name, Type, Description);
        }

        protected override void SetButtons()
        {
            DPL.AddButton("Read", () => ShowInfo());
            DPL.AddButton("Drop", () => Drop());
            DPL.AddButton("Quit", () => quitMenu());
        }

    }
}
