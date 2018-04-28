using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Spells
{
    interface ISpell
    {
        ISpell CreateCopy();
        void Cast(Vector2 position);
        int GetManaCost();
        string GetName();
    }
}
