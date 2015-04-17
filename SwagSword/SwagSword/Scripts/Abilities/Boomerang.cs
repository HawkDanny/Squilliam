using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Names: Nelson Scott

namespace SwagSword
{
    class Boomerang : Ability
    {
        //Fields
        private BoomerangObject boomerang;
        private float speed;

        public Boomerang(Game1 mainMan, Character character):base(mainMan, Abilities.Boomerang, character)
        {
            boomerang = new BoomerangObject(character, mainMan);

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
            boomerang.Update();
            base.Update();
        }

        /// <summary>
        /// Throw the boomerang
        /// </summary>
        public override void Use()
        {
            float velX = (float)(speed * Math.Cos((90f - character.Weapon.Angle) * Math.PI / 180f));
            float velY = (float)(speed * Math.Sin((90f - character.Weapon.Angle) * Math.PI / 180f));

            boomerang.Throw(velX, velY);

            base.Use();
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            boomerang.Draw(spritebatch);
        }
    }
}
