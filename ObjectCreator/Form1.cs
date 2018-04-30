using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyGame;

namespace ObjectCreator
{
    public partial class Form1 : Form
    {
        string[] enemyTextures = File.ReadAllLines(".\\Data\\EnemyTextures.xml");
        string[] enemies = Directory.GetFiles(".\\Data\\Enemies\\");

        public Form1()
        {
            InitializeComponent();

            foreach (string enemy in enemyTextures)
            {
                string entry = enemy.Split(',')[1].Split(':')[1].Split('\\')[1].Trim().Replace("\"", string.Empty);
                listOfEnemyTextures.Items.Add(entry);
            }
            foreach (string enemy in enemies)
            {
                string entry = enemy.Split('\\')[3].Trim();
                enemyDPL.Items.Add(entry);
            }

            enemyDamageTypeDPL.Items.Add("Physical");
            enemyDefenceDPL.Items.Add("Physical");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string picLink = enemyTextures[listOfEnemyTextures.SelectedIndex];
            string _picLink = picLink.Split(',')[1].Split(':')[1].Replace("\"", string.Empty).Trim();
            enemyTextureID.Text = picLink.Split(',')[0].Split(':')[1].Replace("\"", string.Empty).Trim();
            EnemyTexturePreview.BackgroundImage = Image.FromFile(".\\Data\\" + _picLink);
        }

        private void enemyAddDamageButton_Click(object sender, EventArgs e)
        {

            enemyDamageListBox.Items.Add(
                enemyDamageTypeDPL.SelectedItem + "," + enemyDamageNumber.Value.ToString()
                );
        }

        private void enemyRemoveDamageButton_Click(object sender, EventArgs e)
        {
            enemyDamageListBox.Items.Remove(enemyDamageListBox.SelectedItem);
        }

        private void enemyLoad_Click(object sender, EventArgs e)
        {
            enemyDamageListBox.Items.Clear();
            enemyDefenceListBox.Items.Clear();
            string[] lines = File.ReadAllLines(enemies[enemyDPL.SelectedIndex]);
            foreach(string line in lines)
            {
                string _property = line.Split(':')[0].Trim().ToLower();
                string convertedProperty = line.Split(':')[1].Trim().Replace("\"", string.Empty);

                if (_property == "textureid")
                    enemyTextureID.Text = convertedProperty;
                else if (_property == "name")
                    enemyNameTextbox.Text = convertedProperty;
                else if (_property == "dialog")
                {

                }
                else if (_property == "id")
                    enemyIDTextbox.Text = convertedProperty;
                else if (_property == "description")

                {

                }
                else if (_property == "defence")
                {
                    enemyDefenceListBox.Items.Add(convertedProperty);
                }
                else if (_property == "damage")
                {
                    enemyDamageListBox.Items.Add(convertedProperty);
                }
                else if (_property == "loot")
                {

                }
                else if (_property == "gold")
                {
                    
                }
                else
                {
                    //if (!stats.ContainsKey(_property))
                    //{
                    //    stats.Add(_property, int.Parse(convertedProperty));
                    //}
                }
            }

            foreach(string texture in enemyTextures)
            {
                if(texture.Contains(enemyTextureID.Text))
                {
                    string _picLink = texture.Split(',')[1].Split(':')[1].Replace("\"", string.Empty).Trim();
                    EnemyTexturePreview.BackgroundImage = Image.FromFile(".\\Data\\" + _picLink);
                }
            }
            
        }

        private void enemyAddDefenceButton_Click(object sender, EventArgs e)
        {
            enemyDefenceListBox.Items.Add(
                enemyDefenceDPL.SelectedItem + "," + enemyDefenceNumber.Value.ToString()
                );
        }
    }
}
