using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Items.ItemTypes
{
    class Armor : baseItems
    {
        

        public Armor(string ID, Texture2D texture, string name, string type, int durability, int upgrade, string description, string SkillType, Dictionary<string, int> Defences, Dictionary<string, int> Attribiutes)
        {
            Init();
            this.ID = ID;
            this.Attribiutes = Attribiutes;
            _durability = durability;
            _upgrade = upgrade;
            this.texture = texture;
            this.name = name;
            this.SkillType = SkillType;
            stats[Upgrade] = Settings.rnd.Next(upgrade / 2, (int)(upgrade * 1.5));
            stats[Durability] = durability;
            Description = description;
            Type = type;
            foreach(KeyValuePair<string, int> entry in Defences)
            {
                if (defences.ContainsKey(entry.Key))
                    defences[entry.Key] = entry.Value;
                else
                    defences.Add(entry.Key, entry.Value);
            }

        }

        public override IItems CreateCopy()
        {
            return new Armor(ID, texture, name, Type, _durability, _upgrade, Description, SkillType, defences, Attribiutes);
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
