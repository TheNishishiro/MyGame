using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.UI.Controls
{
    class FadingLabel : IUserInterface
    {
        protected string text;
        protected Vector2 position;
        public Color color;
        protected float speed;

        public FadingLabel(string text, Vector2 position, Color color, float speed = 0.5f)
        {
            this.text = text;
            this.position = position;
            this.color = color;
            this.speed = speed;
        }

        public void Draw(ref SpriteBatch sb, float layer = Settings.UILayer)
        {
            if (color.A > 0)
            {
                sb.DrawString(Settings.font, text, position, color, 0, new Vector2(0,0), 1, SpriteEffects.None, layer);
                position.Y-=0.5f;
                color.A--;
            }
        }
    }
}
