using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GridElements.TileTypes
{
    class BasicTile : baseTile
    {
        public BasicTile(Texture2D texture, int x, int y, bool Walkable = true)
        {
            this.texture = texture;
            position = new Vector2(x * 32, y * 32);
            this.Walkable = Walkable;
        }
    }
}
