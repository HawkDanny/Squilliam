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

        //Weapon Textures (Create a dictionary)
        private Texture2D swordTexture;

        //Map Textures
        private Texture2D pathwayTexture;
        private Texture2D notPathwayTexture;
        private Texture2D dirt;
        #endregion

        #region Properties
        //Character texture lists
        public List<Texture2D> GoodGuyTextures { get { return goodGuyTextures; } }

        //Weapon textures
        public Texture2D SwordTexture { get { return swordTexture; } set { swordTexture = value; } }

        //Map textures
        public Texture2D PathwayTexture { get { return pathwayTexture; } set { pathwayTexture = value; } }
        public Texture2D NotPathwayTexture { get { return notPathwayTexture; } set { notPathwayTexture = value; } }
        public Texture2D Dirt { get { return dirt; } set { dirt = value; } }
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
            mainMan.GameMan.MapMan.Draw(spritebatch);
            //Draw all characters
            foreach (Character character in mainMan.GameMan.Characters)
            {
                character.Draw(spritebatch);
            }

            mainMan.UIMan.Screens.Peek().Draw(spritebatch);

            spritebatch.End();
        }
    }
}
