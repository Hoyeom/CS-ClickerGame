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
    public class SaveData
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


        private SaveData _saveData = new SaveData();
        public SaveData SaveData { get { return _saveData; } set { _saveData = value; } }
        public Player Player { get => SaveData.Player; set => SaveData.Player = value; }
        public Combat Combat { get; set; } = new Combat();
        public UpgradeShop UpgradeShop { get => SaveData.UpgradeShop; set=>SaveData.UpgradeShop = value; }
        public bool TryExitGame { get; set; } = false;
        
        public Transform PlayerSpawnArea { get; private set; }
        public Transform EnemySpawnArea { get; private set; }

        public void SetPlayerArea(Transform point) => PlayerSpawnArea = point;
        public void SetEnemyArea(Transform point) => EnemySpawnArea = point;
        public bool NewGame => File.Exists(_savePath) == false;
        
        public void Initialize()
        {
            TryExitGame = false;
            
            if (!LoadGame())
            {
                StartStatusData statusData = GetStartStatus();
                UpgradeData weaponData = Managers.Game.GetUpgradeData(Define.UpgradeType.Attack);
                UpgradeData defenceData = Managers.Game.GetUpgradeData(Define.UpgradeType.Defence);
                UpgradeData healthData = Managers.Game.GetUpgradeData(Define.UpgradeType.Health);

                Managers.Game.SaveData = new SaveData()
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
            Application.targetFrameRate = 30;
        }
        
        #region Save & Load	
        public string _savePath = Application.persistentDataPath + "/SaveData.json";

        public void SaveGame()
        {
            string jsonStr = JsonUtility.ToJson(Managers.Game.SaveData);
            File.WriteAllText(_savePath, jsonStr);
            Debug.Log($"Save Game Completed : {_savePath}");
        }

        private bool LoadGame()
        {
            if (File.Exists(_savePath) == false)
                return false;

            string saveText = File.ReadAllText(_savePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(saveText);
            
            if (saveData != null)
            {
                SaveData = saveData;
                saveData.Inventory.LoadData();
                Managers.Data.Language = saveData.Language;
            }

            Debug.Log($"Save Game Loaded : {_savePath}");
            return true;
        }
        
        #endregion
    }
}