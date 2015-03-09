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
    public class Tile
    {
        Texture2D texture;
        Point center;


        public Tile(Texture2D t, Point c)
        {
            texture = t;
            center = c;
        }
        
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public Point Center
        {
            get { return center; }
            set { center = value; }
        }
    }
}
