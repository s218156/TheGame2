using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheGame.Mics.GUI_components
{
    public class AndroidControl:Component
    {
        private Texture2D texture;
        private Rectangle rectangle;

        public AndroidControl(Texture2D texture, Rectangle rectangle)
        {
            this.texture = texture;
            this.rectangle = rectangle;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

        public override void Update(GameTime gameTime)
        {


        }
    }
}
