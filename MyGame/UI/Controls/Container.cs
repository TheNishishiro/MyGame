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
    class Container : IUserInterface
    {
        Rectangle Size;
        Button button;
        Texture2D background;
        public bool destroy = false;
        string title;
        string description;

        public Container(string title, string description)
        {
            this.Size = Size;
            button = new Button("Exit", () => Close(), 0);
            this.title = title;
            Size = new Rectangle(0, 0, 650, 400);
            for (int i = 0, j = 0; i < description.Length; i++, j++)
            {
                if(j >= Size.Width/8 && description[i-1] == ' ')
                {
                    description = description.Insert(i, "\n");
                    j = 0;
                }
            }
            this.description = description;
            background = NGraphics.Texture_CreatePixel(Game1._GraphicsDevice, Color.Gray);
        }

        public void Update(Vector2 Position)
        {
            Size.X = (int)Position.X;
            Size.Y = (int)Position.Y;

            button.Update(new Vector2(Size.X + Size.Width/2 - (float)(("Exit".Length * Settings.TextButtonScaling)*1.5), Size.Y + Size.Height - 40), "Exit".Length * Settings.TextButtonScaling);
        }

        public void Draw(ref SpriteBatch sb, float layer)
        {
            MenuControls.SetMouseLayer(layer);
            button.Draw(ref sb);
            NDrawing.Draw(ref sb, background, Size, Color.White, layer);
            sb.DrawString(Settings.font3, title, new Vector2(Size.X + Size.Width/2 - title.Length * Settings.TextButtonScaling, Size.Y + 10), Color.White, 0, new Vector2(0,0), 1, SpriteEffects.None, layer += 0.001f);
            sb.DrawString(Settings.font3, description, new Vector2(Size.X + 40, Size.Y + 50), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, layer += 0.005f);
        }

        public void Close()
        {
            destroy = true;
        }
    }
}
