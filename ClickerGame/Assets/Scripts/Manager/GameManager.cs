using System;

namespace Manager
{
    public class GameManager
    {
        private int coin;
        
        public event Action<int> OnChangeCoin;
        
        public int Coin
        {
            get => coin;
            private set
            {
                coin = value;
                OnChangeCoin?.Invoke(coin);
            }
        }


        public void Initialize()
        {
            
        }
    }
}