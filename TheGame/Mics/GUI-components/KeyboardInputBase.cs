using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;

namespace TheGame.Mics.GUI_components
{
    public abstract class KeyboardInputBase
    {

        protected string inputValue;
        public KeyboardInputBase() 
        {
            inputValue = "";
        }


        public string GetValue()
        {
            string tmp = inputValue;
            inputValue = "";
            return tmp;

        }
        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
