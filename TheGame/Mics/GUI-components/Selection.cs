using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace TheGame.Mics.GUI_components
{
    class Selection
    {
        public int selected;
        private Texture2D texture;
        private Rectangle rectangle;
        private bool keysWasUp;

        public Selection(ContentManager content)
        {
            texture = content.Load<Texture2D>("gameUI/selection");
            selected = 0;
            keysWasUp = false;
        }
        public void Update(List<Button> _buttons)
        {

            if (keysWasUp)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    _buttons[selected].InvokeClick();
                    keysWasUp = false;
                }



                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    selected += 1;
                    keysWasUp = false;
                }


                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    selected -= 1;
                    keysWasUp = false;
                }

            }
            if (Keyboard.GetState().IsKeyUp(Keys.W) & Keyboard.GetState().IsKeyUp(Keys.S) & Keyboard.GetState().IsKeyUp(Keys.Enter))
                keysWasUp = true;


            if (selected > _buttons.Count - 1)
                selected = _buttons.Count - 1;
            if (selected < 0)
                selected = 0;
            rectangle.Y = _buttons[selected].rectangle.Y;

        }
        public void SetSelectionPosition(List<Button> _buttons)
        {
            rectangle = new Rectangle(_buttons[selected].rectangle.X - _buttons[selected].rectangle.Height, _buttons[selected].rectangle.Y,
                _buttons[selected].rectangle.Height, _buttons[selected].rectangle.Height);
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

    }
}
