using Microsoft.Xna.Framework;
using MyGame.Items;
using MyGame.Spells;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.SaveManagers
{
    class SaveManager
    {

        public static void SaveGame()
        {
            if (Directory.Exists(".\\Save"))
            {
                Console.WriteLine("Saving game...");
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                FileStream fs = new FileStream(".\\Save\\Save.sav", FileMode.Create, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);

                sw.WriteLine(Settings._player.Position.X / Settings.GridSize + "," + Settings._player.Position.Y / Settings.GridSize);

                foreach (KeyValuePair<string, int> entry in Settings._player.baseStats)
                {
                    sw.WriteLine($"{entry.Key}:{entry.Value}");
                }
                sw.WriteLine("#");

                foreach (KeyValuePair<string, int> entry in Settings._player.Stats)
                {
                    sw.WriteLine($"{entry.Key}:{entry.Value}");
                }
                sw.WriteLine("#");

                foreach (KeyValuePair<string, int> entry in Settings._player.Skills)
                {
                    sw.WriteLine($"{entry.Key}:{entry.Value}");
                }
                sw.WriteLine("#");

                foreach (KeyValuePair<string, int> entry in Settings._player.Materials)
                {
                    sw.WriteLine($"{entry.Key}:{entry.Value}");
                }
                sw.WriteLine("#");

                int i = 0;
                foreach (ISpell entry in Settings._player.Spells)
                {
                    if (entry != null)
                        sw.WriteLine($"{i}:{entry.GetID()}");
                    i++;
                }
                sw.WriteLine("#");

                foreach (IItems entry in Settings._player.Inventory)
                {
                    sw.WriteLine($"InventorySlot:{entry.GetID()}");
                }
                sw.WriteLine("#");

                foreach (KeyValuePair<string, IItems> entry in Settings._player.Equiped)
                {
                    if (entry.Value != null)
                        sw.WriteLine($"{entry.Key}:{entry.Value.GetID()}");
                }
                sw.WriteLine("#");
                sw.Close();
                stopwatch.Stop();
                Console.WriteLine("Done, saving took: " + stopwatch.Elapsed.TotalSeconds + " sec");
            }
            else
            {
                Console.WriteLine("Directory doesn't exist, creating one...");
                Directory.CreateDirectory(".\\Save");
                Console.WriteLine("Done");
                SaveGame();
            }
        }

        public static void LoadGame()
        {
            if (Directory.Exists(".\\Save"))
            {
                if (File.Exists(".\\Save\\Save.sav"))
                {
                    Console.WriteLine("Loading game...");
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    FileStream fs = new FileStream(".\\Save\\Save.sav", FileMode.Open, FileAccess.Read);
                    StreamReader sw = new StreamReader(fs);

                    string _line = sw.ReadLine();

                    Settings._player.SetWalkable(true);
                    Settings._player.Position = new Vector2((float)(Math.Floor(double.Parse(_line.Split(',')[0])) * Settings.GridSize), (float)(Math.Floor(double.Parse(_line.Split(',')[1])) * Settings.GridSize));

                    while (true)
                    {
                        string line = sw.ReadLine();
                        if (line == "#")
                            break;
                        Settings._player.baseStats[line.Split(':')[0]] = int.Parse(line.Split(':')[1]);
                    }

                    while (true)
                    {
                        string line = sw.ReadLine();
                        if (line == "#")
                            break;
                        Settings._player.Stats[line.Split(':')[0]] = int.Parse(line.Split(':')[1]);
                    }

                    while (true)
                    {
                        string line = sw.ReadLine();
                        if (line == "#")
                            break;
                        Settings._player.Skills[line.Split(':')[0]] = int.Parse(line.Split(':')[1]);
                    }

                    while (true)
                    {
                        string line = sw.ReadLine();
                        if (line == "#")
                            break;
                        Settings._player.Materials[line.Split(':')[0]] = int.Parse(line.Split(':')[1]);
                    }

                    while (true)
                    {
                        string line = sw.ReadLine();
                        if (line == "#")
                            break;
                        Settings._player.Spells[int.Parse(line.Split(':')[0])] = Textures.SpellTemplates[line.Split(':')[1]].CreateCopy();
                    }

                    while (true)
                    {
                        string line = sw.ReadLine();
                        if (line == "#")
                            break;
                        Settings._player.Inventory.Add(Textures.ItemTemplates[line.Split(':')[1]].CreateCopy());
                    }

                    while (true)
                    {
                        string line = sw.ReadLine();
                        if (line == "#")
                            break;
                        Settings._player.Equiped[line.Split(':')[0]] = Textures.ItemTemplates[line.Split(':')[1]].CreateCopy();
                        Settings._player.Equiped[line.Split(':')[0]].SetEquiped();
                    }
                    sw.Close();
                    stopwatch.Stop();
                    Console.WriteLine("Done, loading took: " + stopwatch.Elapsed.TotalSeconds + " sec");
                }
                else
                {
                    Console.WriteLine("Save file doesn't exist.");
                }
            }
            else
            {
                Console.WriteLine("Directory doesn't exist, creating one...");
                Directory.CreateDirectory(".\\Save");
                Console.WriteLine("Done");
            }
        }
    }
}
