using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Spells
{
    class baseSpell : ISpell
    {
        protected string name;
        protected Point middlePoint;
        protected Texture2D texture;
        protected int LifeTime;
        protected int[,] array;
        protected int cost;
        protected Dictionary<string, int> Damage = null;
        protected int Heal = -1;

        public ISpell CreateCopy()
        {
            if(Heal == -1)
                return new Spell(name, texture, Damage, LifeTime, middlePoint, array, cost);
            else
                return new Miracle(name, texture, Heal, LifeTime, middlePoint, array, cost);
        }

        public int GetManaCost()
        {
            return cost;
        }

        public virtual void Cast(Vector2 position)
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            return name;
        }
    }
}
