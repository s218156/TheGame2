using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Animations;
using TheGame.Items;
using TheGame.Mics;
using TheGame.Multiplayer.Models;

namespace TheGame.Sprites
{
    public class ForeignPlayer : Sprite
    {
        public int id;
        private string fullname;
        private SpriteFont font;
        private bool isOnMove;
        private bool direction;
        public ForeignPlayer(Texture2D texture, Vector2 position, Texture2D deathTexture,SpriteFont font, PlayerModel model) : base(position, deathTexture)
        {
            this.id = model.id;
            this.animatedTexture = new CharacterAnimation(texture, rectangle);
            crouch = false;
            isOnLadder = false;
            isAlive = true;
            fullname = model.fullname;
            this.font = font;

        }

        public void UpdateData(PlayerModel model)
        {
            this.isAlive = model.isAlive;
            this.crouch = model.crouch;
            this.rectangle.X=model.rectangle.X;
            this.rectangle.Y=model.rectangle.Y;
            this.isOnMove = model.isOnMove;
            this.direction = model.direction;
        }

        public override void Update(GameTime gameTime, Player player, TileMap map, List<MovableItem> movableList, List<WaterArea> waterAreas)
        {
            


            if (isAlive)
            {
                IsOnLadder(map);
            }
            base.Update(gameTime, player, map, movableList, waterAreas);
            if (isOnMove)
                animatedTexture.SetMovement(true);
            animatedTexture.SetDirection(direction);

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, fullname, new Vector2(rectangle.X, rectangle.Y - 30), Color.Black);
            base.Draw(gameTime, spriteBatch);
        }

        private void IsOnLadder(TileMap map)
        {
            isOnLadder = false;
            foreach (Rectangle tmp in map.GetLadders())
            {
                if (rectangle.Intersects(tmp))
                    isOnLadder = true;
            }
        }





    }
}
