using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Names: Nelson Scott

namespace SwagSword
{
    class Decoy : Ability
    {
        //Fields
        Vector2 position;

        public Decoy(Game1 mainMan, Character character):base(mainMan, Abilities.Decoy, character)
        {
            Init();
        }

        public override void Init()
        {
            base.Init();
        }

        /// <summary>
        /// </summary>
        public override void Update()
        {
            if (InUse)
            {
                if (character.CharacterState != CharacterState.Active || character.Weapon.IsSwinging)
                {
                    InUse = false;
                }
            }
            base.Update();
        }

        /// <summary>
        /// Throw the boomerang
        /// </summary>
        public override void Use()
        {
            position = new Vector2(character.X, character.Y);

            base.Use();
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            if (InUse)
            {
                spritebatch.Draw(character.Texture, position, character.Rectangle, Color.White, 0f, character.Center, 1.0f, character.SpriteEffect, 1);
            }
        }
    }
}
