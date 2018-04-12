using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GridElements.Additions
{
    class BloodStain : baseAddition
    {
        public BloodStain(Texture2D texture, int posX, int posY)
        {
            Position = new Microsoft.Xna.Framework.Vector2(posX, posY);
            this.texture = texture;
            Walkable = true;
            layerDepth = Settings.tileAdditionBottomLayer;
        }
    }
}
