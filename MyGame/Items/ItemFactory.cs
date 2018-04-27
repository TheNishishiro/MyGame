using Microsoft.Xna.Framework.Graphics;
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
                    IItems temporary = InternalLoop(file, out string ID);
                    if (!items.ContainsKey(ID))
                    {
                        items.Add(ID, temporary.CreateCopy());
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

        public static IItems CreateItem()
        {
            try
            {
                Console.WriteLine("Loading item objects...");
                string[] AdditionFiles = Directory.GetFiles($".\\Data\\Items\\");
                string file = AdditionFiles[Settings.rnd.Next(AdditionFiles.Length)];
                return InternalLoop(file, out string id).CreateCopy();
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public static IItems InternalLoop(string file, out string ID)
        {
            String[] lines = File.ReadAllLines(file);
            Texture2D texture = null;
            ID = "";
            string skilltype = "";
            Dictionary<string, int> damage = new Dictionary<string, int>();
            Dictionary<string, int> DefenceTypes = new Dictionary<string, int>();
            Dictionary<string, int> Attribiutes = new Dictionary<string, int>();
            string name=""; string type = ""; int durability=0; int upgrade=0; string description="";
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
                            damage.Add(convertedProperty.Split(',')[0].Trim(), int.Parse(convertedProperty.Split(',')[1].Trim()));
                            break;
                        case "durability":
                            durability = int.Parse(convertedProperty);
                            break;
                        case "upgrade":
                            upgrade = int.Parse(convertedProperty);
                            break;
                        case "description":
                            description = convertedProperty;
                            break;
                        case "skilltype":
                            skilltype = convertedProperty;
                            break;
                        case "attribiute":
                            Attribiutes.Add(convertedProperty.Split(',')[0].Trim(), int.Parse(convertedProperty.Split(',')[1].Trim()));
                            break;
                        case "defence":
                            DefenceTypes.Add(convertedProperty.Split(',')[0].Trim(), int.Parse(convertedProperty.Split(',')[1].Trim()));
                            break;
                    }
                }
            }

            if (type == Names.Weapon)
                return new Weapon(texture, name, type, durability, upgrade, description, skilltype, damage, Attribiutes);
            else if (type == Names.Necklace)
                return new Necklace(texture, name, type, upgrade, description, Attribiutes);
            else if (type == Names.Ring)
                return new Necklace(texture, name, type, upgrade, description, Attribiutes);
            else if (type == Names.Armor)
                return new Armor(texture, name, type, durability, upgrade, description, skilltype, DefenceTypes, Attribiutes);
            else if (type == Names.Shield)
                return new Armor(texture, name, type, durability, upgrade, description, skilltype, DefenceTypes, Attribiutes);
            else if (type == Names.Book)
                return new Book(texture, name, type, description);

            Console.WriteLine("\tLoaded: " + file);
            return null;
        }
    }
}
