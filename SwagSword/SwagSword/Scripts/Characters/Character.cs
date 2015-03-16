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

    public enum AnimationState
    {
        FaceDown,
        FaceUp,
        FaceLeft,
        FaceRight,
        MoveDown,
        MoveUp,
        MoveLeft,
        MoveRight
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
        private AnimationState animationState;
        private bool isControlled;

        //Animation
        private int frameWidth;
        private int frameHeight;
        private int frameX;
        private int frameY;
        private int totalFrames;
        private float frameLength; //The length of time a frame is displayed for
        private float frameTime; //How long a frame has been displayed so far

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
        public float X { get { return position.X; } 
            set 
            {
                position.X = value;
                if (position.X < 0)
                    position.X = 0;
                if (position.X > mainMan.MapWidth)
                    position.X = mainMan.MapWidth;

            } }
        public float Y { get { return position.Y; } 
            set 
            {
                position.Y = value;
                if (position.Y < 0)
                    position.Y = 0;
                if (position.Y > mainMan.MapHeight)
                    position.Y = mainMan.MapHeight;
            } }
        public SpriteEffects SpriteEffect { get { return spriteEffect; } set { spriteEffect = value; } }
        public Faction Type { get { return type; } }
        public AnimationState AnimationState { get { return animationState; } set { animationState = value; } }
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
            rectangle = new Rectangle(0, 0, 64, 64);
            position = new Vector2(x, y);
            center = new Vector2(64 / 2, 64 / 2);

            //Set weapon, abilites
            weapon = new Weapon(this, mainMan);
        }

        public virtual void Init()
        {
            //Init Stats
            isControlled = false;
            InitStats();
            health = 10;
            maxHealth = 10;

            //Init Animation
            frameX = 0;
            frameY = 0;
            frameWidth = 64;
            frameHeight = 64;
            totalFrames = 4;
            frameLength = 0.1f;
            frameTime = 0.0f;
            animationState = AnimationState.FaceRight;

            //Init physics
            velocityX = 0;
            velocityY = 0;
            drag = movementSpeed; //Drag should never go above this
            angle = 0f;
        }

        void InitStats()
        {
            //Will init all stats based on a config file
            //Example Health stats
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
        /// Starts a new animation by setting frameY and reseting the frame timer
        /// </summary>
        /// <param name="anim"></param>
        public void StartAnimation(AnimationState anim)
        {
            switch (anim)
            {
                case AnimationState.MoveDown:
                    frameY = 1;
                    break;
                case AnimationState.MoveUp:
                    frameY = 4;
                    break;
                case AnimationState.MoveLeft:
                    frameY = 3;
                    break;
                case AnimationState.MoveRight:
                    frameY = 2;
                    break;
            }

            animationState = anim;
            frameX = 0;
            frameTime = 0.0f;
        }

        /// <summary>
        /// Each character will be responsible for drawing themselves
        /// </summary>
        /// <param name="spritebatch"></param>
        public void Draw(SpriteBatch spritebatch, GameTime gameTime)
        {
            switch (animationState)
            {
                case AnimationState.FaceDown:
                    frameX = 0;
                    frameY = 0;
                    break;
                case AnimationState.FaceUp:
                    frameX = 3;
                    frameY = 0;
                    break;
                case AnimationState.FaceLeft:
                    frameX = 2;
                    frameY = 0;
                    break;
                case AnimationState.FaceRight:
                    frameX = 1;
                    frameY = 0;
                    break;

                default:
                    frameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    while (frameTime > frameLength)
                    {
                        if (frameX < totalFrames - 1)
                        {
                            frameX++;
                        }
                        else
                        {
                            frameX = 0;
                        }
                        frameTime = 0f;
                    }
                    break;
            }

            rectangle = new Rectangle(frameX * frameWidth, frameY * frameHeight, frameWidth, frameHeight);

            spritebatch.Draw(texture, position, rectangle, Color.White, angle, center, 1.0f, spriteEffect, 1);

            weapon.Draw(spritebatch);
        }
    }
}
