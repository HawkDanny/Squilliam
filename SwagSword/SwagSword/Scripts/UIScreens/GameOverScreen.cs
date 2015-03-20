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
            spritebatch.Draw(mainMan.DrawMan.GameOverImage, new Rectangle((int)mainMan.DrawMan.Camera.TopLeftPosition.X, (int)mainMan.DrawMan.Camera.TopLeftPosition.Y, mainMan.WindowWidth, mainMan.WindowHeight), Color.White);
        }
    }
}
