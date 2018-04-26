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
        static int lineNum = 0;
        List<string> description = new List<string>();
        private static Button LineDown = new Button(Textures.UIArrowDown, () => SwitchLine(1));
        private static Button LineUp = new Button(Textures.UIArrowUp, () => SwitchLine(-1));

        public Container(string title, string description)
        {
            button = new Button("Exit", () => Close(), 0);
            this.title = title;
            Size = new Rectangle(0, 0, 650, 400);
            string line = "";
            for (int i = 0, j = 0; i < description.Length; i++, j++)
            {
                line += description[i];
                if (description[i] == '\n')
                {
                    this.description.Add(line);
                    line = "";
                    j = 0;
                }
                if (j >= Size.Width/8 && description[i] == ' ')
                {
                    this.description.Add(line);
                    line = "";
                    j = 0;
                }
            }
            this.description.Add(line);
            background = Textures.UIMessageBoxTexture;
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
            int newLineOffset = 40;
            int newLineStep = 20;
            int i = 0, j = 0;
            foreach (string line in description)
            {
                if (lineNum <= i && j < 16)
                {
                    sb.DrawString(Settings.font3, line.Replace("\t", "     "), new Vector2(Size.X + 40, Size.Y + newLineOffset), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, layer += 0.005f);
                    newLineOffset += newLineStep;
                    j++;
                }
                i++;
            }
            if(description.Count > 16)
            {
                LineDown.Update(new Vector2(Size.X + Size.Width - 80, Size.Y + Size.Width/2 - 15), true, 5);
                LineDown.Draw(ref sb);
            }
            if(lineNum > 0)
            {
                LineUp.Update(new Vector2(Size.X + Size.Width - 80, Size.Y + Size.Width / 2 - 35), true, 5);
                LineUp.Draw(ref sb);
            }
        }

        public void Close()
        {
            destroy = true;
        }

        private static void SwitchLine(int i)
        {
            lineNum += i;
        }
    }
}
