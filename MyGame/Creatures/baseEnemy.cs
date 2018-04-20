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
            Dictionary<string, int> baseStats, List<string> Dialogs, List<string> Loot)
        {
            layerDepth = Settings.entityLayer;
            this.name = name;
            this.texture = texture;
            this.Position = new Vector2(Position.X, Position.Y);
            bounds = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
            color = Color.White;
            healthBar = new ProgresBar(Color.Red, Color.DarkGreen, new Rectangle(bounds.X, bounds.Y - 32, 32, 8), Textures.UIBarBorderTexture);
            Init();
            nameLabel = new UI.Controls.Label(name, Position, Color.White);
            this.Dialogs = Dialogs;
            this.Loot = Loot;
            this.baseStats[Damage] = baseStats[Damage];
            this.baseStats[HP] = this.baseStats[HP_max] = baseStats[HP];
            this.baseStats[Exp] = baseStats[Exp];
            this.baseStats[Level] = baseStats[Level];
            this.baseStats[AttackSpeed] = baseStats[AttackSpeed];
        }
    }
}
