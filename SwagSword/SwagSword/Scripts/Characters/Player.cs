using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        #endregion

        #region Properties
        //Main
        public Character Character { get { return character; } set { character = value; } }
        public float X { get { if (character != null) { return character.X; } else { return 0f; } } }
        public float Y { get { if (character != null) { return character.Y; } else { return 0f; } } }

        //Stats
        public int Health { get { return Health; } }
        public int MaxHealth { get { return MaxHealth; } }
        #endregion

        public Player(Character character, Game1 mainMan)
        {
            this.character = character;
            this.mainMan = mainMan;
        }

        //The main update for player, character's update is not called
        public void Update()
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

            character.Weapon.Angle = mainMan.InputMan.AngleToPointer(X, Y);
        }


    }
}
