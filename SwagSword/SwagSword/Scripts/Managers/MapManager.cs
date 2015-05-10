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
            mapWidth = resWidth / tileSize;
            mapHeight = resHeight / tileSize;

            graphicsDevice = mainMan.GraphicsDevice;
        }
        #endregion

        //called after the textures are loaded
        public void Startup()
        {
            strongholdWidth = tileSize * 3;
            radius = 550;
            MapMaker mapMaker = new MapMaker(radius, tileSize, resWidth, resHeight, graphicsDevice, mainMan);
            //make map
            map = mapMaker.MakeMap();
            
            //Strongholds set
            mainMan.GameMan.LeftStrong = new Rectangle(radius - strongholdWidth / 2, resHeight / 2 - strongholdWidth / 2, strongholdWidth, strongholdWidth);
            mainMan.GameMan.RightStrong = new Rectangle(resWidth - radius - strongholdWidth / 2, resHeight / 2 - strongholdWidth / 2, strongholdWidth, strongholdWidth);
            mainMan.GameMan.TopStrong = new Rectangle(resWidth / 2 - strongholdWidth / 2, radius - strongholdWidth / 2, strongholdWidth, strongholdWidth);
            mainMan.GameMan.LowerStrong = new Rectangle(resWidth / 2 - strongholdWidth / 2, resHeight - radius - strongholdWidth / 2, strongholdWidth, strongholdWidth);
            return;
        }

        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(map, new Rectangle(0, 0, resWidth, resHeight), Color.White);
            //draw the strongholds
            //actual position values will be given later
            //but for now, the paths are straight so there is no need


            spriteBatch.Draw(mainMan.DrawMan.LeftStronghold, mainMan.GameMan.LeftStrong, Color.White);
            spriteBatch.Draw(mainMan.DrawMan.RightStronghold, mainMan.GameMan.RightStrong, Color.White);
            spriteBatch.Draw(mainMan.DrawMan.TopStronghold, mainMan.GameMan.TopStrong, Color.White);
            spriteBatch.Draw(mainMan.DrawMan.LowerStronghold, mainMan.GameMan.LowerStrong, Color.White);
        }




    }
}