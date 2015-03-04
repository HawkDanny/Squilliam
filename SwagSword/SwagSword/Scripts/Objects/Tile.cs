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

        public Tile() { }

        public Tile(Texture2D texture)
        {
            Texture = texture;
        }
        
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }
    }
}
