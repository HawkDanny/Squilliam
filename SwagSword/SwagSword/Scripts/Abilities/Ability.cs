﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Names: Nelson Scott

namespace SwagSword
{
    public enum AbilityType
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
        private AbilityType type;
        protected Character character;
        protected Vector2 position;

       
        private bool inUse; //Each sub class should handle setting this to false 
        #endregion

        #region Properties
        public AbilityType Type { get { return type; } set { type = value; } }

        public bool InUse { get { return inUse; } set { inUse = value; } }

        public Vector2 Position { get { return position; } }
        #endregion


        public Ability(Game1 mainMan, AbilityType type, Character character)
        {
            this.mainMan = mainMan;
            this.type = type;
            this.character = character;
            position = new Vector2(0f, 0f);
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

        /// <summary>
        /// For abilities that need to be drawn
        /// </summary>
        /// <param name="spritebatch"></param>
        public virtual void Draw(SpriteBatch spritebatch)
        {

        }
    }
}
