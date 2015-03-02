using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SwagSword
{
    /// <summary>
    /// Draw Manager should call draw on all game objects, and holds textures
    /// </summary>
    public class DrawManager:Manager
    {
        #region Fields
        //Character texture lists
        private List<Texture2D> goodGuyTextures;

        #endregion

        #region Properties
        //Character texture lists
        public List<Texture2D> GoodGuyTextures { get { return goodGuyTextures; } }

        #endregion


        public DrawManager(Game1 mainMan):base(mainMan)
        {

        }

        //Init
        public override void Init()
        {
            //Init Texture lists
            goodGuyTextures = new List<Texture2D>();
        }


        /// <summary>
        /// Used to call draw on all objects and determine draw order
        /// </summary>
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();

            //Draw all characters
            foreach (Character character in mainMan.GameMan.Characters)
            {
                character.Draw(spritebatch);
            }

            spritebatch.End();
        }
    }
}
