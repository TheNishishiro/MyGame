using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.GridElements.Specials;
using MyGame.UI;
using MyGame.UI.Controls;
using NFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GridElements
{
    class Addition : baseEntity, ITileAddition
    {
        protected Texture2D texture;
        public bool Walkable { get; set; }
        public bool IsClickable = false;
        public bool CreatesFloatingText = false;
        public bool IsTimeLimited = false;
        public bool IsOnTop = false;
        protected int? cooldown = null;
        protected List<FadingLabel> FL;
        protected string resource = null;
        protected string ButtonRename;
        protected string harvestID = "";
        protected Action action;
        protected int? amount = null;
        protected int? hp = null;
        protected int Rarity = 0;
        protected string biom = "";
        protected Dictionary<string, int> lootChances = null;

        public virtual ITileAddition CreateCopy(Vector2 position)
        {
            bounds = new Rectangle((int)position.X, (int)position.Y, 32, 32);
            return new AnyAddition(texture, new Vector2(position.X, position.Y), Rarity, biom, Walkable, IsClickable, harvestID, CreatesFloatingText, IsTimeLimited, IsOnTop, ButtonRename, hp, cooldown, resource, amount, action);
        }

        public virtual void Draw(ref SpriteBatch sb, float layerIncrease)
        {
            NDrawing.Draw(ref sb, texture, new Vector2(Position.X + (Settings.GridSize - texture.Width), Position.Y + (Settings.GridSize - texture.Height)), color, layerDepth + layerIncrease);
            if (IsClickable)
                Update(ref sb, resource, amount);
            if (IsTimeLimited)
                UpdateTime();
        }

        public virtual void Update(ref SpriteBatch sb, string resource = "", int? amount = 0)
        {
            if (IsClickable)
            {
                menuManagment(ref sb);
                DrawAggroRectangle(ref sb);
            }
            if (CreatesFloatingText)
                CreateFloatingText(resource, amount);
            if (hp <= 0)
                RemoveAdditionFromGrid();
            MenuControls.FadingLabelManager(ref sb, FL);
        }

        private void UpdateTime()
        {
            hp--;
            if (hp < 0)
                RemoveAdditionFromGrid();
        }

        // Creates floating text labeling type and amount of given material
        protected void CreateFloatingText(string resource, int? amount)
        {
            if (fighting && hp > 0 && NAction.Get_Distance_Between_Points(Position, Settings._player.GetPosition()) < Settings.GridSize * 2)
            {
                cooldown--;
                if (cooldown <= 0)
                {
                    hp--;
                    cooldown = 60;
                    FL.Add(new FadingLabel($"+{amount} {resource}", Position, Color.White));
                    Settings._player.Skills[Names.Mining + Names.SkillLevelPoints] += 1;
                    if (amount != null)
                        Settings._player.Materials[resource] += (int)amount;
                }
            }
        }
        public void RemoveAdditionFromGrid()
        {
            Settings.grid.map[(int)(Position.X / Settings.GridSize), (int)(Position.Y / Settings.GridSize)].Walkable = true;
            Settings.grid.map[(int)(Position.X / Settings.GridSize), (int)(Position.Y / Settings.GridSize)].RemoveAddition(this);
        }

        public int GetRarity()
        {
            return Rarity;
        }
        public void SetPosition(Vector2 position)
        {
            Position = position;
        }
        public Vector2 GetPosition()
        {
            return Position;
        }
        public string GetBiom()
        {
            return biom;
        }
    }
}
