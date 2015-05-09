#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System.Threading;
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
        Thread t;
        bool freshSet = true;
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
            //graphics.IsFullScreen = true;
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


            //Helpers
            windowWidth = GraphicsDevice.Viewport.Width;
            windowHeight = GraphicsDevice.Viewport.Height;
            windowHalfWidth = windowWidth / 2;
            windowHalfHeight = windowHeight / 2;


            drawMan.ActivateCamera();
            uiMan = new UIManager(this);

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
            drawMan.SwordStatScreen = this.Content.Load<Texture2D>("UIScreens/SwordStatScreen.png");
            drawMan.LoadingScreen = this.Content.Load<Texture2D>("UIScreens/LoadingScreen.jpg");

            //Load UI Textures
            drawMan.PointerTexture = this.Content.Load<Texture2D>("Objects/pointer.png");
            drawMan.ResumeTexture = this.Content.Load<Texture2D>("Buttons/ResumeButton.png");
            drawMan.StatsTexture = this.Content.Load<Texture2D>("Buttons/StatsButton.png");
            drawMan.ExitTexture = this.Content.Load<Texture2D>("Buttons/ExitButton.png");
            drawMan.WinTexture = this.Content.Load<Texture2D>("UIScreens/ryanbell.jpg");

            //Load Weapon textures
            drawMan.SwordTexture = this.Content.Load<Texture2D>("Objects/sword.png");
            drawMan.WhipTexture = this.Content.Load<Texture2D>("Objects/whip.png");
            drawMan.PateTexture = this.Content.Load<Texture2D>("Objects/PAT-E.png");

            //Load Map textures
            drawMan.PathwayTexture = Content.Load<Texture2D>("Map/SamplePath3.png");
            drawMan.SandyTexture = Content.Load<Texture2D>("Map/SampleNotPath.png");
            drawMan.GrassTexture = Content.Load<Texture2D>("Map/Grass.jpg");
            drawMan.LeftStronghold = Content.Load<Texture2D>("Map/LeftStronghold.jpg");
            drawMan.RightStronghold = Content.Load<Texture2D>("Map/RightStronghold.jpg");
            drawMan.TopStronghold = Content.Load<Texture2D>("Map/TopStronghold.jpg");
            drawMan.LowerStronghold = Content.Load<Texture2D>("Map/LowerStronghold.jpg");

            //Load Fonts
            drawMan.HealthFont = Content.Load<SpriteFont>("Fonts/vanillawhale");
            drawMan.StatFont = Content.Load<SpriteFont>("Fonts/pressstart2p");
            t = new Thread(new ThreadStart(gameMan.MapMan.Startup));
            t.Start();
            //gameMan.MapMan.Startup();

            //Load Front Sprites into Dictionary
            drawMan.SpriteDict.Add(Faction.Good, Content.Load<Texture2D>("Sprites/goodGuy.png"));
            drawMan.SpriteDict.Add(Faction.Rich, Content.Load<Texture2D>("Sprites/RichGuy.png"));
            drawMan.SpriteDict.Add(Faction.Thief, Content.Load<Texture2D>("Sprites/BanditGuy.png"));
            drawMan.SpriteDict.Add(Faction.Tribal, Content.Load<Texture2D>("Sprites/TribalGuy.png"));

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
            if (t.ThreadState == ThreadState.Running)
            {
                uiMan.State = GameState.loading;
            }
            if(t.ThreadState == ThreadState.Stopped && freshSet)
            {
                uiMan.State = GameState.title;
                freshSet = false;
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
