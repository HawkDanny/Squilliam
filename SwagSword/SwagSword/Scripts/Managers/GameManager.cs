using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

//Names: Nelson Scott

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
        private Dictionary<Faction, List<Character>> charactersDictionary; //Coming soon
        #endregion


        #region Properties
        //Managers
        public MapManager MapMan { get { return mapMan; } }
        public SpawnManager SpawnMan { get { return spawnMan; } }

        //Characters
        public List<Character> Characters { get {return characters; } }
        public List<Player> Players { get { return players; } }
        public Dictionary<Faction, List<Character>> CharactersDictionary { get { return charactersDictionary; } }
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
            charactersDictionary = new Dictionary<Faction, List<Character>>();
            charactersDictionary.Add(Faction.Good, new List<Character>());
            charactersDictionary.Add(Faction.Rich, new List<Character>());
            charactersDictionary.Add(Faction.Thief, new List<Character>());
            charactersDictionary.Add(Faction.Tribal, new List<Character>());
            players = new List<Player>();
        }

        /// <summary>
        /// Main update method, called by mainMan
        /// </summary>
        public override void Update()
        {
            //Call update on all characters
            for (int i = characters.Count - 1; i >= 0; i--)
            {
                characters[i].Update();
            }

            //Call update on all players
            foreach (Player player in players)
            {
                player.Update();
            }
        }

        /// <summary>
        /// Just a plain start game method
        /// </summary>
        public void StartGame()
        {
            //Spawn some random characters
            SpawnMan.SpawnCharacter(Faction.Good);
            for (int i = 0; i < 30; i++)
            {
                SpawnMan.SpawnCharacter(Faction.Tribal);
            }
        }

    }
}
