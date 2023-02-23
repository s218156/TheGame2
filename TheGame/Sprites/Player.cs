using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

using System.Text;
using TheGame.Animations;
using TheGame.Inventory;
using TheGame.Items;
using TheGame.Mics;

namespace TheGame.Sprites
{
    public class Player:Sprite
    {
        public int points;
        public int lifes;
        public int jumpHeight;
        private List<InventoryItem> inventory;
        public bool doingAction;
        
        public Player(Texture2D texture, Vector2 position,Texture2D deathTexture, int lifes) : base(position, deathTexture)
        {
            this.doingAction= false;
            inventory = new List<InventoryItem>();
            this.animatedTexture = new CharacterAnimation(texture, rectangle);
            crouch = false;
            isOnLadder = false;
            this.lifes = lifes;
            hitPoints = 20;
            jumpHeight = 30;
        }

        public override void Update(GameTime gameTime, Player player, TileMap map,List<MovableItem>movableList, List<WaterArea> waterAreas)
        {
            if (isAlive)
            {
                IsOnLadder(map);
                //GetMovementFormKeyboard(map);
                CrouchingInfluence();
            }
            else
            {
                if (deathTime >= 80)
                    lifes--;
            }
            base.Update(gameTime, player, map,movableList, waterAreas);
            foreach (InventoryItem item in inventory)
                item.Update(gameTime, this);
            
        }

        private void CrouchingInfluence()
        {
            if (crouch&floorColision)
                velocity.X = velocity.X - (int)velocity.X / 4;                
        }

        private void IsOnLadder(TileMap map)
        {
            isOnLadder = false;
            foreach(Rectangle tmp in map.GetLadders())
            {
                if (rectangle.Intersects(tmp))
                    isOnLadder = true;
            }
        }

        public void ColisionWithMovingBug(MovingBug bug, Vector2 bugVelocity, int bugHitPoints)
        {
            if (isAlive)
            {
                if (velocity.Y > 0)
                {
                    bug.AttackedByPlayer();
                    velocity.Y = -20;
                } 
                else
                {
                    lifePoints -= bugHitPoints;
                    velocity.X = bugVelocity.X * 2;
                    velocity.Y = -25;
                }
            }
        }


        public void UpdateInventoryList(InventoryItem item)
        {
            inventory.Add(item);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (isOnLadder)
            {
                foreach (InventoryItem item in inventory)
                {
                    if (item.drawableOnTop)
                        item.Draw(gameTime, spriteBatch);
                }
                base.Draw(gameTime, spriteBatch);
                foreach (InventoryItem item in inventory)
                {
                    if(!item.drawableOnTop)
                        item.Draw(gameTime, spriteBatch);
                }
            }
            else
            {
                foreach (InventoryItem item in inventory)
                {
                    if(!item.drawableOnTop)
                        item.Draw(gameTime, spriteBatch);
                }
                base.Draw(gameTime, spriteBatch);
                foreach (InventoryItem item in inventory)
                {
                    if (item.drawableOnTop)
                        item.Draw(gameTime, spriteBatch);
                }
            } 
        }


        public void GetMovementFormKeyboard(TileMap map,GameInputController controller)
        {
            
            //if (keyState.IsKeyDown(Keys.LeftControl))
            //    attacking = 10;

            if (controller.isUp)
            {
                if(isOnLadder)
                    velocity.Y--;
                if (isUnderWater)
                    velocity.Y -= 2;
            }
                

            if (controller.isRight)
                velocity.X++;
            
            if (controller.isLeft)
                velocity.X--;
            
            if (controller.isJump & floorColision)
            {
                velocity.Y = -1*jumpHeight;
                jump = true;
            }
            if (controller.isDown)
            {
                if (floorColision&!crouch)
                {
                    crouch = true;
                    rectangle.Y = rectangle.Y + 35;
                    rectangle.Height = rectangle.Height - 35;
                }
                else
                    velocity.Y++;
                
            }
            if (crouch&!controller.isDown)
            {
                Rectangle newRectangle = rectangle;
                newRectangle.Height = rectangle.Height + 35;
                newRectangle.Y-=35;
                bool isColiding = false;
                foreach(var obj in map.GetMapObjectList())
                {
                    if (newRectangle.Intersects(obj)){
                        isColiding = true;
                    }
                }
                if (!isColiding)
                {
                    crouch = false;
                    rectangle.Y = rectangle.Y - 35;
                    rectangle.Height = rectangle.Height + 35;
                } 
            }
            doingAction = controller.isAction;
        }

    }
}