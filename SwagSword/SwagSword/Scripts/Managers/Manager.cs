using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

//Names: Nelson Scott

namespace SwagSword
{
    /// <summary>
    /// This is the base Manager class, holds a reference to the game
    /// </summary>
    public class Manager
    {
        //Used to access Game
        protected Game1 mainMan;

        public Manager(Game1 mainMan)
        {
            this.mainMan = mainMan;

            Init();
        }

        //Init
        public virtual void Init()
        {
            
        }

        /// <summary>
        /// Main update method, called by mainMan
        /// </summary>
        public virtual void Update()
        {

        }
    }
}
