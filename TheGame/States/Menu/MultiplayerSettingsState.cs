
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using TheGame.Mics;
using TheGame.Mics.GUI_components;
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


        public MultiplayerSettingsState(Game1 game, GraphicsDevice graphics, ContentManager content, SessionData session) : base(game, graphics, content, session)
        {
            height = graphics.Viewport.Height;
            width = graphics.Viewport.Width;
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
                y = -1*y;
                
            foreach (Button button in _buttons)
            {

                button.MoveButton(0, y);
            }
            ToogleKeyboard();
#endif
        }


        public override void Initialize()
        {
#if ANDROID
            _keyboard = new AndroidKeyboard(graphics.Viewport.Height, graphics.Viewport.Width, content.Load<Texture2D>("gameUI/misc/black_rectangle"), content);
#endif
            Texture2D buttonTexture = content.Load<Texture2D>("gameUI/button");
            //Dodawanie przycisków powrotu i zastosowania zmian
            int x = (graphics.Viewport.Width / 10) * 8;

            DrawableElement formBackground = new DrawableElement(content.Load<Texture2D>("gameUI/form_background"), new Rectangle(Convert.ToInt32((x / 8) * 2), (graphics.Viewport.Height / 10) * 2, (x/8)*3, Convert.ToInt32(graphics.Viewport.Height/2.5)));

            _components.Add(formBackground);

            InputBox loginBox = new InputBox(content.Load<Texture2D>("gameUI/inputBox"), content.Load<SpriteFont>("Fonts/Basic"), new Rectangle((x / 8) * 3, (graphics.Viewport.Height / 10) * 3 - (graphics.Viewport.Height / 20), (graphics.Viewport.Width / 6), (graphics.Viewport.Height / 10)));

            InputBox passBox = new InputBox(content.Load<Texture2D>("gameUI/inputBox"), content.Load<SpriteFont>("Fonts/Basic"), new Rectangle((x / 8) * 3, (graphics.Viewport.Height / 10) * 3 - (graphics.Viewport.Height / 20) + (graphics.Viewport.Height / 10), (graphics.Viewport.Width / 6), (graphics.Viewport.Height / 10)));

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
            _components.Add(loginBox);
            _components.Add(passBox);
            _components.Add(applyButton);
            _buttons.Add(applyButton);
            _components.Add(backButton);
            loginBox.Click += InputBoxClick;
            passBox.Click += InputBoxClick;
            _buttons.Add(backButton);

            base.Initialize();
        }
    }
}
