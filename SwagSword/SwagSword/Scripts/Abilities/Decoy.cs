using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwagSword
{
    class Decoy : Ability
    {
        public Decoy(Game1 mainMan, Character character):base(mainMan, Abilities.Decoy, character)
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
