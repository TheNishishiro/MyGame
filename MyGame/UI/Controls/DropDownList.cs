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
        public Dictionary<string, Button> Buttons;
        Vector2 refPosition;
        private int ids = 0;

        public DropDownList()
        {
            Buttons = new Dictionary<string, Button>();
        }
        
        public void AddButton(string name, Action action)
        {
            Buttons.Add(name, new Button(name, action, ids));
            ids++;
        }

        public void Update(Vector2 refPosition)
        {
            this.refPosition = refPosition;
        }

        public void Draw(ref SpriteBatch sb)
        {
            foreach(KeyValuePair<string, Button> entry in Buttons)
            {
                Buttons[entry.Key].Draw(ref sb);
                Buttons[entry.Key].Update(refPosition);
            }
        }
    }
}
