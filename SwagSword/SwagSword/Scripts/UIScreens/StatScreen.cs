using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
            spritebatch.Draw(mainMan.DrawMan.SwordStatScreen, new Rectangle(-324, 130, mainMan.DrawMan.SwordStatScreen.Width, mainMan.DrawMan.SwordStatScreen.Height), Color.White);
            //Behind Player Sprite
            spritebatch.Draw(rect, new Rectangle(60, 30, 200, 200), Color.SaddleBrown);
            //Player Sprite
            spritebatch.Draw(mainMan.DrawMan.SpriteDict[mainMan.GameMan.Players[0].Character.Type], new Rectangle(110, 80, 100, 100), Color.White);
            //Character stat box
            spritebatch.Draw(rect, new Rectangle(270, 30, 500, 200), Color.SaddleBrown);
            //Character name
            spritebatch.DrawString(mainMan.DrawMan.StatFont, mainMan.GameMan.Players[0].Character.Name, new Vector2(280, 35), Color.White);
            //Names of sword stats
            spritebatch.DrawString(mainMan.DrawMan.StatFont, "Agility", new Vector2(120, 310), Color.RoyalBlue);
            spritebatch.DrawString(mainMan.DrawMan.StatFont, "Strength", new Vector2(100, 385), Color.RoyalBlue);
            spritebatch.DrawString(mainMan.DrawMan.StatFont, "Dexterity", new Vector2(390, 310), Color.RoyalBlue);
            spritebatch.DrawString(mainMan.DrawMan.StatFont, "Force", new Vector2(465, 385), Color.RoyalBlue);
            spritebatch.DrawString(mainMan.DrawMan.StatFont, "Health", new Vector2(690, 333), Color.RoyalBlue);
            //Names of Character Stats
            spritebatch.DrawString(mainMan.DrawMan.StatFont, "Health: ", new Vector2(300, 100), Color.White);
        }
    }
}
