#region Using statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System.IO;
#endregion

//Names: Ryan Bell

namespace SwagSword
{
    public class MapManager : Manager
    {
        #region Fields
        int tileSize;                                                       
        int mapWidth;
        int mapHeight;
        int resWidth;
        int resHeight;
        int strongholdWidth;
        int radius;
        Point rightCenterPoint;
        Point leftCenterPoint;
        Point upperCenterPoint;
        Point lowerCenterPoint;
        Texture2D map;
        GraphicsDevice graphicsDevice;
        Stronghold leftStronghold;
        Stronghold rightStronghold;
        Stronghold topStronghold;
        Stronghold lowerStronghold;
        List<Stronghold> strongholds;
        #endregion

        #region Properties
        public int TileSize { get { return tileSize; } }
        public int MapWidth { get { return mapWidth; } }
        public int MapHeight { get { return mapHeight; } }
        public int ResWidth { get { return resWidth; } }
        public int ResHeight { get { return resHeight; } }
        public Point RightCenterPoint { get { return rightCenterPoint; } set { rightCenterPoint = value; } }
        public Point LeftCenterPoint { get { return leftCenterPoint; } set { leftCenterPoint = value; } }
        public Point UpperCenterPoint { get { return upperCenterPoint; } set { upperCenterPoint = value; } }
        public Point LowerCenterPoint { get { return lowerCenterPoint; } set { lowerCenterPoint = value; } }
        public int Radius { get { return radius; } }
        public List<Stronghold> Strongholds { get { return strongholds; } }
        #endregion

        #region ConstructInit
        public MapManager(Game1 mainMan)
            : base(mainMan)
        {

        }

        public override void Init()
        {
            tileSize = 64;
            resWidth = 5248;
            resHeight = 5248;
            //5248
            mapWidth = resWidth / tileSize;
            mapHeight = resHeight / tileSize;

            graphicsDevice = mainMan.GraphicsDevice;
        }
        #endregion

        //called after the textures are loaded
        public void Startup()
        {
            strongholdWidth = tileSize * 5;
            radius = 560;
            //radius = 560;
            MapMaker mapMaker = new MapMaker(radius, tileSize, resWidth, resHeight, graphicsDevice, mainMan);
            //make map
            map = mapMaker.MakeMap();
            
            //Strongholds set
            leftStronghold = new Stronghold(mainMan.DrawMan.LeftStronghold, new Rectangle(radius - strongholdWidth / 2, resHeight / 2 - strongholdWidth / 2, strongholdWidth, strongholdWidth), mainMan, Faction.Good);
            rightStronghold = new Stronghold(mainMan.DrawMan.RightStronghold, new Rectangle(resWidth - radius - strongholdWidth / 2, resHeight / 2 - strongholdWidth / 2, strongholdWidth, strongholdWidth), mainMan, Faction.Tribal);
            topStronghold = new Stronghold(mainMan.DrawMan.TopStronghold, new Rectangle(resWidth / 2 - strongholdWidth / 2, radius - strongholdWidth / 2, strongholdWidth, strongholdWidth), mainMan, Faction.Rich);
            lowerStronghold = new Stronghold(mainMan.DrawMan.LowerStronghold, new Rectangle(resWidth / 2 - strongholdWidth / 2, resHeight - radius - strongholdWidth / 2, strongholdWidth, strongholdWidth), mainMan, Faction.Thief);

            mainMan.GameMan.LeftStrong = leftStronghold.Rect;
            mainMan.GameMan.RightStrong = rightStronghold.Rect;
            mainMan.GameMan.TopStrong = topStronghold.Rect;
            mainMan.GameMan.LowerStrong = lowerStronghold.Rect;

            strongholds = new List<Stronghold>();
            strongholds.Add(leftStronghold);
            strongholds.Add(rightStronghold);
            strongholds.Add(topStronghold);
            strongholds.Add(lowerStronghold);
            return;
        }

        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(map, new Rectangle(0, 0, resWidth, resHeight), Color.White);
            
            //draw the strongholds
            foreach(Stronghold s in strongholds)
            {
                s.Draw(spriteBatch);
            }
        }

        public double CalcDistance(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow((a.X - b.X), 2) + Math.Pow((a.Y - b.Y), 2));
        }
        public double CalcDistance(int aX, int aY, int bX, int bY)
        {
            return Math.Sqrt(Math.Pow((aX - bX), 2) + Math.Pow((aY - bY), 2));
        }


    }
}