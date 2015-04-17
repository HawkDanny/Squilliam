﻿using System;
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
        Texture2D pathMask;
        Random rand;
        GraphicsDevice graphicsDevice;
        Game1 mainMan;

        public MapMaker(int ts, int rw, int rh, GraphicsDevice graphicsDevice, Game1 mainMan)
        {
            resWidth = rw;
            resHeight = rh;
            radius = 200;
            pathThickness = 230;
            this.graphicsDevice = graphicsDevice;
            rand = new Random();
            this.mainMan = mainMan;
        }

        public Texture2D MakeMap()
        {
            
                PerlinNoise noiseGen = new PerlinNoise(resWidth, resHeight, rand, graphicsDevice);
                noiseOffset = noiseGen.NoiseToTexture();
                pathMask = MergeTextures(ShiftPathHorizontal(BasePathHorizontal(), noiseOffset), ShiftPathVertical(BasePathVertical(), noiseOffset));
                
                noiseOffset = noiseGen.AdjustConstrast(noiseOffset, 4);
                noiseOffset = noiseGen.AddGradient(noiseOffset, Color.Black, Color.White);
                noiseOffset = noiseGen.BlendImages(mainMan.DrawMan.SandyTexture, mainMan.DrawMan.GrassTexture, noiseOffset);
                return noiseGen.BlendImages(noiseOffset, mainMan.DrawMan.PathwayTexture, pathMask);
        }
        private Texture2D BasePathVertical()
        {
            Texture2D HPB = new Texture2D(graphicsDevice, resWidth, resHeight);
            Color[] colorData = new Color[resWidth * resHeight];
            for(int i = radius; i < resWidth - radius; i++)
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
                for (int j = radius; j < resHeight - radius; j++ )
                {
                    colorData[i * resHeight + j] = Color.White;
                }
            }
            VPB.SetData<Color>(colorData);
            return VPB;
        }
        private Texture2D ShiftPathHorizontal(Texture2D subject, Texture2D noise)
        {
            int topRow = ((resHeight / 2) - (pathThickness / 2));
            int bottomRow = ((resHeight / 2) + (pathThickness / 2));
            Color[] subjectData = new Color[subject.Width * subject.Height];
            subject.GetData<Color>(subjectData);
            Color[,] subData2D = new Color[subject.Width,subject.Height];
            Color[] noiseData = new Color[noise.Width * noise.Height];
            noise.GetData<Color>(noiseData);
            Color[,] noiseData2D = new Color[noise.Width, noise.Height];
            //2 series of two for loops for adjusting pixels
            for(int i = 0; i < subject.Width; i++)
                for (int j = 0; j < subject.Height; j++)
                {
                    subData2D[i, j] = subjectData[i * subject.Height + j];
                    noiseData2D[i, j] = noiseData[i * noise.Height + j];
                }
            Color maskColor = Color.Black;
            int Tfalloff;
            int Bfalloff;
            for(int x = radius; x < subject.Width - radius; x++)
            {
                int topShift = noiseData2D[x, 1].R / 2;
                int bottomShift = noiseData2D[x, 50].R / 2;
                Tfalloff = 255 / topShift;
                
                Bfalloff = 255 / bottomShift;
                maskColor = Color.Black;
                for(int y = topRow - topShift; y < topRow; y++)
                {
                    subData2D[y, x] = maskColor;
                    if (y < (topRow - topShift) + 10)
                        maskColor.R += 25;
                    //maskColor.R += (byte)Tfalloff;
                }
                maskColor = Color.Black;
                for(int y = bottomRow + bottomShift; y >= bottomRow; y--)
                {
                    subData2D[y, x] = maskColor;
                    if (y > (bottomRow + bottomShift) - 10)
                        maskColor.R += 25;
                    //maskColor.R += (byte)Bfalloff;
                }
            }
            for (int i = 0; i < subject.Width; i++)
                for (int j = 0; j < subject.Height; j++)
                    subjectData[i * subject.Height + j] = subData2D[i, j];
            subject.SetData<Color>(subjectData);
            return subject;
        }

        private Texture2D ShiftPathVertical(Texture2D subject, Texture2D noise)
        {
            int leftRow = ((resWidth / 2) - (pathThickness / 2));
            int rightRow = ((resWidth / 2) + (pathThickness / 2));
            Color[] subjectData = new Color[subject.Width * subject.Height];
            subject.GetData<Color>(subjectData);
            Color[,] subData2D = new Color[subject.Width, subject.Height];
            Color[] noiseData = new Color[noise.Width * noise.Height];
            noise.GetData<Color>(noiseData);
            Color[,] noiseData2D = new Color[noise.Width, noise.Height];
            //2 series of two for loops for adjusting pixels
            for (int i = 0; i < subject.Width; i++)
                for (int j = 0; j < subject.Height; j++)
                {
                    subData2D[i, j] = subjectData[i * subject.Height + j];
                    noiseData2D[i, j] = noiseData[i * noise.Height + j];
                }
            Color maskColor = Color.Black;
            int Lfalloff;
            int Rfalloff;
            for (int x = radius; x < subject.Width - radius; x++)
            {
                int leftShift = noiseData2D[x, 20].R / 2;
                int rightShift = noiseData2D[x, 90].R / 2;
                Lfalloff = 255 / leftShift;

                Rfalloff = 255 / rightShift;
                maskColor = Color.Black;
                for (int y = leftRow - leftShift; y < leftRow; y++)
                {
                    subData2D[x, y] = maskColor;
                    if (y < (leftRow - leftShift) + 10)
                        maskColor.R += 25;
                    //maskColor.R += (byte)Lfalloff;
                }
                maskColor = Color.Black;
                for (int y = rightRow + rightShift; y >= rightRow; y--)
                {
                    subData2D[x, y] = maskColor;
                    if (y > (rightRow + rightShift) - 10)
                        maskColor.R += 25;
                    //maskColor.R += (byte)Rfalloff;
                }
            }
            for (int i = 0; i < subject.Width; i++)
                for (int j = 0; j < subject.Height; j++)
                    subjectData[i * subject.Height + j] = subData2D[i, j];
            subject.SetData<Color>(subjectData);
            return subject;
        }
        private Texture2D MergeTextures(Texture2D layer1, Texture2D layer2)
        {
            //average the pixels, not just additive
            Color[] layer1Data = new Color[layer1.Width * layer1.Height];
            Color[] layer2Data = new Color[layer2.Width * layer2.Height];
            layer1.GetData<Color>(layer1Data);
            layer2.GetData<Color>(layer2Data);
            for(int i = 0; i < layer1Data.Length; i++)
            {
                layer1Data[i].R = (byte)(MathHelper.Clamp(layer1Data[i].R + layer2Data[i].R, 0, 255));
            }
            layer1.SetData<Color>(layer1Data);
            return layer1;
        }
        /*

        private bool[,] GetWalkables()
        {
           
           
        }
        private Texture2D NoiseyCircle(int radius, Texture2D noise)
        {
            //use shiftpath
            //use blur 2x
        }
*/
    }
}
