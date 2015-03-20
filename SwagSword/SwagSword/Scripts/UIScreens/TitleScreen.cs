using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//Names: Peter Lockhart

namespace SwagSword
{
    class TitleScreen : UIScreen
    {
        /// <summary>
        /// Creates the title screen.
        /// </summary>
        /// <param name="mainMan"></param>
        public TitleScreen(Game1 mainMan):
            base(mainMan)
        {

        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(mainMan.DrawMan.TitleImage, new Rectangle(0, 0, mainMan.WindowWidth, mainMan.WindowHeight), Color.White);
        }

        public override void Update()
        {
            //checking if the player presses the enter button to continue.
            if(mainMan.InputMan.KbState.IsKeyDown(Keys.Enter))
            {
                mainMan.UIMan.Screens.Push(new GameScreen(mainMan));
                mainMan.UIMan.State = GameState.game;
                mainMan.GameMan.StartGame();
            }
        }
    }
}
