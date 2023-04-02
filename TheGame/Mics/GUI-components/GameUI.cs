
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Sprites;

namespace TheGame.Mics.GUI_components
{
    public class GameUI
    {
        private Rectangle boxRectangle;
        private Texture2D boxTexture;
        private Texture2D lifeTexture;
        private Texture2D coinTexture;
#if ANDROID
        private List<AndroidControl> controls;
#endif

        private SpriteFont font;


        public GameUI(ContentManager content, int screenHeight, int screenWidth)
        {
            lifeTexture = content.Load<Texture2D>("gameUI/heart");
            coinTexture = content.Load<Texture2D>("gameUI/coin");
            boxTexture = content.Load<Texture2D>("gameUI/frame");
            boxRectangle = new Rectangle(0, 0, screenWidth/5, screenHeight/5);
            this.font = content.Load<SpriteFont>("Fonts/Basic");
#if ANDROID

            controls= new List<AndroidControl>();
            controls.Add(new AndroidControl(content.Load<Texture2D>("gameUI/controls/arrowLeft"), new Rectangle(40, screenHeight - 240, 200, 200), "left"));
            controls.Add(new AndroidControl(content.Load<Texture2D>("gameUI/controls/arrowRight"), new Rectangle(290, screenHeight - 240, 200, 200),"right"));
            controls.Add(new AndroidControl(content.Load<Texture2D>("gameUI/controls/arrowDown"), new Rectangle(screenWidth - 240, screenHeight - 220, 200, 200),"down"));
            controls.Add(new AndroidControl(content.Load<Texture2D>("gameUI/controls/arrowUp"), new Rectangle(screenWidth - 240, screenHeight - 450, 200, 200),"up"));
            controls.Add(new AndroidControl(content.Load<Texture2D>("gameUI/controls/pause"), new Rectangle(screenWidth - 120, 20, 100, 100), "pause"));
            controls.Add(new AndroidControl(content.Load<Texture2D>("gameUI/controls/action"), new Rectangle(screenWidth - 490, screenHeight - 220, 200, 200),"action"));
            controls.Add(new AndroidControl(content.Load<Texture2D>("gameUI/controls/jump"), new Rectangle(screenWidth - 740, screenHeight - 220, 200, 200), "jump"));
#endif
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch,SessionData session)
        {
            spriteBatch.Draw(boxTexture, boxRectangle, Color.White);
            spriteBatch.Draw(coinTexture,new Rectangle(boxRectangle.X+(boxRectangle.Width/12), boxRectangle.Y+(boxRectangle.Height/4), (boxRectangle.Width / 12), (boxRectangle.Height / 4)),Color.White);
            spriteBatch.DrawString(font, session.GetPlayerPoints().ToString(), new Vector2(boxRectangle.X + (6*boxRectangle.Width)/30, boxRectangle.Y + (boxRectangle.Height / 4)),Color.Goldenrod, 0.0f,Vector2.Zero,1.5f, SpriteEffects.None,0.0f);

            for(int i = 0; i < session.GetPlayerLives(); i++)
                spriteBatch.Draw(lifeTexture, new Rectangle(boxRectangle.X + (boxRectangle.Width / 12) + i * (boxRectangle.Width / 12), boxRectangle.Y + (boxRectangle.Height / 2), (boxRectangle.Width / 12), (boxRectangle.Height / 4)), Color.White);
#if ANDROID
            foreach (AndroidControl item in controls)
                item.Draw(gameTime, spriteBatch);
#endif

        }
        public void Update(GameTime gameTime)
        {
            
        }
        public GameInputController ReadInput(GameInputController controller)
        {
#if ANDROID
            TouchCollection touches = TouchPanel.GetState();
            Rectangle touchRectangle;
            foreach (var touch in touches)
            {
                touchRectangle = new Rectangle((int)touch.Position.X, (int)touch.Position.Y, 1, 1);
                foreach (AndroidControl button in controls)
                {
                    if (button.IsContainedByButton(touchRectangle))
                    {
                        controller.ProcessButton(button.name);
                    }
                }
            }
#endif
#if DESKTOP
            KeyboardState keyboardState = Keyboard.GetState();
            controller.ProcessKeyboardInput(keyboardState);
#endif


            return controller;
        }

    }
}
