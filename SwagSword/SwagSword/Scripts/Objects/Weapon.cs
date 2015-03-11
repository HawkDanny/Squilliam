using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwagSword.Scripts.Objects
{
    //Handles collision with enemies, swinging
    class Weapon
    {
        #region Fields
        //References to Character and game
        private Character character;
        private Faction type;

        //Swinging vars
        private bool isSwinging;
        #endregion

        #region Properties

        #endregion

        public Weapon(Character character)
        {
            this.character = character;
            Init();
        }

        void Init()
        {
            //Init Vars
            type = character.Type;
            isSwinging = false;

            //Set Texture
            SetTexture(character.IsControlled, type);
        }

        public void Update()
        {
            //Check for collisions if swinging
            //Lerp weapon between start and target angle, set isSwinging to false when done
            //Use the character's weapon speed to lerp
        }

        /// <summary>
        /// Starts the sword swing and enables collision detection
        /// </summary>
        public void Swing()
        {
            isSwinging = true;
            //Set init angle and target angle
        }

        /// <summary>
        /// Sets the proper texture for the weapon
        /// </summary>
        /// <param name="isControlled"></param>
        /// <param name="type"></param>
        public void SetTexture(bool isControlled, Faction type)
        {
            if (isControlled)
            {
                //Sets the texture to the main sword

            }
            else
            {
                //Sets the texture to the faction's default
                switch (type)
                {
                    default:

                        break;
                }
            }
        }
    }
}
