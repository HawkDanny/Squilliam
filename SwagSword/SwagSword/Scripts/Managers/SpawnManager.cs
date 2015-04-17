using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Names: Nelson Scott

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

        /// <summary>
        /// Spawns a character given the faction, should decide position and random texture
        /// </summary>
        /// <param name="type"></param>
        public void SpawnCharacter(Faction type)
        {
            Character character = null;

            switch (type)
            {
                case Faction.Good:
                    character = new GoodCharacter(mainMan.WindowHalfWidth, mainMan.WindowHalfHeight, mainMan.DrawMan.GoodGuyTextures[mainMan.Rnd.Next(0, mainMan.DrawMan.GoodGuyTextures.Count)], mainMan);
                    
                    break;

                case Faction.Tribal:
                    character = new TribalCharacter(mainMan.Rnd.Next(0, mainMan.MapWidth), mainMan.Rnd.Next(0, mainMan.MapHeight), mainMan.DrawMan.GoodGuyTextures[0], mainMan);
                    break;

                case Faction.Rich:
                    character = new RichCharacter(mainMan.Rnd.Next(0, mainMan.MapWidth), mainMan.Rnd.Next(0, mainMan.MapHeight), mainMan.DrawMan.GoodGuyTextures[0], mainMan);
                    Player player = new Player(character, mainMan);
                    mainMan.GameMan.Players.Add(player);
                    break;

                case Faction.Thief:
                    character = new ThiefCharacter(mainMan.Rnd.Next(0, mainMan.MapWidth), mainMan.Rnd.Next(0, mainMan.MapHeight), mainMan.DrawMan.GoodGuyTextures[0], mainMan);
                    break;
            }

            mainMan.GameMan.Characters.Add(character);
        }


    }
}
