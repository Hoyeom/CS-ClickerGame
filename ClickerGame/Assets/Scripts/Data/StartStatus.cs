using UnityEngine;

namespace Data
{
    public class StartStatus : ScriptableObject,ITableSetter
    {
        public int ID;
        public int InventorySlot;
        public int AddCoin;
        public int Coin;
        public int AtkPower;
        public int DefPower;
        public int Health;
        
        public int GetID() => ID;
    }
}