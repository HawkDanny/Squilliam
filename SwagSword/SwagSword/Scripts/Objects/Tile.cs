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
        bool pathway;
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
            
            texture = new Texture2D(mapMan.GraphicsDevice, t.Width, t.Height);
            Color[] colors = new Color[t.Width * t.Height];
            t.GetData<Color>(colors);
            texture.SetData<Color>(colors);
            coordinates = coords;
            this.mapMan = mapMan;
            Init();
        }

        protected void Init()
        {
            center = new Point(coordinates.X * mapMan.TileSize + mapMan.TileSize / 2, coordinates.Y * mapMan.TileSize + mapMan.TileSize / 2);
            pathway = false;
        }

        #region Properties
        public bool Pathway { get { return pathway; } set { pathway = value; } }
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
            if (overlay == null)
                return;
            else
            {
                Color[] overlayData = new Color[overlay.Width * overlay.Height];
                Color[] textureData = new Color[texture.Width * texture.Height];
                texture.GetData<Color>(textureData);
                overlay.GetData<Color>(overlayData);
                for (int x = 0; x < texture.Width; x++)
                {
                    for (int y = 0; y < texture.Height; y++)
                    {
                        int value = overlayData[x * texture.Height + y].A / 255;
                        int R;
                        int G;
                        int B;
                        textureData[x * texture.Height + y] = new Color((textureData[x * texture.Height + y].R + 0 * overlayData[x * texture.Height + y].R) / 2, (textureData[x * texture.Height + y].G + 0 * overlayData[x * texture.Height + y].G) / 2, (textureData[x * texture.Height + y].B + value * overlayData[x * texture.Height + y].B) / 2, MathHelper.Clamp((textureData[x * texture.Height + y].A + overlayData[x * texture.Height + y].A) / 2, 0, 255));
                        //textureData[x * texture.Height + y].A = (byte)MathHelper.Clamp((textureData[x * texture.Height + y].A + overlayData[x * texture.Height + y].A) / 2, 0, 255);
                        
                    }
                }
                texture.SetData<Color>(textureData);
                
            }
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
            groupLeft.Add(dll);
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
