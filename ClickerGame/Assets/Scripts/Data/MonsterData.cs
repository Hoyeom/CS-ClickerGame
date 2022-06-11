using UnityEngine;

namespace Data
{
    public class MonsterData : ScriptableObject,ITableSetter
    {
        public int ID;
        public int Health;
        public int AttackPower;
        public int DefencePower;
        public int GetID() => ID;
    }
}