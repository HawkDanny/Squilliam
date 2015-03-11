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
            spritebatch.Draw(rect, new Rectangle(20, 20, 200, 10), Color.Green);
            base.Draw(spritebatch);
        }
    }
}
