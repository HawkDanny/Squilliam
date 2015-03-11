using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SwagSword
{
    //Enum for factions
    public enum Faction
    {
        Tribal,
        Good,
        Rich,
        Thief
    }

    /// <summary>
    /// The base class for every type of Character
    /// </summary>
    public class Character
    {
        #region Fields
        //Used to access Game
        private Game1 mainMan;

        //Main
        private Texture2D texture;
        private Rectangle rectangle;
        private Vector2 position;
        private Vector2 center; //The center point to be used for rotation
        private SpriteEffects spriteEffect; //Used for flipping the sprite
        private Faction type;
        private bool isControlled;

        //Weapon + Abilities
        private Weapon weapon;

        //Stats
        private int health;
        private int maxHealth;
        private int strength;
        private float movementSpeed;

        //Physics
        private float velocityX;
        private float velocityY;
        private float drag;
        private float angle;
        #endregion

        #region Properties
        //Main
        public Texture2D Texture { get { return texture; } set { texture = value; } }
        public Rectangle Rectangle { get { return rectangle; } set { rectangle = value; } }
        public float X { get { return position.X; } set { position.X = value; } }
        public float Y { get { return position.Y; } set { position.Y = value; } }
        public SpriteEffects SpriteEffect { get { return spriteEffect; } set { spriteEffect = value; } }
        public Faction Type { get { return type; } }
        public bool IsControlled { get { return isControlled; } set { isControlled = value; } }

        //Weapon + Abilities
        public Weapon Weapon { get { return weapon; } }

        //Stats
        public int Health { get { return health; } set { health = value; } }
        public int MaxHealth { get { return maxHealth; } }
        public int Strength { get { return strength; } }
        public float MovementSpeed { get { return movementSpeed; } }

        //Physics
        public float VelocityX { get { return velocityX; } set { velocityX = value; } }
        public float VelocityY { get { return velocityY; } set { velocityY = value; } }
        public float Drag { get { return drag; } }
        #endregion

        public Character(int x, int y, Texture2D texture, Game1 mainMan)
        {
            //Manager
            this.mainMan = mainMan;

            //Set texture
            this.texture = texture;

            //Set position
            rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            position = new Vector2(x, y);
            center = new Vector2(texture.Width / 2, texture.Height / 2);

            //Set weapon, abilites
            weapon = new Weapon(this, mainMan);
        }

        public virtual void Init()
        {
            //Init Stats
            isControlled = false;
            InitStats();

            //Init physics
            velocityX = 0;
            velocityY = 0;
            drag = movementSpeed; //Drag should never go above this
            angle = 0f;
        }

        void InitStats()
        {
            //Will init all stats based on a config file
            health = 100;
            maxHealth = 100;
            movementSpeed = 3.5f;
            strength = 10;
        }

        public virtual void Update()
        {
            UpdatePhysics();
            weapon.Update();
        }

        /// <summary>
        /// Each character will handle it's own physics
        /// </summary>
        public void UpdatePhysics()
        {
            //Update position based on velocity X, Y
            X += velocityX;
            Y += velocityY;

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
        }

        /// <summary>
        /// Each character will be responsible for drawing themselves
        /// </summary>
        /// <param name="spritebatch"></param>
        public void Draw(SpriteBatch spritebatch)
        {
            weapon.Draw(spritebatch);
            spritebatch.Draw(texture, position, rectangle, Color.White, angle, center, 1.0f, spriteEffect, 1);
        }
    }
}
