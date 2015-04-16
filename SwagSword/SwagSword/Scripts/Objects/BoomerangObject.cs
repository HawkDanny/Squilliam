using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SwagSword
{
    public class BoomerangObject
    {
        #region Fields
        //Used to access Game
        protected Game1 mainMan;

        //Main
        private Texture2D texture;
        private Rectangle rectangle;
        private Vector2 position;
        private float angle;
        private Vector2 center; //The center point to be used for rotation
        private Character owner; //The thrower of the boomerang

        //Stats
        protected int damage;

        //Physics
        private float velocityX;
        private float velocityY;
        private float drag;
        private float direction; //Think of it as the forward angle
        #endregion

        #region Properties

        #endregion


        public BoomerangObject(int x, int y, Character owner, Game1 mainMan)
        {
            //Manager
            this.mainMan = mainMan;

            this.owner = owner;

            //Set texture
            this.texture = mainMan.DrawMan.SwordTexture;

            //Set position
            rectangle = new Rectangle(0, 0, 30, 30);
            position = new Vector2(x, y);
            center = new Vector2(30.0f / 2f, 30.0f / 2f);
            angle = 0f;
        }

        public void Update()
        {

        }

        public void Throw(float velX, float velY)
        {

        }
    }
}
