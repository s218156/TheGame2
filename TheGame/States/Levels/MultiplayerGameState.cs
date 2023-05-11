using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TheGame.Mics;
using TheGame.Multiplayer;
using TheGame.Multiplayer.Models;
using TheGame.States.Menu;

namespace TheGame.States.Levels
{
    public class MultiplayerGameState : State
    {
        bool exitMultiplayer = false;
        Level level;
        PlayerModel playerModel;
        List<PlayerModel> playersModels = new List<PlayerModel>();
        public MultiplayerGameState(Game1 game, GraphicsDevice graphics, ContentManager content, SessionData session) : base(game, graphics, content, session)
        {
            playerModel = new PlayerModel();
            playerModel.id=game.multiplayerUser.id;
            playerModel.textureID = game.multiplayerUser.textureId;
            playerModel.rectangle = new PlayerRectangle();
            communicationThread = new Thread(MultiplayerWorker);
            communicationThread.Start();
            level = new Level0(game, graphics, content, session);
            controller = new GameInputController();

            level.prepareLevel();
        }

        public async void MultiplayerWorker()
        {
            int connectionAttempts = 0;
            while (true)
            {
                if (level != null)
                {
                    if (level.isReady)
                    {
                        if (await MultiplayerCommunicationService.JoinPlayer(playerModel))
                        {
                            
                            while (true)
                            {
                                Thread.Sleep(500);
                                MultiplayerCommunicationService.SendPlayerData(playerModel);
                                
                            }
                        }
                        else
                            connectionAttempts++;
                    }
                }
                
                if (connectionAttempts > 9)
                    break;
                Thread.Sleep(2000);
            }
            exitMultiplayer = true;
        }

        private void UpdatePlayerModel()
        {
            playerModel.rectangle.X = level.player.rectangle.X;
            playerModel.rectangle.Y = level.player.rectangle.Y;
            playerModel.rectangle.Height = level.player.rectangle.Height;
            playerModel.rectangle.Width = level.player.rectangle.Width;
        }

        public Thread communicationThread;
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (level.isReady)
                level.Draw(gameTime, spriteBatch);
            else
                graphics.Clear(Color.AliceBlue);
        }

        public override void Initialize()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if (exitMultiplayer)
                game.ChangeState(new MainMenuState(game, graphics, content, null));
            if (level.isReady)
            {
                level.Update(gameTime);
                controller = level.controller;
            }
            UpdatePlayerModel();
        }
    }
}
