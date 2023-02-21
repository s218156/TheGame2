using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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
            controls.Add(new AndroidControl(content.Load<Texture2D>("gameUI/controls/arrowLeft"), new Rectangle(40, screenHeight - 240, 200, 200)));
            controls.Add(new AndroidControl(content.Load<Texture2D>("gameUI/controls/arrowRight"), new Rectangle(290, screenHeight - 240, 200, 200)));
            controls.Add(new AndroidControl(content.Load<Texture2D>("gameUI/controls/arrowDown"), new Rectangle(screenWidth - 240, screenHeight - 220, 200, 200)));
            controls.Add(new AndroidControl(content.Load<Texture2D>("gameUI/controls/arrowUp"), new Rectangle(screenWidth - 240, screenHeight - 450, 200, 200)));
            controls.Add(new AndroidControl(content.Load<Texture2D>("gameUI/controls/pause"), new Rectangle(screenWidth - 120, 20, 100, 100)));
            controls.Add(new AndroidControl(content.Load<Texture2D>("gameUI/controls/action"), new Rectangle(screenWidth - 490, screenHeight - 220, 200, 200)));
#endif
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch,SessionData session)
        {
            spriteBatch.Draw(boxTexture, boxRectangle, Color.White);
            spriteBatch.Draw(coinTexture,new Rectangle(boxRectangle.X+25, boxRectangle.Y+25, 25, 25),Color.White);
            spriteBatch.DrawString(font, session.GetPlayerPoints().ToString(), new Vector2(boxRectangle.X + 60, boxRectangle.Y + 25),Color.Goldenrod);

            for(int i = 0; i < session.GetPlayerLives(); i++)
                spriteBatch.Draw(lifeTexture, new Rectangle(boxRectangle.X + 25 + i * 25, boxRectangle.Y + 50, 25, 25), Color.White);
#if ANDROID
            foreach (AndroidControl item in controls)
                item.Draw(gameTime, spriteBatch);
#endif

        }
        public void Update(GameTime gameTime)
        {
        }

    }
}
