﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Items;
using TheGame.Mics;
using TheGame.Sprites;
using TheGame.States;

namespace TheGame.SaveAndLoadControllers
{
    public class SubLevelLevelData
    {
        public int LevelId { set; get; }
        public PlayerData playerData { get; set; }
        public List<SpriteData> spritesData { get; set; }
        public List<ItemData> itemsData { get; set; }
        public int playerLives { get; set; }
        public int PlayerPoints { get; set; }
        public Vector2 spawnPoint { get; set; }
        public List<MovableData> movablesData { get; set; }
        public GameMasterData gameMasterData { get; set; }

        public SubLevelLevelData()
        {
            this.playerData = new PlayerData();
            this.spritesData = new List<SpriteData>();
            this.itemsData = new List<ItemData>();
            this.movablesData = new List<MovableData>();
            this.spawnPoint = Vector2.Zero;
            this.gameMasterData = new GameMasterData();
        }
        public void UpdateSaveGameData(Level level)
        {
            this.gameMasterData.UpdateGameMasterData(level.gameMaster);
            this.LevelId = level.levelId;
            this.playerData.UpdatePlayerData(level.player);
            foreach (Sprite sprite in level.sprites)
            {
                SpriteData tmp = new SpriteData();
                tmp.UpdateSpriteData(sprite);
                spritesData.Add(tmp);
            }
            foreach (Item item in level.items)
            {
                ItemData tmp = new ItemData();
                tmp.UpdateItemData(item);
                itemsData.Add(tmp);
            }
            foreach (MovableItem item in level.movableItems)
            {
                MovableData tmp = new MovableData();
                tmp.UpdateMovableData(item);
                movablesData.Add(tmp);
            }



            this.playerLives = level.session.GetPlayerLives();
            this.PlayerPoints = level.session.GetPlayerPoints();
            this.spawnPoint = level.spawnPoint;
        }

        

    }
}

