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
        Game1 mainMan;
        Rectangle rect;
        Texture2D texture;
        bool captured;
        double percentCaptured;
        double prevTime;
        double curTime;
        Color capColor;
        Texture2D captex;
        Rectangle capRect;
        #endregion

        #region Properties
        public Rectangle Rect { get { return rect; } set { rect = value; } }
        public Texture2D Texture { get { return texture; } set { texture = value; } }
        public bool Captured { get { return captured; } set { captured = value; } }
        public double PercentCaptured { get { return percentCaptured; } set { percentCaptured = value; } }
        #endregion

        public Stronghold(Texture2D tex, Rectangle r, Game1 mainMan)
        {
            texture = tex;
            rect = r;
            this.mainMan = mainMan;
            capColor = Color.Green;
            captex = new Texture2D(mainMan.GraphicsDevice, 1, 1);
            Color[] colors = new Color[1];
            colors[0] = Color.White;
            captex.SetData<Color>(colors);
            capRect = new Rectangle(rect.X, rect.Y, rect.Width, 15);
            Init();
        }

        private void Init()
        {
            captured = false;
            percentCaptured = 0;
            prevTime = mainMan.GameTime.TotalGameTime.TotalMilliseconds;
            curTime = prevTime;
        }

        public void Update()
        {
            curTime = mainMan.GameTime.TotalGameTime.TotalMilliseconds;
            if (curTime - prevTime > 200)
            {
                foreach (Player p in mainMan.GameMan.Players)
                {
                    if (rect.Contains(new Point((int)p.Position.X, (int)p.Position.Y)))
                    {
                        percentCaptured += .05;
                    }
                }
                percentCaptured -= .02;
                if (percentCaptured > 1)
                    percentCaptured = 1;
                if (percentCaptured < 0)
                    percentCaptured = 0;
                capRect.Width = (int)(rect.Width * percentCaptured);
                
                capColor.R = (byte)(255 * percentCaptured);
                capColor.G = (byte)(255 * (1 - percentCaptured));
                prevTime = curTime;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(captex, capRect, capColor);
            spriteBatch.Draw(texture, rect, Color.White);
            spriteBatch.Draw(captex, capRect, capColor);
        }
    }
}
