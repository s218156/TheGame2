using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace TheGame.Mics.GUI_components
{
    public class PCKeyboardInput : KeyboardInputBase
    {
        private string _previousValue = "";
        private bool shifted = false;
        private List<string> avalableValues= new List<string>() { "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "D0", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "A", "S", "D", "F", "G", "H", "J", "K", "L", "OemSemicolon", "Z", "X", "C", "V", "B", "N", "M", "OemComma", "OemPeriod", "OemPlus", "OemBackslash", "OemQuestion", "OemMinus", "OemOpenBrackets", "OemCloseBrackets", "OemQuotes" };
        
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

        private string CharacterFactory(string key)
        {
            string alphaSigns = "ABCDEFGHIJKLMNOPQRSTUWXYZ";
            List<string> numeric = new List<string>() { "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "D0" };
            if (alphaSigns.Contains(key))
            {
                if (shifted)
                    return key.ToLower();
                else
                    return key.ToUpper();
            }
            if (numeric.Contains(key))
            {
                return key.Substring(1,1);
            }
            return key;

                
        }

        public override void Update(GameTime gameTime)
        {
            bool repeated = false;
            Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();
            if (pressedKeys.Length > 0)
            {
                if (pressedKeys.Length == 1)
                {
                    Debug.WriteLine(pressedKeys[0]);
                    if (avalableValues.Contains(pressedKeys[0].ToString()))
                    {
                        if (_previousValue != CharacterFactory(pressedKeys[0].ToString()))
                        {
                            inputValue = CharacterFactory(pressedKeys[0].ToString());
                        }
                        else
                            repeated= true;
                        
                    }
                }
            }
            if (!repeated)
                _previousValue = inputValue;

        }
    }
}
