using UnityEngine;

namespace Data
{
    public class StringData : ScriptableObject,ITableSetter
    {
        public int ID;
        public string Eng;
        public string Kor;
        public int GetID() => ID;
    }
}