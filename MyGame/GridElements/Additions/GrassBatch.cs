using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GridElements.Additions
{
    class GrassBatch : baseAddition
    {
        public GrassBatch(Texture2D texture, int posX, int posY)
        {
            Position = new Microsoft.Xna.Framework.Vector2(posX, posY);
            this.texture = texture;
            Walkable = true;
        }
    }
}
