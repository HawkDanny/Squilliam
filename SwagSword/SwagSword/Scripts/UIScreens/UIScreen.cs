using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Names: Peter Lockhart

namespace SwagSword
{
    //The base class for all screens for the game.
    public class UIScreen
    {
        //Fields
        protected Game1 mainMan;

        public UIScreen(Game1 mainMan)
        {
            this.mainMan = mainMan;
        }

        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spritebatch)
        {

        }
    }
}
