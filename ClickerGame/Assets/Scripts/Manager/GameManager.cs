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
        public Player Player;
        public Inventory Inventory;
        public UpgradeShop UpgradeShop;
    }

    public class GameManager
    {
        public StartStatus GetStartStatus()
            => Managers.Data.StartStatus.First().Value;

        public UpgradeData GetUpgradeData(Define.UpgradeType type) =>
            Managers.Data.Upgrade.Values.FirstOrDefault(data => data.UpgradeType == type);


        private GameData _gameData = new GameData();
        public GameData SaveData { get { return _gameData; } set { _gameData = value; } }
        public Player Player { get => SaveData.Player; set => SaveData.Player = value; }
        public UpgradeShop UpgradeShop { get => SaveData.UpgradeShop; set=>SaveData.UpgradeShop = value; }

        public void Initialize()
        {
            if (!LoadGame())
            {
                StartStatus status = GetStartStatus();
                UpgradeData weaponData = Managers.Game.GetUpgradeData(Define.UpgradeType.Weapon);
                UpgradeData defenceData = Managers.Game.GetUpgradeData(Define.UpgradeType.Defence);
                UpgradeData healthData = Managers.Game.GetUpgradeData(Define.UpgradeType.Health);

                Managers.Game.SaveData = new GameData()
                {
                    Inventory = new Inventory()
                    {
                        SaveData = new List<int>(),
                        Slot = status.InventorySlot
                    },
                    Player = new Player()
                    {
                        AtkPower = status.AtkPower,
                        Coin = status.Coin,
                        CraftLevel = status.CraftLevel,
                        DefPower = status.DefPower,
                        Health = status.Health,
                        AddCoin = status.AddCoin,
                    },
                    UpgradeShop = new UpgradeShop()
                    {
                        AtkPower = new Status(weaponData),
                        DefPower = new Status(defenceData),
                        Health = new Status(healthData),
                    }
                };
                Player.Inventory = SaveData.Inventory;
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
                data.Inventory.LoadData();
            }
            
            Debug.Log($"Save Game Loaded : {_path}");
            return true;
        }
        #endregion
    }
}