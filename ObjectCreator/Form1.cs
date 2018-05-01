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
        public string[] enemyTextures = File.ReadAllLines(".\\Data\\EnemyTextures.xml");
        public string[] itemTextures = File.ReadAllLines(".\\Data\\ItemTextures.xml");
        public string[] enemies = Directory.GetFiles(".\\Data\\Enemies\\");
        public string[] items = Directory.GetFiles(".\\Data\\Items\\");

        public string[] DamageTypes = { "Physical", "Fire", "Magic", "Poison", "Dark", "Frost", "Bleed" };
        public string[] ItemTypes = { "Weapon", "Armor", "Shield", "Necklace", "Ring", "Book", "Scroll", "Consumable", "Misc" };
        EnemyPage ep;
        ItemPage ip;

        public Form1()
        {
            InitializeComponent();
            ep = new EnemyPage(this);
            ip = new ItemPage(this);

        }
        private void button1_Click(object sender, EventArgs e)
        {
            ep.CheckDataValidation();
        }
    }
}
