using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

//Names: Nelson Scott

namespace SwagSword
{
    public enum Abilities
    {
        Boomerang,
        Warp,
        Decoy,
        Minion
    }

    public class Ability
    {
        #region Fields
        protected Game1 mainMan;
        private Abilities type;
        protected Character character;

       
        private bool inUse; //Each sub class should handle setting this to false 
        #endregion

        #region Properties
        public Abilities Type { get { return type; } }

        public bool InUse { get { return inUse; } set { inUse = value; } }
        #endregion


        public Ability(Game1 mainMan, Abilities type, Character character)
        {
            this.mainMan = mainMan;
            this.type = type;
            this.character = character;
        }

        public virtual void Init()
        {

        }


        /// <summary>
        /// Main update for abilities
        /// </summary>
        public virtual void Update()
        {

        }

        /// <summary>
        /// Called when using the ability...
        /// </summary>
        public virtual void Use()
        {
            inUse = true;
        }

        /// <summary>
        /// This is for if the AI uses the ability different than the player
        /// </summary>
        public virtual void AIUse()
        {
            inUse = true;
        }
    }
}
