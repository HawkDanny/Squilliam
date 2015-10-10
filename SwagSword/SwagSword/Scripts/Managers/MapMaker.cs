#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
#endregion

//Ryan Bell

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
        //Boolean that toggles whether the entire map is generated or if it is only shown as rectangles. This allows the art developer to skip loading times
        bool PetersVeryOwnArtDevelopmentVariableForTheAdvancementOfAssetsInAnIndependantGame0000AStudyOfTheAggregateCyclesOfTheAverageArtDeveloper;
        #endregion

        #region Contructor
        /// <summary>
        /// sets the applicable variables to the params given 
        /// </summary>
        public MapMaker(int r, int ts, int rw, int rh, GraphicsDevice graphicsDevice, Game1 mainMan)
        {
            resWidth = rw;
            resHeight = rh;
            radius = r;                                                                     //hardcode in the stronghold radius
            pathThickness = 430;                                                            //hardcode in the path thickness
            this.graphicsDevice = graphicsDevice;                                   
            rand = new Random();                                                            //instantiate a new random object
            this.mainMan = mainMan;                                                         //set the reference to the mainManager
        }
        #endregion

        /// <summary>
        /// Public facing method that runs through the steps in making the map
        /// and interfaces a lot iwth the perlin noise class of which it makes an object of
        /// It handles the creation of the pathways, background, and stronghold circles.
        /// </summary>
        public Texture2D MakeMap()
        {
            PerlinNoise noiseGen = null;
            PetersVeryOwnArtDevelopmentVariableForTheAdvancementOfAssetsInAnIndependantGame0000AStudyOfTheAggregateCyclesOfTheAverageArtDeveloper = false;
            if (!PetersVeryOwnArtDevelopmentVariableForTheAdvancementOfAssetsInAnIndependantGame0000AStudyOfTheAggregateCyclesOfTheAverageArtDeveloper)
            {
                noiseGen = new PerlinNoise(resWidth, resHeight, rand, graphicsDevice);
                noiseOffset = noiseGen.NoiseToTexture();
                pathMask = MergeTextures(ShiftPathHorizontal(BasePathHorizontal(), noiseOffset), ShiftPathVertical(BasePathVertical(), noiseOffset));
                pathMask = MergeTextures(pathMask, NoiseyCircle());
                noiseOffset = noiseGen.AdjustConstrast(noiseOffset, 2);
                noiseOffset = noiseGen.AddGradient(noiseOffset, Color.LightGray, Color.White);
                noiseOffset = noiseGen.BlendImages(mainMan.DrawMan.SandyTexture, mainMan.DrawMan.GrassTexture, noiseOffset);
            }
            else
            {
                Point LeftCenterPoint = new Point(radius, resHeight / 2);
                Point RightCenterPoint = new Point(resWidth - radius, resHeight / 2);
                Point UpperCenterPoint = new Point(resWidth / 2, radius);
                Point LowerCenterPoint = new Point(resWidth / 2, resHeight - radius);
                mainMan.GameMan.MapMan.LeftCenterPoint = LeftCenterPoint;
                mainMan.GameMan.MapMan.RightCenterPoint = RightCenterPoint;
                mainMan.GameMan.MapMan.UpperCenterPoint = UpperCenterPoint;
                mainMan.GameMan.MapMan.LowerCenterPoint = LowerCenterPoint;
            }
            //Boundary set
            mainMan.GameMan.CenterBound = new Rectangle(resWidth / 2 - pathThickness / 2, resHeight / 2 - pathThickness / 2, pathThickness, pathThickness);
            mainMan.GameMan.LeftPathBound = new Rectangle(radius * 2 - 40, resHeight / 2 - pathThickness / 2, (resWidth - 4 * radius + 80) / 2 - pathThickness / 2, pathThickness);
            mainMan.GameMan.RightPathBound = new Rectangle(mainMan.GameMan.CenterBound.X + pathThickness, mainMan.GameMan.CenterBound.Y, (resWidth - 4 * radius + 80) / 2 - pathThickness / 2, pathThickness);
            mainMan.GameMan.TopPathBound = new Rectangle(resHeight / 2 - pathThickness / 2, radius * 2 - 40, pathThickness, (resWidth - radius * 4 + 80) / 2 - pathThickness / 2);
            mainMan.GameMan.LowerPathBound = new Rectangle(resHeight / 2 - pathThickness / 2, mainMan.GameMan.CenterBound.Y + pathThickness, pathThickness, (resWidth - radius * 4 + 80) / 2 - pathThickness / 2);

            if (!PetersVeryOwnArtDevelopmentVariableForTheAdvancementOfAssetsInAnIndependantGame0000AStudyOfTheAggregateCyclesOfTheAverageArtDeveloper)
                return noiseGen.BlendImages(noiseOffset, mainMan.DrawMan.PathwayTexture, pathMask);
            else
                return mainMan.DrawMan.DefaultMap;
            
        }


        #region BasePaths
        /// <summary>
        /// This method creates the basic horizontal pathways on the map as rectangles and saves the info as an alpha map
        /// </summary>
        /// <returns>returns a Texture 2D of the alpha</returns>
        private Texture2D BasePathVertical()
        {
            Texture2D HPB = new Texture2D(graphicsDevice, resWidth, resHeight);             //A new Texture2d to hold the base path values
            Color[] colorData = new Color[resWidth * resHeight];                            //a one dimensional array to allow for the easy mainpulation of the colors
            for(int i = radius * 2 - 40; i < resWidth - radius * 2 + 40; i++)               //doubly nested for loops that iterate within horizontal and veritcal restraints
            {                                                                               //they leave buffer room on the left and right for the circles and are only as tall 
                for (int j = ((resHeight / 2) - (pathThickness / 2)); j < ((resHeight / 2) + (pathThickness / 2)); j++)//as the base path width set above
                {
                    colorData[i * resHeight + j] = Color.Red;
                }
            }
            HPB.SetData<Color>(colorData);                                                  //set the color of the texture2d to the values of the alpha map
            return HPB;                                                                     //return the alpha map
        }
        /// <summary>
        /// THis method creates the basic vertical patheays on the map as rectangles and saves the info as an alpha map
        /// </summary>
        /// <returns>Reeturns a Texture2D of the alpha</returns>
        private Texture2D BasePathHorizontal()
        {
            Texture2D VPB = new Texture2D(graphicsDevice, resWidth, resHeight);             //A new atexture2D to hold the base path values
            Color[] colorData = new Color[resWidth * resHeight];                            //a one dimmensional array to allow for the easy manipulation of the colors
            for (int i = ((resHeight / 2) - (pathThickness / 2)); i < ((resHeight / 2) + (pathThickness / 2)); i++)
            {                                                                               //doubly nested for loops that iterate within horizontal and vertical restriants
                for (int j = radius * 2 - 40; j < resHeight - radius * 2 + 40; j++ )        //the two loops are essentially just flipped as compared to the horizontal method above
                {
                    colorData[i * resHeight + j] = Color.Red;
                }
            }
            VPB.SetData<Color>(colorData);                                                  //set the color of the texture2d to the values of the alpha map
            return VPB;                                                                     //return the alpha map
        }
        #endregion

        #region ShiftPaths
        /// <summary>
        /// This method adds a perlin noise based edge to the base path rectangles to make it look more like old pathways
        /// </summary>
        /// <param name="subject">This is the map alpha mask to be added to</param>
        /// <param name="noise">THis is the custom made perlin noise texture that is used throughout the game</param>
        /// <returns>The updated alpha map</returns>
        private Texture2D ShiftPathHorizontal(Texture2D subject, Texture2D noise)
        {
            int topRow = ((resHeight / 2) - (pathThickness / 2));                           //the top of the base path position
            int bottomRow = ((resHeight / 2) + (pathThickness / 2));                        //the bottom of the base path position
            Color[] subjectData = new Color[subject.Width * subject.Height];                //a one dimmensional array that will be used to manipulate 
            subject.GetData<Color>(subjectData);                                            //save the pixel data from the subject alpha mask into the array
            Color[,] subData2D = new Color[subject.Width,subject.Height];                   //a 2D array that will hold the 1D array pixel info
            Color[] noiseData = new Color[noise.Width * noise.Height];                      //a 1D array to collect the perlin noise color data
            noise.GetData<Color>(noiseData);                                                //save the perlin noise texture data into the 1D array
            Color[,] noiseData2D = new Color[noise.Width, noise.Height];                    //a 2d array that will hold the 1d array perlin pixel data
            for(int i = 0; i < subject.Width; i++)                                          //a doubly nested for loop to iterate overevery pixel
                for (int j = 0; j < subject.Height; j++)                                    //This is where the 1 dimmensional arrays are converted into 
                {                                                                           //two dimmensional arrays
                    subData2D[i, j] = subjectData[i * subject.Height + j];
                    noiseData2D[i, j] = noiseData[i * noise.Height + j];
                }
            Color maskColor = Color.Black;                                                  //setting the mask color that will be used in the falloff calcs
            int Tfalloff;
            int Bfalloff;
            for(int x = radius; x < subject.Width - radius; x++)                            //a for loop to iterate over the length of the path not including what
            {                                                                               //is covered up by the stonghold circles
                int topShift = noiseData2D[x, 1].R / 2;                                     //calcualate the pixel values shifts for each pixel along the way
                int bottomShift = noiseData2D[x, 50].R / 2;
                Tfalloff = 255 / topShift;
                Bfalloff = 255 / bottomShift;
                maskColor = Color.Black;
                for(int y = topRow - topShift; y < topRow; y++)                             //start at the farthest point out from the base path an iterate inward
                {
                    subData2D[y, x] = maskColor;                                            //set the color at that point to the current mask color
                    if (y < (topRow - topShift) + 10)                                       //check that the y range is still within the falloff area
                        maskColor.R += 25;                                                  //increase the opaqueness of the alpha by 10 percent
                }
                maskColor = Color.Black;                                                    //reset the mask color to black
                for(int y = bottomRow + bottomShift; y >= bottomRow; y--)                   //start at the farthes point out an iterate inward
                {
                    subData2D[y, x] = maskColor;                                            //set the color at that point to the current mask color
                    if (y > (bottomRow + bottomShift) - 10)                                 //check that the y range is still with the falloff area
                        maskColor.R += 25;                                                  //increase the opaqueness fo the alpha by 10 percent
                }
            }
            for (int i = 0; i < subject.Width; i++)                                         //a doubly nested for loop to iterate over all of the pixels
                for (int j = 0; j < subject.Height; j++)
                    subjectData[i * subject.Height + j] = subData2D[i, j];                  //converting the 2 dimmensional arrays back into one dimmensional arrays
            subject.SetData<Color>(subjectData);                                            //set the color values of the alpha mask to the updated one dimmmensional array
            return subject;                                                                 //return the updated alpha mask
        }

        /// <summary>
        /// This method adds a perlin noise based edge to the base path rectangles to make it look more like old pathways
        /// </summary>
        /// <param name="subject">This is the map alpha mask to be added to</param>
        /// <param name="noise">THis is the custom made perlin noise texture that is used throughout the game</param>
        /// <returns>The updated alpha map</returns>
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
        /// This method handles the process of combining two textures into one. It only handles alpha masks and uses 
        /// an additive method for the merging.
        /// </summary>
        /// <param name="layer1">The first texture to be merrged</param>
        /// <param name="layer2">The second texture to be merged/param>
        /// <returns>A new texture that is the result of merging the two passed in textures</returns>
        private Texture2D MergeTextures(Texture2D layer1, Texture2D layer2)
        {
            Color[] layer1Data = new Color[layer1.Width * layer1.Height];                   //create color arrays that will hold the color values of each of the
            Color[] layer2Data = new Color[layer2.Width * layer2.Height];                   //two passed in textures. This makes it faster to retrieve colors.
            layer1.GetData<Color>(layer1Data);                                              //set the color array to the pixel colors of the passed in textures
            layer2.GetData<Color>(layer2Data);  
            for(int i = 0; i < layer1Data.Length; i++)                                      //loop through all of the colors in the array and set the color of 
            {                                                                               //the first array at that point to the sum of the two parts after it
                layer1Data[i].R = (byte)(MathHelper.Clamp(layer1Data[i].R + layer2Data[i].R, 0, 255));//has been clamped to values between 0 and 255.
            }
            layer1.SetData<Color>(layer1Data);                                              //set the colors of the first texture to the new values in the array
            return layer1;                                                                  //return the updated texture
        }
        
        /// <summary>
        /// This method handles the drawing of each of the surronding circles for the strongholds. It calculates
        /// The position ofeach of the center points and passed the values along to the mapManger as well. Then
        /// It sets the alpha mask map to include the circles
        /// </summary>
        /// <returns>Returns the updates alpha mask</returns>
        private Texture2D NoiseyCircle()
        {
            Point LeftCenterPoint = new Point(radius, resHeight / 2);                       //calculation of the four center points based on the width of the map
            Point RightCenterPoint = new Point(resWidth - radius, resHeight / 2);           //...
            Point UpperCenterPoint = new Point(resWidth / 2, radius);                       //...
            Point LowerCenterPoint = new Point(resWidth / 2, resHeight - radius);           //...
            mainMan.GameMan.MapMan.LeftCenterPoint = LeftCenterPoint;                       //set the reference to these four points in the MapManager's appropiate properties
            mainMan.GameMan.MapMan.RightCenterPoint = RightCenterPoint;
            mainMan.GameMan.MapMan.UpperCenterPoint = UpperCenterPoint;
            mainMan.GameMan.MapMan.LowerCenterPoint = LowerCenterPoint;
            Texture2D circleMask = new Texture2D(graphicsDevice, resWidth, resHeight);      //Create a texture2D that will hold the alpha colors for the circles
            Color[,] colorData2D = new Color[resWidth, resHeight];                          //create an array of colors to make the changing of these colors faster
            
            
            for (int i = 0; i < resWidth; i++)                                              //double nested for loops to iterate through each pixel on the map
            {
                for (int j = 0; j < resHeight; j++)
                {
                    if(CalcDistance(i, j, LeftCenterPoint.X, LeftCenterPoint.Y) < radius)   //for each pixel, check if the distance between it and the left center point
                    {                                                                       //is less than the radius. If so, set the color at that point in the color array
                        colorData2D[i, j].R = 255;                                          //to be white.
                    }
                    if (CalcDistance(i, j, RightCenterPoint.X, RightCenterPoint.Y) < radius)//same check for right center point
                    {
                        colorData2D[i, j].R = 255;
                    }
                    if (CalcDistance(i, j, UpperCenterPoint.X, UpperCenterPoint.Y) < radius)//same check for upper center point
                    {
                        colorData2D[i, j].R = 255;
                    }
                    if (CalcDistance(i, j, LowerCenterPoint.X, LowerCenterPoint.Y) < radius)//same check for lower center point
                    {
                        colorData2D[i, j].R = 255;
                    }
                }
            }

            Color[] colorData = new Color[resHeight * resWidth];                            //create a one-dimensional array with the same length as the 2d array
            for (int i = 0; i < resWidth; i++)
                for (int j = 0; j < resHeight; j++)
                    colorData[i * resHeight + j] = colorData2D[i, j];                       //loop through all of the pixels in the 2d color array and convert it to a 1d array
            circleMask.SetData<Color>(colorData);                                           //use the 1d array to set the color of the circle mask
            //return ShiftPathHorizontal(ShiftPathVertical(circleMask, noise), noise);
            return circleMask;                                                              //return the circle mask
        }

        /// <summary>
        /// Method to calculate distance between two points in terms of pixels
        /// </summary>
        /// <param name="a">The first point</param>
        /// <param name="b">The second point</param>
        /// <returns></returns>
        private double CalcDistance(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow((a.X - b.X), 2) + Math.Pow((a.Y - b.Y), 2));
        }
        /// <summary>
        /// A method to calculate the distance between two points, in pixels, using x and y coordinates
        /// </summary>
        /// <param name="aX">The x coord of the first point</param>
        /// <param name="aY">The y coord of the first point</param>
        /// <param name="bX">The x coord of the second point</param>
        /// <param name="bY">The y coord of the second point</param>
        /// <returns></returns>
        private double CalcDistance(int aX, int aY, int bX, int bY)
        {
            return Math.Sqrt(Math.Pow((aX - bX), 2) + Math.Pow((aY - bY), 2));
        }

    }
}
