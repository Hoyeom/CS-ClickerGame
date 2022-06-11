using UnityEngine;

namespace Data
{
    public class MonsterData : ScriptableObject,ITableSetter
    {
        public int ID;
        public Define.MonsterType MonsterType;
        public int MonsterIcon;
        public int Health;
        public int AttackPower;
        public int DefencePower;
        public int GetID() => ID;
    }
}