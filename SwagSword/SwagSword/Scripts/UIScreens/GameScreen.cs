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
        private Rectangle abilitySelect;
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
            abilitySelect = new Rectangle(leftX + offset, topY + mainMan.WindowHeight - offset - size, size + 3, size + 3);
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
            if (CheckWin())
            {
                mainMan.UIMan.Screens.Pop();
                mainMan.UIMan.Screens.Push(new WinScreen(mainMan));
            }
            if (CheckLose())
            {
                mainMan.UIMan.Screens.Pop();
                mainMan.UIMan.Screens.Push(new GameOverScreen(mainMan));
            }
            
        }

        private bool CheckWin()
        {
            int i = 0;
            foreach (Stronghold s in mainMan.GameMan.MapMan.Strongholds)
                if (s.Captured)
                    i++;
            if (i == 3)
                return true;
            else
                return false;
        }

        private bool CheckLose()
        {
            if (mainMan.GameMan.Players[0].Lives <= 0)
                return true;
            else
                return false;
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            int leftX = (int)mainMan.DrawMan.Camera.TopLeftPosition.X;
            int topY = (int)mainMan.DrawMan.Camera.TopLeftPosition.Y;
            //Draws the back of the health bar as gray
            spritebatch.Draw(rect, new Rectangle(leftX + 20, topY + 20, 200, 10), Color.Gray);
            spritebatch.Draw(rect, new Rectangle(leftX + 20, topY + 40, 200, 10), Color.Gray);
            //Draws the actual Health meter as green and the EXP as blue
            double health = 0.0 + mainMan.GameMan.Players[0].Health;
            double maxHealth = 0.0 + mainMan.GameMan.Players[0].MaxHealth;
            double exp = 0.0 + mainMan.GameMan.Players[0].Exp;
            double maxExp = 0.0 + mainMan.GameMan.Players[0].MaxExp;
            spritebatch.Draw(rect, new Rectangle(leftX + 20, topY + 20, (int)((health/maxHealth)*200), 10), Color.Green);
            spritebatch.Draw(rect, new Rectangle(leftX + 20, topY + 40, (int)((exp / maxExp) * 200), 10), Color.Blue);
            //Draws the number of health next to the bar
            spritebatch.DrawString(mainMan.DrawMan.HealthFont, mainMan.GameMan.Players[0].Health + "/" + mainMan.GameMan.Players[0].MaxHealth, new Vector2(leftX + 225f, topY + 14f), Color.Green);
            spritebatch.DrawString(mainMan.DrawMan.HealthFont, mainMan.GameMan.Players[0].Exp + "/" + mainMan.GameMan.Players[0].MaxExp, new Vector2(leftX + 225f, topY + 34f), Color.Blue);
            spritebatch.DrawString(mainMan.DrawMan.HealthFont, "Level " + mainMan.GameMan.Players[0].Level, new Vector2(leftX + 20, topY + 58), Color.Blue);
            spritebatch.DrawString(mainMan.DrawMan.HealthFont, "Lives " + mainMan.GameMan.Players[0].Lives, new Vector2(leftX + 100, topY + 58), Color.Red);
            DrawAbilities(spritebatch);
            base.Draw(spritebatch);
        }

        private void DrawAbilities(SpriteBatch spritebatch)
        {
            int leftX = (int)mainMan.DrawMan.Camera.TopLeftPosition.X;
            int topY = (int)mainMan.DrawMan.Camera.TopLeftPosition.Y;
            abilityRect = new Rectangle(leftX + 15, topY + mainMan.WindowHeight - 15 - size, size, size);
            abilitySelect = new Rectangle(leftX + 12, topY + mainMan.WindowHeight - 18 - size, size + 6, size + 6);
            if (mainMan.GameMan.Players[0].CharacterState == CharacterState.Active || mainMan.GameMan.Players[0].CharacterState == CharacterState.Hurt)
            {
                if (mainMan.GameMan.Players[0].Character.CurrentAbility.Type == AbilityType.Boomerang)
                {
                    spritebatch.Draw(rect, abilitySelect, Color.Yellow);
                    //draw texture
                    if (mainMan.GameMan.MapMan.Strongholds[0].Captured || mainMan.GameMan.Players[0].Character.Type == Faction.Good)
                        spritebatch.Draw(mainMan.DrawMan.BoomerangAbility, abilityRect, Color.White);
                    else
                        spritebatch.Draw(mainMan.DrawMan.BoomerangAbility, abilityRect, Color.DarkGray);
                    
                }
                else
                {
                    //draw texture
                    if (mainMan.GameMan.MapMan.Strongholds[0].Captured || mainMan.GameMan.Players[0].Character.Type == Faction.Good)
                        spritebatch.Draw(mainMan.DrawMan.BoomerangAbility, abilityRect, Color.White);
                    else
                        spritebatch.Draw(mainMan.DrawMan.BoomerangAbility, abilityRect, Color.DarkGray);
                }
                abilityRect.X += offset;
                abilitySelect.X += offset;

                if (mainMan.GameMan.Players[0].Character.CurrentAbility.Type == AbilityType.Decoy)
                {
                    spritebatch.Draw(rect, abilitySelect, Color.Yellow);
                    //draw texture
                    if (mainMan.GameMan.MapMan.Strongholds[3].Captured || mainMan.GameMan.Players[0].Character.Type == Faction.Thief)
                        spritebatch.Draw(mainMan.DrawMan.DecoyAbility, abilityRect, Color.White);
                    else
                        spritebatch.Draw(mainMan.DrawMan.DecoyAbility, abilityRect, Color.DarkGray);
                }
                else
                {
                    //draw texture
                    if (mainMan.GameMan.MapMan.Strongholds[3].Captured || mainMan.GameMan.Players[0].Character.Type == Faction.Thief)
                        spritebatch.Draw(mainMan.DrawMan.DecoyAbility, abilityRect, Color.White);
                    else
                        spritebatch.Draw(mainMan.DrawMan.DecoyAbility, abilityRect, Color.DarkGray);
                }
                //update rectangle
                abilityRect.X += offset;
                abilitySelect.X += offset;

                if (mainMan.GameMan.Players[0].Character.CurrentAbility.Type == AbilityType.Minion)
                {
                    spritebatch.Draw(rect, abilitySelect, Color.Yellow);
                    //draw texture
                    if (mainMan.GameMan.MapMan.Strongholds[2].Captured || mainMan.GameMan.Players[0].Character.Type == Faction.Rich)
                        spritebatch.Draw(mainMan.DrawMan.RobotAbility, abilityRect, Color.White);
                    else
                        spritebatch.Draw(mainMan.DrawMan.RobotAbility, abilityRect, Color.DarkGray);
                }
                else
                {
                    //draw texture
                    if (mainMan.GameMan.MapMan.Strongholds[2].Captured || mainMan.GameMan.Players[0].Character.Type == Faction.Rich)
                        spritebatch.Draw(mainMan.DrawMan.RobotAbility, abilityRect, Color.White);
                    else
                        spritebatch.Draw(mainMan.DrawMan.RobotAbility, abilityRect, Color.DarkGray);
                }
                //update rectangle
                abilityRect.X += offset;
                abilitySelect.X += offset;

                if (mainMan.GameMan.Players[0].Character.CurrentAbility.Type == AbilityType.Warp)
                {
                    spritebatch.Draw(rect, abilitySelect, Color.Yellow);
                    //draw texture
                    if (mainMan.GameMan.MapMan.Strongholds[1].Captured || mainMan.GameMan.Players[0].Character.Type == Faction.Tribal)
                        spritebatch.Draw(mainMan.DrawMan.TeleportAbility, abilityRect, Color.White);
                    else
                        spritebatch.Draw(mainMan.DrawMan.TeleportAbility, abilityRect, Color.DarkGray);
                }
                else
                {
                    //draw texture
                    if (mainMan.GameMan.MapMan.Strongholds[1].Captured || mainMan.GameMan.Players[0].Character.Type == Faction.Tribal)
                        spritebatch.Draw(mainMan.DrawMan.TeleportAbility, abilityRect, Color.White);
                    else
                        spritebatch.Draw(mainMan.DrawMan.TeleportAbility, abilityRect, Color.DarkGray);
                }
            }
            //reset rect
            abilityRect.X -= (3 * offset);
        }
    }
}
