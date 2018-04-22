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
        ICreature CreateCopy(Vector2 position);

        void Update();
        void Move();
        Vector2 GetPosition();
        void TakeDamage(int damage);
        int DealDamage(); 
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
        int GetMagicLevel();
        int GetMagicLevelPoints();
        int GetMaxMagicLevelPoints();
        int GetLevelPoints();
        float GetAttackSpeed();
        string GetDescription();
        string GetName();
        Rectangle GetBounds();
        void LevelUp();
        void AddExp(int amount);
        void Draw(ref SpriteBatch sb);
    }

}
