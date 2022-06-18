using UnityEngine;

namespace Data
{
    public class EnemyData : ScriptableObject,ITableSetter
    {
        public int ID;
        public Define.EnemyType EnemyType;
        public int IconID;
        public int Health;
        public int AttackPower;
        public int DefencePower;
        public int DropCoin;
        public int DropExp;
        public int GetID() => ID;
    }
}