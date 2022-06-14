using System;
using Content;

namespace Manager
{
    public class GameManager
    {
        private int coin;
        
        public event Action<int> OnChangeCoin;

        public Player Player { get; private set; } = new Player();
        
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
            Player = new Player();
            Player.Initialize();
        }
    }
}