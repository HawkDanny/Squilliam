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

        public Warp(Game1 mainMan, Character character):base(mainMan, Abilities.Warp, character)
        {
            Init();
        }

        public override void Init()
        {
            speed = 150f;

            coolDownMax = 0.700f;
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

    }
}
