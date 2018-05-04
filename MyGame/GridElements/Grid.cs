using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyGame.GridElements.Specials;
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
        int biomChance = 160;
        public Grid(ContentManager content, int gridSizeX, int gridSizeY)
        {
            Console.WriteLine("Generating grid...");
            map = new ITile[gridSizeY, gridSizeX];
            this.gridSizeX = gridSizeX;
            this.gridSizeY = gridSizeY;
            for(int x = 0; x < gridSizeX; x++)
            {
                for(int y = 0; y < gridSizeY; y++)
                {
                    map[x, y] = new BasicTile(Textures.Grass, x, y);
                }
            }
            GenerateBioms();
            GenerateRivers();
            WaterSmoothing();
            GenerateObjects();
        }

        class BiomGenerator
        {
            public int x, initx, reey,length,hight;
            bool walkable = true;
            Texture2D biomTex;

            int MiddlePoint, currentHightYp, currentHightYm;

            public BiomGenerator(int x, int y, int length, int hight, int biom)
            {
                this.x = x;
                initx = x;
                reey = y;
                this.length = length;
                this.hight = hight;
                MiddlePoint = length / 2;

                if (biom == 0)
                    biomTex = Textures.Grass;
                if (biom == 1)
                    biomTex = Textures.Sand;
                if (biom == 2)
                    biomTex = Textures.Swamp;
                if (biom == 3)
                {
                    biomTex = Textures.Water;
                    walkable = false;
                }

                currentHightYp = 0;
                currentHightYm = 0;
            }

            public void update(ref ITile[,] map)
            {
                for (int yp = 0; yp < currentHightYp; yp++)
                { 
                    int _y = reey + yp;
                    if (_y < Settings.WorldSizeBlocks)
                    {
                        
                        map[x, _y] = new BasicTile(biomTex, x, _y, walkable);
                    }
                }
                for (int yp = 0; yp < currentHightYm; yp++)
                {
                    int _y = reey - yp;
                    if (_y >= 0)
                    {

                        map[x, _y] = new BasicTile(biomTex, x, _y, walkable);
                    }
                }

                if (x < initx + MiddlePoint)
                {
                    currentHightYp += Settings.rnd.Next(0, 4);
                    currentHightYm += Settings.rnd.Next(0, 4);
                }
                else
                {
                    currentHightYp += Settings.rnd.Next(-3, 0);
                    currentHightYm += Settings.rnd.Next(-3, 0);
                }

                if (currentHightYp < 0)
                    currentHightYp = 0;
                if (currentHightYm < 0)
                    currentHightYm = 0;

                if (x + 1 < Settings.WorldSizeBlocks)
                    x++;
                length--;
            }
        }

        private void GenerateBioms()
        {
            Console.WriteLine("Generating bioms...");
            List<BiomGenerator> BG = new List<BiomGenerator>();
            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    if (Settings.rnd.Next(160) == 160/2)
                    {
                        BG.Add(new BiomGenerator(x, y, Settings.rnd.Next(10, 20), Settings.rnd.Next(10, 20), Settings.rnd.Next(4)));
                    }
                }
            }

            
            while (BG.Count > 0)
            {
                for (int i = 0; i < BG.Count; i++)
                {
                    BG[i].update(ref map);

                    if (BG[i].length <= 0)
                    {
                        BG.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        class RiverGenerators
        {
            public int x,y, lifetime;
            public RiverGenerators(int x, int y, int lifetime)
            {
                this.x = x;
                this.y = y;
                this.lifetime = lifetime;
            }

            public void update()
            {
                int dir = Settings.rnd.Next(4);
                if (dir == 0 && x + 1 < Settings.WorldSizeBlocks)
                {
                    x++;
                }  
                else if (dir == 1 && x - 1 >= 0)
                {
                    x--;
                }
                else if (dir == 2 && y + 1 < Settings.WorldSizeBlocks)
                {
                    y++;
                }
                else if(dir == 3 && y - 1 >= 0)
                {
                    y--;
                }
                lifetime--;
            }
        }
        private void GenerateRivers()
        {
            Console.WriteLine("Generating rivers...");
            List<RiverGenerators> rGenetors = new List<RiverGenerators>();
            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    if(Settings.rnd.Next(200) == 37)
                    {
                        rGenetors.Add(new RiverGenerators(x, y, Settings.rnd.Next(20)));
                    }
                }
            }
            while (rGenetors.Count > 1)
            {
                for(int i = 1; i < rGenetors.Count; i++)
                {
                    rGenetors[i].update();
                    if (rGenetors[i].lifetime <= 0)
                    {
                        rGenetors.RemoveAt(i);
                        i--;
                    }
                }
            }

        }

        private void WaterSmoothing()
        {
            Console.WriteLine("Smoothing water...");
            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    if (map[x, y].GetTexture() == Textures.Water)
                    {

                        for(int i = 0; i < Textures.bioms.Count; i++)
                            Textures.bioms[i].UpdateXY(x, y);

                        if (x > 0 && map[x - 1, y].GetTexture() != Textures.Water)
                            foreach(Biom biom in Textures.bioms)
                                if (x > 0 && map[x - 1, y].GetTexture() == biom.tile)
                                    map[x, y].AddAddition(biom.waterBorders[0]);
                        if (x < Settings.WorldSizeBlocks - 1 && map[x + 1, y].GetTexture() != Textures.Water)
                            foreach (Biom biom in Textures.bioms)
                                if (x < Settings.WorldSizeBlocks - 1 && map[x + 1, y].GetTexture() == biom.tile)
                                    map[x, y].AddAddition(biom.waterBorders[2]);
                        if (y > 0 && map[x, y - 1].GetTexture() != Textures.Water)
                            foreach (Biom biom in Textures.bioms)
                                if (y > 0 && map[x, y - 1].GetTexture() == biom.tile)
                                    map[x, y].AddAddition(biom.waterBorders[1]);
                        if (y < Settings.WorldSizeBlocks - 1 && map[x, y + 1].GetTexture() != Textures.Water)
                            foreach (Biom biom in Textures.bioms)
                                if (y < Settings.WorldSizeBlocks - 1 && map[x, y + 1].GetTexture() == biom.tile)
                                    map[x, y].AddAddition(biom.waterBorders[3]);
                    }
                }
            }
        }

        private void GenerateObjects()
        {
            Console.WriteLine("Generating objects...");
            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    if (map[x, y].Walkable)
                    {
                        string biom = "";

                        foreach (Biom _biom in Textures.bioms)
                            if (map[x, y].GetTexture() == _biom.tile)
                                biom = _biom.BiomName;

                        map[x, y].GenerateAddition(biom);
                    }
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

        public void PlaceScriptedObjects()
        {
            Console.WriteLine("Placing predefined objects...");
            foreach (KeyValuePair<string, ITileAddition> addition in Textures.ScriptedAdditions)
            {
                int xOffset = 0;
                while(!map[(int)addition.Value.GetPosition().X + xOffset, (int)addition.Value.GetPosition().Y].Walkable)
                    xOffset++;
                map[(int)addition.Value.GetPosition().X, (int)addition.Value.GetPosition().Y].SetAddition(addition.Value.CreateCopy(new Vector2((addition.Value.GetPosition().X+ xOffset) * Settings.GridSize, addition.Value.GetPosition().Y * Settings.GridSize)));
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
