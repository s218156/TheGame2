using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheGame.Mics.GUI_components
{
    public abstract class KeyboardInputBase
    {


        public string inputValue;

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
