using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Multiplayer;

namespace TheGame.Mics.GUI_components
{
    public class MultiplayerUserHUD : Component
    {
        private Rectangle rectangle;
        private SpriteFont font;
        private Texture2D backgroudTexture;
        private MultiplayerObject data;
        private Texture2D playerMiniature;

        public MultiplayerUserHUD(Texture2D backgroundTexture,Texture2D playerMiniature, Rectangle rectangle, SpriteFont font, MultiplayerObject data)
        {
            this.rectangle = rectangle;
            this.font = font;
            this.backgroudTexture = backgroundTexture;
            this.data = data;
            this.playerMiniature = playerMiniature;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroudTexture, rectangle, Color.White);
            spriteBatch.DrawString(font, data.fullname, new Vector2(rectangle.X + 10, rectangle.Y + rectangle.Height/3), Color.Black);
            spriteBatch.Draw(playerMiniature, new Rectangle(rectangle.X+(rectangle.Width-10-rectangle.Height-20), rectangle.Y+10, rectangle.Height-20,rectangle.Height-20), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
