using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SwagSword
{
    /// <summary>
    /// Game Manager should handle main game update logic, and hold all main game vars
    /// </summary>
    public class GameManager:Manager
    {
        #region Fields
        //Managers
        private MapManager mapMan;
        private SpawnManager spawnMan;

        //Characters
        private List<Character> characters;
        private List<Player> players;
        #endregion


        #region Properties
        //Managers
        public MapManager MapMan { get { return mapMan; } }
        public SpawnManager SpawnMan { get { return spawnMan; } }

        //Characters
        public List<Character> Characters { get {return characters; } }
        public List<Player> Players { get { return players; } }
        #endregion

        public GameManager(Game1 mainMan):base(mainMan)
        {

        }

        //Init
        public override void Init()
        {
            //Managers
            mapMan = new MapManager(mainMan);
            spawnMan = new SpawnManager(mainMan);

            //Characters
            characters = new List<Character>();
            players = new List<Player>();
        }

        /// <summary>
        /// Main update method, called by mainMan
        /// </summary>
        public override void Update()
        {
            //Call update on all characters
            foreach (Character character in characters)
            {
                if (!character.IsControlled)
                {
                    character.Update();
                }
            }

            //Call update on all players
            foreach (Player player in players)
            {
                player.Update();
            }
        }


    }
}
