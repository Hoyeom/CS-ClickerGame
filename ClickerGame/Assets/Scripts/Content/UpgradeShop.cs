using System;
using Data;
using Manager;
using UnityEngine;

namespace Content
{
    [Serializable]
    public class Status
    {
        public Status(UpgradeData data) => LevelID = data.GetID();
        private UpgradeData Data => Managers.Data.Upgrade[LevelID];
        
        [SerializeField] private int _levelID;
        public int Price => Data.Price;
        public int IncreaseStat => Data.IncreaseStat;
        public int LevelID { get => _levelID; set => _levelID = value; }
        
        public event Action<int> OnChangePrice;

        public void Upgrade()
        {
            if (Managers.Game.Player.Coin >= Price)
            {
                Managers.Game.Player.Coin -= Price;
                
                LevelID += 1;
                
                RefreshUIData();
                
                return;
            }

            Debug.Log($"업그레이드 실패");
        }

        public void RefreshUIData()
        {
            OnChangePrice?.Invoke(Price);
        }
    }
    
    [Serializable]
    public class UpgradeShop
    {
        public Status Health;
        public Status AtkPower;
        public Status DefPower;

        public void RefreshUIData()
        {
            Health.RefreshUIData();
            AtkPower.RefreshUIData();
            DefPower.RefreshUIData();
        }
    }
}