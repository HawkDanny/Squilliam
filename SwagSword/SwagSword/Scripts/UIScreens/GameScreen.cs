using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SwagSword
{
    public class GameScreen : UIScreen
    {
        //Fields
        //Rectangle texture
        Texture2D rect;

        public GameScreen(Game1 mainMan):
            base(mainMan)
        {
            rect = new Texture2D(mainMan.GraphicsDevice, 1, 1);
            rect.SetData(new[] { Color.White });
        }

        public override void Update()
        {

        }

        public override void Draw(SpriteBatch spritebatch)
        {
            //Draws the back of the health bar as gray
            spritebatch.Draw(rect, new Rectangle(20, 20, 200, 10), Color.Gray);
            //Draws the actual Health meter as green
            spritebatch.Draw(rect, new Rectangle(20, 20, (mainMan.GameMan.Characters[0].Health/mainMan.GameMan.Characters[0].MaxHealth)*200, 10), Color.Green);
            //Draws the number of health next to the bar
            spritebatch.DrawString(mainMan.DrawMan.HealthFont, mainMan.GameMan.Characters[0].Health + "/" + mainMan.GameMan.Characters[0].MaxHealth, new Vector2(225f, 14f), Color.Green);
            base.Draw(spritebatch);
        }
    }
}
