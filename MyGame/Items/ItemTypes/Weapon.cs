using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Items.ItemTypes
{
    class Weapon : baseItems
    {
        public Weapon(Texture2D texture, string name, string type, int damage, int durability, int elementalDamage, int upgrade, string description)
        {
            Init();
            _damage = damage;
            _durability = durability;
            _elementalDamage = elementalDamage;
            _upgrade = upgrade;
            this.texture = texture;
            this.name = name;
            stats[Damage] = Settings.rnd.Next(damage/2, (int)(damage*1.5));
            stats[ElementalDamage] = Settings.rnd.Next(elementalDamage/2, (int)(elementalDamage *1.5));
            stats[Upgrade] = Settings.rnd.Next(upgrade/2, (int)(upgrade*1.5));
            stats[Durability] = durability;
            Description = description;
            Type = type;

        }

        protected override void SetButtons()
        {
            DPL.AddButton("Equip", () => Use());
            DPL.AddButton("Drop", () => Drop());
            DPL.AddButton("Quit", () => quitMenu());
        }

        public override void Use()
        {
            quitMenu();
        }
    }
}
