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
        Texture2D rect;
        int x;

        public GameScreen(Game1 mainMan):
            base(mainMan)
        {
            rect = new Texture2D(mainMan.GraphicsDevice, 1, 1);
            rect.SetData(new[] { Color.White });
            x = 200;
        }

        public override void Update()
        {
            if(x>=0)
            {
                x--;
            }

        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(rect, new Rectangle(20, 20, 200, 10), Color.Gray);
            spritebatch.Draw(rect, new Rectangle(20, 20, (mainMan.GameMan.Characters[0].Health/mainMan.GameMan.Characters[0].MaxHealth)*200, 10), Color.Green);
            base.Draw(spritebatch);
        }
    }
}
