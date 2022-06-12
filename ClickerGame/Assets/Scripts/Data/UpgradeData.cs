using UnityEngine;

namespace Data
{
    public class UpgradeData : ScriptableObject,ITableSetter
    {
        public int ID;
        public Define.UpgradeType UpgradeType;
        public int IconID;
        public int CurValue;
        public int UpgradeValue;
        public int Cost;
        
        public int GetID() => ID;
    }
}