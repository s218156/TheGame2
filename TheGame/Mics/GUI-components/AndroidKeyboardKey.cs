
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheGame.Mics
{
    internal class AndroidKeyboardKey
    {
        private Rectangle rectangle;
        private Texture2D texture;
        private SpriteFont font;
        private string symbolA;
        private string symbolB;
        private string symbolAShift;
        private string currentSymbol;
        private bool wasPressed = false;

        public AndroidKeyboardKey(int x, int y, int width, int height, Texture2D texture, SpriteFont font, string symbolA, string symbolAShift, string symbolB)
        {
            this.texture = texture;
            this.rectangle = new Rectangle(x, y, width, height);
            this.font = font;
            this.symbolA = symbolA;
            this.symbolB = symbolB;
            this.symbolAShift = symbolAShift;
            this.currentSymbol = symbolA;

        }

        public void Update(GameTime gameTime, bool regular, bool upper, bool symbolic)
        {
            if (regular)
                currentSymbol = symbolA;
            else
            {
                if (upper)
                    currentSymbol = symbolAShift;
                else
                    currentSymbol = symbolB;
            }
        }

        public string GetPhrase()
        {
            return currentSymbol;
        }

        public bool IsPressed(Rectangle rectangle)
        {
            if (this.rectangle.Contains(rectangle))
            {
                if (!wasPressed)
                {
                    wasPressed = true;
                    return true;
                }
                else
                    return false;
            }
            else
            {
                wasPressed = false;
                return false;
            }



        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, this.rectangle, Color.White);
            spriteBatch.DrawString(font, currentSymbol, new Vector2(rectangle.X + (rectangle.Width / 2), rectangle.Y + (rectangle.Height / 4)), Color.Black, 0.0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0.0f);
        }


    }

}
