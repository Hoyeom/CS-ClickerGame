using System;
using Data;
using Manager;
using UnityEngine;

namespace Content
{
    [Serializable]
    public class Status
    {
        public Status(UpgradeData data)
        {
            IncreasePrice = data.IncreasePrice;
            Price = data.Price;
            IncreaseStat = data.IncreaseStat;
        }
        
        [SerializeField] private int _price;
        [SerializeField] private int _increasePrice;
        [SerializeField] private int _increaseStat;
        
        public int Price
        {
            get => _price;
            set
            {
                _price = value;
                OnChangePrice?.Invoke(_price);
            }
        }
        public int IncreasePrice { get => _increasePrice; set => _increasePrice = value; }
        public int IncreaseStat { get => _increaseStat; set => _increaseStat = value; }
        
        public event Action<int> OnChangePrice;

        public void Upgrade(Define.UpgradeType type)
        {
            if (Managers.Game.Player.Coin >= _price)
            {
                Managers.Game.Player.Coin -= _price;
                switch (type)
                {
                    case Define.UpgradeType.Weapon:
                        Managers.Game.Player.AtkPower += IncreaseStat;
                        Debug.Log("무기 업그레이드 완료");
                        break;
                    case Define.UpgradeType.Defence:
                        Managers.Game.Player.DefPower += IncreaseStat;
                        Debug.Log("방어력 업그레이드 완료");
                        break;
                    case Define.UpgradeType.Health:
                        Managers.Game.Player.Health += IncreaseStat;
                        Debug.Log("체력 업그레이드 완료");
                        break;
                }
                Price += _increasePrice;
                return;
            }
            
            Debug.Log($"업그레이드 실패: {type}");
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