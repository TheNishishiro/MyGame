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
        Texture2D background, foreground, border = null;
        Rectangle Size;
        Rectangle Size1, Size2;
        Color color;

        public ProgresBar(Color backgroundColor, Color foregroundColor, Rectangle size, Texture2D border = null)
        {
            background = NFramework.NGraphics.Texture_CreatePixel(Game1._GraphicsDevice, backgroundColor);
            foreground = NFramework.NGraphics.Texture_CreatePixel(Game1._GraphicsDevice, foregroundColor);
            this.border = border;
            Size = size;
            color = Color.White;
        }

        public ProgresBar(Texture2D backgroundTexture, Texture2D foregroundTexture, Rectangle size, Texture2D border = null)
        {
            background = backgroundTexture;
            foreground = foregroundTexture;
            this.border = border;
            Size = size;
            color = Color.White;
        }

        public ProgresBar(Texture2D backgroundTexture, Texture2D foregroundTexture, Rectangle size, Color barColor, Texture2D border = null)
        {
            background = backgroundTexture;
            foreground = foregroundTexture;
            this.border = border;
            Size = size;
            color = barColor;
        }

        public void Update(float current, float max, Vector2 position)
        {
            Size1 = new Rectangle((int)position.X, (int)position.Y-16, Size.Width, Size.Height);
            Size2 = new Rectangle((int)position.X, (int)position.Y-16, (int)(Size.Width * (current / max)), Size.Height);
        }

        public void Draw(ref SpriteBatch sb, float layer = Settings.UILayer)
        {
            NDrawing.Draw(ref sb, background, Size1, Color.White, layer - 0.00001f);
            NDrawing.Draw(ref sb, foreground, Size2, color, layer);
            if(border != null)
                NDrawing.Draw(ref sb, border, Size1, Color.White, layer + 0.00001f);
        }
    }
}
