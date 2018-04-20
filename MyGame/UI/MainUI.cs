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
        public static List<FadingLabel> FL = new List<FadingLabel>();
        float healthBarBeginY = 120, healthBarBeginX = 20, healthBarHeight = 10, healthBarWidth = 255;
        float adjust = 32;

        public MainUI(ICreature _player)
        {
            player = _player;
            hp_bar = new ProgresBar(Textures.UIBarBackgroundTexture, Textures.UIBarTexture, new Rectangle(0, 0, (int)(healthBarWidth), (int)(healthBarHeight )), Color.Red, Textures.UIBarBorderTexture);
            mana_bar = new ProgresBar(Textures.UIBarBackgroundTexture, Textures.UIBarTexture, new Rectangle(0, 0, (int)(healthBarWidth), (int)(healthBarHeight)), Color.Blue, Textures.UIBarBorderTexture);
            exp_bar = new ProgresBar(Textures.UIBarBackgroundTexture, Textures.UIBarTexture, new Rectangle(0, 0, (int)(healthBarWidth), (int)(healthBarHeight)), Color.Yellow, Textures.UIBarBorderTexture);
            magicLevel_bar = new ProgresBar(Textures.UIBarBackgroundTexture, Textures.UIBarTexture, new Rectangle(0, 0, (int)(healthBarWidth), (int)(healthBarHeight)), Color.CornflowerBlue, Textures.UIBarBorderTexture);
        }

        public void Draw(ref SpriteBatch sb)
        {
            position = new Vector2(player.GetPosition().X + 15 - Game1._GraphicsDevice.Viewport.Width / 2 + NCamera.CameraXOffset, player.GetPosition().Y + 20 - Game1._GraphicsDevice.Viewport.Height / 2 + NCamera.CameraYOffset);
            DrawEq(ref sb);
            if (MenuControls.MouseOver(new Rectangle((int)position.X, (int)position.Y, Textures.UIMainSideBar.Width - 45, Textures.UIMainSideBar.Height)))
                MenuControls.SetMouseLayer(Settings.MainUILayer);

            sb.DrawString(Settings.font3, $"Health: {player.GetHealth()}/{player.GetMaxHealth()}", new Vector2(position.X + healthBarBeginX, position.Y + healthBarBeginY), Color.White, 0, new Vector2(0,0), 1, SpriteEffects.None, Settings.MainUILayer + 0.0038f);
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
            NDrawing.Draw(ref sb, Textures.UITopCornerTexture, position, Color.White, Settings.MainUILayer + 0.0005f);
            NDrawing.Draw(ref sb, Textures.Avatar, new Vector2(position.X, position.Y), Color.White, Settings.MainUILayer + 0.0039f);
            NDrawing.Draw(ref sb, Textures.UIAvatarRingTexture, new Vector2(position.X, position.Y), Color.White, Settings.MainUILayer + 0.004f);
            NDrawing.Draw(ref sb, Textures.UIMainSideBar, new Vector2(position.X, position.Y), Color.White, Settings.MainUILayer);

            

            MenuControls.FadingLabelManager(ref sb, FL);
        }

        public void DrawEq(ref SpriteBatch sb)
        {
            Vector2 EqPosition = new Vector2(position.X + 13, position.Y + 372);
            for (int j = 1; j <= 56; j++)
            {
                if (j - 1 < Settings._player.Inventory.Count)
                {
                    NDrawing.Draw(ref sb, Textures.ItemBackground, EqPosition, Color.White, Settings.MainUILayer + 0.0009f);
                    Settings._player.Inventory[j - 1].Draw(ref sb, EqPosition);
                    NDrawing.Draw(ref sb, Textures.ItemBorder, EqPosition, Color.White, Settings.MainUILayer + 0.0011f);
                }
                else
                {
                    NDrawing.Draw(ref sb, Textures.ItemBackground, EqPosition, Color.White, Settings.MainUILayer + 0.0009f);
                    NDrawing.Draw(ref sb, Textures.ItemBorder, EqPosition, Color.White, Settings.MainUILayer + 0.0011f);
                }

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
    }
}
