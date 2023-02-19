﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Animations;
using TheGame.Items;
using TheGame.Mics;

namespace TheGame.Sprites
{
    public abstract class Sprite:PhysicalObject
    {
        public bool isOnLadder;
        public bool jump;
        public AnimatedTexture animatedTexture;
        protected int lifePoints;
        public bool isAlive;
        public int attacking;
        public bool crouch;
        public int hitPoints , deathTime;
        private ItemAnimation deathAnimation;
        protected bool canFly;
        public bool isUnderWater;



        public Sprite(Vector2 position,Texture2D deathTexture):base(null,position)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, 100, 100);
            velocity = Vector2.Zero;
            jump = false;
            lifePoints = 100;
            isAlive = true;
            attacking = 0;
            deathTime = 0;
            deathAnimation = new ItemAnimation(deathTexture, rectangle, 4, 2);
            canFly = false;
            isUnderWater = false;
            
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animatedTexture.Draw(gameTime, spriteBatch);
            if(!(isAlive))
            {
                if (deathTime < 25)
                    deathAnimation.Draw(gameTime, spriteBatch);
                
            }
            
        }
        public virtual void Update(GameTime gameTime,Player player, TileMap map,List<MovableItem> movableList,List<WaterArea> waterAreas)
        {
            if ((isAlive)&(deathTime<=80))
            {
                FrictionCount();
                if (!canFly & isAlive & !isOnLadder)
                    GravitySimulation();
                IsOnObstracles(map);
                CheckEnviromentColision(map);
                CheckColisionWithMovables(movableList,map);
                UpdatePosition();
                
                if (attacking > 0)
                    attacking--;
                

                if (lifePoints <= 0)
                {
                    isAlive = false;
                    deathAnimation.UpdateRectangle(rectangle);
                    velocity = Vector2.Zero;
                }
            }
            else
            {

                GravitySimulation();
                CheckEnviromentColision(map);
                UpdatePosition();

                deathTime++;
                deathAnimation.Update(gameTime,null);
            }
            animatedTexture.Update(gameTime,this);
            CheckIfIsUnderWater(waterAreas);
        }

        public void IsUnderAttack(Sprite enemy)
        {
            if (rectangle.Intersects(enemy.rectangle))
            {
                if (enemy.attacking == 5)
                    lifePoints -= enemy.hitPoints;
                
            }
        }

        private void IsOnObstracles(TileMap map)
        {
            foreach (var tmp in map.GetObstracles())
            {
                if (rectangle.Intersects(tmp))
                    lifePoints -= 100;
                
            }
        }

        public void CheckIfIsUnderWater(List<WaterArea> waterAreas)
        {
            int tmp = 0;
            foreach (WaterArea waterArea in waterAreas)
            {
                if (rectangle.Intersects(waterArea.rectangle))
                {
                    tmp++;
                }
            }
            if (tmp > 0)
            {
                friction = (float)0.15;
                isUnderWater = true;
            }
            else
            {
                SetDefaultFriction();
                isUnderWater = false;
            }
        }

        public void CheckColisionWithMovables(List<MovableItem> items,TileMap map)
        {
            int i;
            foreach (MovableItem obj in items)
            {
                if (new Rectangle(rectangle.X+(int)velocity.X,rectangle.Y,rectangle.Width,rectangle.Height).Intersects(obj.rectangle))
                    obj.UpdatePosition(this, map,items);

                //kolizje po Y
                
                if (velocity.Y >= 0)
                {
                    for (i = 0; i <= (int)velocity.Y; i++)
                    {
                        if ((new Rectangle(rectangle.X, rectangle.Y + i, rectangle.Width, rectangle.Height).Intersects(obj.rectangle)))
                        {
                            floorColision = true;
                            jump = false;
                            velocity.Y = i - 1;
                        }
                    }
                }
                else
                {
                    for (i = -1; i >= (int)velocity.Y; i--)
                    {
                        if ((new Rectangle(rectangle.X, rectangle.Y + i, rectangle.Width, rectangle.Height).Intersects(obj.rectangle)))
                            velocity.Y = i + 1;
                        
                    }
                }

                //kolizje po X

                if (velocity.X >= 0)
                {
                    for (i = 0; i <= (int)velocity.X; i++)
                    {
                        if ((new Rectangle(rectangle.X + i, rectangle.Y, rectangle.Width, rectangle.Height).Intersects(obj.rectangle)))
                            velocity.X = i - 1;

                    }
                }
                else
                {
                    for (i = -1; i >= (int)velocity.X; i--)
                    {
                        if ((new Rectangle(rectangle.X + i, rectangle.Y, rectangle.Width, rectangle.Height).Intersects(obj.rectangle)))
                            velocity.X = i + 1;

                    }
                }
            }
        } 
    }
}