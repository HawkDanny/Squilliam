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
        #region Fields
        //Used to access Game
        protected Game1 mainMan;

        //Character
        private Character character;
        #endregion

        #region Properties
        public Character Character { get { return character; } set { character = value; } }
        public int Health { get { return Health; } set { Health = value; } }
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
            if (mainMan.InputMan.IsKeyDown(mainMan.InputMan.Up))
            {
                character.VelocityY = -character.MovementSpeed;
            }
            if (mainMan.InputMan.IsKeyDown(mainMan.InputMan.Down))
            {
                character.VelocityY = character.MovementSpeed;
            }
            if (mainMan.InputMan.IsKeyDown(mainMan.InputMan.Right))
            {
                character.VelocityX = character.MovementSpeed;
            }
            if (mainMan.InputMan.IsKeyDown(mainMan.InputMan.Left))
            {
                character.VelocityX = -character.MovementSpeed;
            }
        }


    }
}
