using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public MainUI(ICreature _player)
        {
            player = _player;
        }

        public void Draw(ref SpriteBatch sb)
        {
            sb.DrawString(Settings.font, $"Level : {player.GetLevel()}" +
                $"\nHealth: {player.GetHealth()}/{player.GetMaxHealth()}" +
                $"\nExp: {player.GetExp()}/{player.GetExpMax()}", new Vector2(5, 5), Color.White, 0, new Vector2(0,0), 1, SpriteEffects.None, Settings.UILayer);
        }
    }
}
