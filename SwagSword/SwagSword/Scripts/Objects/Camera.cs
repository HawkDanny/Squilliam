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



namespace SwagSword
{
    class Camera : Manager
    {
        protected Matrix transform;
        protected Vector2 position;
        protected int viewportWidth;
        protected int viewportHeight;
        protected int worldWidth;
        protected int worldHeight;

        public Camera(Viewport viewport, Game1 mainMan) : base(mainMan)
        {
            position = Vector2.Zero;
            viewportWidth = viewport.Width;
            viewportHeight = viewport.Height;
            worldWidth = mainMan.GameMan.MapMan.MapWidth * mainMan.GameMan.MapMan.TileSize;
            worldHeight = mainMan.GameMan.MapMan.MapHeight * mainMan.GameMan.MapMan.TileSize;
            mainMan.MapWidth = worldWidth;
            mainMan.MapHeight = worldHeight;
        }

        public void Move(Vector2 amount)
        {
            position += amount;
        }

        public Vector2 Position
        {
            get { return position; }
            set
            {
                float leftBarrier = (float)viewportWidth * .5f;
                float rightBarrier = worldWidth - leftBarrier;
                float topBarrier = (float)viewportHeight * 0.5f;
                float bottomBarrier = worldHeight - topBarrier;
                position = value;
                if (position.X < leftBarrier)
                    position.X = leftBarrier;
                if (position.X > rightBarrier)
                    position.X = rightBarrier;
                if (position.Y < topBarrier)
                    position.Y = topBarrier;
                if (position.Y > bottomBarrier)
                    position.Y = bottomBarrier;
            }
        }

        public Matrix getTransformation()
        {
            transform =
                Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) *
                Matrix.CreateTranslation(new Vector3(viewportWidth * 0.5f, viewportHeight * 0.5f, 0));
            return transform;
        }

    }
}
