using UnityEngine;

namespace Data
{
    public class ShopData : ScriptableObject,ITableSetter
    {
        public int ID;
        public int IconPath;
        public Define.Cost CostType;
        public int EngPrice;
        public int KorPrice;
        public Define.Cost RewardType;
        public int Reward;

        public int GetID() => ID;
    }
}