using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TheGame.Mics;
using TheGame.SaveAndLoadControllers;
using TheGame.Sprites;
using TheGame.States.Levels;

namespace TheGame.States.Menu
{
    class StartGameMenuState:MenuState
    {
        public StartGameMenuState(Game1 game, GraphicsDevice graphics, ContentManager content, SessionData session) : base(game,graphics,content,session)
        {
            Initialize();
        }

        public override void Initialize()
        {
            Texture2D buttonTexture = content.Load<Texture2D>("gameUI/button");
            int y;
            y = (graphics.Viewport.Height / 10) * 3;


            Button newGameButton = new Button(buttonTexture, content.Load<SpriteFont>("Fonts/Basic"), new Rectangle(graphics.Viewport.Width / 3, y, (graphics.Viewport.Width / 3), (graphics.Viewport.Height / 10)), new string("New Game"));
            y += graphics.Viewport.Height / 30 + graphics.Viewport.Height / 10;
            newGameButton.Click += NewGameButtonClick;

            Button backButton = new Button(buttonTexture, content.Load<SpriteFont>("Fonts/Basic"), new Rectangle((graphics.Viewport.Width / 10) * 9, (graphics.Viewport.Height / 10) * 9 - (graphics.Viewport.Height / 20), (graphics.Viewport.Width / 11), (graphics.Viewport.Height / 10)), new string("Back"));
            backButton.Click += backButtonClick;


            _components = new List<Component>()
            {
                newGameButton,
                backButton
            };
            _buttons = new List<Button>()
            {
                newGameButton,
                backButton
            };


            string path;
#if DESKTOP
            path = Environment.ExpandEnvironmentVariables("%userprofile%/documents/TheGame/Save1.xml");
#endif
#if ANDROID
            path = "/sdcard/TheGame/Save1.xml";
#endif
            if (File.Exists(path))
            {
                Button loadGameButton = new Button(buttonTexture, content.Load<SpriteFont>("Fonts/Basic"), new Rectangle(graphics.Viewport.Width / 3, y, (graphics.Viewport.Width / 3), (graphics.Viewport.Height / 10)), new string("Load Game"));
                y += graphics.Viewport.Height / 30 + graphics.Viewport.Height / 10;
                loadGameButton.Click += LoadGameButtonClick;
                _buttons.Add(loadGameButton);
                _components.Add(loadGameButton);
            }

            base.Initialize();

        }
        private void NewGameButtonClick(object sender, EventArgs e)
        {
            SessionData session = new SessionData();
            game.ChangeState(new LoadingScreen(game,graphics,content,session,0,false));
        }

        private void backButtonClick(object sender, EventArgs e)
        {
            game.ChangeState(new MainMenuState(game, graphics, content, null));
        }

        
        private void LoadGameButtonClick(object sender, EventArgs e)
        {
            SessionData session = new SessionData();
            game.ChangeState(new LoadingScreen(game, graphics, content, session, 0, true));
        }
    }
}
