#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace SwagSword
{
    //Created by Squilliam
    public class Game1 : Game
    {
        //Important
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        #region Fields
        //Managers
        private GameManager gameMan;
        private DrawManager drawMan;
        private InputManager inputMan;
        private SoundManager soundMan;
        private UIManager uiMan;

        //Helpers
        private int windowWidth;
        private int windowHeight;
        private int windowHalfWidth;
        private int windowHalfHeight;
        #endregion

        #region Properties
        //Managers
        public GameManager GameMan { get { return gameMan; } }
        public DrawManager DrawMan { get { return drawMan; } }
        public InputManager InputMan { get { return inputMan; } }
        public SoundManager SoundMan { get { return soundMan; } }
        public UIManager UIMan { get { return uiMan; } }

        //Helpers
        public int WindowWidth { get { return windowWidth; } }
        public int WindowHeight { get { return windowHeight; } }
        public int WindowHalfWidth { get { return windowHalfWidth; } }
        public int WindowHalfHeight { get { return windowHalfHeight; } }
        #endregion

        public Game1(): base()
        {
            //Peter's the best
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
        }


        protected override void Initialize()
        {
            //Init Managers
            drawMan = new DrawManager(this);
            inputMan = new InputManager(this);
            gameMan = new GameManager(this);
            soundMan = new SoundManager(this);
            uiMan = new UIManager(this);

            //Helpers
            windowWidth = GraphicsDevice.Viewport.Width;
            windowHeight = GraphicsDevice.Viewport.Height;
            windowHalfWidth = windowWidth / 2;
            windowHalfHeight = windowHeight / 2;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //Load and set textures in draw manager

            //Good Guys
            System.IO.Stream goodGuyStream = TitleContainer.OpenStream("Content/Sprites/goodGuy.png");
            drawMan.GoodGuyTextures.Add(Texture2D.FromStream(GraphicsDevice, goodGuyStream));
            goodGuyStream.Close();


            //Load Weapon textures
            drawMan.SwordTexture = this.Content.Load<Texture2D>("Objects/sword.png");

            //Load Map textures
            System.IO.Stream pathStream = TitleContainer.OpenStream("Content/Map/SamplePath.png");
            drawMan.PathwayTexture = (Texture2D.FromStream(GraphicsDevice, pathStream));
            pathStream.Close();

            System.IO.Stream notPathStream = TitleContainer.OpenStream("Content/Map/SampleNotPath.png");
            drawMan.NotPathwayTexture = (Texture2D.FromStream(GraphicsDevice, notPathStream));
            notPathStream.Close();

            System.IO.Stream dirtStream = TitleContainer.OpenStream("Content/Map/Dirt.png");
            drawMan.Dirt = Texture2D.FromStream(GraphicsDevice, dirtStream);
            dirtStream.Close();



            gameMan.SpawnMan.SpawnCharacter();
            gameMan.MapMan.Startup();

            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {

        }

        //Main Update
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Call update on all Managers that need it
            gameMan.Update();
            inputMan.Update();
            uiMan.Update();

            base.Update(gameTime);
        }

        //Draw
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.MediumPurple);

            drawMan.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
