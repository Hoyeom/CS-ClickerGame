using System;
using Manager;

namespace Content
{
    public class Player
    {
        private int _addCoin = 5;
        private int _coin;
        private int _atkPower;
        private int _defPower;
        private int _health;
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

        private int _craftLevel = 1;

        public void AddCoin()
        {
            Coin += _addCoin;
        }
        
        public int CraftLevel
        {
            get => _craftLevel;
            set
            {
                _craftLevel = value;
            }
        }

        public void SetData(GameData data)
        {
            _coin = data.Coin;
            _addCoin = data.AddCoin;
            _health = data.Health;
            _atkPower = data.AtkPower;
            _defPower = data.DefPower;
            
            Inventory = new Inventory
            {
                Slot = data.InventorySlot,
                Items = data.ItemData
            };
        }


        public Inventory Inventory { get; private set; } = new Inventory();

    }
}