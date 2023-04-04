using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheGame.Mics
{
    public class ClickableComponent:Component
    {
        protected MouseState _currentMouse;
        protected MouseState _previousMouse;
        protected bool _isHovering;
        public bool isAbleToClick;
        public event EventHandler Click;
        public bool Clicked { get; private set; }
        public Rectangle rectangle;

#if ANDROID
        public bool wasUntouched;
#endif



        public override void Update(GameTime gameTime)
        {
#if DESKTOP
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            _isHovering = false;

            if (mouseRectangle.Intersects(rectangle))
            {
                _isHovering = true;

                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                    Click?.Invoke(this, new EventArgs());
            }
#endif
#if ANDROID

            TouchCollection touch = TouchPanel.GetState();
            if (touch.Count == 0)
                wasUntouched = true;
            if ((touch.Count > 0) && (wasUntouched))
            {
                Rectangle touchRectangle = new Rectangle((int)touch[0].Position.X, (int)touch[0].Position.Y, 1, 1);
                if (rectangle.Contains(touchRectangle))
                {
                    wasUntouched= false;
                    Click?.Invoke(this, new EventArgs());
                }
            }


#endif

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public void ButtonSelected()
        {
            //Click?.Invoke(this, new EventArgs());
        }
    }
}
