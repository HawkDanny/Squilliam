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
        #region Fields
        int lx, ly, lw, lh, rx, ry, rw, rh, tx, ty, tw, th, bx, by, bw, bh;
        #endregion

        public SpawnManager(Game1 mainMan):base(mainMan)
        {

        }

        //Init
        public override void Init()
        {

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
                    character = new GoodCharacter(mainMan.Rnd.Next(lx, lx + lw), mainMan.Rnd.Next(ly, ly + lh), mainMan.DrawMan.SpriteDict[Faction.Good], mainMan);
                    break;

                case Faction.Tribal:
                    character = new TribalCharacter(mainMan.Rnd.Next(rx, rx + rw), mainMan.Rnd.Next(ry, ry + rh), mainMan.DrawMan.SpriteDict[Faction.Tribal], mainMan);
                    break;

                case Faction.Rich:
                    character = new RichCharacter(mainMan.Rnd.Next(tx, tx + tw), mainMan.Rnd.Next(ty, ty + th), mainMan.DrawMan.SpriteDict[Faction.Rich], mainMan);
                    break;

                case Faction.Thief:
                    character = new ThiefCharacter(mainMan.Rnd.Next(bx, bx + bw), mainMan.Rnd.Next(by, by + bh), mainMan.DrawMan.SpriteDict[Faction.Thief], mainMan);
                    break;
            }

            mainMan.GameMan.Characters.Add(character);
        }

        public void SpawnPlayer(Faction type)
        {
            Character character = null;
            switch (type)
            {
                case Faction.Good:
                    character = new GoodCharacter(mainMan.Rnd.Next(lx, lx + lw), mainMan.Rnd.Next(ly, ly + lh), mainMan.DrawMan.GoodGuyTextures[0], mainMan);
                    break;

                //add the other types for multiplayer support
            }

            Player player = new Player(character, mainMan);
            mainMan.GameMan.Players.Add(player);
            mainMan.GameMan.Characters.Add(character);
        }


    }
}
