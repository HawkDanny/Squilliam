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
        private int width;
        private int height;
        Random rand;
        GraphicsDevice graphicsDevice;
        #endregion

        #region Constructor
        public PerlinNoise(int resolutionWidth, int resolutionHeight, Random r, GraphicsDevice graphicsDevice)
        {
            width = resolutionWidth;
            height = resolutionHeight;
            rand = r;
            this.graphicsDevice = graphicsDevice;
        }
        #endregion

        #region HelperMethods
        private float[][] GetEmptyArray(int width, int height)
        {
            float[][] returned = new float[width][];
            for(int i = 0; i < width; i++)
            {
                returned[i] = new float[height];
            }
            return returned;
        }

        private Color[][] GetEmptyColorArray(int width, int height)
        {
            Color[][] returned = new Color[width][];
            for (int i = 0; i < width; i++)
            {
                returned[i] = new Color[height];
            }
            return returned;
        }

        float InterpLinear(float x0, float x1, float weight)
        {
            return x0 * (1 - weight) + weight * x1;
        }

        float InterpCosine(float x0, float x1, float weight)
        {
            double ft = x0 * Math.PI;
            double f = (1 - Math.Cos(ft)) * 0.5;
            return (float)(x0 * (1 - f) + x1 * f);
        }

        Color InterpLinearColor(Color a, Color b, float weight)
        {
            return new Color
                (
                (int)(a.R * (1 - weight) + b.R * weight),
                (int)(a.G * (1 - weight) + b.G * weight),
                (int)(a.B * (1 - weight) + b.B * weight)
                );
        }

        public Texture2D NoiseToTexture()
        {
            Texture2D newNoise = new Texture2D(graphicsDevice, width, height);
            float[][] noiseData = GeneratePerlinNoise();

            Color[] colorData = new Color[width * height];
            for (int x = 0; x < width - 1; x++)
            {
                for (int y = 0; y < height - 1; y++)
                {
                    int val = (int)(noiseData[x][y] * 255);
                    colorData[x * height + y] = new Color(val, val, val);
                }
            }
            newNoise.SetData<Color>(colorData);
            return newNoise;
        }

        private Texture2D ConvertArrayToTexture(Color[][] colors)
        {
            width = colors.Length;
            height = colors[0].Length;
            Texture2D textureToReturn = new Texture2D(graphicsDevice, width, height);

            Color[] colorData = new Color[width * height];
            for (int x = 0; x < width - 1; x++)
            {
                for (int y = 0; y < height - 1; y++)
                {
                    colorData[x * height + y] = colors[x][y];
                }
            }
            textureToReturn.SetData<Color>(colorData);
            return textureToReturn;
        }

        private Color[][] ConvertTextureToArray(Texture2D tex)
        {
            width = tex.Width;
            height = tex.Height;
            Color[][] colorsToReturn = GetEmptyColorArray(width, height);
            Color[] colorData = new Color[width * height];
            tex.GetData<Color>(colorData);
            for (int x = 0; x < width - 1; x++)
            {
                for (int y = 0; y < height - 1; y++)
                {
                    colorsToReturn[x][y] = colorData[x * height + y];
                }
            }
            return colorsToReturn;
        }
        #endregion

        #region NoiseFunctions
        float[][] GenerateWhiteNoise()
        {
            float[][] whiteNoise = GetEmptyArray(width, height);
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    whiteNoise[i][j] = (float)rand.NextDouble() % 1;
            return whiteNoise;
        }

        float[][] GenerateSmoothNoise(float[][] baseNoise, int octave)
        {
            float[][] smoothNoise = GetEmptyArray(width, height);

            int period = 1 << octave;
            float frequency = 1.0f / period;
 
            for (int i = 0; i < width; i++)
            {
                int valueX0 = (i / period) * period;
                int valueX1 = (valueX0 + period) % width;
                float Xblend = (i - valueX0) * frequency;
                for (int j = 0; j < height; j++)
                {
                    int valueY0 = (j / period) * period;
                    int valueY1 = (valueY0 + period) % height;
                    float Yblend = (j - valueY0) * frequency;

                    float top = InterpLinear(baseNoise[valueX0][valueY0], baseNoise[valueX1][valueY0], Xblend);
                    float bottom = InterpLinear(baseNoise[valueX0][valueY1], baseNoise[valueX1][valueY1], Xblend);
                    smoothNoise[i][j] = InterpLinear(top, bottom, Yblend);
                }
            }
            return smoothNoise;
        }

        public float[][] GeneratePerlinNoise()
        {
            return GeneratePerlinNoise(GenerateWhiteNoise(), 8);
        }

        private float[][] GeneratePerlinNoise(float[][] baseNoise, int octaveCount)
        {
            float[][][] smoothNoise = new float[octaveCount][][];
            float persistance = 0.5f;

            for(int i = 0; i < octaveCount; i++)
            {
                smoothNoise[i] = GenerateSmoothNoise(baseNoise, i);
            }

            float[][] perlinNoise = GetEmptyArray(width, height);
            float amplitude = 1.0f;
            float totalAmp = 0.0f;

            for (int octave = octaveCount - 1; octave >= 0; octave--)
            {
                amplitude *= persistance;
                totalAmp += amplitude;
                for (int i = 0; i < width; i++)
                    for (int j = 0; j < height; j++)
                        perlinNoise[i][j] += smoothNoise[octave][i][j] * amplitude;
                //normalization

                
            }
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    perlinNoise[i][j] /= totalAmp;
            return perlinNoise;

        }
        #endregion

        #region ImageManip

        public Texture2D AddGradient(Texture2D baseTexture, Color gradientStart, Color gradientEnd)
        {
            return ConvertArrayToTexture(MapGradient(gradientStart, gradientEnd, ConvertTextureToArray(baseTexture)));
        }

        private Color GetColor(Color gradientStart, Color gradientEnd, float weight)
        {
            Color color = new Color
                (
                (int)(gradientStart.R * (1 - weight) + gradientEnd.R * weight),
                (int)(gradientStart.G * (1 - weight) + gradientEnd.G * weight),
                (int)(gradientStart.B * (1 - weight) + gradientEnd.B * weight)
                );
            return color;
        }

        private Color[][] MapGradient(Color gradientStart, Color gradientEnd, Color[][] noise)
        {
            width = noise.Length;
            height = noise[0].Length;
            
            Color[][] gradient = GetEmptyColorArray(width, height);
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    gradient[i][j] = GetColor(gradientStart, gradientEnd, noise[i][j].R);
            return gradient;
        }

        public Texture2D BlendImages(Texture2D texture1, Texture2D texture2, Texture2D mask)
        {
            return ConvertArrayToTexture(BlendImages(ConvertTextureToArray(texture1), ConvertTextureToArray(texture2), ConvertTextureToArray(mask)));
        }

        private Color[][] BlendImages(Color[][] image1, Color[][] image2, Color[][] noise)
        {
            width = image1.Length;
            height = image1[0].Length;

            Color[][] image = GetEmptyColorArray(width, height);
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    image[i][j] = InterpLinearColor(image1[i][j], image2[i][j], noise[i][j].R);
            return image;
        }

        #endregion
    }
}
