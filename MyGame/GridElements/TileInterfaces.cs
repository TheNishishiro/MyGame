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
        void RemoveAddition(ITileAddition addition);
    }

    interface ITileAddition
    {
        bool Walkable { get; set; }
        void Draw(ref SpriteBatch sb);
        void RemoveAdditionFromGrid();
    }
}
