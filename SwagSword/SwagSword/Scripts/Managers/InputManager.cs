using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SwagSword
{
    //Enum necessary for specifying mouse buttons in the inputMouse class   
    public enum MouseButton
    {
        Left, Right, Middle
    }

    //Enum containg all the different actions bindable to keys/buttons
    public enum Binds
    {
        Up, Down, Left, Right, Attack, UseAbility, Ability1, Ability2, Ability3, Ability4
    }

    public class InputManager:Manager
    {
        //Fields
        private KeyboardState prevKbState; //previous keyboard state
        private KeyboardState kbState;
        private MouseState prevMState; //previous mouse state
        private MouseState mState;
        
        //list of all bound keys/buttons, in order of enum Binds (Line 19)
        private InputType[] binds;

        //Get properties for all actions
        public InputType Up { get { return binds[0]; } }
        public InputType Down { get { return binds[1]; } }
        public InputType Left { get { return binds[2]; } }
        public InputType Right { get { return binds[3]; } }
        public InputType Attack { get { return binds[4]; } }
        public InputType UseAbility { get { return binds[5]; } }
        public InputType Ability1 { get { return binds[6]; } }
        public InputType Ability2 { get { return binds[7]; } }
        public InputType Ability3 { get { return binds[8]; } }
        public InputType Ability4 { get { return binds[9]; } }

        public KeyboardState PrevKbState { get { return prevKbState; } }
        public KeyboardState KbState { get { return kbState; } }
        public MouseState PrevMState { get { return prevMState; } }
        public MouseState MState { get { return mState; } }

        

        public InputManager(Game1 mainMan):base(mainMan)
        {

        }

        //Initialize
        public override void Init()
        {
            kbState = new KeyboardState();
            prevKbState = new KeyboardState();
            mState = new MouseState();

            binds = new InputType[10];

            Default();
        }

        /// <summary>
        /// Main update method, called by mainMan
        /// </summary>
        public override void Update()
        {
            prevKbState = kbState;
            kbState = Keyboard.GetState();
            mState = Mouse.GetState();
        }

        /// <summary>
        /// Sets the default key bindings
        /// </summary>
        public void Default()
        {
            binds[0] = new InputKeyboard(kbState, Keys.W);
            binds[1] = new InputKeyboard(kbState, Keys.S);
            binds[2] = new InputKeyboard(kbState, Keys.A);
            binds[3] = new InputKeyboard(kbState, Keys.D);
            binds[4] = new InputMouse(mState, MouseButton.Left);
            binds[5] = new InputMouse(mState, MouseButton.Right);
            binds[6] = new InputKeyboard(kbState, Keys.D1);
            binds[7] = new InputKeyboard(kbState, Keys.D2);
            binds[8] = new InputKeyboard(kbState, Keys.D3);
            binds[9] = new InputKeyboard(kbState, Keys.D4);
        }

        /// <summary>
        /// Checks if a key is pressed just once instead f
        /// being held down.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool SingleKeyPress(Keys key)
        {
            bool temp1 = kbState.IsKeyDown(key);
            bool temp2 = prevKbState.IsKeyUp(key);
            if (temp1 && temp2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// When called, it checks between previous states of both mouse and keyboard to bind the next key/button pressed to the specified action
        /// </summary>
        /// <param name="keyToBind">The action that you wish to be bound</param>
        public void Bind(Binds keyToBind)
        {
            //Previous states of mouse buttons
            ButtonState leftState, middleState, rightState;
            leftState = prevMState.LeftButton;
            middleState = prevMState.MiddleButton;
            rightState = prevMState.RightButton;

            Keys[] prevPressedKeys = prevKbState.GetPressedKeys(); //list of the key presses in the previous keyboard state
            Keys[] pressedKeys = kbState.GetPressedKeys(); //list of the key presses in the current keyboard state

            /*
             * Each case is for a different action
             * Creates an inputType object that's either mouse or keyboard depending on the input
             * Stores the inputType object in binds in its corresponding index
             * */
            switch (keyToBind)
            {
                case Binds.Up:
                    if (!(prevPressedKeys.Equals(pressedKeys)))
                    {
                        InputKeyboard up = new InputKeyboard(kbState, pressedKeys[pressedKeys.Length - 1]);
                        binds[0] = up;
                    }
                    else if (leftState != mState.LeftButton)
                    {
                        InputMouse up = new InputMouse(mState, MouseButton.Left);
                        binds[0] = up;
                    }
                    else if (middleState != mState.MiddleButton)
                    {
                        InputMouse up = new InputMouse(mState, MouseButton.Middle);
                        binds[0] = up;
                    }
                    else if (rightState != mState.RightButton)
                    {
                        InputMouse up = new InputMouse(mState, MouseButton.Right);
                        binds[0] = up;
                    }
                    else
                        break;
                    break;
                case Binds.Down:
                    if (!(prevPressedKeys.Equals(pressedKeys)))
                    {
                        InputKeyboard down = new InputKeyboard(kbState, pressedKeys[pressedKeys.Length - 1]);
                        binds[1] = down;
                    }
                    else if (leftState != mState.LeftButton)
                    {
                        InputMouse down = new InputMouse(mState, MouseButton.Left);
                        binds[1] = down;
                    }
                    else if (middleState != mState.MiddleButton)
                    {
                        InputMouse down = new InputMouse(mState, MouseButton.Middle);
                        binds[1] = down;
                    }
                    else if (rightState != mState.RightButton)
                    {
                        InputMouse down = new InputMouse(mState, MouseButton.Right);
                        binds[1] = down;
                    }
                    else
                        break;
                    break;
                case Binds.Left:
                    if (!(prevPressedKeys.Equals(pressedKeys)))
                    {
                        InputKeyboard left = new InputKeyboard(kbState, pressedKeys[pressedKeys.Length - 1]);
                        binds[2] = left;
                    }
                    else if (leftState != mState.LeftButton)
                    {
                        InputMouse left = new InputMouse(mState, MouseButton.Left);
                        binds[2] = left;
                    }
                    else if (middleState != mState.MiddleButton)
                    {
                        InputMouse left = new InputMouse(mState, MouseButton.Middle);
                        binds[2] = left;
                    }
                    else if (rightState != mState.RightButton)
                    {
                        InputMouse left = new InputMouse(mState, MouseButton.Right);
                        binds[2] = left;
                    }
                    else
                        break;
                    break;
                case Binds.Right:
                    if (!(prevPressedKeys.Equals(pressedKeys)))
                    {
                        InputKeyboard right = new InputKeyboard(kbState, pressedKeys[pressedKeys.Length - 1]);
                        binds[3] = right;
                    }
                    else if (leftState != mState.LeftButton)
                    {
                        InputMouse right = new InputMouse(mState, MouseButton.Left);
                        binds[3] = right;
                    }
                    else if (middleState != mState.MiddleButton)
                    {
                        InputMouse right = new InputMouse(mState, MouseButton.Middle);
                        binds[3] = right;
                    }
                    else if (rightState != mState.RightButton)
                    {
                        InputMouse right = new InputMouse(mState, MouseButton.Right);
                        binds[3] = right;
                    }
                    else
                        break;
                    break;
                case Binds.Attack:
                    if (!(prevPressedKeys.Equals(pressedKeys)))
                    {
                        InputKeyboard attack = new InputKeyboard(kbState, pressedKeys[pressedKeys.Length - 1]);
                        binds[4] = attack;
                    }
                    else if (leftState != mState.LeftButton)
                    {
                        InputMouse attack = new InputMouse(mState, MouseButton.Left);
                        binds[4] = attack;
                    }
                    else if (middleState != mState.MiddleButton)
                    {
                        InputMouse attack = new InputMouse(mState, MouseButton.Middle);
                        binds[4] = attack;
                    }
                    else if (rightState != mState.RightButton)
                    {
                        InputMouse attack = new InputMouse(mState, MouseButton.Right);
                        binds[4] = attack;
                    }
                    else
                        break;
                    break;
                case Binds.UseAbility:
                    if (!(prevPressedKeys.Equals(pressedKeys)))
                    {
                        InputKeyboard useAbility = new InputKeyboard(kbState, pressedKeys[pressedKeys.Length - 1]);
                        binds[5] = useAbility;
                    }
                    else if (leftState != mState.LeftButton)
                    {
                        InputMouse useAbility = new InputMouse(mState, MouseButton.Left);
                        binds[5] = useAbility;
                    }
                    else if (middleState != mState.MiddleButton)
                    {
                        InputMouse useAbility = new InputMouse(mState, MouseButton.Middle);
                        binds[5] = useAbility;
                    }
                    else if (rightState != mState.RightButton)
                    {
                        InputMouse useAbility = new InputMouse(mState, MouseButton.Right);
                        binds[5] = useAbility;
                    }
                    else
                        break;
                    break;
                case Binds.Ability1:
                    if (!(prevPressedKeys.Equals(pressedKeys)))
                    {
                        InputKeyboard ability1 = new InputKeyboard(kbState, pressedKeys[pressedKeys.Length - 1]);
                        binds[6] = ability1;
                    }
                    else if (leftState != mState.LeftButton)
                    {
                        InputMouse ability1 = new InputMouse(mState, MouseButton.Left);
                        binds[6] = ability1;
                    }
                    else if (middleState != mState.MiddleButton)
                    {
                        InputMouse ability1 = new InputMouse(mState, MouseButton.Middle);
                        binds[6] = ability1;
                    }
                    else if (rightState != mState.RightButton)
                    {
                        InputMouse ability1 = new InputMouse(mState, MouseButton.Right);
                        binds[6] = ability1;
                    }
                    else
                        break;
                    break;
                case Binds.Ability2:
                    if (!(prevPressedKeys.Equals(pressedKeys)))
                    {
                        InputKeyboard ability2 = new InputKeyboard(kbState, pressedKeys[pressedKeys.Length - 1]);
                        binds[7] = ability2;
                    }
                    else if (leftState != mState.LeftButton)
                    {
                        InputMouse ability2 = new InputMouse(mState, MouseButton.Left);
                        binds[7] = ability2;
                    }
                    else if (middleState != mState.MiddleButton)
                    {
                        InputMouse ability2 = new InputMouse(mState, MouseButton.Middle);
                        binds[7] = ability2;
                    }
                    else if (rightState != mState.RightButton)
                    {
                        InputMouse ability2 = new InputMouse(mState, MouseButton.Right);
                        binds[7] = ability2;
                    }
                    else
                        break;
                    break;
                case Binds.Ability3:
                    if (!(prevPressedKeys.Equals(pressedKeys)))
                    {
                        InputKeyboard ability3 = new InputKeyboard(kbState, pressedKeys[pressedKeys.Length - 1]);
                        binds[8] = ability3;
                    }
                    else if (leftState != mState.LeftButton)
                    {
                        InputMouse ability3 = new InputMouse(mState, MouseButton.Left);
                        binds[8] = ability3;
                    }
                    else if (middleState != mState.MiddleButton)
                    {
                        InputMouse ability3 = new InputMouse(mState, MouseButton.Middle);
                        binds[8] = ability3;
                    }
                    else if (rightState != mState.RightButton)
                    {
                        InputMouse ability3 = new InputMouse(mState, MouseButton.Right);
                        binds[8] = ability3;
                    }
                    else
                        break;
                    break;
                case Binds.Ability4:
                    if (!(prevPressedKeys.Equals(pressedKeys)))
                    {
                        InputKeyboard ability4 = new InputKeyboard(kbState, pressedKeys[pressedKeys.Length - 1]);
                        binds[9] = ability4;
                    }
                    else if (leftState != mState.LeftButton)
                    {
                        InputMouse ability4 = new InputMouse(mState, MouseButton.Left);
                        binds[9] = ability4;
                    }
                    else if (middleState != mState.MiddleButton)
                    {
                        InputMouse ability4 = new InputMouse(mState, MouseButton.Middle);
                        binds[9] = ability4;
                    }
                    else if (rightState != mState.RightButton)
                    {
                        InputMouse ability4 = new InputMouse(mState, MouseButton.Right);
                        binds[9] = ability4;
                    }
                    else
                        break;
                    break;
            }
        }
    }
}
