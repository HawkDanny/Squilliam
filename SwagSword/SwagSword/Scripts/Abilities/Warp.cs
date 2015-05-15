using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

//Names: Nelson Scott

namespace SwagSword
{
    public class Warp : Ability
    {
        #region Fields
        private float speed;

        private float coolDownTimer;
        private float coolDownMax;
        #endregion



        public Warp(Game1 mainMan, Character character):base(mainMan, AbilityType.Warp, character)
        {
            Init();
        }

        public override void Init()
        {
            speed = 150f;

            coolDownMax = 0.600f;
            coolDownTimer = 0f;

            base.Init();
        }

        /// <summary>
        /// Handles the cooldown timer
        /// </summary>
        public override void Update()
        {
            if (InUse)
            {
                coolDownTimer -= mainMan.GameTime.ElapsedGameTime.Milliseconds * 0.001f;

                if (coolDownTimer <= 0)
                {
                    InUse = false;
                }
            }

            base.Update();
        }


        public override void Use()
        {
            character.VelocityX = (float)(speed * Math.Cos((90f - character.Direction) * Math.PI / 180f));
            character.VelocityY = (float)(speed * Math.Sin((90f - character.Direction) * Math.PI / 180f));
            
            coolDownTimer = coolDownMax;

            base.Use();
        }

        

        /// <summary>
        /// Used for the AI to warp to the player's position
        /// </summary>
        /// <param name="target"></param>
        public override void AIUse()
        {
            if (!InUse)
            {
                character.SetDirectionToPoint(mainMan.GameMan.Players[0].X, mainMan.GameMan.Players[0].Y);
                character.AnimateFaceDirection();

                

                if (character.DistanceToPlayer(0) < 120f)
                {
                    //character is too close, so warp away
                    character.VelocityX = (float)(70f * Math.Cos((90f - character.Direction - 180f) * Math.PI / 180f));
                    character.VelocityY = (float)(70f * Math.Sin((90f - character.Direction - 180f) * Math.PI / 180f));
                }
                else
                {
                    float targetX = mainMan.GameMan.Players[0].X + (float)(60f * Math.Cos((90f - character.Direction - 180f) * Math.PI / 180f));
                    float targetY = mainMan.GameMan.Players[0].Y + (float)(60f * Math.Sin((90f - character.Direction - 180f) * Math.PI / 180f));
                    character.X = targetX;
                    character.Y = targetY;
                }


                coolDownTimer = coolDownMax;

                base.AIUse();
            }
        }
    }
}
