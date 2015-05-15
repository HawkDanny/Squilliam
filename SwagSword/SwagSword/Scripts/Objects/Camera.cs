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

//Names: Ryan Bell, Nelson Scott

namespace SwagSword
{
    public class Camera
    {
        #region Fields
        //Main man
        Game1 mainMan;

        Vector2 deltaMovement; //the change in position of the camera

        protected Matrix transform;
        protected Vector2 position;
        protected int viewportWidth;
        protected int viewportHeight;
        protected int worldWidth;
        protected int worldHeight;
        #endregion

        #region Properties
        public Vector2 Position
        {
            get { return position; }
            set
            {
                float leftBarrier = (float)viewportWidth * 0.5f;
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

        public Vector2 TopLeftPosition { get { return new Vector2(position.X - viewportWidth / 2, position.Y - viewportHeight / 2); } }
        #endregion

        public Camera(Viewport viewport, Game1 mainMan)
        {
            this.mainMan = mainMan;
            viewportWidth = viewport.Width;
            viewportHeight = viewport.Height;
            worldWidth = mainMan.GameMan.MapMan.ResWidth;
            worldHeight = mainMan.GameMan.MapMan.ResHeight;
            position = Vector2.Zero;
            mainMan.MapWidth = worldWidth;
            mainMan.MapHeight = worldHeight;
        }

        public void SnapToCenter()
        {
            position = new Vector2(worldWidth / 2, worldHeight / 2);
        }

        public void Move(Vector2 amount)
        {
            position += amount;
        }

        public Matrix getTransformation()
        {
            transform =
                Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) *
                Matrix.CreateTranslation(new Vector3(viewportWidth * 0.5f, viewportHeight * 0.5f, 0));
            return transform;
        }


        /// <summary>
        /// Updates the camera position and delta movement
        /// </summary>
        public void Update()
        {
            Vector2 movement = Vector2.Zero;
            deltaMovement = movement;
            
            if (mainMan.GameMan.Players.Count > 0)
            {
                if (!mainMan.GameMan.Players[0].NoCharacter && mainMan.GameMan.Players[0].CharacterState != CharacterState.Dead)
                {
                    if(mainMan.GameMan.MapMan.CalcDistance((int)position.X, (int)position.Y, (int)mainMan.GameMan.Players[0].X, (int)mainMan.GameMan.Players[0].Y) > mainMan.WindowHeight)
                    {
                        position = mainMan.GameMan.Players[0].Position;
                    }
                    if (mainMan.GameMan.Players[0].X < Position.X - mainMan.WindowWidth * 0.1)
                        movement.X--;
                    if (mainMan.GameMan.Players[0].X > Position.X + mainMan.WindowWidth * 0.1)
                        movement.X++;
                    if (mainMan.GameMan.Players[0].Y < Position.Y - mainMan.WindowHeight * 0.1)
                        movement.Y--;
                    if (mainMan.GameMan.Players[0].Y > Position.Y + mainMan.WindowHeight * 0.1)
                        movement.Y++;
                    deltaMovement = movement * mainMan.GameMan.Players[0].Character.MovementSpeed;       
                }
            }
            Position += deltaMovement;
        }
    }
}
