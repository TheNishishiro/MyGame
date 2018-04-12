using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.GridElements.Additions;
using MyGame.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class baseEntity
    {
        public const string
            HP = "HP",
            HP_max = "HP_MAX",
            Damage = "Damage",
            Level = "Level",
            Exp = "Exp",
            Exp_max = "EXP_MAX",
            AttackSpeed = "Attack Speed";

        protected Dictionary<string,int> baseStats = new Dictionary<string, int>();
        public Vector2 Position;
        protected Rectangle bounds;
        protected Color color = Color.White;
        protected bool menuOpened = false;
        protected bool fighting = false;
        protected ProgresBar healthBar;
        protected DropDownList DPL;

        protected void menuManagment(ref SpriteBatch sb)
        {
            if (menuOpened == false)
                menuOpened = MenuControls.MouseOver(bounds);
            if (menuOpened)
            {
                DPL.Update(Position);
                DPL.Draw(ref sb);
            }
        }

        protected void FightToggle()
        {
            fighting = !fighting;
            quitMenu();
        }

        protected void quitMenu()
        {
            menuOpened = false;
        }

        protected bool isWalkable(float x, float y)
        {
            return Settings.grid.map[(int)(x / Settings.GridSize), (int)(y / Settings.GridSize)].Walkable;
        }

        protected void SetWalkable(bool walkable)
        {
            Settings.grid.map[(int)(Position.X / Settings.GridSize), (int)(Position.Y / Settings.GridSize)].Walkable = walkable;
        }
    }
}
