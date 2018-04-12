using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.UI.Controls
{
    class Label : IUserInterface
    {
        protected string text;
        protected Vector2 position;
        protected Color color;

        public Label(string text, Vector2 position, Color color)
        {
            this.text = text;
            this.position = position;
            this.color = color;
        }

        public void Draw(ref SpriteBatch sb)
        {
            sb.DrawString(Settings.font, text, position, color);
        }
    }
}
