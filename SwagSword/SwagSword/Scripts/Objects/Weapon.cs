using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Names: Nelson Scott

namespace SwagSword
{
    //Handles collision with enemies, swinging
    public class Weapon
    {
        #region Fields
        //References to Character and game
        private Game1 mainMan;
        private Character character;
        private Faction type;

        //Main
        private Texture2D texture;
        private SpriteEffects spriteEffect;
        private float x;
        private float y;
        private Rectangle rectangle;
        private Vector2 position;
        private Vector2 center;

        //Swinging vars
        private bool isSwinging;
        private bool isClockwise;
        private float spreadAngle;
        private float startAngle;
        private float stopAngle;
        private float swingSpeed;
        private float currentAngle;
        #endregion

        #region Properties
        //Main
        public Texture2D Texture { get { return texture; } set { texture = value; } }
        public float Angle { get { return currentAngle; } set { currentAngle = value; } }

        //Swinging vars
        public bool IsSwinging { get { return isSwinging; } set { isSwinging = value; } }
        #endregion

        public Weapon(Character character, Game1 mainMan)
        {
            this.character = character;
            this.mainMan = mainMan;

            Init();
        }

        void Init()
        {
            //Init Vars
            type = character.Type;
            isSwinging = false;
            spreadAngle = 120f;
            currentAngle = 0f;
            startAngle = 0f;
            stopAngle = 0f;

            //Set Texture
            SetTexture(character.IsControlled, type);

            //Set position, rectangle, and center
            x = character.X;
            y = character.Y;
            rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            position = new Vector2(x, y);
            center = new Vector2(0, texture.Height / 2);
        }

        public void Update()
        {
            if (isSwinging)
            {
                if (isClockwise)
                {
                    currentAngle += swingSpeed;
                    if (currentAngle > stopAngle)
                    {
                        StopSwing();
                    }
                }
                else
                {
                    currentAngle -= swingSpeed;
                    if (currentAngle < stopAngle)
                    {
                        StopSwing();
                    }
                }
            }

            //Set Position
            x = character.X;
            y = character.Y;
            position = new Vector2(x, y);
        }

        /// <summary>
        /// Starts the sword swing and enables collision detection
        /// </summary>
        public void Swing()
        {
            isSwinging = true;

            if (mainMan.Rnd.Next(1, 3) == 1)
            {
                //Is a clockwise swing
                isClockwise = true;
                startAngle = currentAngle - spreadAngle / 2;
                stopAngle = currentAngle + spreadAngle / 2;
                spriteEffect = SpriteEffects.FlipVertically;
            }
            else
            {
                //Is a counter clockwise swing
                isClockwise = false;
                startAngle = currentAngle + spreadAngle / 2;
                stopAngle = currentAngle - spreadAngle / 2;
                spriteEffect = SpriteEffects.None;
            }

            swingSpeed = (float)mainMan.Rnd.Next((int)character.AttackSpeedMin, (int)character.AttackSpeedMax);
            currentAngle = startAngle;
        }

        /// <summary>
        /// Stops a swing if swinging
        /// </summary>
        public void StopSwing()
        {
            if (isSwinging)
            {
                isSwinging = false;
            }
        }

        /// <summary>
        /// Sets the proper texture for the weapon
        /// </summary>
        /// <param name="isControlled"></param>
        /// <param name="type"></param>
        public void SetTexture(bool isControlled, Faction type)
        {
            if (isControlled)
            {
                //Sets the texture to the main sword
                texture = mainMan.DrawMan.SwordTexture;
            }
            else
            {
                //Sets the texture to the faction's default
                switch (type)
                {
                    default:
                        texture = mainMan.DrawMan.SwordTexture;
                        break;
                }
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            //Should only draw when weapon is in motion
            if (isSwinging)
            {
                spritebatch.Draw(texture, position, rectangle, Color.White, (90f - currentAngle) * (float)Math.PI / 180f, center, 1.0f, spriteEffect, 1);
            }
        }
    }
}
