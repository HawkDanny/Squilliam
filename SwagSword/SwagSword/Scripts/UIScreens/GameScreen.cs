using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SwagSword
{
    public class GameScreen : UIScreen
    {
        //Fields
        //Rectangle texture
        Texture2D rect;

        /// <summary>
        /// Creates the screen that holds the health bar and other
        /// in game HUD.
        /// </summary>
        /// <param name="mainMan"></param>
        public GameScreen(Game1 mainMan):
            base(mainMan)
        {
            rect = new Texture2D(mainMan.GraphicsDevice, 1, 1);
            rect.SetData(new[] { Color.White });
        }

        public override void Update()
        {
            //Checks to see if the characters health is done for.
            //Obviously in game, this won't have to be a game over screen,
            //but for checking that it works for now.
            if(mainMan.GameMan.Characters[0].Health == 0)
            {
                mainMan.UIMan.Screens.Push(new GameOverScreen(mainMan));
                mainMan.UIMan.State = GameState.gameOver;
            }
            
            //Used to check how the health bar works.
            //Press enter to decrease health by 1.
            if(mainMan.InputMan.SingleKeyPress(Keys.Enter))
            {
                mainMan.GameMan.Characters[0].Health--;
            }
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            //Draws the back of the health bar as gray
            spritebatch.Draw(rect, new Rectangle(20, 20, 200, 10), Color.Gray);
            //Draws the actual Health meter as green
            double health = 0.0 + mainMan.GameMan.Characters[0].Health;
            double maxHealth = 0.0 + mainMan.GameMan.Characters[0].MaxHealth;
            spritebatch.Draw(rect, new Rectangle(20, 20, (int)((health/maxHealth)*200), 10), Color.Green);
            //Draws the number of health next to the bar
            spritebatch.DrawString(mainMan.DrawMan.HealthFont, mainMan.GameMan.Characters[0].Health + "/" + mainMan.GameMan.Characters[0].MaxHealth, new Vector2(225f, 14f), Color.Green);
            base.Draw(spritebatch);
        }
    }
}
