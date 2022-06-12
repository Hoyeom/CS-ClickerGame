using UnityEngine;

namespace Data
{
    public class WeaponData : ScriptableObject,ITableSetter
    {
        public int ID;
        public int IconID;
        public int Level;
        public int AttackPower;
        
        public int GetID() => ID;
    }
}