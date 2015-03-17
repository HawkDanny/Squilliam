using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SwagSword
{
    /// <summary>
    /// Used by game manager to spawn characters
    /// </summary>
    public class SpawnManager:Manager
    {

        public SpawnManager(Game1 mainMan):base(mainMan)
        {

        }

        //Init
        public override void Init()
        {

        }

        /// <summary>
        /// Main update method, called by mainMan
        /// </summary>
        public override void Update()
        {

        }

        public void SpawnCharacter()
        {
            //Spawn a dummy character
            GoodCharacter character = new GoodCharacter(mainMan.WindowHalfWidth, mainMan.WindowHalfHeight, mainMan.DrawMan.GoodGuyTextures[mainMan.Rnd.Next(0, mainMan.DrawMan.GoodGuyTextures.Count)], mainMan);
            mainMan.GameMan.Characters.Add(character);
            Player player = new Player(character, mainMan);
            mainMan.GameMan.Players.Add(player);
        }
    }
}
