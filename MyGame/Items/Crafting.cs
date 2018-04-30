using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Items
{
    class Crafting
    {

        public static IItems Result, Slot1 = null, Slot2 = null, Slot3 = null;

        public static void AddToSlot(IItems item)
        {
            if (Slot1 == null)
                Slot1 = item;
            else if (Slot2 == null)
                Slot2 = item;
            else if (Slot3 == null)
                Slot3 = item;
        }

        public static void RemoveFromSlot(IItems item)
        {
            if (Slot1 == item)
                Slot1 = null;
            else if (Slot2 == item)
                Slot2 = null;
            else if (Slot3 == item)
                Slot3 = null;
        }

        public static void CraftItem()
        {
            string ID1 = "-", ID2 = "-", ID3 = "-";

            if (Slot1 != null)
                ID1 = Slot1.GetID();
            if (Slot2 != null)
                ID2 = Slot2.GetID();
            if (Slot3 != null)
                ID3 = Slot3.GetID();

            string[] recipes = File.ReadAllLines(".\\Data\\Crafting.xml");
            foreach(string recipe in recipes)
            {
                string[] ingredients = recipe.Replace(" ", string.Empty).Split(':')[0].Split(',');
                List<string> _ingredients = ingredients.ToList();
                if (_ingredients.Contains(ID1))
                {
                    _ingredients.Remove(ID1);
                    if (_ingredients.Contains(ID2))
                    {
                        _ingredients.Remove(ID2);
                        if (_ingredients.Contains(ID3))
                        {
                            _ingredients.Remove(ID3);
                            Result = Textures.ItemTemplates[recipe.Split(':')[1].Trim()];
                            Result.SetAsCraftingResult();
                            break;
                        }
                    }   
                }
            }
        }
    }
}
