using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheGame.Mics
{
    internal class AndroidKeyboardKey
    {
        private Rectangle rectangle;
        private Texture2D texture;

        public AndroidKeyboardKey(int x, int y, int width, int height, Texture2D texture)
        {
            this.texture= texture;
            this.rectangle=new Rectangle(x,y, width, height);
        }

        public void Update(GameTime gameTime) 
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

        
    }

}
