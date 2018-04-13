using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyGame.GridElements.TileTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GridElements
{
    class Grid
    {
        public ITile[,] map;
        int gridSizeX, gridSizeY;
        Texture2D grass;
        public Grid(ContentManager content, int gridSizeX, int gridSizeY)
        {
            grass = content.Load<Texture2D>("grass");
            map = new ITile[gridSizeY, gridSizeX];
            this.gridSizeX = gridSizeX;
            this.gridSizeY = gridSizeY;
            for(int x = 0; x < gridSizeX; x++)
            {
                for(int y = 0; y < gridSizeY; y++)
                {
                    map[x, y] = new Grass(grass, x, y);
                }
            }
        }

        public void Draw(ref SpriteBatch sb, Vector2 pointFrom, int renderDistance)
        {
            renderDistance *= 32;
            for (int x = (int)((pointFrom.X - renderDistance) / 32); x <= (int)((pointFrom.X + renderDistance) / 32); x++)
            {
                for (int y = (int)((pointFrom.Y - renderDistance)/32); y <= (int)((pointFrom.Y + renderDistance) / 32); y++)
                {
                    ControlBounds(x, y, out int x_o, out int y_o);
                    map[x_o, y_o].Draw(ref sb);
                }
            }
            
        }

        public void DrawAdditions(ref SpriteBatch sb, Vector2 pointFrom, int renderDistance)
        {
            renderDistance *= 32;
            for (int x = (int)((pointFrom.X - renderDistance) / 32); x <= (int)((pointFrom.X + renderDistance) / 32); x++)
            {
                for (int y = (int)((pointFrom.Y - renderDistance) / 32); y <= (int)((pointFrom.Y + renderDistance) / 32); y++)
                {
                    ControlBounds(x, y, out int x_o, out int y_o);
                    map[x_o, y_o].DrawAddition(ref sb);
                }
            }
        }

        private void ControlBounds(int x_in, int y_in, out int x, out int y)
        {

            if (x_in < 0)
                x = 0;
            else if (x_in >= gridSizeX)
                x = gridSizeX - 1;
            else
                x = x_in;
            if (y_in < 0)
                y = 0;
            else if (y_in >= gridSizeY)
                y = gridSizeY - 1;
            else
                y = y_in;
        }
    }
}
