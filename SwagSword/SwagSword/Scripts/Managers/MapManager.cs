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
        List<Rectangle> tents;
        List<Rectangle> centerpieces;
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
            tents = new List<Rectangle>();
            centerpieces = new List<Rectangle>();
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

            foreach(Stronghold s in strongholds)
            {
                tents.Add(new Rectangle(s.Rect.Center.X - 328, s.Rect.Center.Y + 200, 128, 128));
                tents.Add(new Rectangle(s.Rect.Center.X - 328, s.Rect.Center.Y - 328, 128, 128));
                tents.Add(new Rectangle(s.Rect.Center.X + 200, s.Rect.Center.Y + 200, 128, 128));
                tents.Add(new Rectangle(s.Rect.Center.X + 200, s.Rect.Center.Y - 328, 128, 128));
            }
            centerpieces.Add(new Rectangle(leftStronghold.Rect.Center.X - 500, leftStronghold.Rect.Y + 60, 160, 160));
            centerpieces.Add(new Rectangle(rightStronghold.Rect.Center.X + 400, rightStronghold.Rect.Y + 60, 160, 160));
            centerpieces.Add(new Rectangle(topStronghold.Rect.Center.X - 80, topStronghold.Rect.Center.Y - 500, 160, 160));
            centerpieces.Add(new Rectangle(lowerStronghold.Rect.Center.X - 80, lowerStronghold.Rect.Center.Y + 400, 160, 160));
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
            spriteBatch.Draw(mainMan.DrawMan.BanditCenterpiece, centerpieces[3], Color.White);
            spriteBatch.Draw(mainMan.DrawMan.GoodGuyCenterpiece, centerpieces[0], Color.White);
            spriteBatch.Draw(mainMan.DrawMan.RichCenterpiece, centerpieces[2], Color.White);
            spriteBatch.Draw(mainMan.DrawMan.TribalCenterpiece, centerpieces[1], Color.White);
            for(int i = 0; i < 4; i ++)
            {
                spriteBatch.Draw(mainMan.DrawMan.GoodGuyTents, tents[i], Color.White);
            }
            for(int i = 4; i < 8; i ++)
            {
                spriteBatch.Draw(mainMan.DrawMan.TribalTents, tents[i], Color.White);
            }
            for(int i = 8; i < 12; i ++)
            {
                spriteBatch.Draw(mainMan.DrawMan.RichTents, tents[i], Color.White);
            }
            for(int i = 12; i < 16; i ++)
            {
                spriteBatch.Draw(mainMan.DrawMan.BanditTents, tents[i], Color.White);
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