
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
        private SpriteFont font;
        private string symbol;

        public AndroidKeyboardKey(int x, int y, int width, int height, Texture2D texture, SpriteFont font, string symbol)
        {
            this.texture= texture;
            this.rectangle=new Rectangle(x,y, width, height);
            this.font= font;
            this.symbol= symbol;
        }

        public void Update(GameTime gameTime) 
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, this.rectangle, Color.White);
            spriteBatch.DrawString(font,symbol, new Vector2(rectangle.X+(rectangle.Width/2), rectangle.Y+(rectangle.Height/4)), Color.Black,0.0f, Vector2.Zero,1.5f,SpriteEffects.None,0.0f);
        }

        
    }

}
