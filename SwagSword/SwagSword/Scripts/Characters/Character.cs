using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SwagSword
{
    /// <summary>
    /// The base class for every type of Character
    /// </summary>
    public class Character
    {
        #region Fields
        //Main
        private Texture2D texture;
        private Rectangle rectangle;
        private Vector2 position;
        private Vector2 center; //The center point to be used for rotation
        private SpriteEffects spriteEffect; //Used for flipping the sprite

        //Physics
        private float velocityX;
        private float velocityY;
        private float drag;
        private float movementSpeed;
        private float angle;
        #endregion

        #region Properties
        //Main
        public Texture2D Texture { get { return texture; } set { texture = value; } }
        public Rectangle Rectangle { get { return rectangle; } set { rectangle = value; } }
        public float X { get { return position.X; } set { position.X = value; } }
        public float Y { get { return position.Y; } set { position.Y = value; } }
        public SpriteEffects SpriteEffect { get { return spriteEffect; } set { spriteEffect = value; } }

        //Physics
        public float VelocityX { get { return velocityX; } set { velocityX = value; } }
        public float VelocityY { get { return velocityY; } set { velocityY = value; } }
        public float Drag { get { return drag; } }
        public float MovementSpeed { get { return movementSpeed; } }
        #endregion

        public Character(int x, int y, Texture2D texture)
        {
            //Set texture
            this.texture = texture;

            //Set position
            rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            position = new Vector2(x, y);
            center = new Vector2(texture.Width / 2, texture.Height / 2);

            Init();
        }

        public virtual void Init()
        {
            //Init physics
            velocityX = 0;
            velocityY = 0;
            drag = movementSpeed; //Drag should never go above this
            angle = 0f;
        }

        public virtual void Update()
        {
            UpdatePhysics();
        }

        /// <summary>
        /// Each character will handle it's own physics
        /// </summary>
        protected void UpdatePhysics()
        {
            
        }

        /// <summary>
        /// Each character will be responsible for drawing themselves
        /// </summary>
        /// <param name="spritebatch"></param>
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, position, rectangle, Color.White, angle, center, 1.0f, spriteEffect, 1);
        }
    }
}
