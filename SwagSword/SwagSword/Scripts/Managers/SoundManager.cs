#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
//using Microsoft.Xna.Framework.GamerServices;
#endregion

//Names: Nelson Scott

namespace SwagSword
{
    /// <summary>
    /// A place to play any sound effect
    /// </summary>
    public class SoundManager:Manager
    {
        
        #region Fields
        //Will hold sounds in a list
        private Song openingMusic;
        private Song gameMusic;
        //TODO: phantom menace song
        #endregion

        #region Properties
        public Song OpeningMusic { get { return openingMusic; } set { openingMusic = value; } }
        public Song GameMusic { get { return gameMusic; } set { gameMusic = value; } }
        #endregion

        public SoundManager(Game1 mainMan):base(mainMan)
        {

        }

        //Init
        public override void Init()
        {
            
        }

        public void StartIntro()
        {
            MediaPlayer.Play(openingMusic);
        }

        public void StopIntro()
        {
            MediaPlayer.Stop();
        }


    }
}
