using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SwagSword
{
    /// <summary>
    /// Input Manager figures out the input, but shouldn't change anything using it
    /// </summary>
    public class InputManager:Manager
    {
        //Fields

        public InputManager(Game1 mainMan):base(mainMan)
        {

        }

        //Init
        public override void Init()
        {
            
        }

        /// <summary>
        /// Main update method, called by mainMan
        /// </summary>
        public override void Update()
        {

        }
    }
}
