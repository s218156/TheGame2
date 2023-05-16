using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using TheGame.Sprites;

namespace TheGame.Animations
{
    public class LoadingBarAnimation : AnimatedTexture
    {
        private string _caption;
        private int _length;
        private bool _growing;
        private SpriteFont _font;
        public LoadingBarAnimation(Texture2D texture, Rectangle rectangle, int columns, int rows, string caption, SpriteFont font) : base(texture, rectangle, columns, rows)
        {
            this._caption= caption;
            this._length = 0;
            this._font = font;
            this._growing = true;
        }

        public override void Update(GameTime gameTime, Sprite sprite)
        {
            timer++;
            if (timer == 10)
            {
                timer = 0;
                if (_growing)
                    _length++;
                else
                    _length--;
                if (_length >= 10)
                    _growing = false;
                else
                {
                    if (_length <= 0)
                        _growing = true;
                }
            }
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            int textureWidth = texture.Width / 3;
            int textureHeight = texture.Height;
            int singleCellWidth = rectangle.Width / 12;
            spriteBatch.DrawString(_font, _caption, new Vector2(rectangle.X, rectangle.Y), Color.Black);
            spriteBatch.Draw(texture, new Rectangle(rectangle.X, rectangle.Y+ rectangle.Height / 2, singleCellWidth, rectangle.Height/2), new Rectangle(textureWidth, 0, textureWidth-10, textureHeight), Color.White);
            for(int i = 0; i < _length; i++) 
            {
                spriteBatch.Draw(texture, new Rectangle(rectangle.X+textureWidth*(i+1), rectangle.Y+ rectangle.Height / 2, singleCellWidth, rectangle.Height/2), new Rectangle(0, 0, textureWidth, textureHeight), Color.White);
            }
            spriteBatch.Draw(texture, new Rectangle(rectangle.X+textureWidth*(_length+1), rectangle.Y+ rectangle.Height / 2, singleCellWidth, rectangle.Height / 2), new Rectangle(textureWidth * 2, 0, textureWidth-10, textureHeight), Color.White);

        }

		public override void SetMovement(bool isOnMove)
		{
			throw new NotImplementedException();
		}

		public override void SetDirection(bool direction)
		{
			throw new NotImplementedException();
		}
	}
}
