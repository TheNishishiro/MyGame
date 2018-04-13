﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Creatures.Hostile
{
    class Rat : baseCreature
    {
        public Rat(float posX, float posY, Texture2D texture)
        {
            Position = new Vector2(posX, posY);
            bounds = new Rectangle((int)posX, (int)posY, texture.Width, texture.Height);
            moveToPosition = Position;
            this.texture = texture;
            color = Color.Red;
            Init();
            baseStats[Damage] = 5;
            baseStats[HP] = baseStats[HP_max] = 40;
            baseStats[Exp] = 10;
            baseStats[Level] = 1;
            baseStats[AttackSpeed] = 30;
            healthBar = new ProgresBar(Color.Red, Color.DarkGreen, new Rectangle(bounds.X, bounds.Y - 32, 32, 8));
        }
    }
}
