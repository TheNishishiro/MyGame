using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.UI;
using MyGame.UI.Controls;
using NFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GridElements
{
    class baseAddition : baseEntity, ITileAddition
    {
        protected Texture2D texture;
        protected float layerDepth = Settings.tileAdditionTopLayer;
        public bool Walkable { get; set; }
        public bool IsClickable = false;
        protected int? cooldown = null;
        protected List<FadingLabel> FL;
        protected string resource = null;
        protected int? amount = null;
        protected int? hp = null;

        public virtual void Draw(ref SpriteBatch sb)
        {
            NDrawing.Draw(ref sb, texture, Position, color, layerDepth);
            if(IsClickable)
                Update(ref sb, resource, amount);
        }

        public virtual void Update(ref SpriteBatch sb, string resource, int? amount)
        {
            menuManagment(ref sb);
            if (fighting && hp > 0 && NAction.Get_Distance_Between_Points(Position, Settings._player.GetPosition()) < Settings.GridSize * 2)
            {
                cooldown--;
                if (cooldown <= 0)
                {
                    hp--;
                    cooldown = 60;
                    FL.Add(new FadingLabel($"+{amount} {resource}", Position, Color.White));
                }
            }
            if(hp <= 0)
            {
                Settings.grid.map[(int)(Position.X / Settings.GridSize), (int)(Position.Y / Settings.GridSize)].Walkable = true;
                Settings.grid.map[(int)(Position.X / Settings.GridSize), (int)(Position.Y / Settings.GridSize)].AddAddition(null);
            }

            MenuControls.FadingLabelManager(ref sb, FL);
        }
    }
}
