using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.UI.Controls
{
    class ScrollableText
    {
        List<string> textLines = new List<string>();

        int Size;
        int DisplayLines;
        private static int lineNum = 0;
        private static Button LineDown = new Button(Textures.UIArrowDown, () => SwitchLine(1));
        private static Button LineUp = new Button(Textures.UIArrowUp, () => SwitchLine(-1));

        public ScrollableText(string text, int Size, int DisplayLines)
        {
            this.Size = Size;
            this.DisplayLines = DisplayLines;
            string line = "";
            for (int i = 0, j = 0; i < text.Length; i++, j++)
            {
                line += text[i];
                if (text[i] == '\n')
                {
                    textLines.Add(line);
                    line = "";
                    j = 0;
                }
                if (j >= Size / 8 && text[i] == ' ')
                {
                    textLines.Add(line);
                    line = "";
                    j = 0;
                }
            }
            textLines.Add(line);
        }

        public void Draw(ref SpriteBatch sb, Vector2 Position, int ButtonPosWidth, float layer)
        {
            int newLineOffset = 40;
            int newLineStep = 20;
            int i = 0, j = 0;
            foreach (string line in textLines)
            {
                if (lineNum <= i && j < DisplayLines)
                {
                    sb.DrawString(Settings.font3, line.Replace("\t", "     "), new Vector2(Position.X, Position.Y + newLineOffset), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, layer += 0.005f);
                    newLineOffset += newLineStep;
                    j++;
                }
                i++;
            }
            if (textLines.Count > DisplayLines)
            {
                LineDown.Update(new Vector2(Position.X + Size - 130, Position.Y + 40 + (DisplayLines / 2) * newLineStep - 15), true, 5);
                LineDown.Draw(ref sb);
            }
            if (lineNum > 0)
            {
                LineUp.Update(new Vector2(Position.X + Size - 130, Position.Y + 40 + (DisplayLines / 2) * newLineStep - 35), true, 5);
                LineUp.Draw(ref sb);
            }
        }

        private static void SwitchLine(int i)
        {
            lineNum += i;
        }
    }
}
