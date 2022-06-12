using UnityEngine;

namespace Data
{
    public class UpgradeData : ScriptableObject,ITableSetter
    {
        public int ID;
        public Define.UpgradeType UpgradeType;
        public int IconID;
        public int Price;
        public int IncreaseStat;

        public int GetID() => ID;
    }
}