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

namespace SwagSword
{
    /// <summary>
    /// Will draw the map, and hold a reference to it to be used by other classes such as spawn manager
    /// </summary>
    public class MapManager : Manager
    {
        Tile[,] map;
        int tileSize;
        int mapWidth;
        int mapHeight;

        public MapManager(Game1 mainMan)
            : base(mainMan)
        {

        }

        //Init
        public override void Init()
        {
            tileSize = 64;
            mapWidth = 31;
            mapHeight = 19;
            map = new Tile[mapWidth, mapHeight];
        }

        public void Startup()
        {
            PopulateMap();
            generateSimplePath();
            BoldenMap();
        }

        void PopulateMap()
        {
            for(int x = 0; x < mapWidth; x++)
                for (int y = 0; y < mapHeight; y++)
                {
                    map[x, y] = new Tile(mainMan.DrawMan.NotPathwayTexture, new Point(tileSize * x + (tileSize / 2), tileSize * y + (tileSize / 2)));
                }
            foreach (Tile t in map)
            {
                Console.WriteLine(t.Texture.Width);
            }
        }

        List<Tile> getNeighbors(Tile subject)
        {
            Point positionInMap = new Point((subject.Center.X - (tileSize / 2)) / tileSize, (subject.Center.Y - (tileSize / 2)) / tileSize);
            List<Tile> neighbors = new List<Tile>();

            if (positionInMap.X > 0 && positionInMap.X < mapWidth - 1)
            {
                if (positionInMap.Y > 0 && positionInMap.Y < mapHeight - 1)
                {
                    //normal 8 neighbors
                    //iterate for the above row and below row 
                    for (int change = -1; change <= 1; change++)
                    {
                        neighbors.Add(map[positionInMap.X + change, positionInMap.Y - 1]);
                        neighbors.Add(map[positionInMap.X + change, positionInMap.Y + 1]);
                    }
                    //add 2 other points
                    neighbors.Add(map[positionInMap.X - 1, positionInMap.Y]);
                    neighbors.Add(map[positionInMap.X + 1, positionInMap.Y]);
                }
                else if (positionInMap.Y > 0)
                {
                    //at botttom edge return 5 neighbors
                    for (int change = -1; change <= 0; change++)
                    {
                        neighbors.Add(map[positionInMap.X + 1, positionInMap.Y + change]);
                        neighbors.Add(map[positionInMap.X - 1, positionInMap.Y + change]);
                    }
                    neighbors.Add(map[positionInMap.X, positionInMap.Y - 1]);
                }
                else if (positionInMap.Y < mapHeight - 1)
                {
                    //at upper edge return 5 neighbors
                    for (int change = 0; change <= 1; change++)
                    {
                        neighbors.Add(map[positionInMap.X + 1, positionInMap.Y + change]);
                        neighbors.Add(map[positionInMap.X - 1, positionInMap.Y + change]);
                    }
                    neighbors.Add(map[positionInMap.X, positionInMap.Y + 1]);
                }
            }
            else if (positionInMap.X > 0)
            {
                if (positionInMap.Y > 0 && positionInMap.Y < mapHeight - 1)
                {
                    //far right edge return 5 neighbors
                    for (int change = -1; change <= 0; change++)
                    {
                        neighbors.Add(map[positionInMap.X + change, positionInMap.Y + 1]);
                        neighbors.Add(map[positionInMap.X + change, positionInMap.Y - 1]);
                    }
                    neighbors.Add(map[positionInMap.X - 1, positionInMap.Y]);
                }
                else if (positionInMap.Y > 0)
                {
                    //far right lower corner return 3 neighbors
                    neighbors.Add(map[positionInMap.X - 1, positionInMap.Y]);
                    neighbors.Add(map[positionInMap.X - 1, positionInMap.Y - 1]);
                    neighbors.Add(map[positionInMap.X, positionInMap.Y - 1]);
                }
                else if (positionInMap.Y < mapHeight - 1)
                {
                    //far right upper corner return 3 neighbors
                    neighbors.Add(map[positionInMap.X - 1, positionInMap.Y]);
                    neighbors.Add(map[positionInMap.X - 1, positionInMap.Y + 1]);
                    neighbors.Add(map[positionInMap.X, positionInMap.Y + 1]);
                }
            }
            else if (positionInMap.X < mapWidth - 1)
            {
                if (positionInMap.Y > 0 && positionInMap.Y < mapHeight - 1)
                {
                    //far left edge return 5 neighbors
                    for (int change = -1; change <= 1; change++)
                    {
                        neighbors.Add(map[positionInMap.X + 1, positionInMap.Y + change]);
                    }
                    neighbors.Add(map[positionInMap.X, positionInMap.Y + 1]);
                    neighbors.Add(map[positionInMap.X, positionInMap.Y - 1]);
                }
                else if (positionInMap.Y > 0)
                {
                    //far left lower corner return 3 neighbors
                    neighbors.Add(map[positionInMap.X + 1, positionInMap.Y]);
                    neighbors.Add(map[positionInMap.X + 1, positionInMap.Y - 1]);
                    neighbors.Add(map[positionInMap.X, positionInMap.Y - 1]);

                }
                else if (positionInMap.Y < mapHeight - 1)
                {
                    //far left upper corner return 3 neighbors
                    neighbors.Add(map[positionInMap.X, positionInMap.Y + 1]);
                    neighbors.Add(map[positionInMap.X + 1, positionInMap.Y]);
                    neighbors.Add(map[positionInMap.X + 1, positionInMap.Y + 1]);
                }
            }
            return neighbors;
        }

        protected double CalcDistance(Point a0, Point a1)
        {
            return Math.Sqrt(Math.Pow(a1.X - a0.X, 2) + Math.Pow(a1.Y - a0.Y, 2));
        }


        void generateSimplePath()
        {
            Random rand = new Random();
            Tile origin = map[mapWidth / 2, mapHeight / 2];
            origin.Texture = mainMan.DrawMan.PathwayTexture;
            Tile subject = map[mapWidth / 2, mapHeight / 2 - 1];
            for (int i = 0; i < 12; i++)
            {
                subject.Texture = mainMan.DrawMan.PathwayTexture;
                List<Tile> neighbors = getNeighbors(subject);
                List<Tile> bestNeighbors = new List<Tile>();
                
                foreach(Tile t in neighbors)
                {
                    if (/*(CalcDistance(t.Center, origin.Center) >= 2 * tileSize - 1) &&*/ t.Center.Y < subject.Center.Y)
                    {
                        bestNeighbors.Add(t);
                    }
                }
                //origin = subject;
                int choice = 0;
                if (bestNeighbors.Count > 1)
                    choice = rand.Next(bestNeighbors.Count - 1);
                if (bestNeighbors.Count > 0)
                    subject = bestNeighbors.ElementAt(choice);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Rectangle re = new Rectangle(mainMan.WindowHalfWidth, mainMan.WindowHalfHeight, 64, 64);
            int vx =  (int)mainMan.GameMan.Players.ElementAt(0).Character.VelocityX;
            int vy = (int)mainMan.GameMan.Players.ElementAt(0).Character.VelocityY;
            int x = (int)mainMan.GameMan.Players.ElementAt(0).Character.X;
            int y = (int)mainMan.GameMan.Players.ElementAt(0).Character.Y;
            

            foreach (Tile t in map)
            {
                Rectangle r = new Rectangle(t.Center.X - tileSize / 2 - x, t.Center.Y - tileSize / 2 - y, tileSize, tileSize);
                spriteBatch.Draw(t.Texture, r, Color.White);
            }

        }

        protected void BoldenMap()
        {
            for (int x = mapWidth - 1; x >=0; x--)
            {
                for (int y = mapHeight - 1; y >= 0; y--)
                {
                    if (map[x,y].Texture == mainMan.DrawMan.PathwayTexture)
                    {
                        if (x < mapWidth + 2)
                        {
                            map[x + 1, y].Texture = mainMan.DrawMan.PathwayTexture;
                            if (y < mapHeight + 2)
                            {
                                map[x + 1, y + 1].Texture = mainMan.DrawMan.PathwayTexture;
                            }
                        }
                        if (y < mapHeight + 2)
                        {
                            map[x, y + 1].Texture = mainMan.DrawMan.PathwayTexture;
                        }

                    }
                }
            }
        }

    }
}








        /*
        //Fields
        Point origin;
        List<Point> leftBranch;
        List<Point> rightBranch;
        List<Point> upperBranch;
        List<Point> lowerBranch;

        int maxOffset;
        int numPoints;
        int length;
        int tileSize;
        int mapWidth;
        int mapHeight;
        int mapTileWidth;
        int mapTileHeight;
        Tile[,] map;

        public MapManager(Game1 mainMan)
            : base(mainMan)
        {


        }

        //Init
        public override void Init()
        {
            origin = new Point(mainMan.WindowHalfWidth, mainMan.WindowHalfHeight);
            maxOffset = 25;
            numPoints = 5;
            length = 100;
            tileSize = 64;
            mapTileWidth = 150;
            mapTileHeight = 100;
            mapWidth = mapTileWidth * tileSize;
            mapHeight = mapTileHeight * tileSize;
            rightBranch = generateBranch(maxOffset, numPoints, length, true);
            leftBranch = generateBranch(maxOffset, numPoints, length, true);
            lowerBranch = generateBranch(maxOffset, numPoints, length, false);
            upperBranch = generateBranch(maxOffset, numPoints, length, false);
            map = new Tile[mapTileWidth, mapTileHeight];
            fillGrid();
            //map[0, 0] = new Tile(mainMan.DrawMan.FirstTile);
            /*Console.WriteLine(rightBranch.Count);
            foreach (Point p in rightBranch)
            {
                Console.WriteLine(p.X + " " + p.Y);
            }
        }

        /// <summary>
        /// Main update method, called by mainMan
        /// </summary>
        public override void Update()
        {

        }

        protected List<Point> generateBranch(int maxOffset, int numPoints, int length, bool horizontal)
        {
            Random rand = new Random();
            List<Point> newbranch = new List<Point>();
            newbranch.Add(origin);
            int displacePoint;


            if (horizontal)
                for (int i = 1; i <= numPoints; i++)
                {
                    displacePoint = i * (length / numPoints);
                    int offset = rand.Next(maxOffset * 2) - maxOffset;
                    Point tempPoint = new Point(displacePoint, offset + mainMan.WindowHalfHeight);
                    newbranch.Add(tempPoint);
                }
            else
                for (int i = 1; i <= numPoints; i++)
                {
                    displacePoint = i * (length / numPoints);
                    int offset = rand.Next(maxOffset * 2) - maxOffset;
                    Point tempPoint = new Point(offset + mainMan.WindowHalfWidth, displacePoint);
                    newbranch.Add(tempPoint);
                }
            return newbranch;
        }

        protected void fillGrid()
        {
            List<Point> allBranch = new List<Point>();
            foreach (Point p in leftBranch)
                allBranch.Add(p);
            foreach (Point p in rightBranch)
                allBranch.Add(p);
            foreach (Point p in upperBranch)
                allBranch.Add(p);
            foreach (Point p in lowerBranch)
                allBranch.Add(p);
            for (int x = 0; x < mapTileWidth; x++)
            {
                for (int y = 0; y < mapTileHeight; y++)
                {
                    Point tempCenterTile = new Point((x + 1) * tileSize - (tileSize / 2), (y + 1) * tileSize - (tileSize / 2));
                    foreach (Point p in allBranch)
                        if (CalcDistance(tempCenterTile, p) < 20)
                        {
                            //map[x, y] = new Tile(mainMan.DrawMan.PathwayTexture);
                        }
                        else
                        {
                            //map[x, y] = new Tile(mainMan.DrawMan.NotPathwayTexture);
                        }
                }
            }
        }


        protected double CalcDistance(Point a0, Point a1)
        {
            return Math.Sqrt(Math.Pow(a1.X - a0.X, 2) + Math.Pow(a1.Y - a0.Y, 2));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle re = new Rectangle(mainMan.WindowHalfWidth, mainMan.WindowHalfHeight, 64, 64);
            foreach (Tile t in map)
            {
                //spriteBatch.Draw(t.Texture, re, Color.White);
            }

        }
    }
}

//greenish
//desert
//cave
//mechanical
//center is generic

/*
//interpolation math from other project
 * 
 * 
 * 
 public List<CustomPoint1D> Linear_Interpolate1(List<CustomPoint1D> controls, int numTweens)
{
    List<CustomPoint1D> interps = new List<CustomPoint1D>();
    for (int i = 0; i < controls.Count; i++)
    {
        double x;
        //interps.Add(controls.ElementAt(i));
        for (double j = 0; j < numTweens; j++)
        {
            CustomPoint1D pointA0 = controls.ElementAt(i);
            CustomPoint1D pointA1;
            if ((i + 1) >= controls.Count)
                pointA1 = controls.ElementAt(i);
            else
                pointA1 = controls.ElementAt(i + 1);
            x = (1 / numTweens) + (j / numTweens);
            Color colorA0 = controls.ElementAt(i).PointColor;
            Color colorA1;
            if ((i + 1) >= controls.Count)
                colorA1 = controls.ElementAt(i).PointColor;
            else
                colorA1 = controls.ElementAt(i + 1).PointColor;
            int a = (int)((1 - x) * colorA0.A + x * colorA0.A);
            int r = (int)((1 - x) * colorA0.R + x * colorA0.R);
            int g = (int)((1 - x) * colorA0.G + x * colorA0.G);
            int b = (int)((1 - x) * colorA0.B + x * colorA0.B);
            Color interpColor = Color.FromArgb(a, r, g, b);
            double interpX = pointA0.X + x * (pointA1.X - pointA0.X);
            double interpXW = (1 - x) * pointA0.XWeight + x * pointA1.XWeight;
            interps.Add(new CustomPoint1D(interpX,  interpXW, interpColor));
        }
        //interps.Add(new CustomPoint1D(controls.ElementAt(i + 1).X, controls.ElementAt(i + 1).XWeight, controls.ElementAt(i + 1).PointColor));
        //interps.Add(controls.ElementAt(i + 1));
    }

    return interps;
}

public List<CustomPoint1D> Cosine_Interpolate1(List<CustomPoint1D> controls, int numTweens)
{
    List<CustomPoint1D> interps = new List<CustomPoint1D>();
    for (int i = 0; i < controls.Count; i++)
    {
        double x;
        for (double j = 0; j < numTweens; j++)
        {
            CustomPoint1D pointA0 = controls.ElementAt(i);
            CustomPoint1D pointA1;
            if ((i + 1) >= controls.Count)
                pointA1 = controls.ElementAt(i);
            else
                pointA1 = controls.ElementAt(i + 1);
            x = (1 / numTweens) + (j / numTweens);
            Color colorA0 = controls.ElementAt(i).PointColor;
            Color colorA1;
            if ((i + 1) >= controls.Count)
                colorA1 = controls.ElementAt(i).PointColor;
            else
                colorA1 = controls.ElementAt(i + 1).PointColor;
            double ft = x * Math.PI;
            double f = (1 - Math.Cos(ft)) * 0.5;
            int a = (int)((1 - f) * colorA0.A + f * colorA1.A);
            int r = (int)((1 - f) * colorA0.R + f * colorA1.R);
            int g = (int)((1 - f) * colorA0.G + f * colorA1.G);
            int b = (int)((1 - f) * colorA0.B + f * colorA1.B);
            Color interpColor = Color.FromArgb(a, r, g, b);
            double interpX = pointA0.X + x * (pointA1.X - pointA0.X);
            double interpXW = (1 - f) * pointA0.XWeight + f * pointA1.XWeight;
            interps.Add(new CustomPoint1D(interpX, interpXW, interpColor));
        }
    }
    return interps;
}

public List<CustomPoint1D> Cubic_Interpolate1(List<CustomPoint1D> controls, int numTweens)
{
    List<CustomPoint1D> interps = new List<CustomPoint1D>();
    for (int i = 0; i < controls.Count - 1; i++)
    {
        double x;
        for (double j = 0; j < numTweens; j++)
        {
            CustomPoint1D pointA0;
            CustomPoint1D pointA1;
            CustomPoint1D pointA2;
            CustomPoint1D pointA3;
            Color colorA0;
            Color colorA1;
            Color colorA2;
            Color colorA3;
            x = (1 / numTweens) + (j / numTweens);

            if (i == 0 && controls.Count >= (i + 2))
            {
                //no a - 1 point
                pointA0 = controls.ElementAt(i);
                pointA1 = pointA0;
                pointA2 = controls.ElementAt(i + 1);
                pointA3 = controls.ElementAt(i + 2);
                colorA0 = controls.ElementAt(i).PointColor;
                colorA1 = pointA0.PointColor;
                colorA2 = controls.ElementAt(i + 1).PointColor;
                colorA3 = controls.ElementAt(i + 2).PointColor;
            }
            else if (i >= controls.Count - 2)
            {
                //no a + 2 point
                pointA0 = controls.ElementAt(i - 1);
                pointA1 = controls.ElementAt(i);
                pointA2 = controls.ElementAt(i + 1);
                pointA3 = controls.ElementAt(i + 1);
                colorA0 = controls.ElementAt(i - 1).PointColor;
                colorA1 = controls.ElementAt(i).PointColor;
                colorA2 = controls.ElementAt(i + 1).PointColor;
                colorA3 = controls.ElementAt(i + 1).PointColor;
            }
            else
            {
                pointA0 = controls.ElementAt(i - 1);
                pointA1 = controls.ElementAt(i);
                pointA2 = controls.ElementAt(i + 1);
                pointA3 = controls.ElementAt(i + 2);
                colorA0 = controls.ElementAt(i - 1).PointColor;
                colorA1 = controls.ElementAt(i).PointColor;
                colorA2 = controls.ElementAt(i + 1).PointColor;
                colorA3 = controls.ElementAt(i + 2).PointColor;
            }
            double coefP = (pointA3.XWeight - pointA2.XWeight) - (pointA0.XWeight - pointA1.XWeight);
            double coefQ = (pointA0.XWeight - pointA1.XWeight) - coefP;
            double coefR = pointA2.XWeight - pointA0.XWeight;
            double coefS = pointA1.XWeight;

            double coefPA = (pointA3.PointColor.A - pointA2.PointColor.A) - (pointA0.PointColor.A - pointA1.PointColor.A);
            double coefQA = (pointA0.PointColor.A - pointA1.PointColor.A) - coefPA;
            double coefRA = pointA2.PointColor.A - pointA0.PointColor.A;
            double coefSA = pointA1.PointColor.A;
            double coefPR = (pointA3.PointColor.R - pointA2.PointColor.R) - (pointA0.PointColor.R - pointA1.PointColor.R);
            double coefQR = (pointA0.PointColor.R - pointA1.PointColor.R) - coefPR;
            double coefRR = pointA2.PointColor.R - pointA0.PointColor.R;
            double coefSR = pointA1.PointColor.R;
            double coefPG = (pointA3.PointColor.G - pointA2.PointColor.G) - (pointA0.PointColor.G - pointA1.PointColor.G);
            double coefQG = (pointA0.PointColor.G - pointA1.PointColor.G) - coefPG;
            double coefRG = pointA2.PointColor.G - pointA0.PointColor.G;
            double coefSG = pointA1.PointColor.G;
            double coefPB = (pointA3.PointColor.B - pointA2.PointColor.B) - (pointA0.PointColor.B - pointA1.PointColor.B);
            double coefQB = (pointA0.PointColor.B - pointA1.PointColor.B) - coefPB;
            double coefRB = pointA2.PointColor.B - pointA0.PointColor.B;
            double coefSB = pointA1.PointColor.B;

            int a = (int)((coefPA * Math.Pow(x, 3)) + (coefQA * Math.Pow(x, 2)) + (coefRA * x) + coefSA);
            int r = (int)((coefPR * Math.Pow(x, 3)) + (coefQR * Math.Pow(x, 2)) + (coefRR * x) + coefSR);
            int g = (int)((coefPG * Math.Pow(x, 3)) + (coefQG * Math.Pow(x, 2)) + (coefRG * x) + coefSG);
            int b = (int)((coefPB * Math.Pow(x, 3)) + (coefQB * Math.Pow(x, 2)) + (coefRB * x) + coefSB);

            double interpX = pointA0.X + x * (pointA1.X - pointA0.X);
            double interpXW = (coefP * Math.Pow(x, 3)) + (coefQ * Math.Pow(x, 2)) + (coefR * x) + coefS;
            Color interpColor = Color.FromArgb(a, r, g, b);
            //if (interpXW > 1 || interpXW < -1)          //temporary until the entire screen is filled
            //    interpXW = 0;
            interps.Add(new CustomPoint1D(interpX, interpXW, interpColor));
        }
    }
    return interps;
}
 */
