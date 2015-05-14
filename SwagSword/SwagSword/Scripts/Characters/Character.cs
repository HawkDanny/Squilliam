using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


//Names: Nelson Scott

namespace SwagSword
{
    //Enum for factions
    public enum Faction
    {
        Tribal,
        Good,
        Rich,
        Thief,
        Neutral
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

    public enum CharacterState
    {
        Spawn,
        Switch,
        Active,
        Hurt,
        Dead
    }

    public enum AIState
    {
        Attack, //Attack the player
        Flank, //Move around the player
        Defend, //Retreat from the player
        Ability, //Use ability
        Ready, //Face and wait for opportunity
        Idle, //Waltz around
        Swing, //Swing the sword
        Switch, //The sword is up for grabs, get it!!!
        Cower //Run away
    }

    /// <summary>
    /// The base class for every type of Character
    /// </summary>
    public class Character
    {
        #region Fields
        //Used to access Game
        protected Game1 mainMan;

        //Main
        private Texture2D texture;
        private Rectangle rectangle;
        private Vector2 position;
        private Vector2 center; //The center point to be used for rotation
        private SpriteEffects spriteEffect; //Used for flipping the sprite
        private Color color;
        private Color flashColor;
        private Color normalColor;
        private Faction type;
        private AnimationState animationState;
        private bool isControlled;
        private CharacterState characterState;
        private float stateTimer;
        private Faction currentArea;

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
        private List<Ability> abilities;
        private Ability currentAbility;

        //Stats
        protected String name;
        protected int health;
        protected int maxHealth;
        protected int damage;
        protected float strength;
        protected float movementSpeed;
        protected float attackSpeedMin; //In degrees
        protected float attackSpeedMax;

        //Physics
        private float velocityX;
        private float velocityY;
        private float drag;
        private float direction; //Think of it as the forward angle

        //AI
        private AIState aiState;
        private float aiStateTimer;
        private Dictionary<AIState, float> aiProbs;
        private Dictionary<AIState, float> aiTimers;
        private List<AIState> encounterAIStates;
        private float sightRange;
        private float attackRange;
        #endregion

        #region Properties
        //Main
        public Texture2D Texture { get { return texture; } set { texture = value; } }
        public Rectangle Rectangle { get { return rectangle; } set { rectangle = value; } }
        public Rectangle HitBox { get { return new Rectangle((int)position.X - frameWidth / 2, (int)position.Y - frameWidth / 2, frameWidth, frameHeight); } }
        public float X 
        { 
            get { return position.X; } 
            set 
            {
                position.X = value;
                if (position.X < 0)
                    position.X = 0;
                if (position.X > mainMan.MapWidth)
                    position.X = mainMan.MapWidth;

            } 
        }
        public float Y 
        { 
            get { return position.Y; } 
            set 
            {
                position.Y = value;
                if (position.Y < 0)
                    position.Y = 0;
                if (position.Y > mainMan.MapHeight)
                    position.Y = mainMan.MapHeight;
            } 
        }
        public Vector2 Center { get { return center; } }
        public SpriteEffects SpriteEffect { get { return spriteEffect; } set { spriteEffect = value; } }
        public Color Color { get { return color; } set { color = value; } }
        public Color FlashColor { get { return flashColor; } set { flashColor = value; } }
        public Color NormalColor { get { return normalColor; } set { normalColor = value; } }
        public Faction Type { get { return type; } set { type = value; } }
        public AnimationState AnimationState { get { return animationState; } set { animationState = value; } }
        public CharacterState CharacterState { get { return characterState; } set { characterState = value; } }
        public float StateTimer { get { return stateTimer; } set { stateTimer = value; } }
        public bool IsControlled { get { return isControlled; } set { isControlled = value; } }
        public Faction CurrentArea { get { return currentArea; } set { currentArea = value; } }

        //Weapon + Abilities
        public Weapon Weapon { get { return weapon; } }
        public List<Ability> Abilities { get { return abilities; } }
        public Ability CurrentAbility { get { return currentAbility; } set { currentAbility = value; } }
        

        //Stats
        public String Name { get { return name; } set { name = value; } }
        public int Health { get { return health; } set { health = value; } }
        public int MaxHealth { get { return maxHealth; } }
        public float Strength { get { return strength; } }
        public int Damage { get { return damage; } }
        public float MovementSpeed { get { return movementSpeed; } }
        public float AttackSpeedMin { get { return attackSpeedMin; } }
        public float AttackSpeedMax { get { return attackSpeedMax; } }

        //Physics
        public float VelocityX { get { return velocityX; } set { velocityX = value; } }
        public float VelocityY { get { return velocityY; } set { velocityY = value; } }
        public float Drag { get { return drag; } }
        public float Direction { get { return direction; } set { direction = value; } }

        //AI
        public AIState AIState { get { return aiState; } set { aiState = value; } }
        public float AIStateTimer { get { return aiStateTimer; } set { aiStateTimer = value; } }
        public Dictionary<AIState, float> AIProbs { get { return aiProbs; } set { aiProbs = value; } }
        public Dictionary<AIState, float> AITimers { get { return aiTimers; } set { aiTimers = value; } }
        public List<AIState> EncounterAIStates { get { return encounterAIStates; } set { encounterAIStates = value; } }
        public float SightRange { get { return sightRange; } set { sightRange = value; } }
        public float AttackRange { get { return attackRange; } set { attackRange = value; } }
        #endregion

        public Character(int x, int y, Texture2D texture, Game1 mainMan)
        {
            //Manager
            this.mainMan = mainMan;

            //Set texture
            this.texture = texture;

            //AI
            AIProbs = new Dictionary<AIState, float>();
            AITimers = new Dictionary<AIState, float>();
            encounterAIStates = new List<AIState>();

            //Set position
            rectangle = new Rectangle(0, 0, 66, 66);
            position = new Vector2(x, y);
            center = new Vector2(66.0f / 2f, 66.0f / 2f);

            //Set weapon, abilites
            weapon = new Weapon(this, mainMan);
        }

        public virtual void Init()
        {
            //Init Stats
            isControlled = false;
            SwitchState(CharacterState.Spawn);
            Color = normalColor;
            InitStats();

            //Init Animation
            frameX = 0;
            frameY = 0;
            frameWidth = 66;
            frameHeight = 66;
            totalFrames = 4;
            frameLength = 0.1f;
            frameTime = 0.0f;
            animationState = AnimationState.FaceDown;

            //Init AI
            SwitchAIState(AIState.Idle);

            //Init physics
            velocityX = 0;
            velocityY = 0;
            drag = movementSpeed; //Drag should never go above this
            direction = 0f;
        }

        protected virtual void InitStats()
        {
            //Will init all stats based on a config file
            //Example Health stats
            name = "Peter";
            health = 100;
            maxHealth = 100;
            strength = 30f;
            damage = 25;
            attackSpeedMin = 10.0f;
            attackSpeedMax = 20.0f;
            movementSpeed = 5.0f;
        }

        /// <summary>
        /// The main update method
        /// </summary>
        public virtual void Update()
        {
            weapon.Update();
            currentAbility.Update();

            //Swap between character states
            switch (characterState)
            {
                case CharacterState.Spawn:
                    if (stateTimer % 0.2f < 0.1f)
                    {
                        FlashColors();
                    }

                    UpdatePhysics();

                    stateTimer -= (float)mainMan.GameTime.ElapsedGameTime.TotalSeconds;
                    if (stateTimer <= 0f)
                    {
                        SwitchState(CharacterState.Active);
                    }
                    break;

                case CharacterState.Switch:
                    if (stateTimer % 0.2f < 0.1f)
                    {
                        FlashColors();
                    }

                    UpdatePhysics();

                    stateTimer -= (float)mainMan.GameTime.ElapsedGameTime.TotalSeconds;
                    if (stateTimer <= 0f)
                    {
                        SwitchState(CharacterState.Active);
                    }
                    break;

                case CharacterState.Active:
                    UpdatePhysics();
                    break;

                case CharacterState.Hurt:
                    if (stateTimer % 0.2f < 0.1f)
                    {
                        FlashColors();
                    }

                    UpdatePhysics();

                    stateTimer -= (float)mainMan.GameTime.ElapsedGameTime.TotalSeconds;
                    if (stateTimer <= 0f)
                    {
                        SwitchState(CharacterState.Active);
                    }
                    break;

                case CharacterState.Dead:
                    UpdatePhysics();
                    if (color.A - (byte)mainMan.GameTime.ElapsedGameTime.TotalMilliseconds <= 0)
                    {
                        Kill();
                    }
                    else
                    {
                        color.A -= (byte)mainMan.GameTime.ElapsedGameTime.TotalMilliseconds;
                    }
                    break;
            }
        }

        /// <summary>
        /// Switches the characterState and updates stateTimer
        /// </summary>
        public void SwitchState(CharacterState state)
        {
            characterState = state;
            color = normalColor;
            switch (characterState)
            {
                case CharacterState.Spawn:
                    stateTimer = 1.0f;
                    flashColor = Color.GreenYellow;
                    SwitchAIState(AIState.Idle);
                    break;

                case CharacterState.Switch:
                    stateTimer = 1.0f;
                    flashColor = Color.Gold;
                    AnimateFaceDirection();
                    break;

                case CharacterState.Active:
                    stateTimer = 0f;
                    break;

                case CharacterState.Hurt:
                    stateTimer = 0.2f;
                    flashColor = Color.Red;
                    break;

                case CharacterState.Dead:
                    
                    break;
            }
        }

        /// <summary>
        /// Switches the current AI state and updates timers
        /// </summary>
        /// <param name="state"></param>
        public void SwitchAIState(AIState state)
        {
            AIState = state;
            switch (AIState)
            {
                case AIState.Attack:
                    aiStateTimer = aiTimers[state];
                    break;

                case AIState.Swing:
                    aiStateTimer = aiTimers[state];
                    weapon.Angle = direction;
                    weapon.Swing();
                    AnimateFaceDirection();
                    break;

                case AIState.Defend:
                    aiStateTimer = aiTimers[state];
                    break;

                case AIState.Idle:
                    AnimateFaceDirection();
                    aiStateTimer = aiTimers[state];
                    break;
            }
        }

        /// <summary>
        /// returns a random AI state from a list of state options. Returns AIState.Idle if one is not chosen
        /// </summary>
        /// <param name="states"></param>
        public AIState GetRandomAIState(List<AIState> states)
        {
            foreach (AIState state in states)
            {
                if ((float)mainMan.Rnd.Next(0, 100) / 100f < AIProbs[state])
                {
                    return state;
                }
            }

            return AIState.Idle;
        }


        /// <summary>
        /// Swaps fast between the normal color and the flash color
        /// </summary>
        public void FlashColors()
        {
            if (color == normalColor)
            {
                color = flashColor;
            }
            else
            {
                color = normalColor;
            }
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

            //Keep character in map
            CheckBounds();
        }

        public void CheckBounds()
        {
            //break if in rects
            if (mainMan.GameMan.CenterBound.Contains(X, Y))
            {
                currentArea = Faction.Neutral;
                return;
            }
            if (mainMan.GameMan.LeftPathBound.Contains(X, Y))
            {
                currentArea = Faction.Good;
                return;
            }
            if (mainMan.GameMan.RightPathBound.Contains(X, Y))
            {
                currentArea = Faction.Tribal;
                return;
            }
            if (mainMan.GameMan.LowerPathBound.Contains(X, Y))
            {
                currentArea = Faction.Thief;
                return;
            }
            if (mainMan.GameMan.TopPathBound.Contains(X, Y))
            {
                currentArea = Faction.Rich;
                return;
            }

            //break if in circles
            if (Distance(mainMan.GameMan.MapMan.LeftCenterPoint, X, Y) < mainMan.GameMan.MapMan.Radius)
            {
                currentArea = Faction.Good;
                return;
            }
            if (Distance(mainMan.GameMan.MapMan.RightCenterPoint, X, Y) < mainMan.GameMan.MapMan.Radius)
            {
                currentArea = Faction.Tribal;
                return;
            }
            if (Distance(mainMan.GameMan.MapMan.UpperCenterPoint, X, Y) < mainMan.GameMan.MapMan.Radius)
            {
                currentArea = Faction.Rich;
                return;
            }
            if (Distance(mainMan.GameMan.MapMan.LowerCenterPoint, X, Y) < mainMan.GameMan.MapMan.Radius)
            {
                currentArea = Faction.Thief;
                return;
            }

            //if it is not in bounds make it so
            //top triangle
            if(Y < X && (Y < mainMan.GameMan.MapMan.ResHeight - X))
            {
                //in top tri
                //check indiv x and y bs
                //and circle

                //check if in line with circle
                if (Y < mainMan.GameMan.MapMan.Radius * 2)
                {
                    //find theta
                    double deltaY = mainMan.GameMan.MapMan.UpperCenterPoint.Y - Y;
                    double delatX = mainMan.GameMan.MapMan.UpperCenterPoint.X - X;
                    double theta = Math.Atan2(deltaY, delatX);
                    X = (float)(mainMan.GameMan.MapMan.UpperCenterPoint.X + Math.Cos(theta + Math.PI) * mainMan.GameMan.MapMan.Radius);
                    Y = (float)(mainMan.GameMan.MapMan.UpperCenterPoint.Y + Math.Sin(theta + Math.PI) * mainMan.GameMan.MapMan.Radius);
                }
                else
                {
                    X = MathHelper.Clamp(X, mainMan.GameMan.TopPathBound.X, mainMan.GameMan.TopPathBound.X + mainMan.GameMan.TopPathBound.Width);
                }
            }
            //left triangle
            else if(Y > X && (Y < mainMan.GameMan.MapMan.ResHeight - X))
            {
                //in left tri

                //check if in line with circle
                if(X < mainMan.GameMan.MapMan.Radius * 2)
                {
                    //find theta
                    double deltaY = mainMan.GameMan.MapMan.LeftCenterPoint.Y - Y;
                    double delatX = mainMan.GameMan.MapMan.LeftCenterPoint.X - X;
                    double theta = Math.Atan2(deltaY, delatX);
                    X = (float)(mainMan.GameMan.MapMan.LeftCenterPoint.X + Math.Cos(theta + Math.PI) * mainMan.GameMan.MapMan.Radius);
                    Y = (float)(mainMan.GameMan.MapMan.LeftCenterPoint.Y + Math.Sin(theta + Math.PI) * mainMan.GameMan.MapMan.Radius);
                }
                else
                {
                    Y = MathHelper.Clamp(Y, mainMan.GameMan.LeftPathBound.Y, mainMan.GameMan.LeftPathBound.Y + mainMan.GameMan.LeftPathBound.Height);
                }
            }
            //right triangle
            else if(Y < X && (Y > mainMan.GameMan.MapMan.ResHeight - X))
            {
                //in right tri

                if (X > mainMan.GameMan.MapMan.ResWidth - mainMan.GameMan.MapMan.Radius * 2)
                {
                    //find theta
                    double deltaY = mainMan.GameMan.MapMan.RightCenterPoint.Y - Y;
                    double delatX = mainMan.GameMan.MapMan.RightCenterPoint.X - X;
                    double theta = Math.Atan2(deltaY, delatX);
                    X = (float)(mainMan.GameMan.MapMan.RightCenterPoint.X + Math.Cos(theta + Math.PI) * mainMan.GameMan.MapMan.Radius);
                    Y = (float)(mainMan.GameMan.MapMan.RightCenterPoint.Y + Math.Sin(theta + Math.PI) * mainMan.GameMan.MapMan.Radius);
                }
                else
                {
                    Y = MathHelper.Clamp(Y, mainMan.GameMan.RightPathBound.Y, mainMan.GameMan.RightPathBound.Y + mainMan.GameMan.RightPathBound.Height);
                }
            }
            //lower triangle
            else if(Y > X && (Y > mainMan.GameMan.MapMan.ResHeight - X))
            {
                //in lower tri

                //check if in line with circle
                if (Y > mainMan.GameMan.MapMan.ResHeight - mainMan.GameMan.MapMan.Radius * 2)
                {
                    //find theta
                    double deltaY = mainMan.GameMan.MapMan.LowerCenterPoint.Y - Y;
                    double delatX = mainMan.GameMan.MapMan.LowerCenterPoint.X - X;
                    double theta = Math.Atan2(deltaY, delatX);
                    X = (float)(mainMan.GameMan.MapMan.LowerCenterPoint.X + Math.Cos(theta + Math.PI) * mainMan.GameMan.MapMan.Radius);
                    Y = (float)(mainMan.GameMan.MapMan.LowerCenterPoint.Y + Math.Sin(theta + Math.PI) * mainMan.GameMan.MapMan.Radius);
                }
                else
                {
                    X = MathHelper.Clamp(X, mainMan.GameMan.LowerPathBound.X, mainMan.GameMan.LowerPathBound.X + mainMan.GameMan.LowerPathBound.Width);
                }
            }


        }

        public float Distance(Point a, float bx, float by)
        {
            return (float)Math.Sqrt(Math.Pow(a.X - bx, 2) + Math.Pow(a.Y - by, 2));
        }

        /// <summary>
        /// Returns the characters distance to player specified at index
        /// </summary>
        /// <returns>Distance as a float</returns>
        public float DistanceToPlayer(int index)
        {
            return (float)Math.Sqrt(Math.Pow(mainMan.GameMan.Players[index].X - X, 2) + Math.Pow(mainMan.GameMan.Players[index].Y - Y, 2));
        }

        /// <summary>
        /// Sets the facing direction to the point specified
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetDirectionToPoint(float x, float y)
        {
            direction = (float)Math.Atan2(x - position.X, y - position.Y) * 180f / (float)Math.PI;
        }

        /// <summary>
        /// Moves the AI to this point
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MoveToPoint(float x, float y)
        {
            SetDirectionToPoint(x, y);

            velocityX += (float)(MovementSpeed * Math.Cos((90f - direction) * Math.PI / 180f));
            velocityY += (float)(MovementSpeed * Math.Sin((90f - direction) * Math.PI / 180f));

            //Animation
            if (Math.Abs(velocityX) > Math.Abs(VelocityY))
            {
                if (velocityX > 0)
                {
                    if (animationState != AnimationState.MoveRight)
                        StartAnimation(AnimationState.MoveRight);
                }
                else if (velocityX < 0)
                {
                    if (animationState != AnimationState.MoveLeft)
                        StartAnimation(AnimationState.MoveLeft);
                }
            }
            else if (Math.Abs(velocityX) < Math.Abs(VelocityY))
            {
                if (velocityY > 0)
                {
                    if (animationState != AnimationState.MoveDown)
                        StartAnimation(AnimationState.MoveDown);
                }
                else if (velocityY < 0)
                {
                    if (animationState != AnimationState.MoveUp)
                        StartAnimation(AnimationState.MoveUp);
                }
            }
        }

        /// <summary>
        /// Returns if player is in this character's area
        /// </summary>
        /// <returns></returns>
        public bool PlayerInArea()
        {
            return (mainMan.GameMan.Players[0].CurrentArea == currentArea);
        }

        /// <summary>
        /// Animates the AI to face its direction
        /// </summary>
        public void AnimateFaceDirection()
        {
            float adjustedAngle = 180f - direction;
            if (adjustedAngle >= 45f && adjustedAngle < 135f)
            {
                StartAnimation(AnimationState.FaceRight);
            }
            else if (adjustedAngle < 225f)
            {
                StartAnimation(AnimationState.FaceDown);
            }
            else if (adjustedAngle < 315f)
            {
                StartAnimation(AnimationState.FaceLeft);
            }
            if (adjustedAngle >= 315f || adjustedAngle < 45f)
            {
                StartAnimation(AnimationState.FaceUp);
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
        /// Used to damage character, knock them back
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="force"></param>
        public void TakeHit(int damage, float force, float hitDirection)
        {
            //Take down health;
            health -= damage;
            if (health <= 0)
            {
                SwitchState(CharacterState.Dead);
                health = 0;
            }
            else
            {
                SwitchState(CharacterState.Hurt);
            }

            velocityX += force * (float)Math.Cos((90f - hitDirection) * Math.PI / 180f);
            velocityY += force * (float)Math.Sin((90f - hitDirection) * Math.PI / 180f);
        }

        /// <summary>
        /// Kills this character
        /// </summary>
        public void Kill()
        {
            mainMan.GameMan.Players[0].Exp += 10;
            mainMan.GameMan.CharactersDictionary[type].Remove(this);
            mainMan.GameMan.Characters.Remove(this);
            mainMan.GameMan.SpawnMan.ReplenishCharacter(type);
        }

        /// <summary>
        /// Each character will be responsible for drawing themselves
        /// </summary>
        /// <param name="spritebatch"></param>
        public void Draw(SpriteBatch spritebatch, GameTime gameTime)
        {
            currentAbility.Draw(spritebatch);

            //if (type == Faction.Neutral)
            //{
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
            /*}
            else
            {
                rectangle = new Rectangle(0, 0, frameWidth, frameHeight);
            }*/


            if (!(CurrentAbility.Type == SwagSword.Abilities.Decoy && currentAbility.InUse))
            {
                spritebatch.Draw(texture, position, rectangle, color, 0f, center, 1.0f, spriteEffect, 1);
            }


            weapon.Draw(spritebatch);

            
        }
    }
}
