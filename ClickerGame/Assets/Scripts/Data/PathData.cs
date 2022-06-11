using System;
using UnityEngine;

namespace Data
{
    public class PathData : ScriptableObject,ITableSetter
    {
        public int ID;
        public Define.AssetType AssetType;
        public string Path;
        public int GetID() => ID;
    }
}