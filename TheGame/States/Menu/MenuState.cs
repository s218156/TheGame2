﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using TheGame.Mics;
using TheGame.Mics.GUI_components;
using TheGame.Sprites;

namespace TheGame.States.Menu
{
    class MenuState : State
    {
        private bool wasControllerUp = true;
        protected List<Paralax> _paralaxes;
        protected List<Component> _components;
        protected List<Button> _buttons;
        protected Selection selection;
        protected bool isKeyboardVisible;
        protected InputBox inputTarger;
        protected KeyboardInputBase _keyboard;


        public MenuState(Game1 game, GraphicsDevice graphics, ContentManager content, SessionData session) : base(game, graphics, content, session)
        {
            _buttons = new List<Button>();
            _components = new List<Component>();
            isKeyboardVisible = false;
        }

        public override void Initialize()
        {
#if DESKTOP
            selection = new Selection(content);
            selection.SetSelectionPosition(_buttons);
#endif

            _paralaxes = new List<Paralax>();

            _paralaxes.Add(new Paralax(content.Load<Texture2D>("Backgrounds/Level0/l0p2"), graphics, new Vector2(3, 0), new Vector2((float)0.5, (float)0.9)));
            _paralaxes.Add(new Paralax(content.Load<Texture2D>("Backgrounds/Level0/l0p3"), graphics, new Vector2((float)2, 0), new Vector2((float)0.5, (float)0.9)));
            _paralaxes.Add(new Paralax(content.Load<Texture2D>("Backgrounds/Level0/l0p0"), graphics, new Vector2((float)1, 0), new Vector2((float)0.5, (float)0.9)));
            _paralaxes.Add(new Paralax(content.Load<Texture2D>("Backgrounds/Level0/l0p1"), graphics, new Vector2(3, 0), new Vector2((float)0.5, (float)0.9)));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (var tmp in _paralaxes)
                tmp.Draw(gameTime, spriteBatch);

            foreach (var item in _components)
                item.Draw(gameTime, spriteBatch);
#if DESKTOP
            selection.Draw(gameTime, spriteBatch);
#endif
            spriteBatch.End();
#if ANDROID
            if (isKeyboardVisible)
                _keyboard.Draw(gameTime, spriteBatch);
#endif
        }

        public override void Update(GameTime gameTime)
        {

            if (wasControllerUp)
            {
                foreach (Component item in _components)
                    item.Update(gameTime);
            }

#if DESKTOP
            selection.Update(_buttons);
#endif
            if (isKeyboardVisible && wasControllerUp)
                _keyboard.Update(gameTime);

            if (inputTarger != null)
            {
                inputTarger.UpdateValue(_keyboard.GetValue());
            }
                


            foreach (Paralax tmp in _paralaxes)
                tmp.Update(new Player(null, Vector2.Zero, null, 1), graphics);
            

#if DESKTOP
            wasControllerUp = ((Mouse.GetState().LeftButton != ButtonState.Pressed));
            
#endif

#if ANDROID
            wasControllerUp = TouchPanel.GetState().Count == 0;
#endif

        }

        public void ToogleKeyboard()
        {
            isKeyboardVisible = !isKeyboardVisible;
        }



    }

}
