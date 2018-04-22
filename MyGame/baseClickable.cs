using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.GridElements;
using MyGame.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class baseClickable
    {
        protected DropDownList DPL = null;
        protected string name = null;
        protected bool menuOpened = false;
        protected bool fighting = false;
        protected Rectangle bounds;
        public Vector2 Position;
        protected float layerDepth;

        protected void menuManagment(ref SpriteBatch sb)
        {
            if (MenuControls.MouseOver(bounds))
                MenuControls.SetMouseLayer(layerDepth);

            if (menuOpened == false && DPL == null && layerDepth >= Settings.highestLayerTarget)
            {

                menuOpened = MenuControls.MouseOverAndRMB(bounds);

                if (menuOpened)
                {
                    
                    DPL = new UI.DropDownList();
                    SetButtons();
                }
            }
            if (menuOpened)
            {
                DPL.Update(Position);
                DPL.Draw(ref sb);
            }
        }

        protected virtual void SetButtons()
        {
            DPL.AddButton("Engage", () => FightToggle());
            DPL.AddButton("Quit", () => quitMenu());
        }

        protected void FightToggle()
        {
            fighting = !fighting;
            quitMenu();
            Settings.highestLayerTarget = 0;
        }

        protected void quitMenu()
        {
            menuOpened = false;
            DPL = null;
        }

        
    }
}
