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
    public class GameScreen : UIScreen
    {
        //Fields
        //Rectangle texture
        private Texture2D rect;
        private Rectangle abilityRect;
        private int offset;
        private int size;
        private int leftX;
        private int topY;

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
            leftX = (int)mainMan.DrawMan.Camera.TopLeftPosition.X;
            topY = (int)mainMan.DrawMan.Camera.TopLeftPosition.Y;
            offset = 15;
            size = 40;
            abilityRect = new Rectangle(leftX + offset, topY + mainMan.WindowHeight - offset - size, size, size);
            offset += size;
        }

        public override void Update()
        {
            
            //Pauses the game
            if(mainMan.InputMan.SingleKeyPress(Keys.P))
            {
                mainMan.UIMan.Screens.Push(new PauseScreen(mainMan, mainMan.UIMan.Screens.Pop()));
                mainMan.UIMan.State = GameState.pause;
            }

            //Win Screen
            if(mainMan.InputMan.SingleKeyPress(Keys.O))
            {
                mainMan.UIMan.Screens.Push(new WinScreen(mainMan));
                mainMan.UIMan.State = GameState.win;
            }

            
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            int leftX = (int)mainMan.DrawMan.Camera.TopLeftPosition.X;
            int topY = (int)mainMan.DrawMan.Camera.TopLeftPosition.Y;
            //Draws the back of the health bar as gray
            spritebatch.Draw(rect, new Rectangle(leftX + 20, topY + 20, 200, 10), Color.Gray);
            //Draws the actual Health meter as green
            double health = 0.0 + mainMan.GameMan.Players[0].Health;
            double maxHealth = 0.0 + mainMan.GameMan.Players[0].MaxHealth;
            spritebatch.Draw(rect, new Rectangle(leftX + 20, topY + 20, (int)((health/maxHealth)*200), 10), Color.Green);
            //Draws the number of health next to the bar
            spritebatch.DrawString(mainMan.DrawMan.HealthFont, mainMan.GameMan.Players[0].Health + "/" + mainMan.GameMan.Players[0].MaxHealth, new Vector2(leftX + 225f, topY + 14f), Color.Green);
            DrawAbilities(spritebatch);
            base.Draw(spritebatch);
        }

        private void DrawAbilities(SpriteBatch spritebatch)
        {
            int leftX = (int)mainMan.DrawMan.Camera.TopLeftPosition.X;
            int topY = (int)mainMan.DrawMan.Camera.TopLeftPosition.Y;
            abilityRect = new Rectangle(leftX + 15, topY + mainMan.WindowHeight - 15 - size, size, size);
            if (mainMan.GameMan.Players[0].CharacterState == CharacterState.Active)
            {
                //draw texture
                if (mainMan.GameMan.MapMan.Strongholds[0].Captured || mainMan.GameMan.Players[0].Character.Type == Faction.Good)
                    spritebatch.Draw(mainMan.DrawMan.BoomerangAbility, abilityRect, Color.White);
                else
                    spritebatch.Draw(mainMan.DrawMan.BoomerangAbility, abilityRect, Color.DarkGray);
                //update rectangle
                abilityRect.X += offset;

                //draw texture
                if (mainMan.GameMan.MapMan.Strongholds[3].Captured || mainMan.GameMan.Players[0].Character.Type == Faction.Thief)
                    spritebatch.Draw(mainMan.DrawMan.DecoyAbility, abilityRect, Color.White);
                else
                    spritebatch.Draw(mainMan.DrawMan.DecoyAbility, abilityRect, Color.DarkGray);
                //update rectangle
                abilityRect.X += offset;

                //draw texture
                if (mainMan.GameMan.MapMan.Strongholds[2].Captured || mainMan.GameMan.Players[0].Character.Type == Faction.Rich)
                    spritebatch.Draw(mainMan.DrawMan.RobotAbility, abilityRect, Color.White);
                else
                    spritebatch.Draw(mainMan.DrawMan.RobotAbility, abilityRect, Color.DarkGray);
                //update rectangle
                abilityRect.X += offset;

                //draw texture
                if (mainMan.GameMan.MapMan.Strongholds[1].Captured || mainMan.GameMan.Players[0].Character.Type == Faction.Tribal)
                    spritebatch.Draw(mainMan.DrawMan.TeleportAbility, abilityRect, Color.White);
                else
                    spritebatch.Draw(mainMan.DrawMan.TeleportAbility, abilityRect, Color.DarkGray);
            }
            //reset rect
            abilityRect.X -= (3 * offset);
        }
    }
}
