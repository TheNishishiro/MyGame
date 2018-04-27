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
        ScrollableText ST = null;

        public Container(string title)
        {
            button = new Button("Exit", () => Close(), 0);
            this.title = title;
            Size = new Rectangle(0, 0, 650, 400);
            background = Textures.UIMessageBoxTexture;
        }

        public Container(string title, string Description)
        {
            button = new Button("Exit", () => Close(), 0);
            this.title = title;
            Size = new Rectangle(0, 0, 650, 400);
            background = Textures.UIMessageBoxTexture;
            SetScrollableText(Description, 16);
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
            sb.DrawString(Settings.font3, title, new Vector2(Size.X + 15, Size.Y + 15), Color.White, 0, new Vector2(0,0), 1, SpriteEffects.None, layer += 0.001f);
            if(ST != null)
                ST.Draw(ref sb, new Vector2(Size.X + 40, Size.Y), Size.Width - 130, layer);
        }

        public void SetScrollableText(string text, int displayLines)
        {
            ST = new ScrollableText(text, Size.Width, displayLines);
        }

        public void Close()
        {
            destroy = true;
        }
    }
}
