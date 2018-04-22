using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Items
{
    interface IItems
    {
        int GetStat();
        IItems CreateCopy();
        void Update();
        void Draw(ref SpriteBatch sb, Vector2 position);
        void Use();
        void DecreaseDurability();
        void Break();
        string GetName();
        string GetSkill();
        Texture2D GetTexture();
    }
}
