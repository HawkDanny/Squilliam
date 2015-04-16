using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwagSword
{
    class PerlinNoise
    {
        private int width;
        private int height;
        Random rand;

        private float[][] GetEmptyArray(int width, int height)
        {
            float[][] returned = new float[width][];
            for(int i = 0; i < width; i++)
            {
                returned[i] = new float[height];
            }
            return returned;
        }

        public PerlinNoise(int w, int h, Random r)
        {
            width = w;
            height = h;
            rand = r;
        }
        public float[][] Init()
        {
            return GeneratePerlinNoise(GenerateWhiteNoise(), 8);
        }

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

        public float[][] GeneratePerlinNoise(float[][] baseNoise, int octaveCount)
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

        #region HelperFunctions
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
        #endregion
    }
}
