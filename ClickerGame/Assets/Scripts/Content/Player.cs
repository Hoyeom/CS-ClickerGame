using System;
using Manager;
using UnityEngine;

namespace Content
{
    [Serializable]
    public class Player
    {
        [SerializeField] private int _addCoin;

        public int AddCoin
        {
            get => _addCoin;
            set => _addCoin = value;
        }

        [SerializeField] private int _atkPower;

        public int AtkPower
        {
            get => _atkPower;
            set
            {
                _atkPower = value;
                OnChangeAtkPower?.Invoke(_atkPower);
            }
        }

        [SerializeField] private int _defPower;

        public int DefPower
        {
            get => _defPower;
            set
            {
                _defPower = value;
                OnChangeDefPower?.Invoke(_defPower);
            }
        }

        [SerializeField] private int _health;

        public int Health
        {
            get => _health;
            set
            {
                _health = value;
                OnChangeHealth?.Invoke(_health);
            }
        }

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
            OnChangeHealth?.Invoke(_health);
            OnChangeAtkPower?.Invoke(_atkPower);
            OnChangeDefPower?.Invoke(_defPower);
            OnChangeCoin?.Invoke(_coin);
            Inventory.RefreshUIData();
        }

        [SerializeField] private int _craftLevel = 1;

        public void TabToAddCoin()
        {
            Coin += AddCoin;
        }

        public int CraftLevel
        {
            get => _craftLevel;
            set { _craftLevel = value; }
        }

        public Inventory Inventory
        {
            get => Managers.Game.SaveData.Inventory;
            set => Managers.Game.SaveData.Inventory = value;
        }
    }
}