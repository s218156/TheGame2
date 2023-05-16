using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TheGame.Mics;
using TheGame.Multiplayer;
using TheGame.Multiplayer.Models;
using TheGame.Sprites;
using TheGame.States.Menu;

namespace TheGame.States.Levels
{
    public class MultiplayerGameState : State
    {
        bool exitMultiplayer = false;
        Level level;
        PlayerModel playerModel;
        List<PlayerModel> playersModels = new List<PlayerModel>();
        List<ForeignPlayer> foreignPlayers = new List<ForeignPlayer>();
        List<Texture2D> playersTextures = new List<Texture2D>();
        Texture2D deathTexture;
        SpriteFont font;

        public MultiplayerGameState(Game1 game, GraphicsDevice graphics, ContentManager content, SessionData session) : base(game, graphics, content, session)
        {
            playerModel = new PlayerModel();
            playerModel.id=game.multiplayerUser.id;
            playerModel.textureID = game.multiplayerUser.textureId;
            playerModel.rectangle = new PlayerRectangle();
            communicationThread = new Thread(MultiplayerWorker);
            communicationThread.Start();
            level = new Level0(game, graphics, content, session);
            level.isMultiplayer = true;
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
                                
                                Thread.Sleep(1);
                                MultiplayerCommunicationService.SendPlayerData(playerModel);
                                playersModels = await MultiplayerCommunicationService.RefreshSessionData(playerModel);

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
            playerModel.ClearMovementData();
            playerModel.rectangle.X = level.player.rectangle.X;
            playerModel.rectangle.Y = level.player.rectangle.Y;
            playerModel.rectangle.Height = level.player.rectangle.Height;
            playerModel.rectangle.Width = level.player.rectangle.Width;
            playerModel.fullname = game.multiplayerUser.fullname;
            playerModel.isAlive=level.player.isAlive;
            if (level.player.velocity.Y < 0)
            {
                playerModel.isFalling = true;
            }    
            else
            {
                if (level.player.velocity.Y > 0)
                {
                    playerModel.isJumping = true;
                }
                else
                {
                    if (level.player.crouch)
                        playerModel.crouch = true;
                    else
                    {
                        if (Math.Abs(level.player.velocity.X) > 1)
                        {
                            playerModel.isOnMove = true;
                            if (level.player.velocity.X > 0)
                                playerModel.direction = true;
                            else
                                playerModel.direction = false;

                        }
                            
                    }
                }
            }
        }

        public Thread communicationThread;
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            if (level.isReady)
                level.Draw(gameTime, spriteBatch);
            else
                graphics.Clear(Color.AliceBlue);

            spriteBatch.Begin();
            foreach (ForeignPlayer foreignPlayer in foreignPlayers)
            {
                foreignPlayer.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();
        }

        public override void Initialize()
        {
            
        }

        

        public override void Update(GameTime gameTime)
        {
            if (level.isReady)
            {
                level.UpdateForeignPlayers(gameTime, playersModels);
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
}
