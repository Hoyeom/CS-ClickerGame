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
        public Define.Language Language;
    }
    
    

    public class GameManager
    {
        public StartStatusData GetStartStatus()
            => Managers.Data.StartStatus.First().Value;

        public UpgradeData GetUpgradeData(Define.UpgradeType type) =>
            Managers.Data.Upgrade.Values.FirstOrDefault(data => data.UpgradeType == type);


        private GameData _gameData = new GameData();
        public GameData SaveData { get { return _gameData; } set { _gameData = value; } }
        public Player Player { get => SaveData.Player; set => SaveData.Player = value; }
        public Combat Combat { get; set; } = new Combat();
        public UpgradeShop UpgradeShop { get => SaveData.UpgradeShop; set=>SaveData.UpgradeShop = value; }

        public Transform PlayerSpawnArea { get; private set; }
        public Transform EnemySpawnArea { get; private set; }

        public void SetPlayerArea(Transform point) => PlayerSpawnArea = point;
        public void SetEnemyArea(Transform point) => EnemySpawnArea = point;
        public bool NewGame => File.Exists(_path) == false;
        
        public void Initialize()
        {
            if (!LoadGame())
            {
                
                StartStatusData statusData = GetStartStatus();
                UpgradeData weaponData = Managers.Game.GetUpgradeData(Define.UpgradeType.Attack);
                UpgradeData defenceData = Managers.Game.GetUpgradeData(Define.UpgradeType.Defence);
                UpgradeData healthData = Managers.Game.GetUpgradeData(Define.UpgradeType.Health);

                Managers.Game.SaveData = new GameData()
                {
                    Inventory = new Inventory()
                    {
                        SaveData = new List<int>(),
                    },
                    Player = new Player()
                    {
                        LevelID = statusData.GetID(),
                        Coin = 0,
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
            
            Player.OnChangePlayerLevel += (data) => Managers.UI.RefreshUI();
            Managers.Game.Combat.Initialize();
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
                Managers.Data.Language = data.Language;
            }
            
            Debug.Log(data);
            
            Debug.Log($"Save Game Loaded : {_path}");
            return true;
        }
        #endregion
    }
}