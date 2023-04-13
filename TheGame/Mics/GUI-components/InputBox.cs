using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheGame.Mics.GUI_components
{
    internal class InputBox : ClickableComponent
    {
        private Texture2D texture;
        private string value;
        private SpriteFont font;
        private string placeholder;
        private string _previousValue;
        public InputBox(Texture2D texture, SpriteFont font, Rectangle rectangle, string placeholder)
        {
            this.rectangle = rectangle;
            this.texture = texture;
            this.font = font;
            this.value = "";
            this.placeholder = placeholder;

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
            if (value.Length == 0)
                spriteBatch.DrawString(font, placeholder, new Vector2(rectangle.X + (rectangle.Width / 10), rectangle.Y + (rectangle.Height / 5)), Color.Gray);
            else
                spriteBatch.DrawString(font, value, new Vector2(rectangle.X + (rectangle.Width / 10), rectangle.Y + (rectangle.Height / 5)), Color.Black);
        }

        public void UpdateValue(string symbol)
        {
            _previousValue = value;
            switch (symbol)
            {
                case "REMOVE":
                    if (value.Length > 0)
                        value = value.Remove(value.Length - 1);
                    break;
                case "OK":
                    InvokeClick();
                    break;
                default:
                    value += symbol;
                    break;
            }
        }

        public bool DidValueChanged()
        {
            return value != _previousValue;
        }
    }
}
