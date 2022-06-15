using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Content;
using Data;
using UnityEngine;

namespace Manager
{
    [Serializable]
    public class GameData
    {
        public int AddCoin;
        public int Coin;
        public int AtkPower;
        public int DefPower;
        public int Health;
        public int InventorySlot;
        public List<ItemData> ItemData;
    }
    
    
    public class GameManager
    {
        private GameData _gameData = new GameData();
        public GameData SaveData { get { return _gameData; } set { _gameData = value; } }
        public Player Player { get; private set; } = new Player();
        
        public void Initialize()
        {
            if (!LoadGame())
            {
                StartStatus status = Managers.Data.StartStatus.First().Value;
                
                Managers.Game.SaveData = new GameData()
                {
                    AddCoin = status.AddCoin,
                    Coin = status.Coin,
                    AtkPower = status.AtkPower,
                    DefPower = status.DefPower,
                    Health = status.Health,
                    InventorySlot = status.InventorySlot,
                    ItemData = new List<ItemData>()
                };
                
                Managers.Game.Player.SetData(Managers.Game.SaveData);
            }
        }
        
        #region Save & Load	
        public string _path = Application.persistentDataPath + "/SaveData.json";

        public void SaveGame()
        {
            string jsonStr = JsonUtility.ToJson(Managers.Game.SaveData);
            File.WriteAllText(_path, jsonStr);
            Debug.Log($"Save Game Completed : {_path}");
        }

        public bool LoadGame()
        {
            if (File.Exists(_path) == false)
                return false;

            string fileStr = File.ReadAllText(_path);
            GameData data = JsonUtility.FromJson<GameData>(fileStr);
            if (data != null)
            {
                Managers.Game.SaveData = data; 
                Managers.Game.Player.SetData(data);
            }
            
            Debug.Log($"Save Game Loaded : {_path}");
            return true;
        }
        #endregion
    }
}