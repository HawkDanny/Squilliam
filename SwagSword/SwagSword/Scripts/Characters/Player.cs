using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SwagSword
{
    /// <summary>
    /// Holds the character being controlled, will process input from input manager
    /// </summary>
    public class Player
    {
        //Fields
        private Character character;

        //Properties
        public Character Character { get { return character; } set { character = value; } }

        public  Player()
        {

        }
    }
}
