using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.GridElements.Specials;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GridElements
{
    class AdditionFactory
    {
        public static ITileAddition CreateAddition(Vector2 position)
        {
            while (true)
            {
                string[] keys = Textures.GeneratorAdditionTemplates.Keys.ToArray();
                ITileAddition addition =Textures.GeneratorAdditionTemplates[keys[Settings.rnd.Next(keys.Length)]].CreateCopy(position);
                if (addition.GetRarity() >= Settings.rnd.Next(1000))
                {
                    addition.SetPosition(position);
                    return addition;
                }
            }
        }

        public static void SpawnAddition(Vector2 position, string type = "g", string ID = "")
        {
            if (type == "g")
            {
                if (ID == "")
                {
                    string[] keys = Textures.GeneratorAdditionTemplates.Keys.ToArray();
                    Settings.grid.map[(int)(position.X / Settings.GridSize), (int)(position.Y / Settings.GridSize)].AddAddition(Textures.GeneratorAdditionTemplates[keys[Settings.rnd.Next(keys.Length)]].CreateCopy(new Vector2(position.X, position.Y)));
                }
                else
                {
                    Settings.grid.map[(int)(position.X / Settings.GridSize), (int)(position.Y / Settings.GridSize)].AddAddition(Textures.GeneratorAdditionTemplates[ID].CreateCopy(new Vector2(position.X, position.Y)));
                }
            }
            else if(type == "sp")
            {
                if (ID == "")
                {
                    string[] keys = Textures.SpawnableAdditionTemplates.Keys.ToArray();
                    Settings.grid.map[(int)(position.X / Settings.GridSize), (int)(position.Y / Settings.GridSize)].AddAddition(Textures.SpawnableAdditionTemplates[keys[Settings.rnd.Next(keys.Length)]].CreateCopy(new Vector2(position.X, position.Y)));
                }
                else
                {
                    Settings.grid.map[(int)(position.X / Settings.GridSize), (int)(position.Y / Settings.GridSize)].AddAddition(Textures.SpawnableAdditionTemplates[ID].CreateCopy(new Vector2(position.X, position.Y)));
                }
            }
            else if (type == "sc")
            {
                if (ID == "")
                {
                    string[] keys = Textures.ScriptedAdditions.Keys.ToArray();
                    Settings.grid.map[(int)(position.X / Settings.GridSize), (int)(position.Y / Settings.GridSize)].AddAddition(Textures.ScriptedAdditions[keys[Settings.rnd.Next(keys.Length)]].CreateCopy(new Vector2(position.X, position.Y)));
                }
                else
                {
                    Settings.grid.map[(int)(position.X / Settings.GridSize), (int)(position.Y / Settings.GridSize)].AddAddition(Textures.ScriptedAdditions[ID].CreateCopy(new Vector2(position.X, position.Y)));
                }
            }
        }

        public static void CreateAdditionTemplate(Dictionary<string, ITileAddition> list, string type)
        {
            Console.WriteLine($"Loading {type} Additions...");
            try
            {
                string[] AdditionFiles = Directory.GetFiles($".\\Data\\Additions\\{type}\\");
                foreach (string file in AdditionFiles)
                {
                    String[] lines = File.ReadAllLines(file);
                    Texture2D texture = null;
                    string ID = "";
                    int Rarity = 1000;
                    int Gold = 0;
                    string _type = "";
                    Vector2 position = new Vector2(0,0);
                    Dictionary<string, int> dropChance = new Dictionary<string, int>();
                    bool walkable = false; bool clickable = false; bool CreatesFloatingText = false; bool IsTimeLimited = false; bool IsOnTop = false;
                    string ButtonRename = null; int? HP = null; int? UseCooldown = null; string resource = null; int? amount = null;
                    foreach (string line in lines)
                    {
                        if (line != "")
                        {
                            string[] property = line.Split(':');
                            string _property = property[0].Trim().ToLower();
                            string convertedProperty = property[1].Trim().Replace("\"", String.Empty);

                            switch (_property)
                            {
                                case "textureid":
                                    texture = Textures.AdditionTextures[convertedProperty];
                                    break;
                                case "id":
                                    ID = convertedProperty;
                                    break;
                                case "iswalkable":
                                    walkable = bool.Parse(convertedProperty);
                                    break;
                                case "isclickable":
                                    clickable = bool.Parse(convertedProperty);
                                    break;
                                case "createsfloatingtext":
                                    CreatesFloatingText = bool.Parse(convertedProperty);
                                    break;
                                case "istimelimited":
                                    IsTimeLimited = bool.Parse(convertedProperty);
                                    break;
                                case "isontop":
                                    IsOnTop = bool.Parse(convertedProperty);
                                    break;
                                case "buttonrename":
                                    ButtonRename = convertedProperty;
                                    break;
                                case "health":
                                    HP = int.Parse(convertedProperty);
                                    break;
                                case "usecooldown":
                                    UseCooldown = int.Parse(convertedProperty);
                                    break;
                                case "resource":
                                    resource = convertedProperty;
                                    break;
                                case "amount":
                                    amount = int.Parse(convertedProperty);
                                    break;
                                case "rarity":
                                    Rarity = int.Parse(convertedProperty);
                                    break;
                                case "type":
                                    _type = convertedProperty;
                                    break;
                                case "position":
                                    position = new Vector2(float.Parse(convertedProperty.Split(',')[0].Trim()), float.Parse(convertedProperty.Split(',')[1].Trim()));
                                    break;
                                case "gold":
                                    Gold = int.Parse(convertedProperty); 
                                    break;
                                case "loot":
                                    dropChance.Add(convertedProperty.Split(',')[0].Trim(), int.Parse(convertedProperty.Split(',')[1].Trim()));
                                    break;
                            }
                        }
                    }

                    if(_type == "")
                        list.Add(ID, new AnyAddition(texture, position, Rarity, walkable, clickable, CreatesFloatingText, IsTimeLimited, IsOnTop, ButtonRename, HP, UseCooldown, resource, amount));
                    else if(_type == "chest")
                        list.Add(ID, new Chest(position, texture, dropChance, ButtonRename, Rarity, Gold));

                    Console.WriteLine("\tLoaded: " + file);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
