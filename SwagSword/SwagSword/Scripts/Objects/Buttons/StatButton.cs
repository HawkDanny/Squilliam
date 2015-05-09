using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SwagSword.Scripts.Objects
{
    class StatButton
    {
        //Fields
        private Game1 mainMan;
        private Texture2D texture;
        private Rectangle rect;
        private bool mousedOver;
        private Player player;
        private string type;
        private bool enabled;

        //Properties
        public Texture2D Texture { get { return texture; } set { texture = value; } }
        public Rectangle Rect { get { return rect; } set { rect = value; } }

        /// <summary>
        /// Creates a button used for increasing a player's stat multipliers
        /// </summary>
        /// <param name="texture">Button Texture</param>
        /// <param name="rect">Position and Size of the button</param>
        /// <param name="player">Player whos stats will be multiplied</param>
        /// <param name="type">the type of button</param>
        public StatButton(Game1 mainMan, Texture2D texture, Rectangle rect, Player player, string type)
        {
            this.mainMan = mainMan;
            this.texture = texture;
            this.rect = rect;
            mousedOver = false;
            enabled = false;
            this.type = type;
        }

        public void Update()
        {
            isMousedOver();
            if (mousedOver)
            {
                isClicked();
            }
        }

        public void isMousedOver()
        {
            if (rect.Contains(mainMan.InputMan.PointerPosition.X, mainMan.InputMan.PointerPosition.Y))
            {
                mousedOver = true;
            }
            else
            {
                mousedOver = false;
            }
        }

        public virtual void isClicked()
        {
            if(type == "health")
            {
                player.HealthMultiplier += .2f;
            }
            if(type == "damage")
            {
                player.DamageMultiplier += .2f;
            }
            if(type == "knockback")
            {
                player.KnockbackMultplier += .2f;
            }
            if(type == "attackspeed")
            {
                player.AttackSpeedMultiplier += .2f;
            }
            if(type == "movementspeed")
            {
                player.MovementSpeedMultiplier += .2f;
            }
            enabled = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (mousedOver)
            {
                spriteBatch.Draw(texture, rect, Color.Blue);
            }
            else
            {
                spriteBatch.Draw(texture, rect, Color.White);
            }

        }
    }
}
