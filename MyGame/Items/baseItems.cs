using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.GridElements.Specials;
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

        protected string ID = "";
        protected int _durability = 0, _upgrade;
        protected bool Equiped = false, InCrafting = false, IsCraftingResult = false;
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

        public virtual IItems CreateCopy()
        {
            throw new NotImplementedException();
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
            if(_durability > stats[Durability])
            {
                DPL.AddButton("Fix", () => FixItem());
            }
            if (!Equiped && Settings._player.Equiped[Type] == null && !InCrafting && !IsCraftingResult)
            {
                DPL.AddButton("Equip", () => Use());
                DPL.AddButton("Add to crafting", () => PutInCrafting());
                DPL.AddButton("Drop", () => Drop());
                DPL.AddButton("Discard", () => Discard());
            }
            else if (InCrafting)
                DPL.AddButton("Take out", () => TakeFromCrafting());
            else if(IsCraftingResult)
                DPL.AddButton("Take out", () => TakeFromCraftingResult());
            else if (Equiped)
                DPL.AddButton("Unequip", () => Use());


            DPL.AddButton("Information", () => ShowInfo());
            DPL.AddButton("Quit", () => quitMenu());
        }

        protected void PutInCrafting()
        {
            InCrafting = true;
            Settings._player.Inventory.Remove(this);
            Crafting.AddToSlot(this);
            quitMenu();
        }
        protected void TakeFromCrafting()
        {
            InCrafting = false;
            Settings._player.Inventory.Add(this);
            Crafting.RemoveFromSlot(this);
            quitMenu();
        }
        protected void TakeFromCraftingResult()
        {
            IsCraftingResult = false;
            Settings._player.Inventory.Add(Crafting.Result);
            Crafting.Result = null;
            Crafting.Slot1 = null;
            Crafting.Slot2 = null;
            Crafting.Slot3 = null;
            quitMenu();
        }

        public void SetEquiped()
        {
            Equiped = true;
        }

        protected virtual void FixItem()
        {
            stats[Durability] = _durability;
            quitMenu();
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

        public virtual void Discard()
        {
            Settings._player.Inventory.Remove(this);
        }

        public virtual void Drop()
        {
            if(texture!=null)
                Settings.grid.map[(int)(Settings._player.Position.X / Settings.GridSize), (int)(Settings._player.Position.Y / Settings.GridSize)].AddAddition(
                            new Bag(
                            Textures.ItemTemplates[ID].CreateCopy()
                            , new Vector2(Settings._player.Position.X, Settings._player.Position.Y)
                            ));
            Discard();
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

        public string GetID()
        {
            return ID;
        }

        public void SetAsCraftingResult()
        {
            IsCraftingResult = true;
        }
    }
}
