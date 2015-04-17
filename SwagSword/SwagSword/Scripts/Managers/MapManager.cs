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
        int tileSize;
        int mapWidth;
        int mapHeight;
        int resWidth;
        int resHeight;
        //temp variable
        Texture2D map;
        GraphicsDevice graphicsDevice;

        public int TileSize { get { return tileSize; } }
        public int MapWidth { get { return mapWidth; } }
        public int MapHeight { get { return mapHeight; } }

        public MapManager(Game1 mainMan)
            : base(mainMan)
        {

        }

        public override void Init()
        {
            tileSize = 64;
            resWidth = 3260;
            resHeight = 3260;
            mapWidth = resWidth / tileSize;
            mapHeight = resHeight / tileSize;

            graphicsDevice = mainMan.GraphicsDevice;
        }

        
        //called after the textures are loaded
        public void Startup()
        {
            SHO = mainMan.DrawMan.Stronghold.Width / 4;
            radius = 200;
            MapMaker mapMaker = new MapMaker(tileSize, resWidth, resHeight, graphicsDevice, mainMan);
            map = mapMaker.MakeMap();
            //make map
            //temporarily keep as single texture
            //map = ...
        }
        int SHO;
        int radius;
        public void Draw(SpriteBatch spriteBatch)
        {
            
            //while loading draw the loading screen (would require threading)
            spriteBatch.Draw(map, new Rectangle(0, 0, resWidth, resHeight), Color.White);
            //draw the strongholds
            //actual position values will be given later
            //but for now, the paths are straight so there is no need
            spriteBatch.Draw(mainMan.DrawMan.Stronghold, new Rectangle(resWidth / 2 - SHO, radius - SHO, SHO * 2, SHO * 2), Color.White);
            spriteBatch.Draw(mainMan.DrawMan.Stronghold, new Rectangle(resWidth / 2 - SHO, resHeight - radius - SHO, SHO * 2, SHO * 2), Color.White);
            spriteBatch.Draw(mainMan.DrawMan.Stronghold, new Rectangle(radius - SHO, resHeight / 2 - SHO, SHO * 2, SHO * 2), Color.White);
            spriteBatch.Draw(mainMan.DrawMan.Stronghold, new Rectangle(resWidth - radius - SHO, resHeight / 2 - SHO, SHO * 2, SHO * 2), Color.White);
        }




    }
}