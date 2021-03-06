﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GridElements
{
    interface ITile
    {
        bool Walkable { get; set; }
        void Draw(ref SpriteBatch sb);
        void DrawAddition(ref SpriteBatch sb);
        void AddAddition(ITileAddition addition);
        void SetAddition(ITileAddition addition);
        void RemoveAddition(ITileAddition addition);
        void RemoveAdditions();
        Texture2D GetTexture();
        void GenerateAddition(string biom);
    }

    interface ITileAddition
    {
        bool Walkable { get; set; }
        void Draw(ref SpriteBatch sb, float layerIncrease);
        void RemoveAdditionFromGrid();
        int GetRarity();
        ITileAddition CreateCopy(Vector2 position);
        void SetPosition(Vector2 position);
        Vector2 GetPosition();
        string GetBiom();
    }
}
