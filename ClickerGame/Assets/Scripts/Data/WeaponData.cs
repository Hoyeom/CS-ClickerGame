using UnityEngine;

namespace Data
{
    public class WeaponData : ScriptableObject,ITableSetter
    {
        public int ID;
        public int IconPath;
        public int Level;
        public int AttackPower;
        
        public int GetID() => ID;
    }
}