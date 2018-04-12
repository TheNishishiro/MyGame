using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.UI
{
    class ProgresBar : IUserInterface
    {
        Texture2D background, foreground;
        Rectangle Size;
        Rectangle Size1, Size2;

        public ProgresBar(Color backgroundColor, Color foregroundColor, Rectangle size)
        {
            background = NFramework.NGraphics.Texture_CreatePixel(Game1._GraphicsDevice, backgroundColor);
            foreground = NFramework.NGraphics.Texture_CreatePixel(Game1._GraphicsDevice, foregroundColor);
            Size = size;
        }

        public void Update(float current, float max, Vector2 position)
        {
            Size1 = new Rectangle((int)position.X, (int)position.Y, Size.Width, Size.Height);
            Size2 = new Rectangle((int)position.X, (int)position.Y, (int)(Size.Width * (current / max)), Size.Height);
        }

        public void Draw(ref SpriteBatch sb)
        {
            //  sb.Draw(background, Size1, Color.White);
            //  sb.Draw(foreground, Size2, Color.White);
            NDrawing.Draw(ref sb, background, Size1, Color.White, Settings.UILayer - 0.001f);
            NDrawing.Draw(ref sb, foreground, Size2, Color.White, Settings.UILayer);
        }
    }
}
