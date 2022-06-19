using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class StartStatusData : ScriptableObject,ITableSetter
    {
        public int ID;
        public int IconID;
        public int Level;
        public int TargetExp;
        public int InventorySlot;
        public int AddCoin;
        public int AtkPower;
        public int DefPower;
        public int Health;
        public int CraftLevel;
        public float ChargeSpeed;

        public int GetID() => ID;
    }
}