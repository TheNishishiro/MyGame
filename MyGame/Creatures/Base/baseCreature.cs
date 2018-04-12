using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.GridElements.Additions;
using MyGame.UI;
using MyGame.UI.Controls;
using NFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Creatures
{
    class baseCreature : baseEntity, ICreature
    {
        

        protected Texture2D texture;
        public Vector2 moveToPosition;
        protected bool moving = false;
        private int decision;
        protected List<FadingLabel> FL = new List<FadingLabel>();

        public virtual void InitMenu()
        {
            DPL = new UI.DropDownList();

            DPL.AddButton("Engage", () => FightToggle());
            DPL.AddButton("Quit", () => quitMenu());
        }

        public virtual void Init()
        {
            baseStats.Add(HP, 0);
            baseStats.Add(HP_max, 0);
            baseStats.Add(Damage, 0);
            baseStats.Add(Level, 0);
            baseStats.Add(Exp, 0);
            baseStats.Add(Exp_max, 0);
            baseStats.Add(AttackSpeed, 60);
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

        public virtual void Update()
        {
            UpdateBounds();
            Move();
            healthBar.Update(baseStats[HP], baseStats[HP_max], new Vector2(Position.X, Position.Y - 16));
        }

        public virtual void Draw(ref SpriteBatch sb)
        {
            if (fighting == true)
                color = Color.Yellow;
            else
                color = Color.White;
            NDrawing.Draw(ref sb, texture, Position, color, Settings.entityLayer);
            
            healthBar.Update(GetHealth(), GetMaxHealth(), Position);
            healthBar.Draw(ref sb);
            menuManagment(ref sb);

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
            player.AddExp(baseStats[Exp]);
            Settings.grid.map[(int)(Position.X / Settings.GridSize), (int)(Position.Y / Settings.GridSize)].AddAddition(new BloodStain(Game1._Content.Load<Texture2D>("BloodStain"), (int)(Position.X), (int)(Position.Y)));
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

        public int GetDamage()
        {
            return baseStats[Damage];
        }
        public int GetHealth()
        {
            return baseStats[HP];
        }
        public int GetMaxHealth()
        {
            return baseStats[HP_max];
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

        public void DealDamage(int _damage)
        {
            int dmg = Settings.rnd.Next(_damage);
            FL.Add(new FadingLabel($"-{dmg}", Position, Color.Red, 0.2f));
            baseStats[HP] -= dmg;
        }
        public void LevelUp()
        {
            baseStats[Level]++;
            baseStats[Exp_max] = (int)(baseStats[Exp_max] * 1.5);
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
    }
}
