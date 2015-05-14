﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

//Names: Nelson Scott, Ryan Bell, Danny Hawk

namespace SwagSword
{
    /// <summary>
    /// Used by game manager to spawn characters
    /// </summary>
    public class SpawnManager:Manager
    {
        #region Fields
        int lx, ly, lw, lh, rx, ry, rw, rh, tx, ty, tw, th, bx, by, bw, bh;
        int goodNum, tribalNum, richNum, thiefNum;
        int respawnTime;

        List<CustomCharacter> customCharacterList;
        #endregion

        public SpawnManager(Game1 mainMan):base(mainMan)
        {

        }

        //Init
        public override void Init()
        {
            customCharacterList = new List<CustomCharacter>();
        }

        public void Start()
        {
            lx = mainMan.GameMan.LeftPathBound.X;
            ly = mainMan.GameMan.LeftPathBound.Y;
            lw = mainMan.GameMan.LeftPathBound.Width;
            lh = mainMan.GameMan.LeftPathBound.Height;
            rx = mainMan.GameMan.RightPathBound.X;
            ry = mainMan.GameMan.RightPathBound.Y;
            rw = mainMan.GameMan.RightPathBound.Width;
            rh = mainMan.GameMan.RightPathBound.Height;
            tx = mainMan.GameMan.TopPathBound.X;
            ty = mainMan.GameMan.TopPathBound.Y;
            tw = mainMan.GameMan.TopPathBound.Width;
            th = mainMan.GameMan.TopPathBound.Height;
            bx = mainMan.GameMan.LowerPathBound.X;
            by = mainMan.GameMan.LowerPathBound.Y;
            bw = mainMan.GameMan.LowerPathBound.Width;
            bh = mainMan.GameMan.LowerPathBound.Height;
            tribalNum = 30;
            goodNum = 30;
            thiefNum = 30;
            richNum = 30;
            respawnTime = 5;



            //Loading in the Custom Characters
            String[] fileNames = Directory.GetFiles("../../../Content/CustomEnemies");

            StreamReader input;
            for (int i = 0; i < fileNames.Length; i++)
            {
                input = new StreamReader(fileNames[i]);

                string customCharacterString = input.ReadLine();
                CustomCharacter dummy = JsonConvert.DeserializeObject<CustomCharacter>(customCharacterString);
                customCharacterList.Add(dummy);
            }
        }

        /// <summary>
        /// Main update method, called by mainMan
        /// </summary>
        public override void Update()
        {
            /*
            Character repChar;
            foreach (Faction type in Enum.GetValues(typeof(Faction)))
                if(type != Faction.Neutral)
                for (int i = 0; i < mainMan.GameMan.NumCharacters - mainMan.GameMan.CharactersDictionary[type].Count; i++)
                    if ((repChar = ReplenishCharacter(type)) != null)
                        mainMan.GameMan.CharactersDictionary[type].Add(repChar);
             * 
             */
        }

        public void ReplenishCharacter(Faction type)
        {
            Character character = null;
            //double initialTime = mainMan.GameTime.TotalGameTime.TotalSeconds;
            //double elapsed = 0;
            //while(elapsed < respawnTime)
            //{
            //    elapsed = (mainMan.GameTime.TotalGameTime.TotalSeconds - initialTime);
            //}

            switch (type)
            {
                case Faction.Good:
                    if (goodNum > 0)
                    {
                        character = new GoodCharacter(mainMan.GameMan.LeftStrong.Center.X, mainMan.GameMan.LeftStrong.Center.Y, mainMan.DrawMan.CharacterTextures[Faction.Good], mainMan);
                        goodNum--;
                    }
                    break;

                case Faction.Tribal:
                    if (tribalNum > 0)
                    {
                        character = new TribalCharacter(mainMan.GameMan.RightStrong.Center.X, mainMan.GameMan.RightStrong.Center.Y, mainMan.DrawMan.CharacterTextures[Faction.Tribal], mainMan);
                        tribalNum--;
                    }
                    break;

                case Faction.Rich:
                    if (richNum > 0)
                    {
                        character = new RichCharacter(mainMan.GameMan.TopStrong.Center.X, mainMan.GameMan.TopStrong.Center.Y, mainMan.DrawMan.CharacterTextures[Faction.Rich], mainMan);
                        richNum--;
                    }
                    break;

                case Faction.Thief:
                    if (thiefNum > 0)
                    {
                        character = new ThiefCharacter(mainMan.GameMan.LowerStrong.Center.X, mainMan.GameMan.LowerStrong.Center.Y, mainMan.DrawMan.CharacterTextures[Faction.Thief], mainMan);
                        thiefNum--;
                    }
                    break;
            }
            if (character != null)
            {
                mainMan.GameMan.Characters.Add(character);
                mainMan.GameMan.CharactersDictionary[type].Add(character);
            }
            
        }

        /// <summary>
        /// Spawns a character given the faction, should decide position and random texture
        /// </summary>
        /// <param name="type"></param>
        public Character SpawnCharacter(Faction type)
        {
            Character character = null;

            switch (type)
            {
                case Faction.Good:
                    character = new GoodCharacter(mainMan.Rnd.Next(lx, lx + lw), mainMan.Rnd.Next(ly, ly + lh), mainMan.DrawMan.CharacterTextures[Faction.Good], mainMan);
                    break;

                case Faction.Tribal:
                    character = new TribalCharacter(mainMan.Rnd.Next(rx, rx + rw), mainMan.Rnd.Next(ry, ry + rh), mainMan.DrawMan.CharacterTextures[Faction.Tribal], mainMan);
                    break;

                case Faction.Rich:
                    character = new RichCharacter(mainMan.Rnd.Next(tx, tx + tw), mainMan.Rnd.Next(ty, ty + th), mainMan.DrawMan.CharacterTextures[Faction.Rich], mainMan);
                    break;

                case Faction.Thief:
                    character = new ThiefCharacter(mainMan.Rnd.Next(bx, bx + bw), mainMan.Rnd.Next(by, by + bh), mainMan.DrawMan.CharacterTextures[Faction.Thief], mainMan);
                    break;
            }

            mainMan.GameMan.Characters.Add(character);
            return character;
        }

        public void SpawnPlayer(Faction type)
        {
            Character character = null;
            switch (type)
            {
                case Faction.Good:
                    character = new GoodCharacter(mainMan.Rnd.Next(lx, lx + lw), mainMan.Rnd.Next(ly, ly + lh), mainMan.DrawMan.CharacterTextures[Faction.Good], mainMan);
                    break;

                //add the other types for multiplayer support
            }

            Player player = new Player(character, mainMan);
            mainMan.GameMan.Players.Add(player);
            mainMan.GameMan.Characters.Add(character);
        }


    }
}
