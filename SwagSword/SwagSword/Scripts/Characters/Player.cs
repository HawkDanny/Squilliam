using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Names: Nelson Scott

namespace SwagSword
{
    /// <summary>
    /// Holds the character being controlled, will process input from input manager
    /// </summary>
    public class Player
    {
        #region Fields
        //Used to access Game
        protected Game1 mainMan;

        //Character
        private Character character;
        private Character targetCharacter;

        //Player
        private float x;
        private float y;
        #endregion

        #region Properties
        //Main
        public Character Character { get { return character; } set { character = value; } }
        public Character TargetCharacter { get { return targetCharacter; } set { targetCharacter = value; } }
        public float X { get { return x; } }
        public float Y { get { return y; } }
        public Vector2 Position { get { return new Vector2(x, y); } }

        //Shortcuts to character
        public float VelocityX { get { return character.VelocityX; } }
        public float VelocityY { get { return character.VelocityY; } }
        public float Direction { get { return character.Direction; } set { character.Direction = value; } }
        public CharacterState CharacterState { get { if (NoCharacter) { return CharacterState.Dead; } else { return character.CharacterState; } } }

        //Stats
        public int Health { get { if (NoCharacter) { return 0; } else { return character.Health; } } }
        public int MaxHealth { get { if (NoCharacter) { return 0; } else { return character.MaxHealth; } } }

        //Switch helpers
        public bool NoCharacter { get { return character == null; } }
        public Rectangle SwordRect { get { return new Rectangle(0, 0, mainMan.DrawMan.SwordTexture.Width, mainMan.DrawMan.SwordTexture.Height); } }
        public Vector2 SwordCenter { get { return new Vector2(mainMan.DrawMan.SwordTexture.Width / 2f, mainMan.DrawMan.SwordTexture.Height / 2f); } }
        #endregion

        public Player(Character character, Game1 mainMan)
        {
            this.mainMan = mainMan;
            SwitchBlade(character);
        }

        /// <summary>
        /// Put a character under control of the player
        /// </summary>
        /// <param name="character"></param>
        public void SwitchBlade(Character character)
        {
            this.character = character;
            this.character.IsControlled = true;
            this.character.Health = this.character.MaxHealth;
            this.character.Weapon.SetTexture(character.IsControlled, this.character.Type);
            this.character.SwitchState(CharacterState.Switch);
            targetCharacter = null;
        }

        //The main update for player, character's update is not called
        public void Update()
        {
            if (CharacterState == CharacterState.Dead && !mainMan.GameMan.Characters.Contains(character))
            {
                //remove the character reference
                character = null;
            }
            else if (!NoCharacter)
            {
                //Only allow input when active
                if (CharacterState == CharacterState.Active)
                {
                    #region Movement + animation
                    //Move character based on input and set proper animation
                    if (mainMan.InputMan.Right.IsDown())
                    {
                        if (character.AnimationState != AnimationState.MoveRight)
                        {
                            character.StartAnimation(AnimationState.MoveRight);
                        }
                        character.VelocityX = character.MovementSpeed;

                        //In case moving diagonal
                        if (mainMan.InputMan.Up.IsDown())
                        {
                            character.VelocityY = -character.MovementSpeed;
                        }
                        else if (mainMan.InputMan.Down.IsDown())
                        {
                            character.VelocityY = character.MovementSpeed;
                        }
                    }
                    else if (mainMan.InputMan.Left.IsDown())
                    {
                        if (character.AnimationState != AnimationState.MoveLeft)
                        {
                            character.StartAnimation(AnimationState.MoveLeft);
                        }
                        character.VelocityX = -character.MovementSpeed;

                        //In case moving diagonal
                        if (mainMan.InputMan.Up.IsDown())
                        {
                            character.VelocityY = -character.MovementSpeed;
                        }
                        else if (mainMan.InputMan.Down.IsDown())
                        {
                            character.VelocityY = character.MovementSpeed;
                        }
                    }
                    else
                    {
                        if (mainMan.InputMan.Up.IsDown())
                        {
                            if (character.AnimationState != AnimationState.MoveUp)
                            {
                                character.StartAnimation(AnimationState.MoveUp);
                            }
                            character.VelocityY = -character.MovementSpeed;
                        }
                        else if (mainMan.InputMan.Down.IsDown())
                        {
                            if (character.AnimationState != AnimationState.MoveDown)
                            {
                                character.StartAnimation(AnimationState.MoveDown);
                            }
                            character.VelocityY = character.MovementSpeed;
                        }
                    }

                    //Set proper rest animation
                    if (mainMan.InputMan.AllMovementKeysUp)
                    {
                        switch (character.AnimationState)
                        {
                            case AnimationState.MoveDown:
                                character.StartAnimation(AnimationState.FaceDown);
                                break;
                            case AnimationState.MoveUp:
                                character.StartAnimation(AnimationState.FaceUp);
                                break;
                            case AnimationState.MoveLeft:
                                character.StartAnimation(AnimationState.FaceLeft);
                                break;
                            case AnimationState.MoveRight:
                                character.StartAnimation(AnimationState.FaceRight);
                                break;
                        }
                    }
                    #endregion

                    #region Attack Input
                    if (mainMan.InputMan.Attack.IsDown() && mainMan.InputMan.AttackHeld == false)
                    {
                        //So the player can't just hold to swing
                        mainMan.InputMan.AttackHeld = true;

                        //Swing the sword
                        character.Weapon.Angle = mainMan.InputMan.AngleToPointer(X, Y);
                        Direction = character.Weapon.Angle;

                        //Minor Animation
                        if (mainMan.InputMan.AllMovementKeysUp)
                        {
                            float adjustedAngle = 180f - character.Weapon.Angle;
                            if (adjustedAngle >= 45f && adjustedAngle < 135f)
                            {
                                character.AnimationState = AnimationState.FaceRight;
                            }
                            else if (adjustedAngle < 225f)
                            {
                                character.AnimationState = AnimationState.FaceDown;
                            }
                            else if (adjustedAngle < 315f)
                            {
                                character.AnimationState = AnimationState.FaceLeft;
                            }
                            if (adjustedAngle >= 315f || adjustedAngle < 45f)
                            {
                                character.AnimationState = AnimationState.FaceUp;
                            }
                        }

                        character.Weapon.Swing();
                    }
                    else if (mainMan.InputMan.Attack.IsUp())
                    {
                        //Resets AttackHeld so the player can swing again
                        mainMan.InputMan.AttackHeld = false;
                    }
                    #endregion

                    //Update the Camera
                    /*if (VelocityX != 0.0f || VelocityY != 0.0f)
                    {
                        mainMan.DrawMan.Camera.Update();
                    }*/
                }

                x = character.X;
                y = character.Y;
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            //Draw the sword to be picked up
            if (NoCharacter)
            {
                spritebatch.Draw(mainMan.DrawMan.SwordTexture, new Vector2(x, y), SwordRect, Color.LightGoldenrodYellow, 270f * (float)Math.PI / 180f, SwordCenter, 1.0f, SpriteEffects.None, 1);
            }
        }
    }
}
