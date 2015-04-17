using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Names: Peter Lockhart

namespace SwagSword
{
    class PauseScreen : UIScreen
    {
        //Fields
        private Button resume;
        private Button stats;
        private Button exit;
        private UIScreen gameScreen;

        //Properties
        public Button Resume { get { return resume; } }
        public Button Stats { get { return stats; } }
        public Button Exit { get { return exit; } }
        public UIScreen GameScreen { get { return gameScreen; } }

        public PauseScreen(Game1 mainMan, UIScreen gameScreen):
            base(mainMan)
        {
            //Setting up the buttons
            resume = new Button(mainMan,
                                mainMan.DrawMan.ResumeTexture,
                                new Rectangle((int)mainMan.DrawMan.Camera.TopLeftPosition.X + (mainMan.WindowWidth / 2) - (mainMan.DrawMan.ResumeTexture.Width/2),
                                              (int)mainMan.DrawMan.Camera.TopLeftPosition.Y + (mainMan.WindowHeight / 2) - 100,
                                              mainMan.DrawMan.ResumeTexture.Width,
                                              50),
                                gameScreen,
                                GameState.game
                                );
            stats = new Button(mainMan,
                               mainMan.DrawMan.StatsTexture,
                               new Rectangle((int)mainMan.DrawMan.Camera.TopLeftPosition.X + (mainMan.WindowWidth / 2) - (mainMan.DrawMan.StatsTexture.Width/2),
                                              (int)mainMan.DrawMan.Camera.TopLeftPosition.Y + (mainMan.WindowHeight / 2) - 25,
                                              mainMan.DrawMan.StatsTexture.Width,
                                              50),
                               new StatScreen(mainMan),
                               GameState.pause
                               );
            exit = new Button(mainMan,
                              mainMan.DrawMan.ExitTexture,
                               new Rectangle((int)mainMan.DrawMan.Camera.TopLeftPosition.X + (mainMan.WindowWidth / 2) - (mainMan.DrawMan.ExitTexture.Width/2),
                                              (int)mainMan.DrawMan.Camera.TopLeftPosition.Y + (mainMan.WindowHeight / 2) + 50,
                                              mainMan.DrawMan.ExitTexture.Width,
                                              50),
                               gameScreen,
                               GameState.exit
                               );
        }

        public override void Update()
        {
            resume.Update();
            stats.Update();
            exit.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mainMan.DrawMan.PauseImage, 
                             new Rectangle((int)mainMan.DrawMan.Camera.TopLeftPosition.X, 
                                           (int)mainMan.DrawMan.Camera.TopLeftPosition.Y,
                                           mainMan.WindowWidth,
                                           mainMan.WindowHeight), 
                             Color.White);
            resume.Draw(spriteBatch);
            stats.Draw(spriteBatch);
            exit.Draw(spriteBatch);
        }
    }
}
