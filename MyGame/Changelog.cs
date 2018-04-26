using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MyGame
{
    class Changelog
    {
        public static string changelog = "";

        public static void LoadChangelog()
        {
            if (File.Exists("./Changelog.txt"))
            {
                string[] lines = File.ReadAllLines("./Changelog.txt");
                foreach (string line in lines)
                {
                    changelog += line + "\n";
                }
            }
            else
            {
                changelog = "Couldn't find changelogs, welcome to the game anyway :D";
                Console.WriteLine("Couldn't find changelog.txt");
            }
        }
    }
}
