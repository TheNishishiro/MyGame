using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.GridElements;
using MyGame.GridElements.Specials;
using MyGame.Items;
using MyGame.Items.ItemTypes;
using MyGame.UI;
using MyGame.UI.Controls;
using NFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Class containing functions used only by Creatures

namespace MyGame.Creatures
{
    class baseCreature : baseEntity, ICreature
    {
        public const string
            HP = "health",
            HP_max = "health_max",
            Damage = "damage",
            DamageElemental = "elementaldamage",
            Level = "level",
            Exp = "experience",
            Exp_max = "experience_max",
            Mana = "mana",
            Mana_max = "mana_xax",
            Magic_Level = "magic_level",
            Magic_Level_points = "magic_level_points",
            Magic_Level_max_points = "magic_level_max_points",
            AttackSpeed = "attackspeed",
            Regeneration = "regeneration",
            ManaRegeneration = "manaregeneration",
            Points = "levelpoints";

        public Dictionary<string,int> baseStats;

        protected Texture2D texture;
        public Vector2 moveToPosition;
        protected bool moving = false;
        protected bool canMove = true;
        private int decision;
        protected int RegenTimer = 0, RegenTimerReset = 180;
        protected int MPRegenTimer = 0, MPRegenTimerReset = 240;
        protected List<FadingLabel> FL = new List<FadingLabel>();
        protected List<string> Dialogs = new List<string>();
        protected List<string> Loot = new List<string>();
        protected int Gold = 0, Gold_Chance = 0;
        protected Dictionary<string, int> damage = null;
        protected Dictionary<string, int> defences = null;
        protected Label nameLabel;
        protected string Description;

        public virtual void Init()
        {
            baseStats = new Dictionary<string, int>();
            damage = new Dictionary<string, int>();
            damage.Add("Physical", 10);
            defences = new Dictionary<string, int>();
            baseStats.Add(HP, 0);
            baseStats.Add(HP_max, 0);
            baseStats.Add(Level, 0);
            baseStats.Add(Exp, 0);
            baseStats.Add(Exp_max, 0);
            baseStats.Add(Mana, 0);
            baseStats.Add(Mana_max, 0);
            baseStats.Add(AttackSpeed, 60);
            baseStats.Add(Regeneration, 0);
            baseStats.Add(ManaRegeneration, 0);
            if(Settings.grid!=null)
                SetWalkable(false);
        }

        public virtual void Move()
        {
            if(!moving)
                decision = Settings.rnd.Next(1000);

            if (decision == 1 && !moving && Position.Y - Settings.GridSize >= 0 && isWalkable(Position.X, Position.Y - Settings.GridSize))
            { moving = true; moveToPosition = Position; moveToPosition.Y -= Settings.GridSize; SetWalkable(true); }
            if (decision == 3 && !moving && Position.Y + Settings.GridSize < Settings.WorldSizePixels && isWalkable(Position.X, Position.Y + Settings.GridSize))
            { moving = true; moveToPosition = Position; moveToPosition.Y += Settings.GridSize; SetWalkable(true); }
            if (decision == 5 && !moving && Position.X - Settings.GridSize >= 0 && isWalkable(Position.X - Settings.GridSize, Position.Y))
            { moving = true; moveToPosition = Position; moveToPosition.X -= Settings.GridSize; SetWalkable(true); }
            if (decision == 8 && !moving && Position.X + Settings.GridSize < Settings.WorldSizePixels && isWalkable(Position.X + Settings.GridSize, Position.Y))
            { moving = true; moveToPosition = Position; moveToPosition.X += Settings.GridSize; SetWalkable(true); }

            if (moving)
                AlignToGrid();
        }

        public ICreature CreateCopy(Vector2 position)
        {
            return new baseEnemy(texture, position, name, baseStats, Dialogs, Loot, Description, damage, defences, Gold, Gold_Chance);
        }

        public virtual void Update()
        {
            RegenerateHP();

            if (Settings.rnd.Next(5000) > 4990 && Dialogs.Count > 0 && fighting)
                FL.Add(new FadingLabel(Dialogs[Settings.rnd.Next(Dialogs.Count)], Position, Color.White, 0.2f));

            UpdateBounds();
            if(canMove)
                Move();
            healthBar.Update(baseStats[HP], baseStats[HP_max], new Vector2(Position.X, Position.Y - 16));
        }

        public virtual void Draw(ref SpriteBatch sb)
        {
            NDrawing.Draw(ref sb, texture, Position, color, layerDepth);
            
            healthBar.Update(GetHealth(), GetMaxHealth(), Position);
            healthBar.Draw(ref sb);
            nameLabel.UpdatePosition(new Vector2(Position.X, Position.Y - 32));
            nameLabel.Draw(ref sb, Settings.UILayer);
            menuManagment(ref sb);
            DrawAggroRectangle(ref sb);
            MenuControls.FadingLabelManager(ref sb, FL);
        }

        protected void UpdateBounds()
        {
            bounds.X = (int)Position.X;
            bounds.Y = (int)Position.Y;
        }

        public void Die(ICreature player)
        {
            SetWalkable(true);
            bool looted = false;
            player.AddExp(baseStats[Exp]);
            if(Settings.rnd.Next(Settings.MaxRandomValue) <= Gold_Chance)
            {
                AddGold(Gold);
            }
            if (Settings.rnd.Next(2) == 0)
            {
                foreach(string l in Loot)
                {
                    string[] properties = l.Split(',');
                    string itemID = properties[0].Trim();
                    int Chance = int.Parse(properties[1].Trim());

                    if(Settings.rnd.Next(Settings.MaxRandomValue) <= Chance + (Settings._player.Stats[Names.Luck]-1)*10)
                    {
                        looted = true;
                        Settings.grid.map[(int)(Position.X / Settings.GridSize), (int)(Position.Y / Settings.GridSize)].AddAddition(
                            new Bag(
                            Textures.ItemTemplates[itemID].CreateCopy()
                            , new Vector2(Position.X, Position.Y)
                            ));
                    }
                }

                
            }
            if (!looted)
            {
                string[] keys = Textures.SpawnableAdditionTemplates.Keys.ToArray();
                Settings.grid.map[(int)(Position.X / Settings.GridSize), (int)(Position.Y / Settings.GridSize)].AddAddition(Textures.SpawnableAdditionTemplates[keys[Settings.rnd.Next(keys.Length)]].CreateCopy(new Vector2(Position.X, Position.Y)));
            }
        }

        protected void RegenerateHP()
        {
            if (baseStats[HP] < baseStats[HP_max] && RegenTimer <= 0)
            {
                HealUp(baseStats[Regeneration]);
                RegenTimer = RegenTimerReset;
            }
            if (RegenTimer > 0)
                RegenTimer--;
        }

        protected void RegenerateMana()
        {

            if (baseStats[Mana] < baseStats[Mana_max] && MPRegenTimer <= 0)
            {
                AddMana(baseStats[ManaRegeneration]);
                MPRegenTimer = MPRegenTimerReset;
            }
            if (MPRegenTimer > 0)
                MPRegenTimer--;
        }

        protected void AlignToGrid()
        {

            if (Position.X > moveToPosition.X)
                Position.X -= 1;
            else if (Position.X < moveToPosition.X)
                Position.X += 1;

            if (Position.Y > moveToPosition.Y)
                Position.Y -= 1;
            else if (Position.Y < moveToPosition.Y)
                Position.Y += 1;

            if (Position == moveToPosition)
            {
                SetWalkable(false);
                moving = false;
                return;
            }
        }

        public virtual Dictionary<string, int> GetDamage()
        {
            return damage;
        }
        public int GetHealth()
        {
            return baseStats[HP];
        }
        public int GetMaxHealth()
        {
            return baseStats[HP_max];
        }
        public int GetMana()
        {
            return baseStats[Mana];
        }
        public int GetMaxMana()
        {
            return baseStats[Mana_max];
        }
        public int GetExp()
        {
            return baseStats[Exp];
        }
        public void AddExp(int amount)
        {
            baseStats[Exp] += amount;
        }
        public int GetExpMax()
        {
            return baseStats[Exp_max];
        }
        public int GetLevel()
        {
            return baseStats[Level];
        }
        public bool GetFightingState()
        {
            return fighting;
        }
        public void HealUp(int amount)
        {
            baseStats[HP] += amount;
            if (baseStats[HP] > baseStats[HP_max])
                baseStats[HP] = baseStats[HP_max];
        }
        public void AddMana(int amount)
        {
            baseStats[Mana] += amount;
            if (baseStats[Mana] > baseStats[Mana_max])
                baseStats[Mana] = baseStats[Mana_max];
        }

        public virtual Dictionary<string, int> DealDamage()
        {
            return GetDamage();
        }

        public virtual void TakeDamage(Dictionary<string, int> damage)
        {
            int dmg = 0;
            foreach(KeyValuePair<string, int > entry in damage)
            {
                float reduction = 0;
                if(defences.ContainsKey(entry.Key))
                {
                    reduction = entry.Value * ((float)defences[entry.Key]/100);
                }
                dmg += Settings.rnd.Next((int)(entry.Value - reduction));

                if (entry.Key == "Physical")
                    dmg = (int)(dmg * (1.0 + ((float)(Settings._player.Stats[Names.Strength]-1) / 20)));
            }
            if(dmg > 0)
                FL.Add(new FadingLabel($"-{dmg}", Position, Color.Red, 0.5f));
            baseStats[HP] -= dmg;
        }
        public virtual void LevelUp()
        {
            
        }
        public Vector2 GetPosition()
        {
            return Position;
        }
        public Rectangle GetBounds()
        {
            return bounds;
        }
        public float GetAttackSpeed()
        {
            return baseStats[AttackSpeed];
        }
        public int GetMagicLevel()
        {
            return baseStats[Magic_Level];
        }
        public int GetMagicLevelPoints()
        {
            return baseStats[Magic_Level_points];
        }
        public int GetMaxMagicLevelPoints()
        {
            return baseStats[Magic_Level_max_points];
        }
        public int GetLevelPoints()
        {
            return baseStats[Points];
        }
        public string GetDescription()
        {
            return Description;
        }
        public string GetName()
        {
            return name;
        }
    }
}
