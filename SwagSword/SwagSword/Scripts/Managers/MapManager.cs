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
            mapWidth = 15;
            mapHeight = 15;
            resWidth = 1920;
            resHeight = 1920;
            graphicsDevice = mainMan.GraphicsDevice;
        }

        //called after the textures are loaded
        public void Startup()
        {
            NoiseToTexture();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //noiseButton_Click();
            //while loading draw the loading screen (would require threading)
            spriteBatch.Draw(noise, new Rectangle(10, 10, resWidth, resHeight), Color.White);
        }

        Texture2D noise;
        private void NoiseToTexture()
        {
            Random rand = new Random();
            PerlinNoise perlinNoise = new PerlinNoise(resWidth, resHeight, rand);
            noise = new Texture2D(graphicsDevice, resWidth, resHeight);
            float[][] noiseData = perlinNoise.Init();

            Color[] colorData = new Color[resWidth * resHeight];
            for (int x = 0; x < resWidth - 1; x++)
            {
                for (int y = 0; y < resHeight - 1; y++)
                {
                    int val = (int)(noiseData[x][y] * 255);
                    colorData[x * resHeight + y] = new Color(val, val, val);
                }
            }
            noise.SetData<Color>(colorData);
        }

    }
}