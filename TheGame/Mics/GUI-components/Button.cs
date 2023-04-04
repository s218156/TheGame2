using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheGame.Mics
{
    class Button : ClickableComponent
    {

        private SpriteFont _font;


        private Texture2D _texture;
        public Color PenColour { get; set; }
        public string caption;

        public Button(Texture2D texture, SpriteFont font, Rectangle rectangle, String caption)
        {
            _texture = texture;
            _font = font;
            PenColour = Color.Black;
            this.rectangle = rectangle;
            this.caption = caption;
            this.isAbleToClick = true;
#if ANDROID
            this.wasUntouched= false;
#endif
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var colour = Color.White;

            if (_isHovering)
                colour = Color.Gray;
            if (!isAbleToClick)
            {
                colour = Color.DarkGray;
                PenColour = Color.Gray;
            }
                

            spriteBatch.Draw(_texture, rectangle, colour);

            if (!string.IsNullOrEmpty(caption))
            {
                var x = (rectangle.X + (rectangle.Width / 2)) - (_font.MeasureString(caption).X / 2);
                var y = (rectangle.Y + (rectangle.Height / 2)) - (_font.MeasureString(caption).Y / 2);

                spriteBatch.DrawString(_font, caption, new Vector2(x, y), PenColour);
            }
        }

        public void unHoverButton()
        {
            _isHovering = false;
        }

    }
}
