using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyGame.Settings;

namespace MyGame.Creatures
{
    class CreatureFactory
    {
        static float posXRange, posYRange;
        public static void AddCreature(List<ICreature> creatures)
        {
            
            posXRange = 50 * GridSize;
            posYRange = 50 * GridSize;

            float posX, posY;

            posX = rnd.Next((int)(_player.GetPosition().X - posXRange), (int)(_player.GetPosition().X + posXRange));
            posY = rnd.Next((int)(_player.GetPosition().Y - posYRange), (int)(_player.GetPosition().Y + posYRange));

            posX = GridSize * (posX / GridSize) - (posX % GridSize);
            posY = GridSize * (posY / GridSize) - (posY % GridSize);

            if (posX < 0)
                posX = 0;
            else if (posX > WorldSizePixels)
                posX = WorldSizePixels;
            if (posY < 0)
                posY = 0;
            else if (posY > WorldSizePixels)
                posY = WorldSizePixels;
            if (!TestRenderBounds(new Vector2(posX, posY), _player.GetPosition(), RenderDistance)
                && grid.map[(int)(posX/GridSize), (int)(posY/GridSize)].Walkable)
            {
                creatures.Add(Textures.EnemyTemplates[Settings.rnd.Next(Textures.EnemyTemplates.Count)].CreateCopy(new Vector2(posX, posY)));
            }
        }

        public static void ClearCreaturesOutsideBounds(List<ICreature> creatures)
        {
            posXRange = 50 * GridSize;
            posYRange = 50 * GridSize;

            for (int i = 0; i < creatures.Count; i++)
            {
                if(!TestRenderBounds(creatures[i].GetPosition(), _player.GetPosition(), 50))
                {
                    creatures.RemoveAt(i);
                    i--;
                }
            }
        }

        public static void LoadCreatureTemplate(List<ICreature> list)
        {
            Console.WriteLine("Loading creature objects...");
            try
            {
                string[] EnemyFiles = Directory.GetFiles(".\\Data\\Enemies\\");
                foreach (string file in EnemyFiles)
                {
                    String[] lines = File.ReadAllLines(file);
                    Texture2D texture = null;
                    string name = "";
                    Dictionary<string, int> stats = new Dictionary<string, int>();
                    List<string> dialogs = new List<string>();
                    List<string> loot = new List<string>();
                    foreach (string line in lines)
                    {
                        string[] property = line.Split(':');
                        string _property = property[0].ToLower().Trim();
                        string convertedProperty = property[1].Trim().Replace("\"", String.Empty);

                        if (_property == "textureid")
                            texture = Textures.EnemyTextures[convertedProperty];
                        else if (_property == "name")
                            name = convertedProperty;
                        else if (_property == "dialog")
                            dialogs.Add(convertedProperty);
                        else if(_property == "loot")
                        {
                            loot.Add(convertedProperty);
                        }
                        else
                        {
                            if (!stats.ContainsKey(_property))
                            {
                                stats.Add(_property, int.Parse(convertedProperty));
                            }
                        }
                    }
                    list.Add(new baseEnemy(texture, new Vector2(0, 0), name, stats, dialogs, loot));
                    Console.WriteLine("\tLoaded: " + file);
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
