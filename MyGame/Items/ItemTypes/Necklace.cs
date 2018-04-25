using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Items.ItemTypes
{
    class Necklace : baseItems
    {
        public Necklace(Texture2D texture, string name, string type, int upgrade, string description, Dictionary<string, int> Attribiutes)
        {
            Init();
            this.Attribiutes = Attribiutes;
            _upgrade = upgrade;
            this.texture = texture;
            this.name = name;
            SkillType = type;
            stats[Upgrade] = Settings.rnd.Next(upgrade/2, (int)(upgrade*1.5));
            Description = description;
            Type = type;

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
