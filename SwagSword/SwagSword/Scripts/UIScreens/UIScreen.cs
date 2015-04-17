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

        protected int leftX;
        protected int topY;

        //Properties
        public int LeftX { get { return leftX; } set { leftX = value; } }
        public int TopY { get { return topY; } set { topY = value; } }

        public UIScreen(Game1 mainMan)
        {
            this.mainMan = mainMan;
            leftX = (int)mainMan.DrawMan.Camera.TopLeftPosition.X;
            topY = (int)mainMan.DrawMan.Camera.TopLeftPosition.Y;
        }

        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spritebatch)
        {

        }
    }
}
