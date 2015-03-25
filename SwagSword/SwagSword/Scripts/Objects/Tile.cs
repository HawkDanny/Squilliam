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
//using Microsoft.Xna.Framework.GamerServices;
#endregion

//Names: Ryan Bell

namespace SwagSword
{
    public class Tile
    {

        #region fields
        Texture2D texture;
        Point center;
        Point coordinates;
        MapManager mapMan;
        List<Tile> cardinals;
        List<Tile> neighbors;
        List<Tile> groupTop;
        List<Tile> groupLeft;
        List<Tile> groupRight;
        List<Tile> groupLower;
        Tile top;
        Tile left;
        Tile right;
        Tile lower;
        Tile dul;
        Tile dur;
        Tile dll;
        Tile dlr;
        #endregion

        public Tile(Texture2D t, Point coords, MapManager mapMan)
        {
            texture = t;
            coordinates = coords;
            this.mapMan = mapMan;
            Init();
        }

        protected void Init()
        {
            center = new Point(coordinates.X * mapMan.TileSize + mapMan.TileSize / 2, coordinates.Y * mapMan.TileSize + mapMan.TileSize / 2);
        }

        #region Properties
        public Texture2D Texture { get { return texture; } set { texture = value; } }
        public Point Center { get { return center; } set { center = value; } }
        public Tile Top { get { return top; } set { top = value; } }
        public Tile Left { get { return left; } set { left = value; } }
        public Tile Right { get { return right; } set { right = value; } }
        public Tile Lower { get { return lower; } set { lower = value; } }
        public Tile DUL { get { return dul; } set { dul = value; } }
        public Tile DUR { get { return dur; } set { dur = value; } }
        public Tile DLL { get { return dll; } set { dll = value; } }
        public Tile DLR { get { return dlr; } set { dlr = value; } }
        public List<Tile> Cardinals
        {
            get
            {
                List<Tile> temp = new List<Tile>();
                foreach(Tile t in cardinals)
                {
                    if (t != null)
                        temp.Add(t);
                }
                return temp;
            }
        }
        public List<Tile> Neighbors
        {
            get
            {
                List<Tile> temp = new List<Tile>();
                foreach (Tile t in neighbors)
                {
                    if (t != null)
                        temp.Add(t);
                }
                return temp;
            }
        }
        public List<Tile> GroupTop
        {
            get
            {
                List<Tile> temp = new List<Tile>();
                foreach (Tile t in groupTop)
                {
                    if (t != null)
                        temp.Add(t);
                }
                return temp;
            }
        }
        public List<Tile> GroupLeft
        {
            get
            {
                List<Tile> temp = new List<Tile>();
                foreach (Tile t in groupLeft)
                {
                    if (t != null)
                        temp.Add(t);
                }
                return temp;
            }
        }
        public List<Tile> GroupRight
        {
            get
            {
                List<Tile> temp = new List<Tile>();
                foreach (Tile t in groupRight)
                {
                    if (t != null)
                        temp.Add(t);
                }
                return temp;
            }
        }
        public List<Tile> GroupLower
        {
            get
            {
                List<Tile> temp = new List<Tile>();
                foreach (Tile t in groupLower)
                {
                    if (t != null)
                        temp.Add(t);
                }
                return temp;
            }
        }

        #endregion

        public void OverlayTexture(Texture2D overlay)
        {
            return;
        }

        public void FormGroups()
        {
            cardinals = new List<Tile>();
            cardinals.Add(top);
            cardinals.Add(left);
            cardinals.Add(right);
            cardinals.Add(lower);
            neighbors = new List<Tile>();
            neighbors.Add(top);
            neighbors.Add(left);
            neighbors.Add(right);
            neighbors.Add(lower);
            neighbors.Add(dul);
            neighbors.Add(dur);
            neighbors.Add(dll);
            neighbors.Add(dlr);
            groupTop = new List<Tile>();
            groupTop.Add(dul);
            groupTop.Add(top);
            groupTop.Add(dur);
            groupLeft = new List<Tile>();
            groupLeft.Add(dul);
            groupLeft.Add(left);
            groupLeft.Add(dlr);
            groupRight = new List<Tile>();
            groupRight.Add(dur);
            groupRight.Add(right);
            groupRight.Add(dlr);
            groupLower = new List<Tile>();
            groupLower.Add(dll);
            groupLower.Add(lower);
            groupLower.Add(dlr);
        }
        public override string ToString()
        {
            if (lower != null)
                return "Not null";
            return "Null";

        }
    }
}
