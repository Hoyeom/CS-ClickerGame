using System;

namespace Content
{
    public class Player
    {
        private int _coin;
        public int Coin
        {
            get => _coin;
            set
            {
                _coin = value;
                OnChangeCoin?.Invoke(_coin);
            }
        }

        private int _craftLevel = 1;

        public int CraftLevel
        {
            get => _craftLevel;
            set
            {
                _craftLevel = value;
            }
        }

        public event Action<int> OnChangeCoin;

        public Inventory Inventory { get; private set; } = new Inventory();

        public void Initialize()
        {
            Inventory = new Inventory();
        }

    }
}