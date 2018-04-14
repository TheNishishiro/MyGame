using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    interface ICreature
    {


        void Update();
        void Move();
        Vector2 GetPosition();
        void DealDamage(int damage);
        void Die(ICreature player);
        bool GetFightingState();
        int GetHealth();
        int GetMaxHealth();
        int GetMana();
        int GetMaxMana();
        int GetDamage();
        void HealUp(int amount);
        int GetLevel();
        int GetExp();
        int GetExpMax();
        float GetAttackSpeed();
        Rectangle GetBounds();
        void LevelUp();
        void AddExp(int amount);
        void Draw(ref SpriteBatch sb);
    }

}
