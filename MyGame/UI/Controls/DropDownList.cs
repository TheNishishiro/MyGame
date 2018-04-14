using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.UI
{
    class DropDownList : IUserInterface
    {
        public List<Button> Buttons;
        Vector2 refPosition;
        private int ids = 0;
        int longestTextSize = 0;

        public DropDownList()
        {
            Buttons = new List<Button>();
        }
        
        public void AddButton(string name, Action action)
        {
            Buttons.Add(new Button(name, action, ids));
            if (name.Length > longestTextSize)
                longestTextSize = name.Length;
            ids++;
        }

        public void RenameElement(int ElementID, string newText)
        {
            if (ElementID < Buttons.Count)
            {
                Buttons[ElementID].Rename(newText);
                if (newText.Length > longestTextSize)
                    longestTextSize = newText.Length;
            }
        }

        public void Update(Vector2 refPosition)
        {
            this.refPosition = refPosition;
        }

        public void Draw(ref SpriteBatch sb)
        {
            foreach(Button button in Buttons)
            {
                button.Draw(ref sb);
                button.Update(refPosition, longestTextSize*Settings.TextButtonScaling);
            }
        }
    }
}
