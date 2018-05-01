using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Spells
{
    class SpellCell
    {
        public Rectangle Position;
        Texture2D texture;
        int LifeTime;

        public Dictionary<string, int> Damage;
        public bool createdByPlayer;
        
        public SpellCell(Rectangle Position, Texture2D texture, Dictionary<string, int> Damage, int LifeTime, bool createdByPlayer = true)
        {
            this.createdByPlayer = createdByPlayer;
            this.texture = texture;
            this.Position = Position;
            this.LifeTime = LifeTime;
            this.Damage = Damage;
        }

        public void Update()
        {
            if (LifeTime <= 0)
                Destroy();

            LifeTime--;
        }

        private void Destroy()
        {
            Global.spellCells.Remove(this);
        }

        public void Draw(ref SpriteBatch sb)
        {
            NDrawing.Draw(ref sb, texture, Position, Color.White, Settings.tileAdditionBottomLayer);
        }
    }
}
