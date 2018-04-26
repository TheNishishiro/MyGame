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
            Durability = "durability",
            Upgrade = "upgrade";

        protected int _durability, _upgrade;
        protected bool Equiped = false;
        protected string Type;
        protected string Description;
        protected string SkillType;
        protected Dictionary<string, int> damage = new Dictionary<string, int>();
        protected Dictionary<string, int> stats = new Dictionary<string, int>();
        protected Dictionary<string, int> defences = new Dictionary<string, int>();
        protected Dictionary<string, int> Attribiutes = new Dictionary<string, int>();
        protected Texture2D texture = null;

        protected void Init()
        {
            stats.Add(Durability, 0);
            stats.Add(Upgrade, 0);
            layerDepth = Settings.MainUILayer;
        }

        public IItems CreateCopy()
        {
            if (Type == Names.Weapon)
                return new Weapon(texture, name, Type, _durability, _upgrade, Description, SkillType, damage, Attribiutes);
            else if (Type == Names.Necklace)
                return new Necklace(texture, name, Type, _upgrade, Description, Attribiutes);
            else if (Type == Names.Ring)
                return new Necklace(texture, name, Type, _upgrade, Description, Attribiutes);
            else if (Type == Names.Armor)
                return new Armor(texture, name, Type, _durability, _upgrade, Description, SkillType, defences, Attribiutes);
            else if (Type == Names.Shield)
                return new Armor(texture, name, Type, _durability, _upgrade, Description, SkillType, defences, Attribiutes);

            return null;
        }

        public virtual void Update()
        {
            
        }

        public virtual void Draw(ref SpriteBatch sb, Vector2 position)
        {
            NDrawing.Draw(ref sb, texture, new Rectangle((int)position.X, (int)position.Y, 32, 32), Color.White, layerDepth + 0.01f);
            bounds = new Rectangle((int)position.X, (int)position.Y, 32, 32);
            Position = position;
            menuManagment(ref sb);
        }

        public virtual void Use()
        {
            if (!Equiped)
            {
                Settings._player.Equiped[Type] = this;
                Settings._player.Inventory.Remove(this);
                foreach (KeyValuePair<string, int> entry in Attribiutes)
                {
                    if (Settings._player.Stats.ContainsKey(entry.Key))
                    {
                        Settings._player.Stats[entry.Key] += entry.Value;
                    }
                }
            }
            else
            {
                Settings._player.Inventory.Add(this);
                Settings._player.Equiped[Type] = null;
                foreach (KeyValuePair<string, int> entry in Attribiutes)
                {
                    if (Settings._player.Stats.ContainsKey(entry.Key))
                    {
                        Settings._player.Stats[entry.Key] -= entry.Value;
                    }
                }
            }
            Equiped = !Equiped;
            quitMenu();
        }

        protected void SetEquipableButtons(bool Equiped)
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

        public void ShowInfo()
        {
            string header = $"Type: {SkillType}\n";
            if (Type == Names.Weapon)
            {
                header += "Damage type: ";
                foreach (KeyValuePair<string, int> entry in damage)
                {
                    header += $", {entry.Key}";
                }
                header += $"\nDurability: { stats[Durability]}\n";
            }
            if(Type == Names.Armor)
            {
                header += "Defences: ";
                foreach(KeyValuePair<string, int> entry in defences)
                {
                    header += $", {entry.Key}:{(entry.Value)}%";
                }
                header += $"\nDurability: {stats[Durability]}\n";
            }

            Settings._player._UI.container = new UI.Controls.Container(name, header + Description);
            quitMenu();
        }

        public virtual void Drop()
        {
            Settings._player.Inventory.Remove(this);
        }

        public virtual string GetName()
        {
            return name;
        }

        public Texture2D GetTexture()
        {
            return texture;
        }

        public virtual Dictionary<string, int> GetDamage()
        {
            return damage;
        }

        public void DecreaseDurability()
        {
            stats[Durability]--;
            if (stats[Durability] <= 0)
                Break();
        }

        public virtual void Break()
        {
            throw new NotImplementedException();
        }

        public string GetSkill()
        {
            return SkillType;
        }
        public float GetDefence(string type)
        {
            if (defences.ContainsKey(type))
                return defences[type];
            else
                return 0;
        }
        public Dictionary<string, int> GetAttribiutes()
        {
            return Attribiutes;
        }
    }
}
