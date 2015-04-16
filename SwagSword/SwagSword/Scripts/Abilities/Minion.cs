using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwagSword
{
    class Minion : Ability
    {
        public Minion(Game1 mainMan, Character character):base(mainMan, Abilities.Minion, character)
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
