using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        ProgresBar hp_bar, mana_bar, exp_bar;
        float healthBarBeginY = 40, healthBarBeginX = 50, healthBarHeight = 20, healthBarWidth = 555;
        float adjust = 16;

        public MainUI(ICreature _player)
        {
            player = _player;
            hp_bar = new ProgresBar(Textures.UIBarBackgroundTexture, Textures.UIBarTexture, new Rectangle(0, 0, (int)healthBarWidth, (int)healthBarHeight), Color.Red, Textures.UIBarBorderTexture);
            mana_bar = new ProgresBar(Textures.UIBarBackgroundTexture, Textures.UIBarTexture, new Rectangle(0, 0, (int)healthBarWidth, (int)healthBarHeight), Color.Blue, Textures.UIBarBorderTexture);
            exp_bar = new ProgresBar(Textures.UIBarBackgroundTexture, Textures.UIBarTexture, new Rectangle(0, 0, (int)healthBarWidth, (int)healthBarHeight), Color.Yellow, Textures.UIBarBorderTexture);
        }

        public void Draw(ref SpriteBatch sb)
        {
            position = new Vector2(player.GetPosition().X - Game1._GraphicsDevice.Viewport.Width / 2, player.GetPosition().Y - Game1._GraphicsDevice.Viewport.Height / 2);
            hp_bar.Update(player.GetHealth(), player.GetMaxHealth(), new Vector2(position.X + healthBarBeginX, position.Y + healthBarBeginY + adjust));
            hp_bar.Draw(ref sb, Settings.MainUILayer - 0.0041f);
            mana_bar.Update(player.GetMana(), player.GetMaxMana(), new Vector2(position.X + healthBarBeginX, position.Y + healthBarBeginY + adjust + healthBarHeight));
            mana_bar.Draw(ref sb, Settings.MainUILayer - 0.0041f);
            exp_bar.Update(player.GetExp(), player.GetExpMax(), new Vector2(position.X + healthBarBeginX, position.Y + healthBarBeginY + adjust + healthBarHeight*2));
            exp_bar.Draw(ref sb, Settings.MainUILayer - 0.0041f);
            NDrawing.Draw(ref sb, Textures.UITopCornerTexture, position, Color.White, Settings.MainUILayer - 0.005f);
            NDrawing.Draw(ref sb, Textures.Avatar, new Vector2(position.X + 20, position.Y + 20), Color.White, Settings.MainUILayer - 0.004f);
            NDrawing.Draw(ref sb, Textures.UIAvatarRingTexture, new Vector2(position.X + 20, position.Y + 20), Color.White, Settings.MainUILayer - 0.0039f);



            //sb.DrawString(Settings.font, $"Level : {player.GetLevel()}" +
            //    $"\nHealth: {player.GetHealth()}/{player.GetMaxHealth()}" +
            //    $"\nExp: {player.GetExp()}/{player.GetExpMax()}", new Vector2(5, 5), Color.White, 0, new Vector2(0,0), 1, SpriteEffects.None, Settings.UILayer);
        }
    }
}
