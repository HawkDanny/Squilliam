using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace SwagSword
{
    class MapMaker
    {

        int resWidth;
        int resHeight;
        int radius;
        int pathThickness;
        Texture2D noiseOffset;
        Random rand;
        GraphicsDevice graphicsDevice;
        Game1 mainMan;

        public MapMaker(int ts, int rw, int rh, GraphicsDevice graphicsDevice, Game1 mainMan)
        {
            resWidth = rw;
            resHeight = rh;
            radius = 200;
            pathThickness = 200;
            this.graphicsDevice = graphicsDevice;
            rand = new Random();
            this.mainMan = mainMan;
        }

        public Texture2D MakeMap()
        {
            
            
                PerlinNoise noiseGen = new PerlinNoise(resWidth, resHeight, rand, graphicsDevice);
                noiseOffset = noiseGen.NoiseToTexture();
                //return ShiftPath(BasePathHorizontal(), noiseOffset);
                return BasePathHorizontal();
            /*    noiseOffset = noiseGen.AdjustConstrast(noiseOffset, 4);
                noiseOffset = noiseGen.AddGradient(noiseOffset, Color.Black, Color.White);
                noiseOffset = noiseGen.BlendImages(mainMan.DrawMan.SandyTexture, mainMan.DrawMan.GrassTexture, noiseOffset);
             * */
        }
        private Texture2D BasePathVertical()
        {
            Texture2D HPB = new Texture2D(graphicsDevice, resWidth, resHeight);
            Color[] colorData = new Color[resWidth * resHeight];
            for(int i = 0; i < resWidth; i++)
            {
                for (int j = ((resHeight / 2) - (pathThickness / 2)); j < ((resHeight / 2) + (pathThickness / 2)); j++)
                {
                    colorData[i * resHeight + j] = Color.White;
                }
            }
            HPB.SetData<Color>(colorData);
            return HPB;
        }
        private Texture2D BasePathHorizontal()
        {
            Texture2D VPB = new Texture2D(graphicsDevice, resWidth, resHeight);
            Color[] colorData = new Color[resWidth * resHeight];
            for (int i = ((resHeight / 2) - (pathThickness / 2)); i < ((resHeight / 2) + (pathThickness / 2)); i++)
            {
                for (int j = 0; j < resHeight; j++ )
                {
                    colorData[i * resHeight + j] = Color.White;
                }
            }
            VPB.SetData<Color>(colorData);
            return VPB;
        }
        /*private Texture ShiftPathHorizontal(Texture2D subject, Texture2D noise)
        {
            
        }/*
        private void Blur(Texture2D subject, bool horizontal)
        {

        }
        private Texture2D NoiseyCircle(int radius, Texture2D noise)
        {
            //use shiftpath
            //use blur 2x
        }
        private Texture2D MergeTextures(Texture2D layer1, Texture2D layer2)
        {
            //average the pixels, not just additive
        }*/
    }
}
