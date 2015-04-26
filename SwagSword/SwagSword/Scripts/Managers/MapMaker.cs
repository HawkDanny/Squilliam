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
        public MapMaker(int ts, int rw, int rh, GraphicsDevice graphicsDevice, Game1 mainMan)
        {
            resWidth = rw;
            resHeight = rh;
            radius = 200;                                                                   //hardcode in the stronghold radius
            pathThickness = 230;                                                            //hardcode in the path thickness
            this.graphicsDevice = graphicsDevice;                                   
            rand = new Random();
            this.mainMan = mainMan;
        }
        #endregion

        /// <summary>
        /// Public facing method that runs through the steps in making the map
        /// and interfaces a lot with the perlin noise class of which it makes an object of
        /// </summary>
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

        #region BasePaths
        /// <summary>
        /// Creates the basic masking layer for the horizontal path
        /// before a noise shift is applied
        /// </summary>
        /// <returns></returns>
        private Texture2D BasePathVertical()
        {
            Texture2D HPB = new Texture2D(graphicsDevice, resWidth, resHeight);                                         //create a new texture
            Color[] colorData = new Color[resWidth * resHeight];                                                        //create color array to hold the color data
            for(int i = radius; i < resWidth - radius; i++)                                                             //start and end leaving a buffer of size radius
            {
                for (int j = ((resHeight / 2) - (pathThickness / 2)); j < ((resHeight / 2) + (pathThickness / 2)); j++) //vertically loop from 1/2 path width less than 
                {                                                                                                       //center to 1/2 path width more than center
                    colorData[i * resHeight + j] = Color.Red;                                                           //change the color at all of these pixels to red
                }
            }
            HPB.SetData<Color>(colorData);                                                                              //set the color data of the texture to the color array
            return HPB;
        }

        /// <summary>
        /// Creates the basic masking layer for the vertical path
        /// before a noise shift is applied
        /// </summary>
        /// <returns></returns>
        private Texture2D BasePathHorizontal()
        {
            Texture2D VPB = new Texture2D(graphicsDevice, resWidth, resHeight);                                         //create a new texture
            Color[] colorData = new Color[resWidth * resHeight];                                                        //create color array to hold the color data
            for (int i = ((resHeight / 2) - (pathThickness / 2)); i < ((resHeight / 2) + (pathThickness / 2)); i++)     //horizontally loop from 1/2 path width less than
            {                                                                                                           //center to 1/2 path width more than center
                for (int j = radius; j < resHeight - radius; j++ )                                                      //start and end leaving a buffer of size radius
                {
                    colorData[i * resHeight + j] = Color.Red;                                                           //change the color at all of these pixels to red
                }
            }
            VPB.SetData<Color>(colorData);                                                                              //set the color data of the texture to the color array
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

        /// <summary>
        /// Merges 2 texture masks together using the red channel of each
        /// </summary>
        private Texture2D MergeTextures(Texture2D layer1, Texture2D layer2)
        {
            Color[] layer1Data = new Color[layer1.Width * layer1.Height];                                               //color array to hold extracted color data of tex1
            Color[] layer2Data = new Color[layer2.Width * layer2.Height];                                               //color array to hold extracted color data of tex2
            layer1.GetData<Color>(layer1Data);                                                                          //extract tex1 color data
            layer2.GetData<Color>(layer2Data);                                                                          //extract tex2 color data
            for(int i = 0; i < layer1Data.Length; i++)                                                                  //loop through the pixels
            {
                layer1Data[i].R = (byte)(MathHelper.Clamp(layer1Data[i].R + layer2Data[i].R, 0, 255));                  //add the 2 red channels but clamp them to between 0-255
            }
            layer1.SetData<Color>(layer1Data);                                                                          //set the color data of tex1 and return it
            return layer1;
        }
        /*

        private bool[,] GetWalkables()
        {}
        private Texture2D NoiseyCircle(int radius, Texture2D noise)
        {
            //use shiftpath
            //use blur 2x
        }
*/
    }
}
