using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectCreator
{
    class EnemyPage
    {
        Form1 f1;

        public EnemyPage(Form1 form1)
        {
            f1 = form1;
            foreach (string enemy in f1.enemyTextures.ToArray())
            {
                string entry = enemy.Split(',')[1].Split(':')[1].Split('\\')[1].Trim().Replace("\"", string.Empty);
                f1.listOfEnemyTextures.Items.Add(entry);
            }
            foreach (string enemy in f1.enemies.ToArray())
            {
                string entry = enemy.Split('\\')[3].Trim();
                f1.enemyDPL.Items.Add(entry);
            }
            foreach (string items in f1.items.ToArray())
            {
                string entry = items.Split('\\')[3].Trim();
                f1.enemyLootComboBox.Items.Add(entry);
            }

            foreach(string type in f1.DamageTypes)
            {
                f1.enemyDamageTypeDPL.Items.Add(type);
                f1.enemyDefenceDPL.Items.Add(type);
            }
            
            f1.listOfEnemyTextures.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            f1.enemySaveButton.Click += new EventHandler(enemySave_Click);

            f1.enemyAddDamageButton.Click += new EventHandler(enemyAddDamageButton_Click);
            f1.enemyRemoveDamageButton.Click += new EventHandler(enemyRemoveDamageButton_Click);

            f1.enemyLoad.Click += new EventHandler(enemyLoad_Click);
            f1.enemyAddDefenceButton.Click += new EventHandler(enemyAddDefenceButton_Click);

            f1.addQuoteButton.Click += new EventHandler(addQuoteButton_Click);
            f1.removeQuoteButton.Click += new EventHandler(removeQuoteButton_Click);

            f1.enemyAddLootButton.Click += new EventHandler(addLootButton_Click);
            f1.enemyRemoveLootButton.Click += new EventHandler(removeLootButton_Click);

            f1.enemyAddSpellButton.Click += new EventHandler(addSpellButton_Click);
            f1.enemyRemoveSpellButton.Click += new EventHandler(removeSpellButton_Click);
        }

        public void CheckDataValidation()
        {
            bool exists = false;
            foreach(string id in f1.enemyTextures.ToArray())
            {
                if (id.Split(',')[0].Split(':')[1].Replace("\"", string.Empty).Trim() == f1.enemyTextureID.Text)
                {
                    exists = true;
                }
            }
            if (!exists)
                f1.enemyTextureID.BackColor = Color.Red;
            else
                f1.enemyTextureID.BackColor = Color.White;

            exists = false;
            foreach (string file in f1.enemies.ToArray())
            {
                if (file.Split('\\')[3].Split('.')[0] != f1.enemyFileNameTextbox.Text)
                {
                    string[] lines = File.ReadAllLines(file);
                    foreach (string line in lines)
                    {
                        string _property = line.Split(':')[0].Trim().ToLower();
                        string convertedProperty = line.Split(':')[1].Trim().Replace("\"", string.Empty);

                        if (_property == "id" && convertedProperty == f1.enemyIDTextbox.Text)
                        {
                            exists = true;
                            break;
                        }
                    }
                    if (exists)
                        break;
                }
                if (exists)
                    break;
            }
        }

        public void enemyLoad_Click(object sender, EventArgs e)
        {
            f1.enemyDamageListBox.Items.Clear();
            f1.enemyDefenceListBox.Items.Clear();
            f1.enemyLootListBox.Items.Clear();
            f1.enemySpellsListbox.Items.Clear();
            f1.quoteListBox.Items.Clear();

            string[] lines = File.ReadAllLines(f1.enemies[f1.enemyDPL.SelectedIndex]);
            f1.enemyFileNameTextbox.Text = f1.enemies[f1.enemyDPL.SelectedIndex].Split('\\')[3].Split('.')[0];
            foreach (string line in lines)
            {
                string _property = line.Split(':')[0].Trim().ToLower();
                string convertedProperty = line.Split(':')[1].Trim().Replace("\"", string.Empty);

                if (_property == "textureid")
                    f1.enemyTextureID.Text = convertedProperty;
                else if (_property == "name")
                    f1.enemyNameTextbox.Text = convertedProperty;
                else if (_property == "dialog")
                    f1.quoteListBox.Items.Add(convertedProperty);
                else if (_property == "id")
                    f1.enemyIDTextbox.Text = convertedProperty;
                else if (_property == "description")
                    f1.enemyDescriptionTextbox.Text = convertedProperty;
                else if (_property == "defence")
                    f1.enemyDefenceListBox.Items.Add(convertedProperty);
                else if (_property == "damage")
                    f1.enemyDamageListBox.Items.Add(convertedProperty);
                else if (_property == "loot")
                {
                    f1.enemyLootListBox.Items.Add(convertedProperty);
                }
                else if (_property == "gold")
                {
                    f1.enemyGoldTextBox.Text = convertedProperty.Split(',')[0].Trim();
                    f1.enemyGoldChanceTextBox.Text = convertedProperty.Split(',')[1].Trim();
                }
                else if (_property == "health")
                    f1.enemyHealthTextbox.Text = convertedProperty;
                else if (_property == "experience")
                    f1.enemyExpTextbox.Text = convertedProperty;
                else if (_property == "level")
                    f1.enemyLevelTextBox.Text = convertedProperty;
                else if (_property == "attackspeed")
                    f1.enemyASTextbox.Text = convertedProperty;
                else if (_property == "regeneration")
                    f1.enemyRegenTextbox.Text = convertedProperty;
                else if (_property == "spell")
                {
                    f1.enemySpellsListbox.Items.Add(convertedProperty);
                }

            }

            foreach (string texture in f1.enemyTextures.ToArray())
            {
                if (texture.Contains(f1.enemyTextureID.Text))
                {
                    string _picLink = texture.Split(',')[1].Split(':')[1].Replace("\"", string.Empty).Trim();
                    f1.EnemyTexturePreview.BackgroundImage = Image.FromFile(".\\Data\\" + _picLink);
                }
            }

        }

        public void enemySave_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream($".\\Data\\Enemies\\{f1.enemyFileNameTextbox.Text}.xml", FileMode.Create, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            
            if(f1.enemyTextureID.Text != "")
                sw.WriteLine($"textureid: {f1.enemyTextureID.Text}");
            sw.WriteLine($"name: {f1.enemyNameTextbox.Text}");
            foreach(string entry in f1.quoteListBox.Items)
            {
                sw.WriteLine($"dialog: {entry}");
            }
            if (f1.enemyIDTextbox.Text != "")
                sw.WriteLine($"id: {f1.enemyIDTextbox.Text}");
            sw.WriteLine($"description: {f1.enemyDescriptionTextbox.Text}");
            foreach (string entry in f1.enemyDefenceListBox.Items)
            {
                sw.WriteLine($"defence: {entry}");
            }
            foreach (string entry in f1.enemyDamageListBox.Items)
            {
                sw.WriteLine($"damage: {entry}");
            }
            foreach (string entry in f1.enemyLootListBox.Items)
            {
                sw.WriteLine($"loot: {entry}");
            }
            if (f1.enemyGoldTextBox.Text != "")
                sw.WriteLine($"gold: {f1.enemyGoldTextBox.Text},{f1.enemyGoldChanceTextBox.Text}");
            if (f1.enemyHealthTextbox.Text != "")
                sw.WriteLine($"health: {f1.enemyHealthTextbox.Text}");
            if (f1.enemyExpTextbox.Text != "")
                sw.WriteLine($"experience: {f1.enemyExpTextbox.Text}");
            if (f1.enemyLevelTextBox.Text != "")
                sw.WriteLine($"level: {f1.enemyLevelTextBox.Text}");
            if (f1.enemyASTextbox.Text != "")
                sw.WriteLine($"attackspeed: {f1.enemyASTextbox.Text}");
            if (f1.enemyRegenTextbox.Text != "")
                sw.WriteLine($"regeneration: {f1.enemyRegenTextbox.Text}");
            foreach (string entry in f1.enemySpellsListbox.Items)
            {
                sw.WriteLine($"spell: {entry}");
            }

            sw.Close();
        }

        public void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string picLink = f1.enemyTextures[f1.listOfEnemyTextures.SelectedIndex];
            string _picLink = picLink.Split(',')[1].Split(':')[1].Replace("\"", string.Empty).Trim();
            f1.enemyTextureID.Text = picLink.Split(',')[0].Split(':')[1].Replace("\"", string.Empty).Trim();
            f1.EnemyTexturePreview.BackgroundImage = Image.FromFile(".\\Data\\" + _picLink);
        }

        public void enemyAddDamageButton_Click(object sender, EventArgs e)
        {

            f1.enemyDamageListBox.Items.Add(
                f1.enemyDamageTypeDPL.SelectedItem + "," + f1.enemyDamageNumber.Value.ToString()
                );
        }

        public void enemyRemoveDamageButton_Click(object sender, EventArgs e)
        {
            f1.enemyDamageListBox.Items.Remove(f1.enemyDamageListBox.SelectedItem);
        }

        public void enemyAddDefenceButton_Click(object sender, EventArgs e)
        {
            f1.enemyDefenceListBox.Items.Add(
                f1.enemyDefenceDPL.SelectedItem + "," + f1.enemyDefenceNumber.Value.ToString()
                );
        }

        public void addQuoteButton_Click(object sender, EventArgs e)
        {

            f1.quoteListBox.Items.Add(
                f1.quoteTextBox.Text
                );
        }

        public void removeQuoteButton_Click(object sender, EventArgs e)
        {
            f1.quoteListBox.Items.Remove(f1.quoteListBox.SelectedItem);
        }

        public void addLootButton_Click(object sender, EventArgs e)
        {
            string[] lines = File.ReadAllLines($".\\Data\\Items\\{f1.enemyLootComboBox.SelectedItem}");
            string id = "", name = "";
            foreach(string line in lines)
            {
                string _property = line.Split(':')[0].Trim().ToLower();
                string convertedProperty = line.Split(':')[1].Trim().Replace("\"", string.Empty);
                if (_property == "id")
                    id = convertedProperty;
                if (_property == "name")
                    name = convertedProperty;
            }
            f1.enemyLootListBox.Items.Add(
                $"{id},{f1.enemyLootChanceNumberic.Value},{name}"
                );
        }

        public void removeLootButton_Click(object sender, EventArgs e)
        {
            f1.enemyLootListBox.Items.Remove(f1.enemyLootListBox.SelectedItem);
        }

        public void addSpellButton_Click(object sender, EventArgs e)
        {


        }

        public void removeSpellButton_Click(object sender, EventArgs e)
        {
            f1.enemySpellsListbox.Items.Remove(f1.enemySpellsListbox.SelectedItem);
        }
    }
}
