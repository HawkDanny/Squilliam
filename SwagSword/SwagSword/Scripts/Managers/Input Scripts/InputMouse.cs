using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

//Names: Danny Hawk

namespace SwagSword
{
    class InputMouse : InputType
    {
        private MouseState mState;
        private MouseButton mButton;

        public InputMouse(MouseState mState, MouseButton mButton)
        {
            this.mState = mState;
            this.mButton = mButton;
        }

        public override bool IsDown()
        {
            mState = Mouse.GetState();
            if (mButton.Equals(MouseButton.Left))
            {
                if (mState.LeftButton == ButtonState.Pressed)
                    return true;
                else
                    return false;
            }
            else if (mButton.Equals(MouseButton.Right))
            {
                if (mState.RightButton == ButtonState.Pressed)
                    return true;
                else
                    return false;
            }
            else if (mButton.Equals(MouseButton.Middle))
            {
                if (mState.MiddleButton == ButtonState.Pressed)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public override bool IsUp()
        {
            mState = Mouse.GetState();
            if (mButton.Equals(MouseButton.Left))
            {
                if (mState.LeftButton == ButtonState.Released)
                    return true;
                else
                    return false;
            }
            else if (mButton.Equals(MouseButton.Right))
            {
                if (mState.RightButton == ButtonState.Released)
                    return true;
                else
                    return false;
            }
            else if (mButton.Equals(MouseButton.Middle))
            {
                if (mState.MiddleButton == ButtonState.Released)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
    }
}
