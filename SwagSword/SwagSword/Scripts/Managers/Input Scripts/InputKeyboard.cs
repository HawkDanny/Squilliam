using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

//Names: Danny Hawk

namespace SwagSword
{
    class InputKeyboard : InputType
    {
        private Keys key;
        private KeyboardState kbState;

        public InputKeyboard(KeyboardState kbState, Keys key)
        {
            this.kbState = kbState;
            this.key = key;
        }

        public override bool IsDown()
        {
            kbState = Keyboard.GetState();
            return kbState.IsKeyDown(key);
        }

        public override bool IsUp()
        {
            kbState = Keyboard.GetState();
            return kbState.IsKeyUp(key);
        }
    }
}
