using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Names: Nelson Scott

namespace SwagSword
{
    public class PATE
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
        private Character owner;
        private Character target;
        private bool reverse;

        //Physics
        private float velocityX;
        private float velocityY;
        private float drag;
        private float direction; //Think of it as the forward angle
        #endregion

        #region Properties
        public Rectangle HitBox { get { return new Rectangle((int)position.X - 40, (int)position.Y - 40, 80, 80); } }
        #endregion


        public PATE(Character owner, Game1 mainMan)
        {
            //Manager
            this.mainMan = mainMan;

            this.owner = owner;

            //Set texture
            texture = mainMan.DrawMan.PateTexture;

            //Set position
            rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            position = new Vector2(owner.X, owner.Y);
            center = new Vector2(texture.Width / 2f, texture.Height / 2f);
            angle = 0f;
            drag = 1f;
        }

        public void Update()
        {
            //Update position based on velocity X, Y
            position.X += velocityX;
            position.Y += velocityY;

            //Add natural drag to movement speed
            if (velocityX != 0)
            {
                if (velocityX > 0)
                {
                    velocityX -= drag;
                    if (velocityX < 0)
                    {
                        velocityX = 0;
                    }
                }
                else
                {
                    velocityX += drag;
                    if (velocityX > 0)
                    {
                        velocityX = 0;
                    }
                }
           }
           if (velocityY != 0)
           {
                if (velocityY > 0)
                {
                    velocityY -= drag;
                    if (velocityY < 0)
                    {
                        velocityY = 0;
                    }
                }
                else
                {
                    velocityY += drag;
                    if (velocityY > 0)
                    {
                        velocityY = 0;
                    }
                }
            }


            //Follow logic
            if (owner.VelocityX != 0f || owner.VelocityY != 0f)
            {
                if ((float)Math.Sqrt(Math.Pow(owner.X - position.X, 2) + Math.Pow(owner.Y - position.Y, 2)) > 250f)
                {
                    direction = (float)Math.Atan2(owner.X - position.X, owner.Y - position.Y) * 180f / (float)Math.PI;
                    velocityX += (float)(2f * Math.Cos((90f - direction) * Math.PI / 180f));
                    velocityY += (float)(2f * Math.Sin((90f - direction) * Math.PI / 180f));

                    if (target == null)
                    {
                        angle = direction;
                    }
                }
            }
        }

        public void Deploy(float x, float y)
        {
            position.X = x;
            position.Y = y;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            //Draw the sword to be picked up
            if (owner.CurrentAbility.InUse)
            {
                spritebatch.Draw(texture, position, rectangle, Color.White, (180f - angle) * (float)Math.PI / 180f, center, 1.0f, SpriteEffects.None, 1);
                //spritebatch.Draw(texture, HitBox, Color.Red);
            }
        }
    }
}
