using UnityEngine;

namespace Data
{
    public class StatusData : ScriptableObject,ITableSetter
    {
        public int ID;
        public int Level;
        public int MaxInventory;
        
        public int GetID() => ID;
    }
}