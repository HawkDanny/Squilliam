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
    public class Tile
    {

        #region fields
        Texture2D texture;
        Point center;
        #endregion

        public Tile(Texture2D t, Point c)
        {
            texture = t;
            center = c;
        }

        #region Properties
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
        #endregion
    }
}
