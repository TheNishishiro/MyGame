using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Creatures;
using MyGame.UI;
using MyGame.UI.Controls;
using NFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GridElements.Additions
{
    class Tree : baseAddition
    {
        private int cooldown = 60;
        private List<FadingLabel> FL = new List<FadingLabel>();

        public Tree(Texture2D texture, int posX, int posY)
        {
            baseStats[HP] = 10;
            Position = new Microsoft.Xna.Framework.Vector2(posX, posY);
            this.texture = texture;
            bounds = new Microsoft.Xna.Framework.Rectangle(posX, posY, texture.Width, texture.Height);
            Walkable = false;
            IsClickable = true;
            DPL = new UI.DropDownList();
            DPL.AddButton("Cut down", () => FightToggle());
            DPL.AddButton("Quit", () => quitMenu());
        }

        public override void Update(ref SpriteBatch sb)
        {
            menuManagment(ref sb);
            if(fighting && baseStats[HP] > 0 && NAction.Get_Distance_Between_Points(Position, Settings._player.GetPosition()) < Settings.GridSize * 2)
            {
                cooldown--;
                if(cooldown <= 0)
                {
                    baseStats[HP]--;
                    cooldown = 60;
                    FL.Add(new FadingLabel("+1 wood", Position, Color.White));
                }
            }
            if(baseStats[HP] <= 0)
            {
                color = Color.Red;
            }

            MenuControls.FadingLabelManager(ref sb, FL);
        }
    }
}
