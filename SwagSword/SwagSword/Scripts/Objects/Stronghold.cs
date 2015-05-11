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
using System.IO;
#endregion

namespace SwagSword
{
    public class Stronghold
    {
        #region Fields
        Rectangle rect;
        Texture2D texture;
        #endregion

        #region Properties
        public Rectangle Rect { get { return rect; } set { rect = value; } }
        public Texture2D Texture { get { return texture; } set { texture = value; } }
        #endregion

        public Stronghold(Texture2D tex, Rectangle r)
        {
            texture = tex;
            rect = r;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rect, Color.White);
        }
    }
}
