using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.UI
{
    class MenuControls
    {
        public static bool MouseOver(Rectangle bounds)
        {
            if (Settings.cursor.bounds.Intersects(bounds))
                return true;
            return false;
        }

        public static bool MouseOverAndRMB(Rectangle bounds)
        {
            if (Settings.cursor.bounds.Intersects(bounds) && Mouse.GetState().RightButton == ButtonState.Pressed)
                return true;
            return false;
        }

        public static bool MouseOver(Rectangle bounds, float layer)
        {
            if (Settings.cursor.bounds.Intersects(bounds) && Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                if (SetMouseLayer(layer))
                    return true;
                else
                    return false;
            }
            return false;
        }

        public static bool SetMouseLayer(float layer)
        {
            if (layer >= Settings.highestLayerTarget)
            {
                Settings.highestLayerTarget = layer;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void FadingLabelManager(ref SpriteBatch sb, List<FadingLabel> FL)
        {
            if(FL != null)
                for (int i = 0; i < FL.Count; i++)
                {
                    FL[i].Draw(ref sb);
                    if (FL[i].color.A <= 0)
                    {
                        FL.RemoveAt(i);
                        i--;
                    }
                }
        }
    }
}
