using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Items.ItemTypes;
using MyGame.UI;
using NFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Items
{
    class baseItems : baseClickable, IItems
    {
        public const string
            Damage = "damage",
            Durability = "durability",
            ElementalDamage = "elementaldamage",
            Upgrade = "upgrade";

        public static string[] Elements = { "Chaos", "Lighting", "Poisonous", "Blessed", "Normal" };
        public int ID;

        protected int _damage, _durability, _elementalDamage, _upgrade; 

        protected string Element;
        protected string Type;
        protected string Description;
        protected Dictionary<string, int> stats = new Dictionary<string, int>();
        protected Dictionary<string, int> defences = new Dictionary<string, int>();
        protected Texture2D texture = null;

        protected void Init()
        {
            stats.Add(Damage, 0);
            stats.Add(Durability, 0);
            stats.Add(ElementalDamage, 0);
            stats.Add(Upgrade, 0);
            Element = Elements[Settings.rnd.Next(Elements.Length)];
            foreach (string element in Elements)
                defences.Add(element, 0);
            layerDepth = Settings.MainUILayer;
        }

        public IItems CreateCopy()
        {
            if (Type == "Weapon")
                return new Weapon(texture, name, Type, _damage, _durability, _elementalDamage, _upgrade, Description);
            return null;
        }

        public virtual void Update()
        {
            
        }

        public virtual void Draw(ref SpriteBatch sb, Vector2 position)
        {
            NDrawing.Draw(ref sb, texture, position, Color.White, layerDepth + 0.01f);
            bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            Position = position;
            menuManagment(ref sb);
        }

        public virtual void Use()
        {

        }

        public virtual void Drop()
        {
            Settings._player.Inventory.Remove(this);
        }

        public virtual string GetName()
        {
            return "";
        }

        public Texture2D GetTexture()
        {
            return texture;
        }

        public virtual int GetStat()
        {
            return 0;
        }
    }
}
