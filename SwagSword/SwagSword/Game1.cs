#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
//using Microsoft.Xna.Framework.GamerServices;
#endregion

//Names: Nelson Scott, Peter Lockhart, Ryan Bell

namespace SwagSword
{
    //Created by Squilliam
    public class Game1 : Game
    {
        //Important
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Random rnd;
        private GameTime gameTime;

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
        private int mapWidth;
        private int mapHeight;
        #endregion

        #region Properties
        //Managers
        public GameManager GameMan { get { return gameMan; } }
        public DrawManager DrawMan { get { return drawMan; } }
        public InputManager InputMan { get { return inputMan; } }
        public SoundManager SoundMan { get { return soundMan; } }
        public UIManager UIMan { get { return uiMan; } }

        //Helpers
        public Random Rnd { get { return rnd; } }
        public GameTime GameTime { get { return gameTime; } }
        public int WindowWidth { get { return windowWidth; } }
        public int WindowHeight { get { return windowHeight; } }
        public int WindowHalfWidth { get { return windowHalfWidth; } }
        public int WindowHalfHeight { get { return windowHalfHeight; } }
        public int MapWidth { get { return mapWidth; } set { mapWidth = value; } }
        public int MapHeight { get { return mapHeight; } set { mapHeight = value; } }
        #endregion

        public Game1(): base()
        {
            graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            //Sets window size
            graphics.PreferredBackBufferWidth = 950;
            graphics.PreferredBackBufferHeight = 534;
            graphics.ApplyChanges();


            //Init Random
            rnd = new Random();

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
            System.IO.Stream goodGuyStream = TitleContainer.OpenStream("Content/Sprites/goodGuy1.png");
            drawMan.GoodGuyTextures.Add(Texture2D.FromStream(GraphicsDevice, goodGuyStream));
            goodGuyStream.Close();

            //Load Screen Textures
            drawMan.TitleImage = this.Content.Load<Texture2D>("UIScreens/TitleScreenMock.png");
            drawMan.GameOverImage = this.Content.Load<Texture2D>("UIScreens/GameOverScreen.png");
            drawMan.PauseImage = this.Content.Load<Texture2D>("UIScreens/PauseScreen.png");

            //Load UI Textures
            drawMan.PointerTexture = this.Content.Load<Texture2D>("Objects/pointer.png");
            drawMan.ResumeTexture = this.Content.Load<Texture2D>("Buttons/ResumeButton.png");
            drawMan.StatsTexture = this.Content.Load<Texture2D>("Buttons/StatsButton.png");
            drawMan.ExitTexture = this.Content.Load<Texture2D>("Buttons/ExitButton.png");

            //Load Weapon textures
            drawMan.SwordTexture = this.Content.Load<Texture2D>("Objects/sword.png");
            drawMan.WhipTexture = this.Content.Load<Texture2D>("Objects/whip.png");

            //Load Map textures
            drawMan.PathwayTexture = Content.Load<Texture2D>("Map/SamplePath.png");
            drawMan.NotPathwayTexture = Content.Load<Texture2D>("Map/SampleNotPath.png");
            drawMan.Stronghold = Content.Load<Texture2D>("Map/Stronghold.jpg");

            //Load Fonts
            drawMan.HealthFont = Content.Load<SpriteFont>("Fonts/vanillawhale");
            gameMan.MapMan.Startup();
            drawMan.ActivateCamera();

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

            this.gameTime = gameTime;

            //Call update on all Managers that need it
            if(uiMan.State == GameState.game)
            {
                gameMan.Update();
                
            }
            inputMan.Update();
            uiMan.Update();

            drawMan.Update();
            base.Update(gameTime);
        }

        //Draw
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            drawMan.Draw(spriteBatch, gameTime);

            base.Draw(gameTime);
        }
    }
}
