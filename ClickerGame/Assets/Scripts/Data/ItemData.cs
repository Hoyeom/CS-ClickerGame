using UnityEngine;

namespace Data
{
    public class ItemData : ScriptableObject,ITableSetter
    {
        public int ID;
        public int IconID;
        public Define.ItemType ItemType;
        public bool Lock;
        public int IconBorderID;
        public int Level;
        public int AttackPower;
        
        public int GetID() => ID;
    }
}