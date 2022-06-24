using UnityEngine;

namespace Data
{
    public class ShopData : ScriptableObject,ITableSetter
    {
        public int ID;
        public int TitleTextID;
        public int IconID;
        public int Price;
        public Define.Cost CostType;
        public Define.Cost RewardType;
        public int PriceTextID;
        public int Reward;

        public int GetID() => ID;
    }
}