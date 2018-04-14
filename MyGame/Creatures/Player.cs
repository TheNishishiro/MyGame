using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Creatures;
using MyGame.UI;
using NFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class Player : baseCreature
    {
        public MainUI _UI;
        int RegenTimer = 0;

        public Player(float posX, float posY, Texture2D texture)
        {
            Position = new Vector2(posX, posY);
            bounds = new Rectangle((int)posX, (int)posY, texture.Width, texture.Height);
            this.texture = texture;
            color = Color.White;
            healthBar = new ProgresBar(Color.Red, Color.DarkGreen, new Rectangle(bounds.X, bounds.Y - 32, 32, 8));
            Init();
            baseStats[Damage] = 20;
            baseStats[HP] = baseStats[HP_max] = 100;
            baseStats[Exp_max] = 100;
            baseStats[Level] = 1;
            
            _UI = new MainUI(this);
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
            if (baseStats[Exp] >= baseStats[Exp_max])
                LevelUp();
            if (baseStats[HP] < baseStats[HP_max] && RegenTimer <= 0)
            {
                HealUp(baseStats[Regeneration]);
                RegenTimer = 120;
            }
            if(RegenTimer > 0)
                RegenTimer--;
        }

        public override void Draw(ref SpriteBatch sb)
        {
            NDrawing.Draw(ref sb, texture, Position, Color.White, Settings.entityLayer);
            healthBar.Update(GetHealth(), GetMaxHealth(), Position);
            healthBar.Draw(ref sb);
            MenuControls.FadingLabelManager(ref sb, FL);
        }
    }
}
