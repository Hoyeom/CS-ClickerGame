using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Manager;
using UnityEngine;

namespace Data
{
    public class BossData : ScriptableObject,ITableSetter
    {
        public int ID;
        public int Health;
        public int AttackPower;
        public int DefencePower;

        public int GetID() => ID;
    }
}