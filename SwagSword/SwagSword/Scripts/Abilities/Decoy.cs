using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;

//Names: Nelson Scott

namespace SwagSword
{
    class Decoy : Ability
    {
        //Fields

        Texture2D previousTexture;

        //Properties
        public Rectangle HitBox { get { return new Rectangle((int)position.X - character.Rectangle.Width / 2, (int)position.Y - character.Rectangle.Height / 2, character.Rectangle.Width, character.Rectangle.Height); } }


        public Decoy(Game1 mainMan, Character character):base(mainMan, AbilityType.Decoy, character)
        {
            previousTexture = character.Texture;
            Init();
        }

        public override void Init()
        {
            base.Init();
        }

        double temp = 0;
        /// <summary>
        /// </summary>
        public override void Update()
        {
            if (InUse)
            {
                if(fresh)
                {
                    temp = mainMan.GameTime.TotalGameTime.TotalSeconds;
                    fresh = false;
                }
                     
                character.Texture = mainMan.DrawMan.GhostSheet;
                if (character.CharacterState != CharacterState.Active || character.Weapon.IsSwinging)
                {
                    InUse = false;
                    character.Texture = previousTexture;
                }
                if(mainMan.GameTime.TotalGameTime.TotalSeconds - temp > 3)
                {
                    InUse = false;
                    character.Texture = previousTexture;
                    fresh = true;
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
            mainMan.SoundMan.Decoying.Play();
            position = new Vector2(character.X-32, character.Y-32);

            base.Use();
        }

        public override void AIUse()
        {
            position = new Vector2(character.X-32, character.Y-32);
            base.AIUse();
            mainMan.SoundMan.Decoying.Play();
        }

        private void TimeTracker()
        {
            timeInvisible++;
        }

        Thread t;
        int timeInvisible = 0;
        bool fresh = true;

        public override void Draw(SpriteBatch spritebatch)
        {
            if (InUse)
            {
                spritebatch.Draw(mainMan.DrawMan.SpriteDict[character.Type], position, Color.White);
                //spritebatch.Draw(character.Texture, position, character.Rectangle, Color.White, 0f, character.Center, 1.0f, character.SpriteEffect, 1);
            }
        }
    }
}
