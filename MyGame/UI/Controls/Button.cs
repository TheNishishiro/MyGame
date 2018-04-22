using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.UI
{
    class Button
    {
        Rectangle position;
        string name;
        Action action;
        private int id;
        Color color;
        float layerDepth = Settings.MainUILayer;
        Texture2D background;

        public Button(string name, Action buttonAction, int id)
        {
            action = buttonAction;
            position = new Rectangle(0, 0, 100, 16);
            this.name = name;
            this.id = id;
            color = Color.White;
            background = Textures.Button;
        }

        public Button(Texture2D background, Action buttonAction)
        {
            action = buttonAction;
            position = new Rectangle(0, 0, background.Width, background.Height);
            name = "";
            id = 0;
            color = Color.White;
            this.background = background;
        }

        public void Rename(string name)
        {
            this.name = name;
        }

        public void EditAction(Action action)
        {
            this.action = action;
        }

        public void Draw(ref SpriteBatch sb, bool TextCenter = false)
        {
            if (MenuControls.MouseOver(position))
                MenuControls.SetMouseLayer(layerDepth + 0.02f);
            

            NDrawing.Draw(ref sb, background, position, Color.White, layerDepth + 0.019f);
            Vector2 textPosition;
            if (TextCenter)
                textPosition = new Vector2(position.X + position.Width/2 - 
                    name.Length/2 * Settings.TextButtonScaling, position.Y);
            else
                textPosition = new Vector2(position.X, position.Y);

            sb.DrawString(Settings.font, name, textPosition, color, 0, new Vector2(0, 0), 1, SpriteEffects.None, layerDepth + 0.02f);
            if (Settings.cursor.bounds.Intersects(position))
                color = Color.Yellow;
            else
                color = Color.White;
            
        }

        bool pressed = false;
        private void testForClick()
        {
            if (Settings.cursor.bounds.Intersects(position) && Mouse.GetState().LeftButton == ButtonState.Pressed && pressed == false)
            {
                ButtonClickMainAction();
                pressed = true;
            }
            else if(Mouse.GetState().LeftButton == ButtonState.Released && pressed == true)
            {
                pressed = false;
            }
        }

        public void Update(Vector2 refPosition, int width)
        {

            testForClick();
            position.Width = width;
            position.X = (int)refPosition.X + 32;
            position.Y = (int)refPosition.Y + (16 * id);
        }

        public void Update(Vector2 refPosition)
        {

            testForClick();
            position.X = (int)refPosition.X + 32;
            position.Y = (int)refPosition.Y + (16 * id);
        }

        public void Update()
        {
            testForClick();
        }

        public void ButtonClickMainAction()
        {
            action();
        }

        public void ButtonClick(Action action)
        {
            action();
        }
    }
}
