using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Spells
{
    class SpellFactory
    {
        public static void CreateSpellTemplate(Dictionary<string, ISpell> spellTemplate)
        {
            Console.WriteLine("Loading spell templates...");
            string[] Spells = Directory.GetFiles(".\\Data\\Spells\\");
            foreach(string spell in Spells)
            {
                String[] lines = File.ReadAllLines(spell);
                Texture2D texture = null;
                string id = "", name = "";
                Dictionary<string, int> damage = new Dictionary<string, int>();
                int lifetime = 0, cost = 0;
                int heal = -1;
                Point size = new Point(0,0);
                int[,] array = new int[0,0];
                int i = 0;
                foreach(string line in lines)
                {
                    string[] property = line.Split(':');
                    string _property = property[0].ToLower().Trim();
                    string convertedProperty = property[1].Trim().Replace("\"", String.Empty);

                    if (_property == "textureid")
                        texture = Textures.SpellTextures[convertedProperty];
                    else if (_property == "id")
                        id = convertedProperty;
                    else if (_property == "name")
                        name = convertedProperty;
                    else if (_property == "damage")
                        damage.Add(convertedProperty.Split(',')[0].Trim(), int.Parse(convertedProperty.Split(',')[1].Trim()));
                    else if (_property == "heal")
                        heal = int.Parse(convertedProperty);
                    else if (_property == "cost")
                        cost = int.Parse(convertedProperty);
                    else if (_property == "lifetime")
                        lifetime = int.Parse(convertedProperty);
                    else if (_property == "size")
                    {
                        size = new Point(int.Parse(convertedProperty.Split(',')[0].Trim()), int.Parse(convertedProperty.Split(',')[1].Trim()));
                        array = new int[size.X, size.Y];
                    }
                    else if (_property == "active")
                    {
                        for (int j = 0; j < convertedProperty.Split(',').Length; j++)
                        {
                            array[i, j] = int.Parse(convertedProperty.Split(',')[j]);
                        }
                        i++;
                    }
                }

                if(damage.Count != 0)
                    spellTemplate.Add(id, new Spell(name, texture, damage, lifetime, new Point((int)Math.Floor((double)size.X / 2), (int)Math.Floor((double)size.Y / 2)), array, cost));
                else if(heal != -1)
                    spellTemplate.Add(id, new Miracle(name, texture, heal, lifetime, new Point((int)Math.Floor((double)size.X / 2), (int)Math.Floor((double)size.Y / 2)), array, cost));
                Console.WriteLine("\tLoaded: " + spell);
            }
        }
    }
}
