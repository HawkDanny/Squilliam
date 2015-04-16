using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwagSword
{
    class Boomerang : Ability
    {
        public Boomerang(Game1 mainMan, Character character):base(mainMan, Abilities.Boomerang, character)
        {

        }

        /// <summary>
        /// </summary>
        public override void Update()
        {

            base.Update();
        }
    }
}
