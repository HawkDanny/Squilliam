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
        private int NUM_CHARACTERS;

        //Boundaries
        private Rectangle leftPathBound;
        private Rectangle rightPathBound;
        private Rectangle topPathBound;
        private Rectangle lowerPathBound;
        private Rectangle centerBound;

        //Strongholds
        private Rectangle leftStrong;
        private Rectangle rightStrong;
        private Rectangle topStrong;
        private Rectangle lowerStrong;

        #endregion


        #region Properties
        //Managers
        public MapManager MapMan { get { return mapMan; } }
        public SpawnManager SpawnMan { get { return spawnMan; } }

        //Characters
        public List<Character> Characters { get {return characters; } }
        public List<Player> Players { get { return players; } }
        public Dictionary<Faction, List<Character>> CharactersDictionary { get { return charactersDictionary; } }
        public int NumCharacters { get { return NUM_CHARACTERS; } }

        //Boundaries
        public Rectangle LeftPathBound { get { return leftPathBound; } set { leftPathBound = value; } }
        public Rectangle RightPathBound { get { return rightPathBound; } set { rightPathBound = value; } }
        public Rectangle TopPathBound { get { return topPathBound; } set { topPathBound = value; } }
        public Rectangle LowerPathBound { get { return lowerPathBound; } set { lowerPathBound = value; } }
        public Rectangle CenterBound { get { return centerBound; } set { centerBound = value; } }

        //Strongholds
        public Rectangle LeftStrong { get { return leftStrong; } set { leftStrong = value; } }
        public Rectangle RightStrong { get { return rightStrong; } set { rightStrong = value; } }
        public Rectangle TopStrong { get { return topStrong; } set { topStrong = value; } }
        public Rectangle LowerStrong { get { return lowerStrong; } set { lowerStrong = value; } }
        #endregion

        public GameManager(Game1 mainMan):base(mainMan)
        {

        }

        //Init
        public override void Init()
        {
            NUM_CHARACTERS = 8;
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
            spawnMan.Update();
        }

        /// <summary>
        /// Just a plain start game method
        /// </summary>
        public void StartGame()
        {
            //Spawn some random characters
            SpawnMan.SpawnPlayer(Faction.Good);
            for (int i = 0; i < NUM_CHARACTERS; i++)
            {
                charactersDictionary[Faction.Good].Add(SpawnMan.SpawnCharacter(Faction.Good));
                charactersDictionary[Faction.Tribal].Add(SpawnMan.SpawnCharacter(Faction.Tribal));
                charactersDictionary[Faction.Thief].Add(SpawnMan.SpawnCharacter(Faction.Thief));
                charactersDictionary[Faction.Rich].Add(SpawnMan.SpawnCharacter(Faction.Rich));
            }

            mainMan.DrawMan.Camera.SnapToCenter();
            //mainMan.SoundMan.StopIntro();
        }

    }
}
