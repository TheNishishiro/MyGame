﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Spells
{
    class Spell : baseSpell
    {
        public Spell(string id, string name, Texture2D texture, Dictionary<string, int> Damage, int LifeTime, Point middlePoint, int[,] array, int cost)
        {
            this.id = id;
            this.name = name;
            this.middlePoint = middlePoint;
            this.texture = texture;
            this.LifeTime = LifeTime;
            this.array = array;
            this.cost = cost;
            this.Damage = Damage;
        }

        public override void Cast(Vector2 position, ICreature creature)
        { 
            position = new Vector2(position.X - (32 * middlePoint.X), position.Y - (32 * middlePoint.Y));

            for(int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] != 0)
                    {
                        bool castByPlayer;
                        if (creature is Player)
                            castByPlayer = true;
                        else
                            castByPlayer = false;

                        Global.spellCells.Add(new SpellCell(new Rectangle((int)position.X + (32 * i - middlePoint.X), (int)position.Y + (32 * j - middlePoint.X), 32, 32), texture, Damage, LifeTime, castByPlayer));
                    }
                }
            } 
        }
    }
}
