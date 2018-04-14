using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.UI
{
    interface IUserInterface
    {
        void Draw(ref SpriteBatch sb, float layer);
    }
}
