﻿using System;
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
        #region Fields
        //Used to access Game
        protected Game1 mainMan;

        //Character
        private Character character;
        #endregion

        #region Properties
        public Character Character { get { return character; } set { character = value; } }
        #endregion

        public Player(Character character, Game1 mainMan)
        {
            this.character = character;
            this.mainMan = mainMan;
        }

        //The main update for player, character's update is not called
        public void Update()
        {
            //Movement
            if (mainMan.InputMan.Up.IsDown())
            {
                character.VelocityY = -character.MovementSpeed;
            }
            if (mainMan.InputMan.Down.IsDown())
            {
                character.VelocityY = character.MovementSpeed;
            }
            if (mainMan.InputMan.Right.IsDown())
            {
                character.VelocityX = character.MovementSpeed;
            }
            if (mainMan.InputMan.Left.IsDown())
            {
                character.VelocityX = -character.MovementSpeed;
            }
        }


    }
}
