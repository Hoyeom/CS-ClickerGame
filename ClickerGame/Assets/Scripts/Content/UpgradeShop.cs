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
        public int Level => Data.Level;
        public int Price => Data.Price;
        public int IncreaseStat => Data.IncreaseStat;
        public int Stat => Data.Stat;
        
        public int LevelID { get => _levelID; set => _levelID = value; }

        public void Upgrade()
        {
            
            if (Managers.Game.Player.Coin >= Price)
            {
                Managers.Game.Player.Coin -= Price;
                
                LevelID += 1;
                
                Managers.UI.RefreshUI();
                
                return;
            }

            Debug.Log($"업그레이드 실패");
        }
    }
    
    [Serializable]
    public class UpgradeShop
    {
        public Status Health;
        public Status AtkPower;
        public Status DefPower;
    }
}