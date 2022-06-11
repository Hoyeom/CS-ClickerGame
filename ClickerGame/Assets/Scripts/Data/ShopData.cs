using UnityEngine;

namespace Data
{
    public class ShopData : ScriptableObject,ITableSetter
    {
        public int ID;
        public int IconPath;
        public Define.CostType CostType;
        public int PriceText;


        public int GetID() => ID;
    }
}