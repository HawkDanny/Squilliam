using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SwagSword
{
    class StatScreen : UIScreen
    {
        #region Fields
        private Texture2D rect;
        #endregion

        #region Properties
        public Texture2D Rect { get { return rect; } set { rect = value; } }
        #endregion

        public StatScreen(Game1 mainMan):
            base(mainMan)
        {
            rect = new Texture2D(mainMan.GraphicsDevice, 1, 1);
            rect.SetData(new[] { Color.White });
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            //Where sword stats go
            spritebatch.Draw(mainMan.DrawMan.SwordStatScreen, new Rectangle(leftX + -324, topY + 130, mainMan.DrawMan.SwordStatScreen.Width, mainMan.DrawMan.SwordStatScreen.Height), Color.White);
            //Behind Player Sprite
            spritebatch.Draw(rect, new Rectangle(leftX + 60, topY + 30, 200, 200), Color.SaddleBrown);
            //Player Sprite
            spritebatch.Draw(mainMan.DrawMan.SpriteDict[mainMan.GameMan.Players[0].Character.Type], new Rectangle(leftX + 110, topY + 80, 100, 100), Color.White);
            //Character stat box
            spritebatch.Draw(rect, new Rectangle(leftX + 270, topY + 30, 500, 200), Color.SaddleBrown);
            //Character name
            spritebatch.DrawString(mainMan.DrawMan.StatFont, mainMan.GameMan.Players[0].Character.Name, new Vector2(leftX + 280, topY + 35), Color.White);
            //Names of sword stats
            spritebatch.DrawString(mainMan.DrawMan.StatFont, "Agility", new Vector2(leftX + 120, topY + 310), Color.RoyalBlue);
            spritebatch.DrawString(mainMan.DrawMan.StatFont, "Strength", new Vector2(leftX + 100, topY + 385), Color.RoyalBlue);
            spritebatch.DrawString(mainMan.DrawMan.StatFont, "Dexterity", new Vector2(leftX + 390, topY + 310), Color.RoyalBlue);
            spritebatch.DrawString(mainMan.DrawMan.StatFont, "Force", new Vector2(leftX + 465, topY + 385), Color.RoyalBlue);
            spritebatch.DrawString(mainMan.DrawMan.StatFont, "Health", new Vector2(leftX + 690, topY + 333), Color.RoyalBlue);
            //Names of Character Stats
            spritebatch.DrawString(mainMan.DrawMan.StatFont, "Health: " + mainMan.GameMan.Players[0].Character.Health + "/" + mainMan.GameMan.Players[0].Character.MaxHealth, new Vector2(leftX + 280, topY + 80), Color.White);
            spritebatch.DrawString(mainMan.DrawMan.StatFont, "Damage: " + mainMan.GameMan.Players[0].Character.Damage, new Vector2(leftX + 280, topY + 110), Color.White);
            spritebatch.DrawString(mainMan.DrawMan.StatFont, "Speed: " + mainMan.GameMan.Players[0].Character.MovementSpeed, new Vector2(leftX + 280, topY + 140), Color.White);
            spritebatch.DrawString(mainMan.DrawMan.StatFont, "Attack Speed: " + mainMan.GameMan.Players[0].Character.AttackSpeedMin, new Vector2(leftX + 280, topY + 170), Color.White);
        }

        public override void Update()
        {
            if (mainMan.InputMan.SingleKeyPress(Keys.P))
            {
                mainMan.UIMan.Screens.Pop();
            }
        }
    }
}
