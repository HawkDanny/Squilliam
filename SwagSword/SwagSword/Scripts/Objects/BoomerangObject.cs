using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Names: Nelson Scott

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
        private bool reverse;

        //Stats
        protected int damage;

        //Physics
        private float velocityX;
        private float velocityY;
        private float drag;
        private float direction; //Think of it as the forward angle
        #endregion

        #region Properties
        public Rectangle HitBox { get { return new Rectangle((int)position.X - 40, (int)position.Y - 40, 80, 80); } }
        #endregion


        public BoomerangObject(Character owner, Game1 mainMan)
        {
            //Manager
            this.mainMan = mainMan;

            this.owner = owner;

            //Set texture
            texture = mainMan.DrawMan.SwordTexture;

            //Set position
            rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            position = new Vector2(owner.X, owner.Y);
            center = new Vector2(texture.Width / 2f, texture.Height / 2f);
            angle = 0f;
            drag = 2f;
        }

        public void Update()
        {
            //Update position based on velocity X, Y
            position.X += velocityX;
            position.Y += velocityY;

            if (!reverse)
            {
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
                if (velocityX == 0f && velocityY == 0f)
                {
                    reverse = true;
                }
            }
            else
            {
                direction = (float)Math.Atan2(owner.X + owner.VelocityX - position.X, owner.Y + owner.VelocityY - position.Y) * 180f / (float)Math.PI;

                velocityX += (float)(3f * Math.Cos((90f - direction) * Math.PI / 180f));
                velocityY += (float)(3f * Math.Sin((90f - direction) * Math.PI / 180f));

                //Return if hit owner
                if (owner.HitBox.Intersects(HitBox))
                {
                    reverse = false;
                    owner.CurrentAbility.InUse = false;
                }
            }

            //Check collisions, this should probably change
            foreach (Character character in mainMan.GameMan.Characters)
            {
                if (character.Type != owner.Type)
                {
                    if (character.CharacterState == CharacterState.Active && character.HitBox.Intersects(HitBox))
                    {
                        character.TakeHit(owner.Damage / 2, 0f, 0f);
                    }
                }
            }
        }

        public void Throw(float velX, float velY)
        {
            velocityX = velX;
            velocityY = velY;
            position.X = owner.X;
            position.Y = owner.Y;
            reverse = false;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            //Draw the sword to be picked up
            if (owner.CurrentAbility.InUse)
            {
                angle += 1.0f;
                Console.WriteLine(position.X);
                spritebatch.Draw(texture, position, rectangle, Color.White, angle, center, 1.0f, SpriteEffects.None, 1);
                //spritebatch.Draw(texture, HitBox, Color.Red);
            }
        }
    }
}
