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

        //Screens Texture
        private Texture2D titleImage;
        private Texture2D gameOverImage;

        //Weapon Textures (Create a dictionary)
        private Texture2D swordTexture;

        //Map Textures
        private Texture2D pathwayTexture;
        private Texture2D notPathwayTexture;
        private Texture2D dirt;

        //Fonts
        private SpriteFont healthFont;
        #endregion

        #region Properties
        //Character texture lists
        public List<Texture2D> GoodGuyTextures { get { return goodGuyTextures; } }

        //Screen Textures
        public Texture2D TitleImage { get { return titleImage; } set { titleImage = value; } }
        public Texture2D GameOverImage { get { return gameOverImage; } set { gameOverImage = value; } }
        //Weapon textures
        public Texture2D SwordTexture { get { return swordTexture; } set { swordTexture = value; } }

        //Map textures
        public Texture2D PathwayTexture { get { return pathwayTexture; } set { pathwayTexture = value; } }
        public Texture2D NotPathwayTexture { get { return notPathwayTexture; } set { notPathwayTexture = value; } }
        public Texture2D Dirt { get { return dirt; } set { dirt = value; } }

        //SpriteFonts
        public SpriteFont HealthFont { get { return healthFont; } set { healthFont = value; } }
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
            //The game states will determine what is being drawn for now.
            if(mainMan.UIMan.State == GameState.title)
            {
                //For the beginning of the game with the title screen.
            }
            
            if(mainMan.UIMan.State == GameState.game)
            {
                mainMan.GameMan.MapMan.Draw(spritebatch);
                //Draw all characters
                foreach (Character character in mainMan.GameMan.Characters)
                {
                    character.Draw(spritebatch);
                }

            }
            if(mainMan.UIMan.State == GameState.gameOver)
            {
                //For the game over/when character dies.
            }
            mainMan.UIMan.Screens.Peek().Draw(spritebatch);

            spritebatch.End();
        }
    }
}
