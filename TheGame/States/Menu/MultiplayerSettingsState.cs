
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using TheGame.Mics;
using TheGame.Mics.GUI_components;
using TheGame.Multiplayer;
using TheGame.Animations;
using System.Collections.Generic;
#if ANDROID
using Android.Content;
using Android.Views;
using Android.Views.InputMethods;
using Android.Systems;
using Android.App;
using Android.Views.InputMethods;
using Android.Views;
#endif


namespace TheGame.States.Menu
{

    class MultiplayerSettingsState : MenuState
    {
        private Button multiplayerButton;
        private int height, width;
        private MultiplayerUserConfig multiplayerConfig;
        private InputBox loginBox, passwordBox;
        private LoadingBarAnimation loadingBar;
        private MultiplayerConfigWorker multiplayerConfigWorker;
        private List<Component> formObjects = new List<Component>();


        public MultiplayerSettingsState(Game1 game, GraphicsDevice graphics, ContentManager content, SessionData session) : base(game, graphics, content, session)
        {
            height = graphics.Viewport.Height;
            width = graphics.Viewport.Width;
            this.multiplayerConfigWorker = new MultiplayerConfigWorker();
            Initialize();
        }



        private void backButtonClick(object sender, EventArgs e)
        {
            game.ChangeState(new MainMenuState(game, graphics, content, null));
        }

        private void ApplyButtonClick(object sender, EventArgs e)
        {
            game.ChangeState(new MainMenuState(game, graphics, content, null));
        }
        private void InputBoxClick(object sender, EventArgs e)
        {
            if (inputTarger == null)
                inputTarger = (InputBox)sender;
            else
                inputTarger = null;

#if ANDROID
            int y = graphics.Viewport.Height / 2;
            if (inputTarger != null)
                y = -1 * y;

            foreach (Button button in _buttons)
            {

                button.MoveButton(0, y);
            }
#endif
            ToogleKeyboard();
        }

        private void LoginButtonClick(object sender, EventArgs e)
        {
            multiplayerConfig.SaveUserConfiguration(loginBox.GetValue(), passwordBox.GetValue());
            multiplayerConfigWorker.StartWorker(loginBox.GetValue(), passwordBox.GetValue());
        }



        public override void Initialize()
        {
            multiplayerConfig = new MultiplayerUserConfig();
#if ANDROID
            _keyboard = new AndroidKeyboard(graphics.Viewport.Height, graphics.Viewport.Width, content.Load<Texture2D>("gameUI/misc/black_rectangle"), content);
#endif
#if DESKTOP
            _keyboard=new PCKeyboardInput();
#endif
            Texture2D buttonTexture = content.Load<Texture2D>("gameUI/button");
            //Dodawanie przycisków powrotu i zastosowania zmian
            int x = (graphics.Viewport.Width / 10) * 8;

            DrawableElement formBackground = new DrawableElement(content.Load<Texture2D>("gameUI/form_background"), new Rectangle(Convert.ToInt32((x / 8)), (graphics.Viewport.Height / 10) * 2, (x / 8) * 4, Convert.ToInt32(graphics.Viewport.Height / 2.5)));

            _components.Add(formBackground);

            loginBox = new InputBox(content.Load<Texture2D>("gameUI/inputBox"), content.Load<SpriteFont>("Fonts/Basic"), new Rectangle((x / 8) * 3, Convert.ToInt32((graphics.Viewport.Height / 10) * 3.5 - (graphics.Viewport.Height / 20)), (graphics.Viewport.Width / 6), (graphics.Viewport.Height / 10)), "Login");

            passwordBox = new InputBox(content.Load<Texture2D>("gameUI/inputBox"), content.Load<SpriteFont>("Fonts/Basic"), new Rectangle((x / 8) * 3, Convert.ToInt32((graphics.Viewport.Height / 10) * 3.5 - (graphics.Viewport.Height / 20) + (graphics.Viewport.Height / 10)), (graphics.Viewport.Width / 6), (graphics.Viewport.Height / 10)), "Password");

            Button loginButton = new Button(buttonTexture, content.Load<SpriteFont>("Fonts/Basic"), new Rectangle((x / 8) * 3, Convert.ToInt32((graphics.Viewport.Height / 10) * 3.5 - (graphics.Viewport.Height / 20) + (graphics.Viewport.Height / 5)), (graphics.Viewport.Width / 6), (graphics.Viewport.Height / 20)), new string("Login"));

            loginButton.Click += LoginButtonClick;
            formObjects.Add(loginButton);
            formObjects.Add(loginBox);
            formObjects.Add(passwordBox);
            loginBox.Click += InputBoxClick;
            passwordBox.Click += InputBoxClick;




            loadingBar = new LoadingBarAnimation(content.Load<Texture2D>("gameUI/loading_bar_animation"), new Rectangle((x / 8) * 3, Convert.ToInt32((graphics.Viewport.Height / 10) * 3.5 - (graphics.Viewport.Height / 20)), (graphics.Viewport.Width / 6), (graphics.Viewport.Height / 9)), 3, 1, "Connecting to server", content.Load<SpriteFont>("Fonts/Basic"));


            Button applyButton = new Button(buttonTexture, content.Load<SpriteFont>("Fonts/Basic"), new Rectangle(x, (graphics.Viewport.Height / 10) * 9 - (graphics.Viewport.Height / 20), (graphics.Viewport.Width / 11), (graphics.Viewport.Height / 10)), new string("Apply"));
            x += graphics.Viewport.Width / 10 + graphics.Viewport.Height / 11;

            Button backButton = new Button(buttonTexture, content.Load<SpriteFont>("Fonts/Basic"), new Rectangle((graphics.Viewport.Width / 10) * 9, (graphics.Viewport.Height / 10) * 9 - (graphics.Viewport.Height / 20), (graphics.Viewport.Width / 11), (graphics.Viewport.Height / 10)), new string("Back"));

            //Dodawanie przycisków ustawień
            int y;
            y = (graphics.Viewport.Height / 10) * 2;
            multiplayerButton = new Button(buttonTexture, content.Load<SpriteFont>("Fonts/Basic"), new Rectangle(graphics.Viewport.Width / 4 * 2, y, (graphics.Viewport.Width / 3), (graphics.Viewport.Height / 10)), "Multiplayer");
            y += graphics.Viewport.Height / 30 + graphics.Viewport.Height / 10;


            backButton.Click += backButtonClick;
            applyButton.Click += ApplyButtonClick;

            _components.Add(applyButton);
            _buttons.Add(applyButton);
            _components.Add(backButton);
            _buttons.Add(backButton);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (multiplayerConfigWorker.attemptCompleted)
            {

            }
            else
            {
                if ((!multiplayerConfig.CheckIfUserIsConfigured()) || (!multiplayerConfigWorker.attemptBegan))
                {
                    foreach (Component obj in formObjects)
                    {
                        obj.Update(gameTime);
                    }
                }
                else
                {
                    multiplayerConfig.UpdateMultiplayerData(multiplayerConfigWorker.GetMultiplayerData());
                    if (loadingBar != null)
                        loadingBar.Update(gameTime, null);
                }
            }




        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);



            spriteBatch.Begin();

            if (multiplayerConfigWorker.attemptCompleted)
            {

            }
            else
            {
                if ((!multiplayerConfig.CheckIfUserIsConfigured()) || (!multiplayerConfigWorker.attemptBegan))
                {
                    foreach (Component obj in formObjects)
                    {
                        obj.Draw(gameTime, spriteBatch);
                    }

                }
                else
                {
                    if (loadingBar != null)
                    {
                        loadingBar.Draw(gameTime, spriteBatch);
                    }
                }
            }


            spriteBatch.End();

        }
    }
}
