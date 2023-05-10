
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#if DESKTOP
using System.Threading;
using System;
#endif
#if ANDROID
using Java.Lang;
using Android.Service.Voice;
using Android.OS;
#endif
using TheGame.Mics;
using TheGame.States;
using TheGame.States.Menu;
using TheGame.Multiplayer;
using TheGame.Mics.GUI_components;
using System.IO;


namespace TheGame
{
    public class Game1 : Game
    {
        public static int screenWidth, screenHeight;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private State _currentState;
        private State _nextState;
        private Camera _camera;
        public ScreenSettings screen;
        private State _gameState;
        public MultiplayerObject multiplayerUser;
        private MultiplayerUserHUD multiplayerHUD;
        public Game1()
        {

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        public void ChangeState(State state)
        {
            _nextState = state;
        }


        protected override void Initialize()
        {
            VerifyLocalPath();
            screen = new ScreenSettings();
            base.Initialize();


        }

        protected override void LoadContent()
        {
#if DESKTOP
            if (screen.ConfigFileExist())
                screen=screen.Load();
            else
            {
                screen.height = 900;
                screen.width = 1600;
                screen.isFullScreen = false;
                screen.Save();
            }
#endif
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _graphics.PreferredBackBufferWidth = screen.width;
            _graphics.PreferredBackBufferHeight = screen.height;
            _graphics.IsFullScreen = screen.isFullScreen;
            _graphics.ApplyChanges();
            _currentState = new MainMenuState(this, GraphicsDevice, Content, null);
            _camera = new Camera();
            StartGettingUserData();
            

        }

        protected override void Update(GameTime gameTime)
        {
#if ANDROID



#endif

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();
            if (!(_currentState is MenuState))
            {
                if ((_currentState.controller.isPause))
                {
                    _currentState.controller.isPause = false;
                    Thread.Sleep(200);
                    _currentState.SaveDataToSession();
                    _gameState = _currentState;
                    _currentState = new PauseMenuState(this, GraphicsDevice, Content, _gameState, null);
                }
            }


            screenHeight = _graphics.PreferredBackBufferHeight;
            screenWidth = _graphics.PreferredBackBufferWidth;
            _currentState.Update(gameTime);

            if (_nextState != null)
            {
                _currentState = _nextState;
                _nextState = null;
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _currentState.Draw(gameTime, _spriteBatch);


            base.Draw(gameTime);
            if((_currentState is  MenuState)&&(!(_currentState is MultiplayerSettingsState))) 
            {
                if (multiplayerHUD != null)
                {
                    _spriteBatch.Begin();
                    multiplayerHUD.Draw(gameTime, _spriteBatch);
                    _spriteBatch.End();
                }
            }
            
        }

        public void ChangeResolution(bool isFullScreen, int height, int width)
        {
            screen.height = height;
            screen.width = width;
            screen.isFullScreen = isFullScreen;
            _graphics.PreferredBackBufferWidth = width;
            _graphics.PreferredBackBufferHeight = height;
            _graphics.IsFullScreen = isFullScreen;
            _graphics.ApplyChanges();
            screen.Save();
        }

        public void StartGettingUserData()
        {
            Thread t1 = new Thread(GetDataForUser);
            t1.Start();
        }

        public async void GetDataForUser()
        {
            MultiplayerUserConfig mutliplayerConfig = new MultiplayerUserConfig();
            try
            {
                if (mutliplayerConfig.CheckIfUserIsConfigured())
                {
                    MultiplayerData data = mutliplayerConfig.GetUserConfigFromFile();
                    await MultiplayerCommunicationService.VerifyUserConfig(data.username, data.userPrivateKey);
                    multiplayerUser = await MultiplayerCommunicationService.GetMultiplayerObject(data);
                    string textureName = "gameUI/playerMiniature/" + multiplayerUser.textureId.ToString();
                    multiplayerHUD = new MultiplayerUserHUD(Content.Load<Texture2D>("gameUI/inputBox"), Content.Load<Texture2D>(textureName), new Rectangle(screenWidth - screenWidth / 5, 10, (screenWidth / 6) + 10, (screenHeight / 10) + 10), Content.Load<SpriteFont>("Fonts/Basic"), multiplayerUser);
                }
                else
                {
                    throw new Exception();
                }
               
            }catch(Exception e)
            {
                mutliplayerConfig.RemoveConfigFile();
                multiplayerUser = null;
            }
        }

        private void VerifyLocalPath()
        {

            string path = "";
#if DESKTOP
            path = System.Environment.ExpandEnvironmentVariables("%userprofile%/documents/TheGame");
#endif
#if ANDROID
            path = "/sdcard/TheGame";
#endif
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}
