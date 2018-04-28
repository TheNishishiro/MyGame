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

        public int Damage;
        
        public SpellCell(Rectangle Position, Texture2D texture, int Damage, int LifeTime)
        {
            this.texture = texture;
            this.Position = Position;
            this.LifeTime = LifeTime;
            this.Damage = Damage;
            if (this.Damage < 0)
                this.Damage = 0;
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
