using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Spells
{
    class Miracle : baseSpell
    {
        public Miracle(string name, Texture2D texture, int Heal, int LifeTime, Point middlePoint, int[,] array, int cost)
        {
            this.name = name;
            this.middlePoint = middlePoint;
            this.texture = texture;
            this.LifeTime = LifeTime;
            this.array = array;
            this.cost = cost;
            this.Heal = Heal;
        }

        public override void Cast(Vector2 position)
        {
            position = new Vector2(position.X - (32 * middlePoint.X), position.Y - (32 * middlePoint.Y));

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] != 0)
                    {
                        Global.spellCells.Add(new SpellCell(new Rectangle((int)position.X + (32 * i - middlePoint.X), (int)position.Y + (32 * j - middlePoint.X), 32, 32), texture, Damage, LifeTime));
                    }
                }
            }


            Settings._player.HealUp((int)(Heal * (1 + ((double)(Settings._player.Stats[Names.Faith] - 1)/10))));
        }
    }
}
