using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwagSword
{
    class Boomerang : Ability
    {
        //Fields
        private BoomerangObject boomerang;

        public Boomerang(Game1 mainMan, Character character):base(mainMan, Abilities.Boomerang, character)
        {

        }

        /// <summary>
        /// </summary>
        public override void Update()
        {

            base.Update();
        }

        /// <summary>
        /// Throw the boomerang
        /// </summary>
        public override void Use()
        {
            character.VelocityX = (float)(speed * Math.Cos((90f - character.Direction) * Math.PI / 180f));
            character.VelocityY = (float)(speed * Math.Sin((90f - character.Direction) * Math.PI / 180f));

            base.Use();
        }
    }
}
