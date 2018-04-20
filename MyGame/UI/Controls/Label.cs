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

        public void UpdatePosition(Vector2 position)
        {
            this.position = position;
        }

        public void UpdateText(string text)
        {
            this.text = text;
        }

        public void Draw(ref SpriteBatch sb, float layer = 0)
        {
            sb.DrawString(Settings.font2, text, position, color, 0, new Vector2(0,0), 1, SpriteEffects.None, layer);
        }
    }
}
