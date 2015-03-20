using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//Names: Peter Lockhart

namespace SwagSword
{
    class Button
    {
        //Fields
        private Texture2D texture;
        private Rectangle rectangle;
        private Game1 mainMan;
        private bool mousedOver;
        private UIScreen nextScreen;
        private GameState nextState;

        //Properties
        public Texture2D Texture { get { return texture; } set { texture = value; } }
        public Rectangle Rectangle { get { return rectangle; } set { rectangle = value; } }
        public bool MousedOver { get { return mousedOver; } }
        public UIScreen NextScreen { get { return nextScreen; } set { nextScreen = value; } }

        public Button(Game1 mainMan, Texture2D texture, Rectangle rectangle, UIScreen nextScreen, GameState nextState)
        {
            this.texture = texture;
            this.rectangle = rectangle;
            this.mainMan = mainMan;
            this.nextScreen = nextScreen;
            this.nextState = nextState;
            mousedOver = false;
        }

        public void Update()
        {
            isMousedOver();
            if(mousedOver)
            {
                isClicked();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(mousedOver)
            {
                spriteBatch.Draw(texture, rectangle, Color.Blue);
            }
            else
            {
                spriteBatch.Draw(texture, rectangle, Color.White);
            }
            
        }

        public void isMousedOver()
        {
            if (rectangle.Contains(mainMan.InputMan.PointerPosition.X, mainMan.InputMan.PointerPosition.Y))
            {
                mousedOver = true;
            }
            else
            {
                mousedOver = false;
            }
        }

        public void isClicked()
        {
            if(mainMan.InputMan.MState.LeftButton == ButtonState.Pressed)
            {
                mainMan.UIMan.State = nextState;
                mainMan.UIMan.Screens.Pop();
                mainMan.UIMan.Screens.Push(nextScreen);
            }
        }
    }
}
