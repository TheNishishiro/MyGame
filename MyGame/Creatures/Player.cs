using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Creatures;
using MyGame.Items;
using MyGame.Spells;
using MyGame.UI;
using MyGame.UI.Controls;
using NFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static MyGame.Settings;

namespace MyGame
{
    class Player : baseCreature
    {
        public MainUI _UI;
        public Dictionary<string, int> Stats = new Dictionary<string, int>();
        public Dictionary<string, int> Skills = new Dictionary<string, int>();
        public List<IItems> Inventory = new List<IItems>();
        public ISpell[] Spells = new ISpell[12];
        public Dictionary<string, int> Materials = new Dictionary<string, int>();
        public Dictionary<string, IItems> Equiped = new Dictionary<string, IItems>();

        public bool ShowCheatMenu = false;

        int baseHealth = 100, healthPerLevel = 50;
        int baseExp = 100;
        int baseAS = 80;
        int baseRegeneration = 1, baseRegenTimerReset = 180;


        public Player(float posX, float posY, Texture2D texture)
        {
            Position = new Vector2(posX, posY);
            bounds = new Rectangle((int)posX, (int)posY, texture.Width, texture.Height);
            this.texture = texture;
            color = Color.White;
            healthBar = new ProgresBar(Color.Red, Color.DarkGreen, new Rectangle(bounds.X, bounds.Y - 32, 32, 8), Textures.UIBarBorderTexture);
            Init();

            Materials.Add(Names.Material_Wood, 0);
            Materials.Add(Names.Material_Stone, 0);
            Materials.Add(Names.Material_Gold, 0);

            Equiped.Add(Names.Necklace, null);
            Equiped.Add(Names.Shield, null);
            Equiped.Add(Names.Weapon, null);
            Equiped.Add(Names.Armor, null);
            Equiped.Add(Names.Ring, null);

            baseStats[Points] = 0;

            baseStats[Damage] = 10;
            baseStats[HP] = baseStats[HP_max] = 100;
            baseStats[Mana] = baseStats[Mana_max] = 70;
            baseStats[Exp_max] = 100;
            baseStats[Magic_Level] = 1;
            baseStats[Magic_Level_points] = 0;
            baseStats[Magic_Level_max_points] = 100;
            baseStats[Level] = 1;
            baseStats[Regeneration] = 1;
            baseStats[ManaRegeneration] = 1;

            SetUpSkill(Names.Fist);
            SetUpSkill(Names.Sword);
            SetUpSkill(Names.Axe);
            SetUpSkill(Names.Mace);
            SetUpSkill(Names.Mining);
            SetUpSkill(Names.Defence);

            Stats.Add(Names.Strength, 1); // Implemented in TakeDamage() in baseCreature
            Stats.Add(Names.Inteligence, 1);
            Stats.Add(Names.Dexterity, 1); 
            Stats.Add(Names.Vitality, 1);
            Stats.Add(Names.Luck, 1); // Implemented in Die() in baseCreature
            Stats.Add(Names.Greed, 1); // Implemented in AddGold() in baseEntity
            Stats.Add(Names.Survivability, 1); // Effects not implemented
            Stats.Add(Names.Faith, 1); // Implemented in Cast() in Miracle
            Stats.Add(Names.Resistance, 1); // Implemented in TakeDamage()
            Stats.Add(Names.Endurance, 1);

            for(int i = 0; i < 12; i++)
            {
                Spells[i] = null;
            }

            Spells[0] = Textures.SpellTemplates["1"].CreateCopy();
            Spells[1] = Textures.SpellTemplates["2"].CreateCopy();
        }

        private void SetUpSkill(string name)
        {
            Skills.Add(name + Names.SkillLevel, 1);
            Skills.Add(name + Names.SkillLevelPoints, 0);
            Skills.Add(name + Names.SkillLevelPointsNeeded, 50);
        }

        public override void Move()
        {
            
            if (Keyboard.GetState().IsKeyDown(Keys.W) && !moving && Position.Y - Settings.GridSize >= 0 && isWalkable(Position.X, Position.Y - Settings.GridSize))
            {  moving = true; moveToPosition = Position; moveToPosition.Y -= Settings.GridSize; SetWalkable(true); }
            if (Keyboard.GetState().IsKeyDown(Keys.S) && !moving && Position.Y + Settings.GridSize < Settings.WorldSizePixels && isWalkable(Position.X, Position.Y + Settings.GridSize))
            {  moving = true; moveToPosition = Position; moveToPosition.Y += Settings.GridSize; SetWalkable(true); }
            if (Keyboard.GetState().IsKeyDown(Keys.A) && !moving && Position.X - Settings.GridSize >= 0 && isWalkable(Position.X - Settings.GridSize, Position.Y))
            {  moving = true; moveToPosition = Position; moveToPosition.X -= Settings.GridSize; SetWalkable(true); }
            if (Keyboard.GetState().IsKeyDown(Keys.D) && !moving && Position.X + Settings.GridSize < Settings.WorldSizePixels && isWalkable(Position.X + Settings.GridSize, Position.Y))
            {  moving = true; moveToPosition = Position; moveToPosition.X += Settings.GridSize; SetWalkable(true); }

            if (moving)
                AlignToGrid();
        }
        
        public override void Update()
        {
            
            Move();
            UpdateBounds();
            CalculateStatsBonuses();
            healthBar.Update(baseStats[HP], baseStats[HP_max], new Vector2(Position.X, Position.Y - 16));
            if (baseStats[HP] < 0)
                baseStats[HP] = 0;

            if (baseStats[Exp] >= baseStats[Exp_max])
                LevelUp();
            RegenerateHP();
            RegenerateMana();
            UpdateSkills();
            CheckSpellCast();
        }

        private void CalculateStatsBonuses()
        {

            baseStats[HP_max] = (int)((baseHealth + (healthPerLevel * (baseStats[Level] - 1))) * (1.0 + ((double)Stats[Names.Endurance]-1)/20));
            baseStats[Exp_max] = (int)((baseExp + (baseExp * (baseStats[Level] - 1))) * (1.0 - ((double)Stats[Names.Inteligence] - 1) / 20));
            baseStats[Regeneration] = baseRegeneration + ((Stats[Names.Vitality] - 1) / 4);
            RegenTimerReset = baseRegenTimerReset - ((Stats[Names.Vitality] - 1) * 2);
            if (RegenTimerReset < 60)
                RegenTimerReset = 60;
            baseStats[AttackSpeed] = baseAS - (Stats[Names.Dexterity]-1);
        }

        public override void Draw(ref SpriteBatch sb)
        {
            menuManagment(ref sb);
            NDrawing.Draw(ref sb, texture, Position, Color.White, Settings.entityLayer);
            healthBar.Update(GetHealth(), GetMaxHealth(), Position);
            healthBar.Draw(ref sb);
            MenuControls.FadingLabelManager(ref sb, FL);
        }

        protected override void SetButtons()
        {
            if (ShowCheatMenu)
            {
                DPL.AddButton("Level Up", () => LevelUp());
                DPL.AddButton("Add item", () => AddItem());
            }
            DPL.AddButton("Quit", () => quitMenu());
        }

        public override void AddMana(int amount)
        {
            base.AddMana(amount);

            if(amount < 0)
            {
                baseStats[Magic_Level_points] -= amount / 2;
                if(baseStats[Magic_Level_points] >= baseStats[Magic_Level_max_points])
                {
                    baseStats[Magic_Level]++;
                    baseStats[Magic_Level_points] = 0;
                    baseStats[Magic_Level_max_points] += 50;
                }
            }
        }

        public void CheckSpellCast()
        {
            NControls._NewKeyState();
            if (NControls.GetSingleKeyPress(Keys.F1) && Spells[0] != null)
                CastSpell(0);
            else if (NControls.GetSingleKeyPress(Keys.F2) && Spells[1] != null)
                CastSpell(1);
            else if (NControls.GetSingleKeyPress(Keys.F3) && Spells[2] != null)
                CastSpell(2);
            else if (NControls.GetSingleKeyPress(Keys.F4) && Spells[3] != null)
                CastSpell(3);
            else if (NControls.GetSingleKeyPress(Keys.F5) && Spells[4] != null)
                CastSpell(4);
            else if (NControls.GetSingleKeyPress(Keys.F6) && Spells[5] != null)
                CastSpell(5);
            else if (NControls.GetSingleKeyPress(Keys.F7) && Spells[6] != null)
                CastSpell(6);
            else if (NControls.GetSingleKeyPress(Keys.F8) && Spells[7] != null)
                CastSpell(7);
            else if (NControls.GetSingleKeyPress(Keys.F9) && Spells[8] != null)
                CastSpell(8);
            else if (NControls.GetSingleKeyPress(Keys.F10) && Spells[9] != null)
                CastSpell(9);
            else if (NControls.GetSingleKeyPress(Keys.F11) && Spells[10] != null)
                CastSpell(10);
            else if (NControls.GetSingleKeyPress(Keys.F11) && Spells[11] != null)
                CastSpell(11);
            NControls._OldKeyState();
        }

        public void CastSpell(int id)
        {
            if (Spells[id].GetManaCost() <= baseStats[Mana])
            {
                Spells[id].Cast(Position);
                AddMana(-Spells[id].GetManaCost());
            }
        }

        public void AddItem()
        {
            Inventory.Add(ItemFactory.CreateItem());
        }

        public override void LevelUp()
        {
            base.LevelUp();
            baseStats[Level]++;
            baseStats[Points]++;
            baseStats[Exp] = 0;
            baseStats[HP] = baseStats[HP_max];
            baseStats[Mana] = baseStats[Mana_max] = (baseStats[Mana_max] + 20);
            
        }

        private void UpdateSkills()
        {
            if (baseStats[Exp] >= baseStats[Exp_max])
                LevelUp();

            CheckSkillLevel(Names.Sword);
            CheckSkillLevel(Names.Fist);
            CheckSkillLevel(Names.Axe);
            CheckSkillLevel(Names.Mace);
            CheckSkillLevel(Names.Mining);
            CheckSkillLevel(Names.Defence);

        }

        private void CheckSkillLevel(string name)
        {
            if (Skills[name + Names.SkillLevelPoints] >= Skills[name + Names.SkillLevelPointsNeeded])
            {
                Skills[name + Names.SkillLevel]++;
                Skills[name + Names.SkillLevelPoints] = 0;
                Skills[name + Names.SkillLevelPointsNeeded] += 50;
            }
        }

        public override Dictionary<string, int> DealDamage()
        {
            string damageModifier = "";
            int levelModifier = 0;
            if (Equiped[Names.Weapon] != null)
                foreach (KeyValuePair<string, int> entry in Equiped[Names.Weapon].GetDamage())
                {
                    levelModifier += (int)Math.Ceiling((float)entry.Value / 10);
                }
            else
                levelModifier += damage["Physical"] / 10;

            try
            {
                if (Equiped[Names.Weapon] != null)
                {
                    Skills[Equiped[Names.Weapon].GetSkill() + Names.SkillLevelPoints] += levelModifier;
                    damageModifier = Equiped[Names.Weapon].GetSkill() + Names.SkillLevel;
                    Equiped[Names.Weapon].DecreaseDurability();
                }
                else
                {
                    Skills[Names.Fist + Names.SkillLevelPoints] += levelModifier;
                    damageModifier = Names.Fist + Names.SkillLevel;
                }
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine($"Couldn't add skill points to {Equiped[Names.Weapon].GetSkill()} as it coulnd't be verified as a valid skill");
            }

            if (Equiped[Names.Weapon] == null)
                return damage;
            else
                return Equiped[Names.Weapon].GetDamage();
        }

        public override void TakeDamage(Dictionary<string, int> _damage)
        {
            int dmg = 0;
            foreach (KeyValuePair<string, int> entry in _damage)
            {
                float reduction = 0;
                dmg += rnd.Next((entry.Value));
                if (Equiped[Names.Armor] != null)
                {
                    reduction = (float)Math.Ceiling(dmg * (Equiped[Names.Armor].GetDefence(entry.Key) / 100));
                }
                if (Equiped[Names.Shield] != null)
                {
                    reduction += (float)Math.Ceiling(dmg * (Equiped[Names.Shield].GetDefence(entry.Key) / 100));
                }
                reduction += (float)Math.Ceiling(dmg * ((float)(Stats[Names.Resistance]-1) / 100));
                Skills[Names.Defence + Names.SkillLevelPoints] += (int)(reduction);
                if (reduction > dmg)
                    reduction = dmg;
                dmg += (int)(-reduction);
            }
            if (dmg > 0)
                FL.Add(new FadingLabel($"-{dmg}", Position, Color.Red, 0.5f));
            baseStats[HP] -= dmg;
        }

        public void IncreaseStat(string name)
        {
            Stats[name]++;
            baseStats[Points]--;
        }
    }
}
