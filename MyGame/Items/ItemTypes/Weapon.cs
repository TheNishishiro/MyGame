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
        public Weapon(string ID, Texture2D texture, string name, string type, int durability, int upgrade, string description, string SkillType, Dictionary<string, int> damage, Dictionary<string, int> Attribiutes)
        {
            Init();
            this.ID = ID;
            this.damage = damage;
            _durability = durability;
            _upgrade = upgrade;
            this.texture = texture;
            this.name = name;
            this.SkillType = SkillType;
            stats[Upgrade] = upgrade;
            stats[Durability] = durability;
            Description = description;
            this.Attribiutes = Attribiutes;
            Type = type;

        }

        public override IItems CreateCopy()
        {
            return new Weapon(ID, texture, name, Type, _durability, _upgrade, Description, SkillType, damage, Attribiutes);
        }

        protected override void SetButtons()
        {
            SetEquipableButtons(Equiped);
        }

        public override void Use()
        {
            base.Use();
        }

        public override void Break()
        {
            Settings._player.Equiped[Type] = null;
        }
    }
}
