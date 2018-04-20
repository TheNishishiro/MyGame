using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFramework
{
    class NControls
    {
        static KeyboardState OldKeyState = Keyboard.GetState();
        static KeyboardState NewKeyState;

        ///<summary>
        ///Use this before your if statements block
        ///</summary>
        public static void _NewKeyState()
        {
            NewKeyState = Keyboard.GetState();
        }

        ///<summary>
        ///Returns true if key is pressed but not when it's hold down
        ///</summary>
        public static bool GetSingleKeyPress(Keys key)
        {
            
            bool pressed = false;
            if (NewKeyState.IsKeyDown(key) && OldKeyState.IsKeyUp(key))
            {
                pressed = true;
            }

            return pressed;
        }

        ///<summary>
        ///Use this after your if statements block
        ///</summary>
        public static void _OldKeyState()
        {
            OldKeyState = NewKeyState;
        }
    }
}
