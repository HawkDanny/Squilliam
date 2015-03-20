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
using Microsoft.Xna.Framework.GamerServices;
#endregion

//Names: Ryan Bell

namespace SwagSword
{
    /// <summary>
    /// Will draw the map, and hold a reference to it to be used by other classes such as spawn manager
    /// </summary>
    public class MapManager : Manager
    {
        #region Fields
        Tile[,] map;                                                        //This is the base level map. It is a 2d array of Tile objects
                                                                            //  -It holds only the arrangment of pathways and not pahtways
        int tileSize;                                                       //The size in pixels of one tile (width and height)
        int mapWidth;                                                       //This is the map width in tiles. It should be odd if possible
        int mapHeight;                                                      //Map height in tiles. It should also be odd if possible
        #endregion

        public int TileSize { get { return tileSize; } }
        public int MapWidth { get { return mapWidth; } }
        public int MapHeight { get { return mapHeight; } }

        /// <summary>
        /// Default Constructor sets up reference to the main manager
        /// </summary>
        /// <param name="mainMan"></param>
        public MapManager(Game1 mainMan)
            : base(mainMan)
        {

        }

        /// <summary>
        /// Method that is called upon instantiation of this class.
        /// (occurs before content is loaded)
        /// assigns values to basic fields
        /// </summary>
        public override void Init()
        {
            tileSize = 64;
            mapWidth = 61;
            mapHeight = 39;
            map = new Tile[mapWidth, mapHeight];

        }


        /// <summary>
        /// This is called right after content is loaded in game1.cs
        /// It is used to carry out functions that should occur before anything is drawn
        /// It begins the process of the creation of the base map
        /// </summary>
        public void Startup()
        {
            PopulateMap();
            generateSimplePath();
            BoldenMap();
            BoldenMap();
        }

        /// <summary>
        /// Fills the map with Tiles that have notPathway textures and appropiatly calculated centers
        /// </summary>
        void PopulateMap()
        {
            for(int x = 0; x < mapWidth; x++)                                                   //Iterate through the grid in x
                for (int y = 0; y < mapHeight; y++)                                             //and y direction
                {
                    //at the current position in the map grid,
                    //assign a new tile with the 'notPathway' texture.
                    //the center is calculated with an initial value of one half tile size,
                    //plus a full size for each additional tile in either direction
                    map[x, y] = new Tile(mainMan.DrawMan.NotPathwayTexture, new Point(tileSize * x + (tileSize / 2), tileSize * y + (tileSize / 2)));
                }
        }

        /// <summary>
        /// This method takes a tile in the map and returns a list of all adjacent tiles (including diagonally)
        /// It is neccessary for spawning branches
        /// It also avoids null exceptions when at the edges or corners of the map
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        List<Tile> getNeighbors(Tile subject)
        {
            //recalculate the position in the map grid based on the center attribute of the given tile (which is in pixels)
            Point positionInMap = new Point((subject.Center.X - (tileSize / 2)) / tileSize, (subject.Center.Y - (tileSize / 2)) / tileSize);

            List<Tile> neighbors = new List<Tile>();                                            //instantiate list that will hold adjacent tiles
                                                                                                //  -this is the returned value

            if (positionInMap.X > 0 && positionInMap.X < mapWidth - 1)                          //check subject tile is not on either vert edge
            {
                if (positionInMap.Y > 0 && positionInMap.Y < mapHeight - 1)                     //check subject tile is not on either horz edge
                {
                    //normal 8 neighbors

                    for (int change = -1; change <= 1; change++)                                //iterate for the above and below row 
                    {
                        neighbors.Add(map[positionInMap.X + change, positionInMap.Y - 1]);      //add the 3 tiles directly above the subject to list
                        neighbors.Add(map[positionInMap.X + change, positionInMap.Y + 1]);      //add the 3 tiles directly below the subject to list
                    }
                                                                                                //add 2 other points
                    neighbors.Add(map[positionInMap.X - 1, positionInMap.Y]);                   //the point directly to the right
                    neighbors.Add(map[positionInMap.X + 1, positionInMap.Y]);                   //the point directly to the left
                }
                else if (positionInMap.Y > 0)                                                   //rule out the subject is on top edge
                {
                    //at botttom edge return 5 neighbors
                    for (int change = -1; change <= 0; change++)                                //iterate for the left and right short columns
                    {
                        neighbors.Add(map[positionInMap.X + 1, positionInMap.Y + change]);      //add the 2 tiles to the right of subject to list
                        neighbors.Add(map[positionInMap.X - 1, positionInMap.Y + change]);      //add the 2 tiles to the left of subject to list
                    }
                    neighbors.Add(map[positionInMap.X, positionInMap.Y - 1]);                   //add the tile directly above to the list
                }
                else if (positionInMap.Y < mapHeight - 1)                                       //rule out the subject is on the lower edge
                {
                    //at upper edge return 5 neighbors
                    for (int change = 0; change <= 1; change++)                                 //iterate for the left and right short columns
                    {
                        neighbors.Add(map[positionInMap.X + 1, positionInMap.Y + change]);      //add the 2 tiles to the right of subject to list
                        neighbors.Add(map[positionInMap.X - 1, positionInMap.Y + change]);      //add the 2 tiles to the left of subject to list
                    }
                    neighbors.Add(map[positionInMap.X, positionInMap.Y + 1]);                   //add the tile directly below to the list
                }
            }
            else if (positionInMap.X > 0)                                                       //if the initial check for vertical walls failed 
            {                                                                                   //    -then rule out subject is on left wall
                if (positionInMap.Y > 0 && positionInMap.Y < mapHeight - 1)                     //check the subject is not also on either horz wall 
                {
                    //far right edge return 5 neighbors
                    for (int change = -1; change <= 0; change++)                                //iterate for the 2 short rows
                    {
                        neighbors.Add(map[positionInMap.X + change, positionInMap.Y + 1]);      //add the 2 tiles below to the list
                        neighbors.Add(map[positionInMap.X + change, positionInMap.Y - 1]);      //add the 2 tiles above to the list
                    }
                    neighbors.Add(map[positionInMap.X - 1, positionInMap.Y]);                   //add the tile directly to the left to list
                }
                else if (positionInMap.Y > 0)                                                   //rule out subject is on upper vert
                {
                    //far right lower corner return 3 neighbors
                    neighbors.Add(map[positionInMap.X - 1, positionInMap.Y]);                   //left tile
                    neighbors.Add(map[positionInMap.X - 1, positionInMap.Y - 1]);               //upper left diagonal tile
                    neighbors.Add(map[positionInMap.X, positionInMap.Y - 1]);                   //upper tile
                }
                else if (positionInMap.Y < mapHeight - 1)                                       //rule out subject is on lower vert
                {
                    //far right upper corner return 3 neighbors
                    neighbors.Add(map[positionInMap.X - 1, positionInMap.Y]);                   //left tile
                    neighbors.Add(map[positionInMap.X - 1, positionInMap.Y + 1]);               //lower left diagonal tile
                    neighbors.Add(map[positionInMap.X, positionInMap.Y + 1]);                   //lower tile
                }
            }
            else if (positionInMap.X < mapWidth - 1)                                            //if the initial check for vertical walls failed
            {                                                                                   //    -then rule out subject is on right wall
                if (positionInMap.Y > 0 && positionInMap.Y < mapHeight - 1)                     //check the subject is not also on either horz wall
                {
                    //far left edge return 5 neighbors
                    for (int change = -1; change <= 1; change++)                                //iterate over the right column
                    {
                        neighbors.Add(map[positionInMap.X + 1, positionInMap.Y + change]);      //add the 3 tiles to the right
                    }
                    neighbors.Add(map[positionInMap.X, positionInMap.Y + 1]);                   //add the tile directly below to the list
                    neighbors.Add(map[positionInMap.X, positionInMap.Y - 1]);                   //add the tile directly above to the list
                }
                else if (positionInMap.Y > 0)                                                   //rule out subject is on upper vert
                {
                    //far left lower corner return 3 neighbors
                    neighbors.Add(map[positionInMap.X + 1, positionInMap.Y]);                   //right tile
                    neighbors.Add(map[positionInMap.X + 1, positionInMap.Y - 1]);               //upper right diagonal tile
                    neighbors.Add(map[positionInMap.X, positionInMap.Y - 1]);                   //upper tile

                }
                else if (positionInMap.Y < mapHeight - 1)                                       //rule out subject is on lower vert
                {
                    //far left upper corner return 3 neighbors
                    neighbors.Add(map[positionInMap.X, positionInMap.Y + 1]);                   //lower tile
                    neighbors.Add(map[positionInMap.X + 1, positionInMap.Y]);                   //right tile
                    neighbors.Add(map[positionInMap.X + 1, positionInMap.Y + 1]);               //lower right diagonal tile
                }
            }
            return neighbors;                                                                   //return the lsit of valid adjacent neighbors
        }

        /// <summary>
        /// this method provides some simple distance calculation 2 points
        /// used with the center points of 2 tiles
        /// </summary>
        /// <param name="a0"></param>
        /// <param name="a1"></param>
        /// <returns></returns>
        protected double CalcDistance(Point a0, Point a1)
        {
            return Math.Sqrt(Math.Pow(a1.X - a0.X, 2) + Math.Pow(a1.Y - a0.Y, 2));              //the distance formula
        }


        void generateSimplePath()
        {
            Random rand = new Random();
            Tile origin = map[mapWidth / 2, mapHeight / 2];
            origin.Texture = mainMan.DrawMan.PathwayTexture;
            Tile subject = map[mapWidth / 2, mapHeight / 2 - 1];
            for (int i = 0; i < 23; i++)
            {
                subject.Texture = mainMan.DrawMan.PathwayTexture;
                List<Tile> neighbors = getNeighbors(subject);
                List<Tile> bestNeighbors = new List<Tile>();
                
                foreach(Tile t in neighbors)
                {
                    if (t.Center.Y < subject.Center.Y)
                    {
                        bestNeighbors.Add(t);
                    }
                }
                int choice = 0;
                if (bestNeighbors.Count > 1)
                    choice = rand.Next(bestNeighbors.Count - 1);
                if (bestNeighbors.Count > 0)
                    subject = bestNeighbors.ElementAt(choice);
            }
            subject = origin;
            for (int i = 0; i < 23; i++)
            {
                subject.Texture = mainMan.DrawMan.PathwayTexture;
                List<Tile> neighbors = getNeighbors(subject);
                List<Tile> bestNeighbors = new List<Tile>();

                foreach (Tile t in neighbors)
                {
                    if (t.Center.Y > subject.Center.Y)
                    {
                        bestNeighbors.Add(t);
                    }
                }
                int choice = 0;
                if (bestNeighbors.Count > 1)
                    choice = rand.Next(bestNeighbors.Count - 1);
                if (bestNeighbors.Count > 0)
                    subject = bestNeighbors.ElementAt(choice);
            }
            subject = origin;
            for (int i = 0; i < 33; i++)
            {
                subject.Texture = mainMan.DrawMan.PathwayTexture;
                List<Tile> neighbors = getNeighbors(subject);
                List<Tile> bestNeighbors = new List<Tile>();

                foreach (Tile t in neighbors)
                {
                    if (t.Center.X < subject.Center.X)
                    {
                        bestNeighbors.Add(t);
                    }
                }
                int choice = 0;
                if (bestNeighbors.Count > 1)
                    choice = rand.Next(bestNeighbors.Count - 1);
                if (bestNeighbors.Count > 0)
                    subject = bestNeighbors.ElementAt(choice);
            }
            subject = origin;
            for (int i = 0; i < 33; i++)
            {
                subject.Texture = mainMan.DrawMan.PathwayTexture;
                List<Tile> neighbors = getNeighbors(subject);
                List<Tile> bestNeighbors = new List<Tile>();

                foreach (Tile t in neighbors)
                {
                    if (t.Center.X > subject.Center.X)
                    {
                        bestNeighbors.Add(t);
                    }
                }
                int choice = 0;
                if (bestNeighbors.Count > 1)
                    choice = rand.Next(bestNeighbors.Count - 1);
                if (bestNeighbors.Count > 0)
                    subject = bestNeighbors.ElementAt(choice);
            }



        }


        /// <summary>
        /// This method runs through the map and widens the paths.
        /// It adds tiles down and to the right to work with all 
        /// four branches and the differing directions
        /// </summary>
        protected void BoldenMap()
        {
            for (int x = mapWidth - 2; x >= 0; x--)                                                  //start at the lower right corner and iterate
            {                                                                                       //  up to the upper left corner 
                for (int y = mapHeight - 1; y >= 0; y--)                                            //...
                {
                    if (map[x, y].Texture == mainMan.DrawMan.PathwayTexture)                         //check that the current tile is a pathway
                    {
                        if (x < mapWidth + 2)                                                       //check bounds to avoid null exception
                        {
                            map[x + 1, y].Texture = mainMan.DrawMan.PathwayTexture;                 //make right tile a path as well
                            if (y < mapHeight - 2)                                                  //check bounds to avoid null exception
                            {
                                map[x + 1, y + 1].Texture = mainMan.DrawMan.PathwayTexture;         //make lower right tile a path as well
                            }
                        }
                        if (y < mapHeight - 2)                                                      //check bounds to avoid null exception
                        {
                            map[x, y + 1].Texture = mainMan.DrawMan.PathwayTexture;                 //make the lower tile a path as well
                        }

                    }
                }
            }
        }

        //**************************************************************************************************************************

        public void Draw(SpriteBatch spriteBatch)
        {
            
            
            foreach (Tile t in map)
            {
                Rectangle r = new Rectangle(t.Center.X - tileSize / 2, t.Center.Y - tileSize / 2,  tileSize, tileSize);
                
                spriteBatch.Draw(t.Texture, r, Color.White);
            }

        }






    }
}