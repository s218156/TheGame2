using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace TheGame.Mics.GUI_components
{
    public class PCKeyboardInput : KeyboardInputBase
    {
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

        public override void Update(GameTime gameTime)
        {
            Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();
            if (pressedKeys.Length > 0)
            {
                if (pressedKeys.Length == 1)
                    inputValue = pressedKeys[0].ToString();
                else
                {
                    foreach (Keys key in pressedKeys)
                    {
                        Debug.WriteLine(key.ToString());
                        Debug.WriteLine(Keyboard.GetState().CapsLock.ToString());
                    }

                }
            }

        }
    }
}
