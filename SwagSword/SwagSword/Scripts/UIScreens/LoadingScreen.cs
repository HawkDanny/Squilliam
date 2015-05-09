using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SwagSword
{
    class LoadingScreen : UIScreen
    {
        /// <summary>
        /// Creates the title screen.
        /// </summary>
        public LoadingScreen(Game1 mainMan):
            base(mainMan)
        {

        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(mainMan.DrawMan.LoadingScreen, new Rectangle(0, 0, mainMan.WindowWidth, mainMan.WindowHeight), Color.White);
        }

        public override void Update()
        {
            if(mainMan.UIMan.State == GameState.title)
            {
                mainMan.UIMan.Screens.Push(new TitleScreen(mainMan));
            }
        }
    }
}
