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

        public Button(string name, Action buttonAction, int id)
        {
            action = buttonAction;
            position = new Rectangle(0, 0, 100, 16);
            this.name = name;
            this.id = id;
            color = Color.White;
        }

        public void Rename(string name)
        {
            this.name = name;
        }

        public void EditAction(Action action)
        {
            this.action = action;
        }

        public void Draw(ref SpriteBatch sb)
        {
            if (MenuControls.MouseOver(position))
                MenuControls.SetMouseLayer(layerDepth + 0.02f);
            NDrawing.Draw(ref sb, Textures.Button, position, Color.White, layerDepth + 0.019f);
           sb.DrawString(Settings.font, name, new Vector2(position.X, position.Y), color, 0, new Vector2(0,0), 1, SpriteEffects.None, layerDepth + 0.02f);
            if (Settings.cursor.bounds.Intersects(position))
                color = Color.Yellow;
            else
                color = Color.White;
            
        }

        private void testForClick()
        {
            if (Settings.cursor.bounds.Intersects(position) && Mouse.GetState().LeftButton == ButtonState.Pressed)
                ButtonClickMainAction();
        }

        public void Update(Vector2 refPosition, int width)
        {

            testForClick();
            position.Width = width;
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
