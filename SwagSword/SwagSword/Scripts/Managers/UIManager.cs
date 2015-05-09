using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

//Names: Peter Lockhart

namespace SwagSword
{
    /// <summary>
    /// Game States that show what screen should be drawn.
    /// </summary>
    public enum GameState
    {
        loading,
        title,
        game,
        gameOver,
        pause,
        win,
        exit
    }

    /// <summary>
    /// UI manager will update all UI screens and handle the camera
    /// </summary>
    public class UIManager:Manager
    {
        //Fields
        protected Stack<UIScreen> screens;
        protected GameState state;


        //Properties
        public Stack<UIScreen> Screens { get { return screens; } }
        public GameState State { get { return state; } set { state = value; } }


        public UIManager(Game1 mainMan):base(mainMan)
        {

        }

        //Init
        public override void Init()
        {
            screens = new Stack<UIScreen>();
            screens.Push(new LoadingScreen(mainMan));
            //screens.Push(new TitleScreen(mainMan));
        }

        /// <summary>
        /// Main update method, called by mainMan
        /// </summary>
        public override void Update()
        {
            screens.Peek().Update();
        }
    }
}
