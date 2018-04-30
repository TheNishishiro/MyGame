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
        Dictionary<string, int> GetDamage();
        Dictionary<string, int> GetAttribiutes();
        IItems CreateCopy();
        void Update();
        void Draw(ref SpriteBatch sb, Vector2 position);
        void Use();
        void DecreaseDurability();
        void Break();
        void SetAsCraftingResult();
        string GetID();
        string GetName();
        string GetSkill();
        float GetDefence(string type);
        Texture2D GetTexture();
    }
}
