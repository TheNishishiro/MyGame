using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectCreator
{
    class ItemPage
    {
        Form1 f1;

        public ItemPage(Form1 form1)
        {
            f1 = form1;
            foreach (string item in f1.itemTextures.ToArray())
            {
                string entry = item.Split(',')[1].Split(':')[1].Split('\\')[1].Trim().Replace("\"", string.Empty);
                f1.itemTexturesDPL.Items.Add(entry);
            }
            foreach (string items in f1.items.ToArray())
            {
                string entry = items.Split('\\')[3].Trim();
                f1.itemDPL.Items.Add(entry);
            }
            foreach (string type in f1.DamageTypes)
            {
                f1.itemDamageDPL.Items.Add(type);
                f1.itemDefenceDPL.Items.Add(type);
            }
            foreach (string type in f1.ItemTypes)
            {
                f1.itemTypesDPL.Items.Add(type);
            }

            f1.itemLoadButton.Click += new EventHandler(LoadItem);
        }

        public void LoadItem(object sender, EventArgs e)
        {
            f1.itemDefenceTL.Items.Clear();
            f1.itemDamageTL.Items.Clear();
            string[] lines = File.ReadAllLines(f1.items[f1.itemDPL.SelectedIndex]);
            f1.itemFileName.Text = f1.items[f1.itemDPL.SelectedIndex].Split('\\')[3].Split('.')[0];
            foreach (string line in lines)
            {
                string _property = line.Split(':')[0].Trim().ToLower();
                string convertedProperty = line.Split(':')[1].Trim().Replace("\"", string.Empty);

                if (_property == "textureid")
                    f1.itemTextureID.Text = convertedProperty;
                else if (_property == "name")
                    f1.itemName.Text = convertedProperty;
                else if (_property == "id")
                    f1.itemID.Text = convertedProperty;
                else if (_property == "description")
                    f1.itemDescription.Text = convertedProperty;
                else if (_property == "defence")
                    f1.itemDefenceTL.Items.Add(convertedProperty);
                else if (_property == "damage")
                    f1.itemDamageTL.Items.Add(convertedProperty);

                foreach (string texture in f1.itemTextures.ToArray())
                {
                    if (texture.Contains(f1.itemTextureID.Text))
                    {
                        string _picLink = texture.Split(',')[1].Split(':')[1].Replace("\"", string.Empty).Trim();
                        f1.itemTexturePreview.BackgroundImage = Image.FromFile(".\\Data\\" + _picLink);
                    }
                }
            }
        }
    }
}
