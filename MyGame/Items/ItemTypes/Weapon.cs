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
        bool Equiped = false;

        public Weapon(Texture2D texture, string name, string type, int damage, int durability, int elementalDamage, int upgrade, string description, string SkillType)
        {
            Init();
            _damage = damage;
            _durability = durability;
            _elementalDamage = elementalDamage;
            _upgrade = upgrade;
            this.texture = texture;
            this.name = name;
            this.SkillType = SkillType;
            stats[Damage] = Settings.rnd.Next(damage/2, (int)(damage*1.5));
            stats[ElementalDamage] = Settings.rnd.Next(elementalDamage/2, (int)(elementalDamage *1.5));
            stats[Upgrade] = Settings.rnd.Next(upgrade/2, (int)(upgrade*1.5));
            stats[Durability] = durability;
            Description = description;
            Type = type;

        }

        protected override void SetButtons()
        {
            if (!Equiped && Settings._player.Equiped[Type] == null)
            {
                DPL.AddButton("Equip", () => Use());
                DPL.AddButton("Drop", () => Drop());
            }
            else if (Equiped)
                DPL.AddButton("Unequip", () => Use());

            DPL.AddButton("Information", () => ShowInfo());
            DPL.AddButton("Quit", () => quitMenu());
        }

        public override void Use()
        {
            if (!Equiped)
            {
                Settings._player.Equiped[Type] = this;
                Settings._player.Inventory.Remove(this);
                Settings._player.SetDamage(stats[Damage]);
            }
            else
            {
                Settings._player.Inventory.Add(this);
                Settings._player.Equiped[Type] = null;
                Settings._player.SetDamage(-stats[Damage]);
            }
            Equiped = !Equiped;
            quitMenu();
        }

        public override void Break()
        {
            Settings._player.Equiped[Type] = null;
            Settings._player.SetDamage(-stats[Damage]);
        }
    }
}
