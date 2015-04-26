#region UsingStatements
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
    class PerlinNoise
    {
        #region Fields
        private int width;                                                  //Width in pixels of the noise
        private int height;                                                 //Height in pixels of the noise
        Random rand;                                                        //Random object for weight generation
        GraphicsDevice graphicsDevice;                                      //graphics device being passed in from game1->MapMan->MapMaker->
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor that sets params
        /// </summary>
        public PerlinNoise(int resolutionWidth, int resolutionHeight, Random r, GraphicsDevice graphicsDevice)
        {
            width = resolutionWidth;                                        //setting the width and height variables
            height = resolutionHeight;                                      //...
            rand = r;                                                       //saving reference to random object
            this.graphicsDevice = graphicsDevice;                           //used in the creation of new texture2Ds
        }
        #endregion

        #region HelperMethods
        /// <summary>
        /// creates/returns an empty jagged float array
        /// </summary>
        private float[][] GetEmptyArray(int width, int height)
        {
            float[][] returned = new float[width][];                        //the jagged float array to be returned of length width
            for(int i = 0; i < width; i++)                                  //loop through all of the secondary arrays
            {   
                returned[i] = new float[height];                            //set the length of the array at the current index to height
            }
            return returned;
        }

        /// <summary>
        /// creates/returns and empty jagged color array
        /// </summary>
        private Color[][] GetEmptyColorArray(int width, int height)
        {
            Color[][] returned = new Color[width][];                        //the jagged color array to be returned of length width
            for (int i = 0; i < width; i++)                                 //loop through all of the secondary arrays
            {
                returned[i] = new Color[height];                            //set the lenght of the array at the current index to height
            }
            return returned;
        }

        /// <summary>
        /// Preforms linear interpolation between two floats 
        /// given a ratio of the two
        /// </summary>
        /// <param name="x0">the first float</param>
        /// <param name="x1">the second float</param>
        /// <param name="weight">the ratio, from 0 to 1, of the two points</param>
        /// <returns></returns>
        float InterpLinear(float x0, float x1, float weight)
        {
            return x0 * (1 - weight) + weight * x1;                         //compute the value with the given weight
        }

        /// <summary>
        /// preforms cosine interpolation between two floats
        /// given a ration of the two
        /// </summary>
        /// <param name="x0">the first float</param>
        /// <param name="x1">the second flaot</param>
        /// <param name="weight">the ratio, from 0 to 1, of the two points</param>
        /// <returns></returns>
        float InterpCosine(float x0, float x1, float weight)
        {
            double ft = x0 * Math.PI;                                       //prepare the first point to be used with cosine
            double f = (1 - Math.Cos(ft)) * 0.5;                            //convert 0-1 weight to a cosine based wieght
            return (float)(x0 * (1 - f) + x1 * f);                          //use the linear interp formula with the adjusted weight
        }

        /// <summary>
        /// Linearly interpolate between two colors given a third color
        /// The red value of the third color will be used for the weight
        /// </summary>
        /// <param name="a">the first color</param>
        /// <param name="b">the second color</param>
        /// <param name="w">the weight carrying color</param>
        /// <returns></returns>
        Color InterpLinearColor(Color a, Color b, Color w)
        {
            double weight = (double)w.R / 255;                              //convert the red value of the weight color to a value between 0-1
            return new Color
                (
                (int)(a.R * (1 - weight) + b.R * weight),                   //calculate the individual channels of the new color using weight
                (int)(a.G * (1 - weight) + b.G * weight),                   //...
                (int)(a.B * (1 - weight) + b.B * weight)                    //...
                );
        }

        /// <summary>
        /// Linearly interpolate between two colors given a value
        /// from 0 to 1
        /// </summary>
        /// <param name="a">the first color</param>
        /// <param name="b">the second color</param>
        /// <param name="w">the ratio of the 2 colors</param>
        /// <returns></returns>
        Color InterpLinearColor(Color a, Color b, double w)
        {
            double weight = w;
            return new Color
                (
                (int)(a.R * (1 - weight) + b.R * weight),                   //calculate the individual channels of the new color using weight
                (int)(a.G * (1 - weight) + b.G * weight),                   //...
                (int)(a.B * (1 - weight) + b.B * weight)                    //...
                );
        }

        /// <summary>
        /// Converts a generated jagged float array of values to a greyscale Texture2D
        /// </summary>
        public Texture2D NoiseToTexture()
        {
            Texture2D newNoise = new Texture2D(graphicsDevice, width, height);  //create a new texture of the class level width and height
            float[][] noiseData = GeneratePerlinNoise();                        //obtain the jagged float array from another method

            Color[] colorData = new Color[width * height];                      //create a color array that will be used to set the texture colors
            for (int x = 0; x < width - 1; x++)                                 //loop through each column of pixels
            {
                for (int y = 0; y < height - 1; y++)                            //loop through each row of pixels
                {
                    int val = (int)(noiseData[x][y] * 255);                     //convert to 0-1 values to integer values between 0 and 255
                    colorData[x * height + y] = new Color(val, val, val);       //set the color at the respective position to the greyscale of the value
                }
            }
            newNoise.SetData<Color>(colorData);                                 //set the colors of the Texture2d to the new color array
            return newNoise;
        }

        /// <summary>
        /// Inefficient method for converting a 2d color array to a texture
        /// *********DO NOT USE*********
        /// </summary>
        /// <param name="colors"></param>
        /// <returns></returns>
        private Texture2D ConvertArrayToTexture(Color[][] colors)
        {
            width = colors.Length;                                              //obtain the width from the 2d array
            height = colors[0].Length;                                          //obtain the height from an element in the array
            Texture2D textureToReturn = new Texture2D(graphicsDevice, width, height);
                                                                                //create a new texture to return
            Color[] colorData = new Color[width * height];                      //create a 1D array with the same number of elements as the 2d
            for (int x = 0; x < width - 1; x++)                                 //loop through the first layer of elements in the 2D array
            {
                for (int y = 0; y < height - 1; y++)                            //loop through the second layer of elements in the 2D array
                {
                    colorData[x * height + y] = colors[x][y];                   //fill in the 1D array with the respective data in the 2D array
                }
            }
            textureToReturn.SetData<Color>(colorData);                          //set the color of the new texture to the data in the 1D array
            return textureToReturn;
        }

        /// <summary>
        /// Inefficient method for converting a texture to a color array
        /// *********DO NOT USE*********
        /// </summary>
        /// <param name="tex"></param>
        /// <returns></returns>
        private Color[][] ConvertTextureToArray(Texture2D tex)
        {
            width = tex.Width;                                                  //obtain the number of columns from the texture
            height = tex.Height;                                                //obtain the number of rows from the texure
            Color[][] colorsToReturn = GetEmptyColorArray(width, height);       //create an empty 2D color array to be returned
            Color[] colorData = new Color[width * height];                      //create an intermediary color array to hold the extracted texture data
            tex.GetData<Color>(colorData);                                      //extract the texture data and stor it in the 1D array
            for (int x = 0; x < width - 1; x++)                                 //loop through the number of columns in the array
            {
                for (int y = 0; y < height - 1; y++)                            //loop through the number of rows in the array
                {
                    colorsToReturn[x][y] = colorData[x * height + y];           //transfer the data from the 1D array to the 2D array
                }
            }
            return colorsToReturn;
        }
        #endregion

        #region NoiseFunctions
        /// <summary>
        /// Generates completely random white noise
        /// (i.e. each pixel will have an uncorrelated number from 0 to 1
        /// </summary>
        float[][] GenerateWhiteNoise()
        {
            float[][] whiteNoise = GetEmptyArray(width, height);                //create an empty jagged array to hold the noise values
            for (int i = 0; i < width; i++)                                     //loop through the number columns in the array
                for (int j = 0; j < height; j++)                                //loop through the number of rows in the array
                    whiteNoise[i][j] = (float)rand.NextDouble() % 1;            //assign a random double from 0 to 1 at that index
            return whiteNoise;
        }

        /// <summary>
        /// Creates one octave of the smooth noise given a white noise map
        /// </summary>
        /// <param name="baseNoise"></param>
        /// <param name="octave">The current octave that is being calculated</param>
        float[][] GenerateSmoothNoise(float[][] baseNoise, int octave)
        {
            float[][] smoothNoise = GetEmptyArray(width, height);               //create a new empty 2d array that will hold the noise values

            int period = 1 << octave;                                           //use bit shifting to calculate the period (generate 2^octave)
            float frequency = 1.0f / period;                                    //convert the period to frequency
 
            for (int i = 0; i < width; i++)                                     //loop through the columns in the array
            {
                int valueX0 = (i / period) * period;                            //using integer division, calculate the floored i * period
                int valueX1 = (valueX0 + period) % width;                       //use modulus of the width to determine a constricting value based on X0
                float Xblend = (i - valueX0) * frequency;                       //calculate how flattening the transformation should be based on the frequency of the octave
                for (int j = 0; j < height; j++)                                //loop through the rows in the array
                {
                    int valueY0 = (j / period) * period;                        //using integer division, calculate the floored j * period
                    int valueY1 = (valueY0 + period) % height;                  //use modulus of the height to determine a constricting value based on Y0
                    float Yblend = (j - valueY0) * frequency;                   //calculate how flattening the transformation should be based on the frequency of the octave

                    //linearly interp between the top two corners, then the bottom 2, then vertically between both of those interp values
                    float top = InterpLinear(baseNoise[valueX0][valueY0], baseNoise[valueX1][valueY0], Xblend);
                    float bottom = InterpLinear(baseNoise[valueX0][valueY1], baseNoise[valueX1][valueY1], Xblend);
                    smoothNoise[i][j] = InterpLinear(top, bottom, Yblend);
                }
            }
            return smoothNoise;
        }

        /// <summary>
        /// An easy public facing method for generating the perlin noise
        /// </summary>
        public float[][] GeneratePerlinNoise()
        {
            return GeneratePerlinNoise(GenerateWhiteNoise(), 8);                //calling the private equivalent and passing in the white noise and octave count
        }

        /// <summary>
        /// Main method for generating the Perlin noise
        /// </summary>
        /// <param name="baseNoise">The white noise that is passd in</param>
        /// <param name="octaveCount">The number of octaves that are to be created</param>
        private float[][] GeneratePerlinNoise(float[][] baseNoise, int octaveCount)
        {
            float[][][] smoothNoise = new float[octaveCount][][];               //An array of 2D arrays that hold the noise values at each octave
            float persistance = 0.5f;                                           //the persistance value that will be used in the noise funciton
                                                                                //....(hard coded at .5 - the most widely used value)
            for(int i = 0; i < octaveCount; i++)                                //loop for the number of octaves
            {
                smoothNoise[i] = GenerateSmoothNoise(baseNoise, i);             //save the octave noise data generated by the smooth noise method
            }

            float[][] perlinNoise = GetEmptyArray(width, height);               //create an empty 2D array that will hold the compressed octave data
            float amplitude = 1.0f;                                             //two variables that will be used for normalization
            float totalAmp = 0.0f;                                              //...

            for (int octave = octaveCount - 1; octave >= 0; octave--)           //loop through the number of octaves
            {
                amplitude *= persistance;                                       //increment the value that will be applyed to each octave
                totalAmp += amplitude;                                          //used for normalization
                for (int i = 0; i < width; i++)                                 //loop though the number of columns
                    for (int j = 0; j < height; j++)                            //loop through the number of rows
                        perlinNoise[i][j] += smoothNoise[octave][i][j] * amplitude;//add the weighted octave values at each point to the perlinNoise array
            }
            //normalization
            for (int i = 0; i < width; i++)                                     //loop through the number of columns
                for (int j = 0; j < height; j++)                                //loop through the number of rows
                    perlinNoise[i][j] /= totalAmp;                              //normalize each 'pixel' to give a cloudier less harsh noise
            return perlinNoise;

        }
        #endregion

        #region ImageManip
        /// <summary>
        /// Adjusts the contrast of greyscale images by a given factor 
        /// </summary>
        public Texture2D AdjustConstrast(Texture2D baseTexture, double factor)
        {
            
            Color[] originalColor = new Color[baseTexture.Width * baseTexture.Height];                              //the color data of the original texture
            Color[] adjustedColor = new Color[baseTexture.Width * baseTexture.Height];                              //array to hold the adjusted color data
            baseTexture.GetData<Color>(originalColor);                                                              //extract the color data from the texture
            double average = 0;                                                                                     //variable to keep a running total of the values
            for(int i = 0; i < originalColor.Length; i++)                                                           //loop through all of the color values in orig
            {
                average += (originalColor[i].R + originalColor[i].G + originalColor[i].B);                          //add the values of each channel to average
            }
            average /= (3 * originalColor.Length);                                                                  //divide average by the mnumber of colors * channel count
            for (int i = 0; i < originalColor.Length; i++)                                                          //loop back through the color values
            {
                double deltaR = originalColor[i].R - average;                                                       //find the offset amount from average of each color
                double deltaG = originalColor[i].G - average;                                                       //...
                double deltaB = originalColor[i].B - average;                                                       //...
                //form a new color that is shifted based on how far it is from the average 
                adjustedColor[i] = new Color((int)(average + (deltaR * factor)), (int)(average + (deltaG * factor)), (int)(average + (deltaB * factor)));
            }
            baseTexture.SetData<Color>(adjustedColor);                                                              //set the color data of the given texture to the adjusted values
            return baseTexture;

        }

        /// <summary>
        /// Adds a color gradient, from left to right, to a given texture
        /// </summary>
        /// <param name="baseTexture">The texture that will have a gradient applyed to it</param>
        /// <param name="gradientStart">The color that will be 100 percent on the left</param>
        /// <param name="gradientEnd">The color that will be 100 percent on the right</param>
        /// <returns></returns>
        public Texture2D AddGradient(Texture2D baseTexture, Color gradientStart, Color gradientEnd)
        {
            Color[] colorData = new Color[baseTexture.Width * baseTexture.Height];                                  //Empty color array to hold the texture color data
            baseTexture.GetData<Color>(colorData);                                                                  //extract the color data from the given texture
            double weight = 0;                                                                                      //variable that keeps track of the ratio of the 2 colors
            for(int i = 0; i < baseTexture.Width; i++)                                                              //loop through the pixel columns in the texture
            {                               
                for (int j = 0; j < baseTexture.Height; j++)                                                        //loop through the pixel rows in the texture
                {
                    weight = ((double)j / (double)baseTexture.Height);                                              //adjust the wieght based on the current row
                    Color addition = InterpLinearColor(gradientStart, gradientEnd, weight);                         //interpolate between the 2 colors based on calculated weight
                    //average the R, G, B values of the original image and the calculated R, G, B values of the gradient
                    colorData[i * baseTexture.Height + j].R = (byte)((colorData[i * baseTexture.Height + j].R + addition.R) / 2);
                    colorData[i * baseTexture.Height + j].G = (byte)((colorData[i * baseTexture.Height + j].G + addition.G) / 2);
                    colorData[i * baseTexture.Height + j].B = (byte)((colorData[i * baseTexture.Height + j].B + addition.B) / 2);
                }
                weight = 0;                                                                                         
            }
            baseTexture.SetData<Color>(colorData);                                                                  //reset the color data of the given texture
            return baseTexture;
        }

        /// <summary>
        /// Blend two images based on a third texture as a mask
        /// The R value of the third texture will be used to calculate the mask
        /// </summary>
        /// <param name="texture1">The base texture</param>
        /// <param name="texture2">The overlay texture</param>
        /// <param name="mask">The texture holidng the ratios</param>
        /// <returns></returns>
        public Texture2D BlendImages(Texture2D texture1, Texture2D texture2, Texture2D mask)
        {
            Color[] dataTex1 = new Color[texture1.Width * texture1.Height];                                         //color array to hold the base texture data
            texture1.GetData<Color>(dataTex1);                                                                      //extracting and saving the base texture data
            Color[] dataTex2 = new Color[texture2.Width * texture2.Height];                                         //color array to hold the overlay texture data
            texture2.GetData<Color>(dataTex2);                                                                      //extracting and saving the overlay texture data
            Color[] dataMask = new Color[mask.Width * mask.Height];                                                 //color array to hold the masking texture data
            mask.GetData<Color>(dataMask);                                                                          //extracting and saving the masking texture data
            Color[] image = new Color[mask.Width * mask.Height];                                                    //color array to hold the combined colors
            Texture2D imageTexture = new Texture2D(graphicsDevice, mask.Width, mask.Height);                        //create a new texture that will hold the combined textures
            
            for (int i = 0; i < mask.Width; i++ )                                                                   //loop through the columns of the mask layer
            {
                for (int j = 0; j < mask.Height; j++)                                                               //loop through the rows of the mask layer
                {
                    
                    image[i * mask.Height + j] = InterpLinearColor(                                                 //use linear color interpolation for both tileable 
                        dataTex1[((i % texture1.Width) * texture1.Height) + j % texture1.Height],                   //colors with the weight being the R value of the mask layer
                        dataTex2[((i % texture2.Width) * texture2.Height) + j % texture2.Height],
                        dataMask[i * mask.Height + j]);
                }
            }
            imageTexture.SetData<Color>(image);                                                                     //set the new texture to the adjusted value data
            return imageTexture;
        }

        #endregion
    }
}
