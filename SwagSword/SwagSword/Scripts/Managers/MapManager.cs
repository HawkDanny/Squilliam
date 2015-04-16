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
        Texture2D noiseOffset;
        Random rand;
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
            resWidth = 1000;
            resHeight = 1000;
            mapWidth = resWidth / tileSize;
            mapHeight = resHeight / tileSize;
            rand = new Random();
            graphicsDevice = mainMan.GraphicsDevice;
        }

        Texture2D noiseOffset2;
        //called after the textures are loaded
        public void Startup()
        {
            PerlinNoise noiseGen = new PerlinNoise(resWidth, resHeight, rand, graphicsDevice);
            noiseOffset = noiseGen.NoiseToTexture();
            noiseOffset2 = noiseGen.BlendImages(mainMan.DrawMan.PathwayTexture, mainMan.DrawMan.Stronghold, noiseOffset);
            //noiseOffset = noiseGen.BlendImages(mainMan.DrawMan.Stronghold, mainMan.DrawMan.PathwayTexture, noiseOffset2);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //while loading draw the loading screen (would require threading)
            spriteBatch.Draw(noiseOffset2, new Rectangle(0, 0, resWidth, resHeight), Color.White);
        }




    }
}