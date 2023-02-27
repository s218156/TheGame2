using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheGame.Mics
{
    public class GameInputController
    {
        public bool isUp;
        public bool isDown;
        public bool isLeft;
        public bool isRight;
        public bool isAction;
        public bool isPause;
        public bool isJump;

        public void Clear()
        {
            isUp = false;
            isDown = false;
            isLeft = false;
            isRight = false;
            isAction = false;
            isPause = false;
            isJump = false;
        }

        public void ProcessButton(string name)
        {
            switch(name)
            {
                case "up":
                    this.isUp = true;
                    break;
                case "down":
                    this.isDown = true;
                    break;
                case "left":
                    this.isLeft = true;
                    break;
                case "right":
                    this.isRight = true;
                    break;
                case "action":
                    this.isAction = true;
                    break;
                case "pause":
                    this.isPause = true;
                    break;
                case "jump":
                    this.isJump = true;
                    break;
                default:
                    break;
            }
        }
        public void ProcessKeyboardInput(KeyboardState currentState)
        {

            if (currentState.IsKeyDown(Keys.W)) this.isUp = true;
            if(currentState.IsKeyDown(Keys.S)) this.isDown = true;
            if(currentState.IsKeyDown(Keys.A)) this.isLeft = true;
            if(currentState.IsKeyDown(Keys.D)) this.isRight = true;
            if (currentState.IsKeyDown(Keys.Escape)) this.isPause = true;
            if(currentState.IsKeyDown(Keys.F)) this.isAction = true;
            if(currentState.IsKeyDown(Keys.Space)) this.isJump = true; 

        }

    }
}
