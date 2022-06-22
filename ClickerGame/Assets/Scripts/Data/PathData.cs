using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Data
{
    public class PathData : ScriptableObject, ITableSetter
    {
        public int ID;
        public string Path;
        public int GetID() => ID;
    }
}