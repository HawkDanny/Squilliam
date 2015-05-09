#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
#endregion

namespace SwagSword
{
    class MapMaker
    {
        #region Fields
        int resWidth;                                                                       //the pixel width of the map
        int resHeight;                                                                      //the pixel height of the map
        int radius;                                                                         //the pixel radius of the stronghold circles
        int pathThickness;                                                                  //the width of the pathways
        Texture2D noiseOffset;                                                              //noise map used to find path offsets
        Texture2D pathMask;                                                                 //holds a mask for the pathway placement
        Random rand;                                                                        //a random object
        GraphicsDevice graphicsDevice;                                                      //holds a reference to the graphics device to create new textures
        Game1 mainMan;                                                                      //holds a reference to game1
        #endregion

        #region Contructor
        /// <summary>
        /// sets the applicable variables to the params given 
        /// </summary>
        public MapMaker(int r, int ts, int rw, int rh, GraphicsDevice graphicsDevice, Game1 mainMan)
        {
            resWidth = rw;
            resHeight = rh;
            radius = r;                                                                   //hardcode in the stronghold radius
            pathThickness = 230;                                                            //hardcode in the path thickness
            this.graphicsDevice = graphicsDevice;                                   
            rand = new Random();
            this.mainMan = mainMan;
        }
        #endregion

        /// <summary>
        /// Public facing method that runs through the steps in making the map
        /// and interfaces a lot iwth the perlin noise class of which it makes an object of
        /// </summary>
        public Texture2D MakeMap()
        {
            PerlinNoise noiseGen = new PerlinNoise(resWidth, resHeight, rand, graphicsDevice);
            noiseOffset = noiseGen.NoiseToTexture();
            pathMask = MergeTextures(ShiftPathHorizontal(BasePathHorizontal(), noiseOffset), ShiftPathVertical(BasePathVertical(), noiseOffset));
            pathMask = MergeTextures(pathMask, NoiseyCircle());
            noiseOffset = noiseGen.AdjustConstrast(noiseOffset, 4);
            noiseOffset = noiseGen.AddGradient(noiseOffset, Color.Black, Color.White);
            noiseOffset = noiseGen.BlendImages(mainMan.DrawMan.SandyTexture, mainMan.DrawMan.GrassTexture, noiseOffset);

            //Boundary set
            mainMan.GameMan.CenterBound = new Rectangle(resWidth / 2 - pathThickness / 2, resHeight / 2 - pathThickness / 2, pathThickness, pathThickness);
            mainMan.GameMan.LeftPathBound = new Rectangle(radius * 2 - 40, resHeight / 2 - pathThickness / 2, (resWidth - 4 * radius + 80) / 2 - pathThickness / 2, pathThickness);
            mainMan.GameMan.RightPathBound = new Rectangle(mainMan.GameMan.CenterBound.X + pathThickness, mainMan.GameMan.CenterBound.Y, (resWidth - 4 * radius + 80) / 2 - pathThickness / 2, pathThickness);
            mainMan.GameMan.TopPathBound = new Rectangle(resHeight / 2 - pathThickness / 2, radius * 2 - 40, pathThickness, (resWidth - radius * 4 + 80) / 2 - pathThickness / 2);
            mainMan.GameMan.LowerPathBound = new Rectangle(resHeight / 2 - pathThickness / 2, mainMan.GameMan.CenterBound.Y + pathThickness, pathThickness, (resWidth - radius * 4 + 80) / 2 - pathThickness / 2);
            

            return noiseGen.BlendImages(noiseOffset, mainMan.DrawMan.PathwayTexture, pathMask);
        }

        #region BasePaths
        private Texture2D BasePathVertical()
        {
            Texture2D HPB = new Texture2D(graphicsDevice, resWidth, resHeight);
            Color[] colorData = new Color[resWidth * resHeight];
            for(int i = radius * 2 - 40; i < resWidth - radius * 2 + 40; i++)
            {
                for (int j = ((resHeight / 2) - (pathThickness / 2)); j < ((resHeight / 2) + (pathThickness / 2)); j++)
                {
                    colorData[i * resHeight + j] = Color.Red;
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
                for (int j = radius * 2 - 40; j < resHeight - radius * 2 + 40; j++ )
                {
                    colorData[i * resHeight + j] = Color.Red;
                }
            }
            VPB.SetData<Color>(colorData);
            return VPB;
        }
        #endregion

        #region ShiftPaths
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
                }
                maskColor = Color.Black;
                for(int y = bottomRow + bottomShift; y >= bottomRow; y--)
                {
                    subData2D[y, x] = maskColor;
                    if (y > (bottomRow + bottomShift) - 10)
                        maskColor.R += 25;
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
                }
                maskColor = Color.Black;
                for (int y = rightRow + rightShift; y >= rightRow; y--)
                {
                    subData2D[x, y] = maskColor;
                    if (y > (rightRow + rightShift) - 10)
                        maskColor.R += 25;
                }
            }
            for (int i = 0; i < subject.Width; i++)
                for (int j = 0; j < subject.Height; j++)
                    subjectData[i * subject.Height + j] = subData2D[i, j];
            subject.SetData<Color>(subjectData);
            return subject;
        }
        #endregion


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
        
        private Texture2D NoiseyCircle()
        {
            Point LeftCenterPoint = new Point(radius, resHeight / 2);
            Point RightCenterPoint = new Point(resWidth - radius, resHeight / 2);
            Point UpperCenterPoint = new Point(resWidth / 2, radius);
            Point LowerCenterPoint = new Point(resWidth / 2, resHeight - radius);
            Texture2D circleMask = new Texture2D(graphicsDevice, resWidth, resHeight);
            Color[,] colorData2D = new Color[resWidth, resHeight];
            
            
            for (int i = 0; i < resWidth; i++)
            {
                for (int j = 0; j < resHeight; j++)
                {
                    if(CalcDistance(i, j, LeftCenterPoint.X, LeftCenterPoint.Y) < radius)
                    {
                        colorData2D[i, j].R = 255;
                    }
                    if (CalcDistance(i, j, RightCenterPoint.X, RightCenterPoint.Y) < radius)
                    {
                        colorData2D[i, j].R = 255;
                    }
                    if (CalcDistance(i, j, UpperCenterPoint.X, UpperCenterPoint.Y) < radius)
                    {
                        colorData2D[i, j].R = 255;
                    }
                    if (CalcDistance(i, j, LowerCenterPoint.X, LowerCenterPoint.Y) < radius)
                    {
                        colorData2D[i, j].R = 255;
                    }
                }
            }

            Color[] colorData = new Color[resHeight * resWidth];
            for (int i = 0; i < resWidth; i++)
                for (int j = 0; j < resHeight; j++)
                    colorData[i * resHeight + j] = colorData2D[i, j];
            circleMask.SetData<Color>(colorData);
            //return ShiftPathHorizontal(ShiftPathVertical(circleMask, noise), noise);
            return circleMask;
        }

        private double CalcDistance(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow((a.X - b.X), 2) + Math.Pow((a.Y - b.Y), 2));
        }
        private double CalcDistance(int aX, int aY, int bX, int bY)
        {
            return Math.Sqrt(Math.Pow((aX - bX), 2) + Math.Pow((aY - bY), 2));
        }

    }
}
