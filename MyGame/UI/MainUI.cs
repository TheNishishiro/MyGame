using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Items;
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
        ICreature player;
        Vector2 position;
        ProgresBar hp_bar, mana_bar, exp_bar, magicLevel_bar;
        ProgresBar fistSkill_bar, swordSkill_bar;
        public static List<FadingLabel> FL = new List<FadingLabel>();
        float healthBarBeginY = 120, healthBarBeginX = 20, healthBarHeight = 10, healthBarWidth = 255;
        float adjust = 32;

        Dictionary<string, Button> levelUpButtons = new Dictionary<string, Button>();

        private static bool displaySkillsMode = false;
        private static Button switchDisplayModeButton = new Button("Skills", () => SwitchDisplayMode(), 0);
        public Container container = new Container("Welcome!", "This is my welcome message, feel free to ignore it for now tho. I might insert some daily news or other stuff in here in a near future, probably changes too!");

        public MainUI(ICreature _player)
        {
            player = _player;

            hp_bar = new ProgresBar(Textures.UIBarBackgroundTexture, Textures.UIBarTexture, new Rectangle(0, 0, (int)(healthBarWidth), (int)(healthBarHeight )), Color.Red, Textures.UIBarBorderTexture);
            mana_bar = new ProgresBar(Textures.UIBarBackgroundTexture, Textures.UIBarTexture, new Rectangle(0, 0, (int)(healthBarWidth), (int)(healthBarHeight)), Color.Blue, Textures.UIBarBorderTexture);
            exp_bar = new ProgresBar(Textures.UIBarBackgroundTexture, Textures.UIBarTexture, new Rectangle(0, 0, (int)(healthBarWidth), (int)(healthBarHeight)), Color.Yellow, Textures.UIBarBorderTexture);
            magicLevel_bar = new ProgresBar(Textures.UIBarBackgroundTexture, Textures.UIBarTexture, new Rectangle(0, 0, (int)(healthBarWidth), (int)(healthBarHeight)), Color.CornflowerBlue, Textures.UIBarBorderTexture);

            swordSkill_bar = new ProgresBar(Textures.UIBarBackgroundTexture, Textures.UIBarTexture, new Rectangle(0, 0, (int)(healthBarWidth), (int)(healthBarHeight)), Color.Red, Textures.UIBarBorderTexture);
            fistSkill_bar = new ProgresBar(Textures.UIBarBackgroundTexture, Textures.UIBarTexture, new Rectangle(0, 0, (int)(healthBarWidth), (int)(healthBarHeight)), Color.Red, Textures.UIBarBorderTexture);

            levelUpButtons.Add(Settings.Strength, new Button(Textures.UIPlusIcon, () => Settings._player.IncreaseStat(Settings.Strength)));
            levelUpButtons.Add(Settings.Inteligence, new Button(Textures.UIPlusIcon, () => Settings._player.IncreaseStat(Settings.Inteligence)));
            levelUpButtons.Add(Settings.Dexterity, new Button(Textures.UIPlusIcon, () => Settings._player.IncreaseStat(Settings.Dexterity)));
            levelUpButtons.Add(Settings.Vitality, new Button(Textures.UIPlusIcon, () => Settings._player.IncreaseStat(Settings.Vitality)));
        }

        public void Draw(ref SpriteBatch sb)
        {
            position = new Vector2(player.GetPosition().X + 15 - Game1._GraphicsDevice.Viewport.Width / 2 + NCamera.CameraXOffset, player.GetPosition().Y + 20 - Game1._GraphicsDevice.Viewport.Height / 2 + NCamera.CameraYOffset);
            DrawContainer(ref sb);
            DrawOnUIButtons(ref sb);
            DrawEquiped(ref sb);
            if (MenuControls.MouseOver(new Rectangle((int)position.X, (int)position.Y, Textures.UIMainSideBar.Width - 45, Textures.UIMainSideBar.Height)))
                MenuControls.SetMouseLayer(Settings.MainUILayer);
            DrawStatsText(ref sb);
            DrawBars(ref sb);
            DrawUIElements(ref sb);

            MenuControls.FadingLabelManager(ref sb, FL);
        }

        private static void SwitchDisplayMode()
        {
            displaySkillsMode = !displaySkillsMode;
            if (displaySkillsMode)
                switchDisplayModeButton.Rename("Equipment");
            else
                switchDisplayModeButton.Rename("Skills");
        }

        private void DrawStatsText(ref SpriteBatch sb)
        {
            Vector2 TextPosition = new Vector2(position.X + 15, position.Y + 260);
            int offset = 0;
            int offsetStep = 15;
            int XPlusOffset = 80;

            sb.DrawString(Settings.font3, $"{Settings.Strength}: {Settings._player.Stats[Settings.Strength]}", new Vector2(TextPosition.X, TextPosition.Y + offset), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, Settings.MainUILayer + 0.0038f);
            DrawLevelUpButton(ref sb, TextPosition, Settings.Strength, offset, XPlusOffset);
            offset += offsetStep;
            sb.DrawString(Settings.font3, $"{Settings.Inteligence}: {Settings._player.Stats[Settings.Inteligence]}", new Vector2(TextPosition.X, TextPosition.Y + offset), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, Settings.MainUILayer + 0.0038f);
            DrawLevelUpButton(ref sb, TextPosition, Settings.Inteligence, offset, XPlusOffset);
            offset += offsetStep;
            sb.DrawString(Settings.font3, $"{Settings.Dexterity}: {Settings._player.Stats[Settings.Dexterity]}", new Vector2(TextPosition.X, TextPosition.Y + offset), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, Settings.MainUILayer + 0.0038f);
            DrawLevelUpButton(ref sb, TextPosition, Settings.Dexterity, offset, XPlusOffset);
            offset += offsetStep;
            sb.DrawString(Settings.font3, $"{Settings.Vitality}: {Settings._player.Stats[Settings.Vitality]}", new Vector2(TextPosition.X, TextPosition.Y + offset), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, Settings.MainUILayer + 0.0038f);
            DrawLevelUpButton(ref sb, TextPosition, Settings.Vitality, offset, XPlusOffset);
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
            switchDisplayModeButton.Update(new Vector2(position.X + 113, position.Y + 113), 105);
            switchDisplayModeButton.Draw(ref sb, true);

            if (!displaySkillsMode)
                DrawInventory(ref sb);
            else
                DrawSkills(ref sb);
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
            sb.DrawString(Settings.font3, $"Health: {player.GetHealth()}/{player.GetMaxHealth()}", new Vector2(position.X + healthBarBeginX, position.Y + healthBarBeginY), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, Settings.MainUILayer + 0.0038f);
            hp_bar.Update(player.GetHealth(), player.GetMaxHealth(), new Vector2(position.X + healthBarBeginX, position.Y + healthBarBeginY + adjust));
            hp_bar.Draw(ref sb, Settings.MainUILayer + 0.0038f);
            sb.DrawString(Settings.font3, $"Mana: {player.GetMana()}/{player.GetMaxMana()}", new Vector2(position.X + healthBarBeginX, position.Y + healthBarBeginY + adjust), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, Settings.MainUILayer + 0.0038f);
            mana_bar.Update(player.GetMana(), player.GetMaxMana(), new Vector2(position.X + healthBarBeginX, position.Y + healthBarBeginY + adjust * 2));
            mana_bar.Draw(ref sb, Settings.MainUILayer + 0.0038f);
            sb.DrawString(Settings.font3, $"Level: {player.GetLevel()}", new Vector2(position.X + healthBarBeginX, position.Y + healthBarBeginY + adjust * 2), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, Settings.MainUILayer + 0.0038f);
            exp_bar.Update(player.GetExp(), player.GetExpMax(), new Vector2(position.X + healthBarBeginX, position.Y + healthBarBeginY + adjust * 3));
            exp_bar.Draw(ref sb, Settings.MainUILayer + 0.0038f);
            sb.DrawString(Settings.font3, $"Magic level: {player.GetMagicLevel()}", new Vector2(position.X + healthBarBeginX, position.Y + healthBarBeginY + adjust * 3), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, Settings.MainUILayer + 0.0038f);
            magicLevel_bar.Draw(ref sb, Settings.MainUILayer + 0.0038f);
            magicLevel_bar.Update(player.GetMagicLevelPoints(), player.GetMaxMagicLevelPoints(), new Vector2(position.X + healthBarBeginX, position.Y + healthBarBeginY + adjust * 4));
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
            for (int j = 1; j <= 56; j++)
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
        }

        private void DrawSkills(ref SpriteBatch sb)
        {
            int offset = -15;
            int barOffset = 32;
            int offsetStep = 30;
            Vector2 skillBeginingPosition = new Vector2(position.X + 15, position.Y + 375);
            sb.DrawString(Settings.font3, $"Fist: {Settings._player.Stats[Settings.Fist + Settings.SkillLevel]}", new Vector2(skillBeginingPosition.X, skillBeginingPosition.Y + healthBarHeight + offset), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, Settings.MainUILayer + 0.0038f);
                swordSkill_bar.Update(Settings._player.Stats[Settings.Fist + Settings.SkillLevelPoints], Settings._player.Stats[Settings.Fist + Settings.SkillLevelPointsNeeded], new Vector2(skillBeginingPosition.X + 5, skillBeginingPosition.Y + healthBarHeight + offset + barOffset));
                swordSkill_bar.Draw(ref sb, Settings.MainUILayer + 0.0038f);
                offset += offsetStep;
            sb.DrawString(Settings.font3, $"Sword: {Settings._player.Stats[Settings.Sword + Settings.SkillLevel]}", new Vector2(skillBeginingPosition.X, skillBeginingPosition.Y + healthBarHeight + offset), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, Settings.MainUILayer + 0.0038f);
                swordSkill_bar.Update(Settings._player.Stats[Settings.Sword + Settings.SkillLevelPoints], Settings._player.Stats[Settings.Sword + Settings.SkillLevelPointsNeeded], new Vector2(skillBeginingPosition.X + 5, skillBeginingPosition.Y + healthBarHeight + offset + barOffset));
                swordSkill_bar.Draw(ref sb, Settings.MainUILayer + 0.0038f);
                offset += offsetStep;
            sb.DrawString(Settings.font3, $"Axe: {Settings._player.Stats[Settings.Axe + Settings.SkillLevel]}", new Vector2(skillBeginingPosition.X, skillBeginingPosition.Y + healthBarHeight + offset), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, Settings.MainUILayer + 0.0038f);
                swordSkill_bar.Update(Settings._player.Stats[Settings.Axe + Settings.SkillLevelPoints], Settings._player.Stats[Settings.Axe + Settings.SkillLevelPointsNeeded], new Vector2(skillBeginingPosition.X + 5, skillBeginingPosition.Y + healthBarHeight + offset + barOffset));
                swordSkill_bar.Draw(ref sb, Settings.MainUILayer + 0.0038f);
                offset += offsetStep;
            sb.DrawString(Settings.font3, $"Mace: {Settings._player.Stats[Settings.Mace + Settings.SkillLevel]}", new Vector2(skillBeginingPosition.X, skillBeginingPosition.Y + healthBarHeight + offset), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, Settings.MainUILayer + 0.0038f);
                swordSkill_bar.Update(Settings._player.Stats[Settings.Mace + Settings.SkillLevelPoints], Settings._player.Stats[Settings.Mace + Settings.SkillLevelPointsNeeded], new Vector2(skillBeginingPosition.X + 5, skillBeginingPosition.Y + healthBarHeight + offset + barOffset));
                swordSkill_bar.Draw(ref sb, Settings.MainUILayer + 0.0038f);
                offset += offsetStep;
            sb.DrawString(Settings.font3, $"Mining: {Settings._player.Stats[Settings.Mining + Settings.SkillLevel]}", new Vector2(skillBeginingPosition.X, skillBeginingPosition.Y + healthBarHeight + offset), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, Settings.MainUILayer + 0.0038f);
                swordSkill_bar.Update(Settings._player.Stats[Settings.Mining + Settings.SkillLevelPoints], Settings._player.Stats[Settings.Mining + Settings.SkillLevelPointsNeeded], new Vector2(skillBeginingPosition.X + 5, skillBeginingPosition.Y + healthBarHeight + offset + barOffset));
                swordSkill_bar.Draw(ref sb, Settings.MainUILayer + 0.0038f);
                offset += offsetStep;
            sb.DrawString(Settings.font3, $"Defence: {Settings._player.Stats[Settings.Defence + Settings.SkillLevel]}", new Vector2(skillBeginingPosition.X, skillBeginingPosition.Y + healthBarHeight + offset), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, Settings.MainUILayer + 0.0038f);
                swordSkill_bar.Update(Settings._player.Stats[Settings.Defence + Settings.SkillLevelPoints], Settings._player.Stats[Settings.Defence + Settings.SkillLevelPointsNeeded], new Vector2(skillBeginingPosition.X + 5, skillBeginingPosition.Y + healthBarHeight + offset + barOffset));
                swordSkill_bar.Draw(ref sb, Settings.MainUILayer + 0.0038f);
        }

        private void DrawUIElements(ref SpriteBatch sb)
        {
            NDrawing.Draw(ref sb, Textures.UITopCornerTexture, position, Color.White, Settings.MainUILayer + 0.0005f);
            NDrawing.Draw(ref sb, Textures.Avatar, new Vector2(position.X, position.Y), Color.White, Settings.MainUILayer + 0.0039f);
            NDrawing.Draw(ref sb, Textures.UIAvatarRingTexture, new Vector2(position.X, position.Y), Color.White, Settings.MainUILayer + 0.004f);
            NDrawing.Draw(ref sb, Textures.UIMainSideBar, new Vector2(position.X, position.Y), Color.White, Settings.MainUILayer);
            NDrawing.Draw(ref sb, Textures.UIEqIcons, new Vector2(position.X + 115, position.Y + 5), Color.White, Settings.MainUILayer + 0.0005f);
        }

    }
}
