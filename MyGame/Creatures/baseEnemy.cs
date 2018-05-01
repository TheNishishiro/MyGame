using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Creatures
{
    class baseEnemy : baseCreature
    {
        public baseEnemy(Texture2D texture, Vector2 Position, string name,
            Dictionary<string, int> baseStats, List<string> Dialogs, List<string> Loot, string Desc,
            Dictionary<string, int> damage, Dictionary<string, int> defences, int Gold, int Gold_Chance,
            Dictionary<string, int> Spells)
        {
            layerDepth = Settings.entityLayer;
            this.Spells = Spells;
            this.name = name;
            this.texture = texture;
            this.Position = new Vector2(Position.X, Position.Y);
            bounds = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
            color = Color.White;
            healthBar = new ProgresBar(Color.Red, Color.DarkGreen, new Rectangle(bounds.X, bounds.Y - 32, 32, 8), Textures.UIBarBorderTexture);
            Init();
            this.Gold = Gold;
            this.Gold_Chance = Gold_Chance;
            this.defences = defences;
            nameLabel = new UI.Controls.Label(name, Position, Color.White);
            this.Dialogs = Dialogs;
            this.Loot = Loot;
            this.damage = damage;
            this.baseStats[HP] = this.baseStats[HP_max] = baseStats[HP];
            this.baseStats[Exp] = baseStats[Exp];
            this.baseStats[Level] = baseStats[Level];
            this.baseStats[AttackSpeed] = baseStats[AttackSpeed];
            Description = Desc;
        }

        protected override void SetButtons()
        {
            DPL.AddButton("Engage", () => FightToggle());
            DPL.AddButton("Information", () => ShowInfo());
            DPL.AddButton("Quit", () => quitMenu());
        }

        private void ShowInfo()
        {
            Settings._player._UI.container = new UI.Controls.Container(name, Description);
            quitMenu();
        }
    }
}
