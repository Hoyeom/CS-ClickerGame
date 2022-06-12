using UnityEngine;

namespace Data
{
    public class CraftData : ScriptableObject,ITableSetter
    {
        public int ID;
        public int IconID;
        public int IconBorderID;
        public int Level;
        public int AttackPower;
        
        public int GetID() => ID;
    }
}