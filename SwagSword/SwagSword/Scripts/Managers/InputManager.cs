using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SwagSword
{
    /// <summary>
    /// Input Manager figures out the input, but shouldn't change anything using it
    /// </summary>
 

    public class InputManager:Manager
    {
        //Fields
        KeyboardState prevKbState;
        KeyboardState kbState;
        MouseState mState;
        Keys up, down, left, right, attack, useAbility, cycleAbilitiesUp, cycleAbilitiesDown;
        int mouseX, mouseY;

        //Gets and Sets for all properties
        public Keys Up
        {
            get { return up; }
            set { up = value; }
        }
        public Keys Down
        {
            get { return down; }
            set { down = value; }
        }
        public Keys Left
        {
            get { return left; }
            set { left = value; }
        }
        public Keys Right
        {
            get { return right; }
            set { right = value; }
        }
        public Keys Attack
        {
            get { return attack; }
            set { attack = value; }
        }
        public Keys UseAbility
        {
            get { return useAbility; }
            set { useAbility = value; }
        }
        public Keys CycleAbilitiesUp
        {
            get { return cycleAbilitiesUp; }
            set { cycleAbilitiesUp = value; }
        }
        public Keys CycleAbilitiesDown
        {
            get { return cycleAbilitiesDown; }
            set { cycleAbilitiesDown = value; }
        }
        public int MouseX
        {
            get { return mouseX; }
            set { mouseX = value; }
        }
        public int MouseY
        {
            get { return mouseY; }
            set { mouseY = value; }
        }

        public InputManager(Game1 mainMan):base(mainMan)
        {

        }

        //Init
        public override void Init()
        {
            kbState = new KeyboardState();
            prevKbState = new KeyboardState();
            mState = new MouseState();
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

        public void Default()
        {
            up = Keys.W;
            left = Keys.A;
            right = Keys.D;
            down = Keys.S;

            attack = Keys.Left;
            useAbility = Keys.Right;
            cycleAbilitiesUp = Keys.Q;
            cycleAbilitiesDown = Keys.E;

            mouseX = mState.X;
            mouseY = mState.Y;
        }

        public bool IsKeyDown(Keys key)
        {
            if (kbState.IsKeyDown(key))
                return true;
            else
                return false;
        }

        public bool IsKeyUp(Keys key)
        {
            if (kbState.IsKeyUp(key))
                return true;
            else
                return false;
        }
    }
}
