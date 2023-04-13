using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;

namespace TheGame.Mics.GUI_components
{
    public class AndroidKeyboard : KeyboardInputBase
    {
        private Rectangle rectangle;
        private Texture2D texture;
        private List<AndroidKeyboardKey> keys;
        private bool regularKeyboard = true;
        private bool upperKeyboard = false;
        private bool symbolicKeyboard = false;
        public string inputValue;


        public AndroidKeyboard(int screenHeight, int screenWidth, Texture2D texture, ContentManager content)
        {
            this.rectangle = new Rectangle(0, screenHeight / 2 + 1, screenWidth, screenHeight / 2);
            this.texture = texture;
            keys = new List<AndroidKeyboardKey>();
            PrepareKeyboard(content);
        }

        public override void Update(GameTime gameTime)
        {
            inputValue = "";
            TouchCollection touches = TouchPanel.GetState();
            Rectangle touchRectangle = new Rectangle();
            string phrase = "";
            foreach (var touch in touches)
            {
                touchRectangle = new Rectangle((int)touch.Position.X, (int)touch.Position.Y, 1, 1);
            }

            foreach (AndroidKeyboardKey key in keys)
            {
                if (key.IsPressed(touchRectangle))
                    phrase = key.GetPhrase();
            }

            switch (phrase)
            {
                case "SHIFT":
                    if (upperKeyboard)
                    {
                        upperKeyboard = false;
                        regularKeyboard = true;
                        symbolicKeyboard = false;
                    }
                    else
                    {
                        if (regularKeyboard)
                        {
                            upperKeyboard = true;
                            regularKeyboard = false;
                            symbolicKeyboard = false;
                        }
                    }
                    break;

                case "123!":
                    upperKeyboard = false;
                    regularKeyboard = false;
                    symbolicKeyboard = true;
                    break;

                case "ABC":
                    upperKeyboard = false;
                    regularKeyboard = true;
                    symbolicKeyboard = false;
                    break;
                default:
                    inputValue = phrase;
                    break;
            }

            foreach (AndroidKeyboardKey key in keys)
            {
                key.Update(gameTime, regularKeyboard, upperKeyboard, symbolicKeyboard);
            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, rectangle, Color.White);
            foreach (AndroidKeyboardKey key in keys)
                key.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }



        public void PrepareKeyboard(ContentManager content)
        {
            List<string> symbolsA = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "a", "s", "d", "f", "g", "h", "j", "k", "l", ";", "SHIFT", "z", "x", "c", "v", "b", "n", "m", "REMOVE", " ", "123!", ",", " ", " ", " ", " ", " ", ".", "OK" };
            List<string> symbolsAShift = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "A", "S", "D", "F", "G", "H", "J", "K", "L", ";", "SHIFT", "Z", "X", "C", "V", "B", "N", "M", "REMOVE", " ", "123!", ",", " ", " ", " ", " ", " ", ".", "OK" };
            List<string> symbolsB = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "+", "x", "\\", "=", "/", "_", "<", ">", "[", "]", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "", "-", "'", "\"", ":", ";", ",", "?", "REMOVE", " ", "ABC", ",", " ", " ", " ", " ", " ", ".", "OK" };
            Texture2D keyTexture = content.Load<Texture2D>("gameUI/button");
            SpriteFont keyFont = content.Load<SpriteFont>("Fonts/Basic");
            int widthUnit = rectangle.Width / 311;
            int heightUnit = rectangle.Height / 156;
            for (int j = 1; j <= 3; j++)
            {
                for (int i = 1; i <= 10; i++)
                {
                    keys.Add(new AndroidKeyboardKey(rectangle.X + (widthUnit * i) + ((i - 1) * 30 * widthUnit), rectangle.Y + (heightUnit * j) + ((j - 1) * 30 * heightUnit), 30 * widthUnit, 30 * heightUnit, keyTexture, keyFont, symbolsA[((j - 1) * 10) + (i - 1)], symbolsAShift[((j - 1) * 10) + (i - 1)], symbolsB[((j - 1) * 10) + (i - 1)]));
                }
            }
            for (int i = 1; i <= 2; i++)
            {
                for (int j = 1; j <= 9; j++)
                {
                    if ((j == 1) || (j == 9))
                    {
                        if (j == 1)
                        {
                            keys.Add(new AndroidKeyboardKey(rectangle.X + ((j - 1) * 30 * widthUnit), rectangle.Y + ((3 + i) * heightUnit) + (2 + i) * 30 * heightUnit, Convert.ToInt32(30 * widthUnit * 1.5), 30 * heightUnit, keyTexture, keyFont, symbolsA[((i + 2) * 10) + (j - 1)], symbolsAShift[((i + 2) * 10) + (j - 1)], symbolsB[((i + 2) * 10) + (j - 1)]));
                        }
                        else
                        {
                            keys.Add(new AndroidKeyboardKey(rectangle.X + (widthUnit * j) + ((j - 1) * 30 * widthUnit) + Convert.ToInt32(30 * widthUnit * 0.5), rectangle.Y + ((3 + i) * heightUnit) + (2 + i) * 30 * heightUnit, Convert.ToInt32(30 * widthUnit * 1.5), 30 * heightUnit, keyTexture, keyFont, symbolsA[((i + 2) * 10) + (j - 1)], symbolsAShift[((i + 2) * 10) + (j - 1)], symbolsB[((i + 2) * 10) + (j - 1)]));
                        }
                    }
                    else
                    {
                        if ((i == 1) || ((i == 2) && ((j == 2) || j == 8)))
                        {
                            keys.Add(new AndroidKeyboardKey(rectangle.X + (widthUnit * j) + ((j - 1) * 30 * widthUnit) + Convert.ToInt32(30 * widthUnit * 0.5), rectangle.Y + ((3 + i) * heightUnit) + (2 + i) * 30 * heightUnit, 30 * widthUnit, 30 * heightUnit, keyTexture, keyFont, symbolsA[((i + 2) * 10) + (j - 1)], symbolsAShift[((i + 2) * 10) + (j - 1)], symbolsB[((i + 2) * 10) + (j - 1)]));
                        }
                        else
                        {
                            if ((i == 2) && (j == 3))
                            {
                                keys.Add(new AndroidKeyboardKey(rectangle.X + (widthUnit * j) + ((j - 1) * 30 * widthUnit) + Convert.ToInt32(30 * widthUnit * 0.5), rectangle.Y + ((3 + i) * heightUnit) + (2 + i) * 30 * heightUnit, (30 * widthUnit * 5) + (4 * widthUnit), 30 * heightUnit, keyTexture, keyFont, symbolsA[((i + 2) * 10) + (j - 1)], symbolsAShift[((i + 2) * 10) + (j - 1)], symbolsB[((i + 2) * 10) + (j - 1)]));
                            }
                        }
                    }
                }
            }


        }
    }
}
