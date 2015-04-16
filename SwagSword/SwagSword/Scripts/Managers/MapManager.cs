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
            resWidth = 2000;
            resHeight = 2000;
            mapWidth = resWidth / tileSize;
            mapHeight = resHeight / tileSize;
            rand = new Random();
            graphicsDevice = mainMan.GraphicsDevice;
        }

        
        //called after the textures are loaded
        public void Startup()
        {
            PerlinNoise noiseGen = new PerlinNoise(resWidth, resHeight, rand, graphicsDevice);
            noiseOffset = noiseGen.NoiseToTexture();
            noiseOffset = noiseGen.AdjustConstrast(noiseOffset, 4);
            noiseOffset = noiseGen.AddGradient(noiseOffset, Color.Black, Color.White);
            noiseOffset = noiseGen.BlendImages(mainMan.DrawMan.SandyTexture, mainMan.DrawMan.GrassTexture, noiseOffset);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //while loading draw the loading screen (would require threading)
            spriteBatch.Draw(noiseOffset, new Rectangle(0, 0, resWidth, resHeight), Color.White);
        }




    }
}