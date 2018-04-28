using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class FightingManager
    {
        public ICreature enemy1, enemy2;
        private float Turn1 = 0, Turn2 = 0;

        public FightingManager(ICreature creature1, ICreature creature2)
        {
            enemy1 = creature1;
            enemy2 = creature2;
        }

        public void Evaluate()
        {
            Turn1--;
            Turn2--;

            if (NFramework.NAction.Get_Distance_Between_Points(enemy1.GetPosition(), enemy2.GetPosition()) < 2 * Settings.GridSize)
            {
                if (Turn1 <= 0)
                {
                    enemy1.TakeDamage(enemy2.DealDamage());
                    Turn1 = enemy2.GetAttackSpeed();
                }
                if (Turn2 <= 0)
                {
                    enemy2.TakeDamage(enemy1.DealDamage());
                    Turn2 = enemy1.GetAttackSpeed();
                }
            }
        }
    }
}
