using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SwagSword
{
    /// <summary>
    /// UI manager will update all UI screens and handle the camera
    /// </summary>
    public class UIManager:Manager
    {
        //Fields
        protected Stack<UIScreen> screens;

        //Properties
        public Stack<UIScreen> Screens { get { return screens; } }

        public UIManager(Game1 mainMan):base(mainMan)
        {

        }

        //Init
        public override void Init()
        {
            screens = new Stack<UIScreen>();
            screens.Push(new GameScreen(mainMan));
        }

        /// <summary>
        /// Main update method, called by mainMan
        /// </summary>
        public override void Update()
        {

        }
    }
}
