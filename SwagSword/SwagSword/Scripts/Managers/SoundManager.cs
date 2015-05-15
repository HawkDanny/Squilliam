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
        private SoundEffect gravel1;
        private SoundEffect gravel2;
        private SoundEffect gravel3;
        private SoundEffect gravel4;
        private SoundEffect minionFire;
        private SoundEffect hurt1;
        private SoundEffect hurt2;
        private SoundEffect hurt3;
        private SoundEffect swing;
        private SoundEffect slash;
        private SoundEffect clash;
        private SoundEffect capture;
        private SoundEffect warping;
        private SoundEffect losing;
        private SoundEffect decoying;
        private List<SoundEffect> hurts;
        private List<SoundEffect> gravels;
        #endregion

        #region Properties
        public Song OpeningMusic { get { return openingMusic; } set { openingMusic = value; } }
        public Song GameMusic { get { return gameMusic; } set { gameMusic = value; } }
        public SoundEffect Gravel1 { get { return gravel1; } set { gravel1 = value; } }
        public SoundEffect Gravel2 { get { return gravel2; } set { gravel2 = value; } }
        public SoundEffect Gravel3 { get { return gravel3; } set { gravel3 = value; } }
        public SoundEffect Gravel4 { get { return gravel4; } set { gravel4 = value; } }
        public SoundEffect MinionFire { get { return minionFire; } set { minionFire = value; } }
        public SoundEffect Hurt1 { get { return hurt1; } set { hurt1 = value; } }
        public SoundEffect Hurt2 { get { return hurt2; } set { hurt2 = value; } }
        public SoundEffect Hurt3 { get { return hurt3; } set { hurt3 = value; } }
        public SoundEffect Swing { get { return swing; } set { swing = value; } }
        public SoundEffect Slash { get { return slash; } set { slash = value; } }
        public SoundEffect Clash { get { return clash; } set { clash = value; } }
        public SoundEffect Capture { get { return capture; } set { capture = value; } }
        public SoundEffect Warping { get { return warping; } set { warping = value; } }
        public SoundEffect Losing { get { return losing; } set { losing = value; } }
        public SoundEffect Decoying { get { return decoying; } set { decoying = value; } }
        public List<SoundEffect> Hurts { get { return hurts; } }
        public List<SoundEffect> Gravels { get { return gravels; } }
        #endregion

        public SoundManager(Game1 mainMan):base(mainMan)
        {
            
        }

        //Init
        public override void Init()
        {
            hurts = new List<SoundEffect>();
            gravels = new List<SoundEffect>();
            hurts.Add(hurt1);
            hurts.Add(hurt2);
            hurts.Add(hurt3);
            gravels.Add(gravel1);
            gravels.Add(gravel2);
            gravels.Add(gravel3);
            gravels.Add(gravel4);
        }

        public void StartIntro()
        {
            MediaPlayer.Play(openingMusic);
            MediaPlayer.Volume = .05f;
        }

        public void StopIntro()
        {
            MediaPlayer.Stop();
            MediaPlayer.Play(gameMusic);
        }


    }
}
