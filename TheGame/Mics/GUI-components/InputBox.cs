using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheGame.Mics.GUI_components
{
    internal class InputBox : ClickableComponent
    {
        private Texture2D texture;
        private string value;
        private SpriteFont font;
        public InputBox(Texture2D texture, SpriteFont font, Rectangle rectangle)
        {
            this.rectangle = rectangle;
            this.texture = texture;
            this.font = font;
            this.value = "";
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
            spriteBatch.DrawString(font, value,new Vector2(rectangle.X+(rectangle.Width/10),rectangle.Y+(rectangle.Height/5)), Color.Black);
        }

        public void UpdateValue(string symbol)
        {
            if (value.Equals("REMOVE"))
                value = value.Remove(value.Length - 1);
            else
                value += symbol;
        }
    }
}
