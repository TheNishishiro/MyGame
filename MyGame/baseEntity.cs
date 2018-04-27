using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.UI;
using MyGame.UI.Controls;
using NFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

// Class containing functions used by both Enemies, Player and Objects on map

namespace MyGame
{
    class baseEntity : baseClickable
    {
        protected Color color = Color.White;
        protected ProgresBar healthBar;

        protected void DrawAggroRectangle(ref SpriteBatch sb)
        {
            if (fighting)
                NDrawing.Draw(ref sb, Textures.UITargetTexture, Position, Color.White, Settings.tileLayer + 0.1f);
        }

        protected bool isWalkable(float x, float y)
        {
            return Settings.grid.map[(int)(x / Settings.GridSize), (int)(y / Settings.GridSize)].Walkable;
        }

        protected void SetWalkable(bool walkable)
        {
            Settings.grid.map[(int)(Position.X / Settings.GridSize), (int)(Position.Y / Settings.GridSize)].Walkable = walkable;
        }

        protected void AddGold(int Gold)
        {
            int gold = Settings.rnd.Next(Gold) + 1;
            if (Settings.rnd.Next(100) <= Settings._player.Stats[Names.Greed] - 1)
            {
                gold *= 2;
                MainUI.FL.Add(new FadingLabel($"Gold +{gold} [greed]", Position, Color.Yellow));
            }
            else
                MainUI.FL.Add(new FadingLabel($"Gold +{gold}", Position, Color.Yellow));
            Settings._player.Materials[Names.Material_Gold] += gold;
        }
    }
}
