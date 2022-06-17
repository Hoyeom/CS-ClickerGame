using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class StartStatus : ScriptableObject,ITableSetter
    {
        public int ID;
        public int Level;
        public int InventorySlot;
        public int AddCoin;
        public int AtkPower;
        public float AtkDelay;
        public float AtkSpeed;
        public int DefPower;
        public int Health;
        public int CraftLevel;

        public int GetID() => ID;
    }
}