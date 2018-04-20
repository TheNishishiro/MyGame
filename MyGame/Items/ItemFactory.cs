﻿using Microsoft.Xna.Framework.Graphics;
using MyGame.Items.ItemTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Items
{
    class ItemFactory
    {
        public static void CreateItemTemplate(Dictionary<string, IItems> items)
        {
            try
            {
                Console.WriteLine("Loading item objects...");
                string[] AdditionFiles = Directory.GetFiles($".\\Data\\Items\\");
                foreach (string file in AdditionFiles)
                {
                    String[] lines = File.ReadAllLines(file);
                    Texture2D texture = null;
                    string ID = "";
                    string name=""; string type = ""; int damage=0; int durability=0; int elementalDamage=0; int upgrade=0; string description="";
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
                                    texture = Textures.ItemTextures[convertedProperty];
                                    break;
                                case "id":
                                    ID = convertedProperty;
                                    break;
                                case "name":
                                    name = convertedProperty;
                                    break;
                                case "type":
                                    type = convertedProperty;
                                    break;
                                case "damage":
                                    damage = int.Parse(convertedProperty);
                                    break;
                                case "durability":
                                    durability = int.Parse(convertedProperty);
                                    break;
                                case "elementaldamage":
                                    elementalDamage = int.Parse(convertedProperty);
                                    break;
                                case "upgrade":
                                    upgrade = int.Parse(convertedProperty);
                                    break;
                                case "description":
                                    description = convertedProperty;
                                    break;
                            }
                        }
                    }

                    if (!items.ContainsKey(ID))
                    {
                        if (type == "Weapon")
                            items.Add(ID, new Weapon(texture, name, type, damage, durability, elementalDamage, upgrade, description));
                        Console.WriteLine("\tLoaded: " + file);
                    }
                    else
                    {
                        Console.WriteLine("\tCouldn't load: " + file + ", item ID already present in dictionary");
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
