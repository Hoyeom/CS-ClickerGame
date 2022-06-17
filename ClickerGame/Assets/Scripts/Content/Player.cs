using System;
using Data;
using Manager;
using UnityEngine;

namespace Content
{
    [Serializable]
    public class Player
    {
        [SerializeField] private int _levelID = 0;
        public int LevelID { get=>_levelID; set => _levelID = value; }
        public StartStatus Status => Managers.Data.StartStatus[LevelID];
        
        private SubItem_Player _view;
        public SubItem_Player View => _view;
        public int AddCoin => Status.AddCoin;
        public int AtkPower => Status.AtkPower;
        public int DefPower => Status.DefPower;
        public int Health => Status.Health;
        public float AttackDelay => Status.AtkDelay;
        public float AttackSpeed => Status.AtkSpeed;
        public int CraftLevel => Status.CraftLevel;
        
        [SerializeField] private int _coin;
        public int Coin
        {
            get => _coin;
            set
            {
                _coin = value;
                OnChangeCoin?.Invoke(_coin);
            }
        }


        

        public event Action<int> OnChangeCoin;
        public event Action<int> OnChangeAtkPower;
        public event Action<int> OnChangeDefPower;
        public event Action<int> OnChangeHealth;
        
        public void RefreshUIData()
        {
            OnChangeHealth?.Invoke(Health);
            OnChangeAtkPower?.Invoke(AtkPower);
            OnChangeDefPower?.Invoke(DefPower);
            OnChangeCoin?.Invoke(Coin);
            Inventory.RefreshUIData();
        }
        public void SetView(SubItem_Player player) => _view = player;

        
        
        
        public void TabToAddCoin()
        {
            Coin += AddCoin;
        }

        public Inventory Inventory
        {
            get => Managers.Game.SaveData.Inventory;
            set => Managers.Game.SaveData.Inventory = value;
        }
    }
}