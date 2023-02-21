﻿
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Threading;
using TheGame.Mics;
using TheGame.States;
using TheGame.States.Menu;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input.Touch;

#if ANDROID
using Android.Service.Voice;
using Android.OS;
#endif

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
            _currentState = new MainMenuState(this, GraphicsDevice, Content,null);
            _camera = new Camera();

        }

        protected override void Update(GameTime gameTime)
        {
#if ANDROID
            TouchCollection touch = TouchPanel.GetState();
            if(touch.Count> 0)
                System.Diagnostics.Debug.WriteLine(touch[0].Position.X+", "+ touch[0].Position.Y);
#endif

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            if ((Keyboard.GetState().IsKeyDown(Keys.Escape))&(!(_currentState is MenuState)))
            {
                Thread.Sleep(200);
                _currentState.SaveDataToSession();
                _gameState = _currentState;
                _currentState = new PauseMenuState(this,GraphicsDevice,Content,_gameState,null);
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
        }

        public void ChangeResolution(bool isFullScreen, int height, int width)
        {
            screen.height = height;
            screen.width = width;
            screen.isFullScreen = isFullScreen;
            _graphics.PreferredBackBufferWidth = width;
            _graphics.PreferredBackBufferHeight = height;
            _graphics.IsFullScreen=isFullScreen;
            _graphics.ApplyChanges();
            screen.Save();
        }

    }
}
