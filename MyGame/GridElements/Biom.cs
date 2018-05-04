using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.GridElements.Specials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GridElements
{
    class Biom
    {
        public Texture2D tile;
        public ITileAddition[] waterBorders = new ITileAddition[4];
        public string BiomName;

        private Texture2D tileLeft;
        private Texture2D tileRight;
        private Texture2D tileUp;
        private Texture2D tileDown;

        int x, y;

        public Biom(string BiomName, Texture2D tile, Texture2D tileLeft, Texture2D tileRight, Texture2D tileUp, Texture2D tileDown)
        {
            this.BiomName = BiomName;
            this.tile = tile;

            this.tileLeft = tileLeft;
            this.tileRight = tileRight;
            this.tileUp = tileUp;
            this.tileDown = tileDown;
        }

        public void UpdateXY(int x, int y)
        {
            this.x = x;
            this.y = y;

            waterBorders[0] = new AnyAddition(tileLeft, new Vector2(x * Settings.GridSize, y * Settings.GridSize), 0);
            waterBorders[1] = new AnyAddition(tileRight, new Vector2(x * Settings.GridSize, y * Settings.GridSize), 0);
            waterBorders[2] = new AnyAddition(tileUp, new Vector2(x * Settings.GridSize, y * Settings.GridSize), 0);
            waterBorders[3] = new AnyAddition(tileDown, new Vector2(x * Settings.GridSize, y * Settings.GridSize), 0);
        }
    }
}
