using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Names: Nelson Scott

namespace SwagSword
{
    public class BulletObject
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
        private PATE owner; //The character that owns the PATE
        private Character target;
        private bool isFired;

        //Physics
        private float velocityX;
        private float velocityY;
        private float drag;
        private float direction; //Think of it as the forward angle
        #endregion

        #region Properties
        public Rectangle HitBox { get { return new Rectangle((int)position.X - 8, (int)position.Y - 8, 16, 16); } }

        public bool IsFired { get { return isFired; } set { isFired = value; } }
        #endregion


        public BulletObject(PATE owner, Game1 mainMan)
        {
            //Manager
            this.mainMan = mainMan;

            this.owner = owner;

            //Set texture
            texture = mainMan.DrawMan.BulletTexture;

            //Set position
            rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            position = new Vector2(owner.X, owner.Y);
            center = new Vector2(texture.Width / 2f, texture.Height / 2f);
            angle = 0f;
            drag = 2f;
        }

        public void Update()
        {
            if (isFired)
            {
                //Update position based on velocity X, Y
                position.X += velocityX;
                position.Y += velocityY;

                velocityX = (float)(5f * Math.Cos((90f - direction) * Math.PI / 180f));
                velocityY = (float)(5f * Math.Sin((90f - direction) * Math.PI / 180f));

                //Collision/bounds check
                if (owner.Owner.Distance(position.ToPoint(), owner.X, owner.Y) > 500f || owner.Target == null)
                {
                    isFired = false;
                }
                else if (target.CharacterState == CharacterState.Active)
                {
                    foreach (Character character in mainMan.GameMan.Characters)
                    {
                        if (character.Type == target.Type && character.HitBox.Intersects(HitBox))
                        {
                            character.TakeHit(owner.Owner.Damage / 3, 0f, 0f);
                            isFired = false;
                        }
                    }
                }

                //Null target if target is killed
                if (target.Health <= 0)
                {
                    owner.Target = null;
                    target = null;
                }
            }
        }

        public void Fire(Character target)
        {
            position.X = owner.X;
            position.Y = owner.Y;
            isFired = true;
            this.target = target;

            //Set direction
            direction = (float)Math.Atan2(target.X - position.X, target.Y - position.Y) * 180f / (float)Math.PI;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (owner.Target == null)
            {
                isFired = false;
            }

            //Draw the bullet
            if (isFired)
            {
                spritebatch.Draw(texture, position, rectangle, Color.White, angle, center, 1.0f, SpriteEffects.None, 1);
                //spritebatch.Draw(texture, HitBox, Color.Red);
            }
        }
    }
}
