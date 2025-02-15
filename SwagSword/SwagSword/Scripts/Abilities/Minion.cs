﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Names: Nelson SCott

namespace SwagSword
{
    class Minion : Ability
    {
        //Fields
        private PATE minion;
        private float speed;

        public Minion(Game1 mainMan, Character character):base(mainMan, AbilityType.Minion, character)
        {
            minion = new PATE(character, mainMan);

            Init();
        }

        public override void Init()
        {
            speed = 40f;

            base.Init();
        }

        /// <summary>
        /// </summary>
        public override void Update()
        {
            if (InUse)
            {
                minion.Update();
            }
            base.Update();
        }

        /// <summary>
        /// Throw the boomerang
        /// </summary>
        public override void Use()
        {
            float velX = (float)(speed * Math.Cos((90f - character.Weapon.Angle) * Math.PI / 180f));
            float velY = (float)(speed * Math.Sin((90f - character.Weapon.Angle) * Math.PI / 180f));

            minion.Deploy(character.X + mainMan.Rnd.Next(-20, 20), character.Y + mainMan.Rnd.Next(-20, 20));

            base.Use();
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            minion.Draw(spritebatch);
        }
    }
}
