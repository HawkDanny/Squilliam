using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SwagSword
{
    class GameOverScreen : UIScreen
    {
        /// <summary>
        /// Creates a screen that draws the game over image.
        /// </summary>
        /// <param name="mainMan"></param>
        public GameOverScreen(Game1 mainMan):
            base(mainMan)
        {

        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(mainMan.DrawMan.GameOverImage, new Rectangle(0, 0, mainMan.WindowWidth, mainMan.WindowHeight), Color.White);
        }
    }
}
