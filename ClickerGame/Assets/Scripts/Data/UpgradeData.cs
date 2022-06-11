using UnityEngine;

namespace Data
{
    public class UpgradeData : ScriptableObject,ITableSetter
    {
        public int ID;
        public int Level;
        public int IconPath;
        public int CurValue;
        public int UpgradeValue;
        public int Cost;
        
        public int GetID() => ID;
    }
}