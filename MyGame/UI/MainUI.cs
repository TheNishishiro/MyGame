using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Items;
using MyGame.Spells;
using MyGame.UI.Controls;
using NFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.UI
{
    class MainUI
    {
        enum InfoState
        {
            Inventory = 0,
            Skills = 1,
            Stats = 2,
            Resources = 3,
            Spells = 4
        };

        ICreature player;
        Vector2 position;
        ProgresBar hp_bar, mana_bar, exp_bar, magicLevel_bar;
        ProgresBar fistSkill_bar, swordSkill_bar;
        public static List<FadingLabel> FL = new List<FadingLabel>();
        float healthBarBeginY = 150, healthBarBeginX = 20, healthBarHeight = 10, healthBarWidth = 255;
        public static int InventoryPage = 0;
        public static int StatsPage = 0;
        public static int SkillsPage = 0;
        public static int SpellsPage = 0;
        static InfoState IS;

        Dictionary<string, Button> levelUpButtons = new Dictionary<string, Button>();

        private static Button inventoryPageDown = new Button(Textures.UIArrowLeft, () => SwitchInventoryPage(-1));
        private static Button inventoryPageUp = new Button(Textures.UIArrowRight, () => SwitchInventoryPage(1));
        private static Button statsPageDown = new Button(Textures.UIArrowDown, () => SwitchStatsPage(1));
        private static Button statsPageUp = new Button(Textures.UIArrowUp, () => SwitchStatsPage(-1));
        private static Button skillsPageDown = new Button(Textures.UIArrowDown, () => SwitchSkillsPage(1));
        private static Button skillsPageUp = new Button(Textures.UIArrowUp, () => SwitchSkillsPage(-1));
        private static Button spellPageDown = new Button(Textures.UIArrowDown, () => SwitchSpellPage(1));
        private static Button spellPageUp = new Button(Textures.UIArrowUp, () => SwitchSpellPage(-1));

        private static Dictionary<InfoState, Button> SwitchButtons = new Dictionary<InfoState, Button>();

        public Container container = new Container("Welcome!");

        public MainUI(ICreature _player)
        {
            container.SetScrollableText(Changelog.changelog, 16);
            IS = InfoState.Inventory;
            player = _player;
            hp_bar = new ProgresBar(Textures.UIBarBackgroundTexture, Textures.UIBarTexture, new Rectangle(0, 0, (int)(healthBarWidth), (int)(healthBarHeight )), Color.Red, Textures.UIBarBorderTexture);
            mana_bar = new ProgresBar(Textures.UIBarBackgroundTexture, Textures.UIBarTexture, new Rectangle(0, 0, (int)(healthBarWidth), (int)(healthBarHeight)), Color.Blue, Textures.UIBarBorderTexture);
            exp_bar = new ProgresBar(Textures.UIBarBackgroundTexture, Textures.UIBarTexture, new Rectangle(0, 0, (int)(healthBarWidth), (int)(healthBarHeight)), Color.Yellow, Textures.UIBarBorderTexture);
            magicLevel_bar = new ProgresBar(Textures.UIBarBackgroundTexture, Textures.UIBarTexture, new Rectangle(0, 0, (int)(healthBarWidth), (int)(healthBarHeight)), Color.CornflowerBlue, Textures.UIBarBorderTexture);

            swordSkill_bar = new ProgresBar(Textures.UIBarBackgroundTexture, Textures.UIBarTexture, new Rectangle(0, 0, (int)(healthBarWidth), (int)(healthBarHeight)), Color.Red, Textures.UIBarBorderTexture);
            fistSkill_bar = new ProgresBar(Textures.UIBarBackgroundTexture, Textures.UIBarTexture, new Rectangle(0, 0, (int)(healthBarWidth), (int)(healthBarHeight)), Color.Red, Textures.UIBarBorderTexture);

            foreach(KeyValuePair<string, int> entry in Settings._player.Stats)
            {
                levelUpButtons.Add(entry.Key, new Button(Textures.UIPlusIcon, () => Settings._player.IncreaseStat(entry.Key)));
            }

            foreach (InfoState _is in Enum.GetValues(typeof(InfoState)))
            {
                SwitchButtons.Add(_is, new Button(Textures.UIButtons[(int)_is], () => SetState(_is)));
            }
        }

        public void Draw(ref SpriteBatch sb)
        {
            position = new Vector2(player.GetPosition().X + 15 - Game1._GraphicsDevice.Viewport.Width / 2 + NCamera.CameraXOffset, player.GetPosition().Y + 20 - Game1._GraphicsDevice.Viewport.Height / 2 + NCamera.CameraYOffset);
            DrawContainer(ref sb);
            DrawOnUIButtons(ref sb);
            DrawEquiped(ref sb);
            if (MenuControls.MouseOver(new Rectangle((int)position.X, (int)position.Y, Textures.UIMainSideBar.Width - 45, Textures.UIMainSideBar.Height)))
                MenuControls.SetMouseLayer(Settings.MainUILayer);
            DrawBars(ref sb);
            DrawUIElements(ref sb);
            MenuControls.FadingLabelManager(ref sb, FL);
        }

        private static void SetState(InfoState _IS)
        {
            IS = _IS;
        }

        private static void SwitchInventoryPage(int i)
        {
            InventoryPage += i;
        }
        private static void SwitchStatsPage(int i)
        {
            StatsPage += i;
        }
        private static void SwitchSkillsPage(int i)
        {
            SkillsPage += i;
        }
        private static void SwitchSpellPage(int i)
        {
            SpellsPage += i;
        }
        private void DrawLevelUpButton(ref SpriteBatch sb, Vector2 TextPosition, string name, int offset, int XPlusOffset)
        {
            if (Settings._player.GetLevelPoints() > 0)
            {
                levelUpButtons[name].Update(new Vector2(TextPosition.X + XPlusOffset, TextPosition.Y + offset));
                levelUpButtons[name].Draw(ref sb);
            }
        }

        private void DrawOnUIButtons(ref SpriteBatch sb)
        {
            int start = -25;
            int offset = 0;
            int offsetStep = 35;
            foreach(KeyValuePair<InfoState, Button> entry in SwitchButtons)
            {
                entry.Value.Update(new Vector2(position.X + start + offset, position.Y + 325));
                entry.Value.Draw(ref sb);
                offset += offsetStep;
            }





            if (IS == InfoState.Inventory)
                DrawInventory(ref sb);
            else if (IS == InfoState.Skills)
                DrawSkills(ref sb);
            else if (IS == InfoState.Stats)
                DrawStats(ref sb);
            else if (IS == InfoState.Resources)
                DrawResources(ref sb);
            else if (IS == InfoState.Spells)
                DrawSpells(ref sb);
        }

        private void DrawContainer(ref SpriteBatch sb)
        {
            if (container != null)
            {
                container.Update(new Vector2(position.X + Textures.UIMainSideBar.Width, position.Y + 100));
                container.Draw(ref sb, Settings.MainUILayer);
                if (container.destroy)
                    container = null;
            }
        }

        private void DrawBars(ref SpriteBatch sb)
        {
            Vector2 Start = new Vector2(position.X + healthBarBeginX, position.Y + healthBarBeginY);
            int offset = 32;
            int offsetStep = 32;
            sb.DrawString(Settings.font3, $"Health: {player.GetHealth()}/{player.GetMaxHealth()}", new Vector2(position.X + healthBarBeginX, position.Y + healthBarBeginY), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, Settings.MainUILayer + 0.0038f);
            hp_bar.Update(player.GetHealth(), player.GetMaxHealth(), new Vector2(position.X + healthBarBeginX, position.Y + healthBarBeginY + offset));
            hp_bar.Draw(ref sb, Settings.MainUILayer + 0.0038f);
            sb.DrawString(Settings.font3, $"Mana: {player.GetMana()}/{player.GetMaxMana()}", new Vector2(position.X + healthBarBeginX, position.Y + healthBarBeginY + offset), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, Settings.MainUILayer + 0.0038f);
            offset += offsetStep;
            mana_bar.Update(player.GetMana(), player.GetMaxMana(), new Vector2(position.X + healthBarBeginX, position.Y + healthBarBeginY + offset));
            mana_bar.Draw(ref sb, Settings.MainUILayer + 0.0038f);
            sb.DrawString(Settings.font3, $"Level: {player.GetLevel()}", new Vector2(position.X + healthBarBeginX, position.Y + healthBarBeginY + offset), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, Settings.MainUILayer + 0.0038f);
            offset += offsetStep;
            exp_bar.Update(player.GetExp(), player.GetExpMax(), new Vector2(position.X + healthBarBeginX, position.Y + healthBarBeginY + offset));
            exp_bar.Draw(ref sb, Settings.MainUILayer + 0.0038f);
            sb.DrawString(Settings.font3, $"Magic level: {player.GetMagicLevel()}", new Vector2(position.X + healthBarBeginX, position.Y + healthBarBeginY + offset), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, Settings.MainUILayer + 0.0038f);
            offset += offsetStep;
            magicLevel_bar.Draw(ref sb, Settings.MainUILayer + 0.0038f);
            magicLevel_bar.Update(player.GetMagicLevelPoints(), player.GetMaxMagicLevelPoints(), new Vector2(position.X + healthBarBeginX, position.Y + healthBarBeginY + offset));
        }

        private void DrawEquiped(ref SpriteBatch sb)
        {
            int i = 0;
            Vector2 EqPosition = new Vector2(position.X + 145, position.Y + 7);
            try
            {
                foreach (KeyValuePair<string, IItems> entry in Settings._player.Equiped.ToArray())
                {
                    if (i == 2)
                    {
                        EqPosition.Y += 33;
                        EqPosition.X = position.X + 145;
                        i = 0;
                    }
                    NDrawing.Draw(ref sb, Textures.ItemBackground, EqPosition, Color.White, Settings.MainUILayer + 0.0009f);
                    if (entry.Value != null)
                    {
                        Settings._player.Equiped[entry.Key].Draw(ref sb, EqPosition);
                    }
                    NDrawing.Draw(ref sb, Textures.ItemBorder, EqPosition, Color.White, Settings.MainUILayer + 0.0011f);
                    i++;
                    EqPosition.X += 40 + 32;
                }
            }
            catch
            {
                Console.WriteLine(">Error: Equipment modified");
            }
        }

        private void DrawInventory(ref SpriteBatch sb)
        {
            Vector2 EqPosition = new Vector2(position.X + 13, position.Y + 372);
            sb.DrawString(Settings.font3, $"Page: {InventoryPage + 1}", new Vector2(EqPosition.X, EqPosition.Y - 13), Color.White,0, new Vector2(0,0), 1, SpriteEffects.None, Settings.MainUILayer + 0.01f);
            for (int j = 1 + (56 * InventoryPage); j <= 56 + (56 * InventoryPage); j++)
            {
                NDrawing.Draw(ref sb, Textures.ItemBackground, EqPosition, Color.White, Settings.MainUILayer + 0.0009f);
                if (j - 1 < Settings._player.Inventory.Count)
                    Settings._player.Inventory[j - 1].Draw(ref sb, EqPosition);
                NDrawing.Draw(ref sb, Textures.ItemBorder, EqPosition, Color.White, Settings.MainUILayer + 0.0011f);
                if (j % 8 == 0)
                {
                    EqPosition.Y += 34;
                    EqPosition.X = position.X + 13;
                }
                else
                {
                    EqPosition.X += 34;
                }

            }
            if (InventoryPage > 0)
            {
                inventoryPageDown.Update(new Vector2(position.X + 217, position.Y + 605));
                inventoryPageDown.Draw(ref sb);
            }
            if (InventoryPage < Settings._player.Inventory.Count / 56)
            {
                inventoryPageUp.Update(new Vector2(position.X + 217 + 19, position.Y + 605));
                inventoryPageUp.Draw(ref sb);
            }
        }
        
        private void DrawSkills(ref SpriteBatch sb)
        {
            int offset = -15;
            int barOffset = 32;
            int offsetStep = 30;
            Vector2 skillBeginingPosition = new Vector2(position.X + 15, position.Y + 375);
            int i = 0, j = 0;

            foreach(KeyValuePair<string, int> entry in Settings._player.Skills)
            {
                if (!entry.Key.Contains(Names.SkillLevelPoints) 
                    && !entry.Key.Contains(Names.SkillLevelPointsNeeded)
                    && i >= SkillsPage && j < 8)
                {
                    sb.DrawString(Settings.font3, $"{entry.Key.Replace(Names.SkillLevel, "")}: {Settings._player.Skills[entry.Key]}", new Vector2(skillBeginingPosition.X, skillBeginingPosition.Y + healthBarHeight + offset), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, Settings.MainUILayer + 0.0038f);
                        swordSkill_bar.Update(Settings._player.Skills[entry.Key.Replace(Names.SkillLevel, "") + Names.SkillLevelPoints], Settings._player.Skills[entry.Key.Replace(Names.SkillLevel, "") + Names.SkillLevelPointsNeeded], new Vector2(skillBeginingPosition.X + 5, skillBeginingPosition.Y + healthBarHeight + offset + barOffset));
                        swordSkill_bar.Draw(ref sb, Settings.MainUILayer + 0.0038f);
                        offset += offsetStep;
                    j++;
                }
                i++;
            }
            if (SkillsPage > 0)
            {
                skillsPageUp.Update(new Vector2(position.X + 243, position.Y + 450), true, 10);
                skillsPageUp.Draw(ref sb);
            }
            if (SkillsPage + 8 < Settings._player.Skills.Count/3)
            {
                skillsPageDown.Update(new Vector2(position.X + 243, position.Y + 450 + 19), true, 10);
                skillsPageDown.Draw(ref sb);
            }
        }

        private void DrawStats(ref SpriteBatch sb)
        {
            Vector2 TextPosition = new Vector2(position.X + 55, position.Y + 375);
            int offset = 0;
            int offsetStep = 25;
            int XLevelOffset = 100;
            int XPlusOffset = 110;
            int i = 0, j = 0;

            sb.DrawString(Settings.font3, $"Points to spend: {player.GetLevelPoints()}", new Vector2(TextPosition.X - 55, TextPosition.Y + offset), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, Settings.MainUILayer + 0.0038f);
            offset += offsetStep + offsetStep/2;
            foreach (KeyValuePair<string, int> entry in Settings._player.Stats.ToArray())
            {
                if (i >= StatsPage && j < 7)
                {
                    sb.DrawString(Settings.font3, $"{entry.Key}:", new Vector2(TextPosition.X, TextPosition.Y + offset), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, Settings.MainUILayer + 0.0038f);
                    sb.DrawString(Settings.font3, $"{entry.Value}", new Vector2(TextPosition.X + XLevelOffset, TextPosition.Y + offset), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, Settings.MainUILayer + 0.0038f);
                    DrawLevelUpButton(ref sb, TextPosition, entry.Key, offset, XPlusOffset);
                    offset += offsetStep;
                    j++;
                }
                i++;
            }

            if (StatsPage > 0)
            {
                statsPageUp.Update(new Vector2(position.X + 243, position.Y + 470), true, 10);
                statsPageUp.Draw(ref sb);
            }
            if (StatsPage + 7 < Settings._player.Stats.Count)
            {
                statsPageDown.Update(new Vector2(position.X + 243, position.Y + 470+19), true, 10);
                statsPageDown.Draw(ref sb);
            }
        }

        private void DrawResources(ref SpriteBatch sb)
        {
            Vector2 TextPosition = new Vector2(position.X + 55, position.Y + 375);
            int offset = 0;
            int offsetStep = 25;
            int XLevelOffset = 100;
            int XPlusOffset = 110;
            int i = 0, j = 0;

            sb.DrawString(Settings.font3, $"Gold: {Settings._player.Materials[Names.Material_Gold]}", new Vector2(TextPosition.X - 55, TextPosition.Y + offset), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, Settings.MainUILayer + 0.0038f);
            offset += offsetStep + offsetStep / 2;
            foreach (KeyValuePair<string, int> entry in Settings._player.Materials.ToArray())
            {
                if (i >= StatsPage && j < 7 && entry.Key != Names.Material_Gold)
                {
                    sb.DrawString(Settings.font3, $"{entry.Key}:", new Vector2(TextPosition.X, TextPosition.Y + offset), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, Settings.MainUILayer + 0.0038f);
                    sb.DrawString(Settings.font3, $"{entry.Value}", new Vector2(TextPosition.X + XLevelOffset, TextPosition.Y + offset), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, Settings.MainUILayer + 0.0038f);
                    offset += offsetStep;
                    j++;
                }
                i++;
            }

            if (StatsPage > 0)
            {
                statsPageUp.Update(new Vector2(position.X + 243, position.Y + 470), true, 10);
                statsPageUp.Draw(ref sb);
            }
            if (StatsPage + 7 < Settings._player.Materials.Count)
            {
                statsPageDown.Update(new Vector2(position.X + 243, position.Y + 470 + 19), true, 10);
                statsPageDown.Draw(ref sb);
            }
        }

        private void DrawUIElements(ref SpriteBatch sb)
        {
            NDrawing.Draw(ref sb, Textures.UITopCornerTexture, position, Color.White, Settings.MainUILayer + 0.0005f);
            NDrawing.Draw(ref sb, Textures.Avatar, new Vector2(position.X, position.Y), Color.White, Settings.MainUILayer + 0.0039f);
            NDrawing.Draw(ref sb, Textures.UIAvatarRingTexture, new Vector2(position.X, position.Y), Color.White, Settings.MainUILayer + 0.004f);
            NDrawing.Draw(ref sb, Textures.UIMainSideBar, new Vector2(position.X, position.Y), Color.White, Settings.MainUILayer);
            NDrawing.Draw(ref sb, Textures.UIEqIcons, new Vector2(position.X + 115, position.Y + 5), Color.White, Settings.MainUILayer + 0.0005f);
        }

        private void DrawSpells(ref SpriteBatch sb)
        {
            Vector2 TextPosition = new Vector2(position.X + 55, position.Y + 375);
            int offset = 0;
            int offsetStep = 25;
            int XLevelOffset = 100;
            int XPlusOffset = 110;
            int i = 0, j = 0;

            sb.DrawString(Settings.font3, "Equiped spells: ", new Vector2(TextPosition.X - 55, TextPosition.Y + offset), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, Settings.MainUILayer + 0.0038f);
            offset += offsetStep + offsetStep / 2;
            foreach (ISpell entry in Settings._player.Spells)
            {
                if (i >= SpellsPage && j < 7)
                {
                    if(entry != null)
                        sb.DrawString(Settings.font3, $"F{i+1}: {entry.GetName()}", new Vector2(TextPosition.X, TextPosition.Y + offset), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, Settings.MainUILayer + 0.0038f);
                    else
                        sb.DrawString(Settings.font3, $"F{i + 1}: -", new Vector2(TextPosition.X, TextPosition.Y + offset), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, Settings.MainUILayer + 0.0038f);
                    offset += offsetStep;
                    j++;
                }
                i++;
            }

            if (SpellsPage > 0)
            {
                spellPageUp.Update(new Vector2(position.X + 243, position.Y + 470), true, 10);
                spellPageUp.Draw(ref sb);
            }
            if (SpellsPage + 7 < Settings._player.Spells.Length)
            {
                spellPageDown.Update(new Vector2(position.X + 243, position.Y + 470 + 19), true, 10);
                spellPageDown.Draw(ref sb);
            }
        }
    }
}
