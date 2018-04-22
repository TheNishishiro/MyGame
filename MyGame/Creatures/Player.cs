using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Creatures;
using MyGame.Items;
using MyGame.UI;
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
        public List<IItems> Inventory = new List<IItems>();
        public Dictionary<string, int> Materials = new Dictionary<string, int>();
        public Dictionary<string, IItems> Equiped = new Dictionary<string, IItems>();

        public bool ShowCheatMenu = false;

        

        public Player(float posX, float posY, Texture2D texture)
        {
            Position = new Vector2(posX, posY);
            bounds = new Rectangle((int)posX, (int)posY, texture.Width, texture.Height);
            this.texture = texture;
            color = Color.White;
            healthBar = new ProgresBar(Color.Red, Color.DarkGreen, new Rectangle(bounds.X, bounds.Y - 32, 32, 8), Textures.UIBarBorderTexture);
            Init();

            Materials.Add(Settings.Material_Wood, 0);
            Materials.Add(Settings.Material_Stone, 0);
            Materials.Add(Settings.Material_Gold, 0);

            Equiped.Add("Necklace", null);
            Equiped.Add("Shield", null);
            Equiped.Add("Weapon", null);
            Equiped.Add("Armor", null);
            Equiped.Add("Ring", null);

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

            SetUpSkill(Fist);
            SetUpSkill(Sword);
            SetUpSkill(Axe);
            SetUpSkill(Mace);
            SetUpSkill(Mining);
            SetUpSkill(Defence);

            Stats.Add(Strength, 1);
            Stats.Add(Inteligence, 1);
            Stats.Add(Dexterity, 1);
            Stats.Add(Vitality, 1);

            _UI = new MainUI(this);
        }

        private void SetUpSkill(string name)
        {
            Stats.Add(name + SkillLevel, 1);
            Stats.Add(name + SkillLevelPoints, 0);
            Stats.Add(name + SkillLevelPointsNeeded, 50);
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
            healthBar.Update(baseStats[HP], baseStats[HP_max], new Vector2(Position.X, Position.Y - 16));
            if (baseStats[HP] < 0)
                baseStats[HP] = 0;

            if (baseStats[Exp] >= baseStats[Exp_max])
                LevelUp();
            RegenerateHP();
            UpdateSkills();
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

        public void AddItem()
        {
            Inventory.Add(ItemFactory.CreateItem());
        }

        public override void LevelUp()
        {
            base.LevelUp();
            baseStats[Level]++;
            baseStats[Points]++;
            baseStats[Exp_max] = (baseStats[Exp_max] + 100 * baseStats[Level]);
            baseStats[Exp] = 0;
            baseStats[HP] = baseStats[HP_max] = (baseStats[HP_max] + 50);
            baseStats[Mana] = baseStats[Mana_max] = (baseStats[Mana_max] + 20);
        }

        private void UpdateSkills()
        {
            if (baseStats[Exp] >= baseStats[Exp_max])
                LevelUp();

            CheckSkillLevel(Sword);
            CheckSkillLevel(Fist);
            CheckSkillLevel(Axe);
            CheckSkillLevel(Mace);
            CheckSkillLevel(Mining);
            CheckSkillLevel(Defence);

        }

        private void CheckSkillLevel(string name)
        {
            if (Stats[name + SkillLevelPoints] >= Stats[name + SkillLevelPointsNeeded])
            {
                Stats[name + SkillLevel]++;
                Stats[name + SkillLevelPoints] = 0;
                Stats[name + SkillLevelPointsNeeded] += 50;
            }
        }

        public override int DealDamage()
        {
            int _damage = baseStats[Damage];
            string damageModifier = "";
            int levelModifier = 1 + _damage/10;
            try
            {
                if (Equiped["Weapon"] != null)
                {
                    Stats[Equiped["Weapon"].GetSkill() + SkillLevelPoints] += levelModifier;
                    damageModifier = Equiped["Weapon"].GetSkill() + SkillLevel;
                    Equiped["Weapon"].DecreaseDurability();
                }
                else
                {
                    Stats[Fist + SkillLevelPoints] += levelModifier;
                    damageModifier = Fist + SkillLevel;
                }

                _damage = _damage + (_damage * Stats[damageModifier] / 10); // Possibly change to simply (_damage + skilllevel)
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine($"Couldn't add skill points to {Equiped["Weapon"].GetSkill()} as it coulnd't be verified as a valid skill");
            }
            return _damage;
        }

        public override void TakeDamage(int _damage)
        {
            
            base.TakeDamage(_damage);
        }

        public void IncreaseStat(string name)
        {
            Stats[name]++;
            baseStats[Points]--;
        }
    }
}
