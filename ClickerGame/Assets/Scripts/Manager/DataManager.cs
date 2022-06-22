using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Manager
{
    public class DataManager
    {
        public List<EnemyData> Boss = new List<EnemyData>();
        public Dictionary<int, EnemyData> Monster = new Dictionary<int, EnemyData>();
        public Dictionary<int, UpgradeData> Upgrade = new Dictionary<int, UpgradeData>();
        public Dictionary<int, ItemData> Item = new Dictionary<int, ItemData>();
        public Dictionary<int, ShopData> Shop = new Dictionary<int, ShopData>();
        public Dictionary<int, StartStatusData> StartStatus = new Dictionary<int, StartStatusData>();
        public Dictionary<int, StringData> String = new Dictionary<int, StringData>();
        public Dictionary<int, PathData> Path = new Dictionary<int, PathData>();
        
        public Define.Language Language { get =>  Managers.Game.SaveData.Language; set { Managers.Game.SaveData.Language = value; Managers.UI.RefreshUI(); } }

        public void Initialize()
        {
            Monster = LoadData<int,EnemyData>();
            Boss = Monster.Values.Where(data => data.EnemyType == Define.EnemyType.Boss).ToList();
            Upgrade = LoadData<int,UpgradeData>();
            Item = LoadData<int,ItemData>();
            Shop = LoadData<int,ShopData>();
            StartStatus = LoadData<int, StartStatusData>();
            String = LoadData<int,StringData>();
            Path = LoadData<int,PathData>();
        }
        
        
        public string GetText(int key)
        {
            string text = System.String.Empty;
            String.TryGetValue(key, out StringData data);

            if (data == null)
                throw new Exception($"Fail GetValue, Key: {key.ToString()}, Language: {Language}");
            
            if (Language == Define.Language.Eng)
                text = data.Eng;
            else
            {
                text = Language switch
                {
                    Define.Language.Kor => data.Kor,
                    _ => text
                };

                if (string.IsNullOrEmpty(text))
                    text = data.Eng;
            }

            return text;
        }

        public T PathIDToData<T>(int id) where T : Object
        {
            if(Path.TryGetValue(id,out PathData data))
                return Managers.Resource.Load<T>(data.Path);
            return null;
        }
        
        private Dictionary<int,T> LoadData<TKey,T>() where T : ScriptableObject, ITableSetter
        {
            Dictionary<int, T> dic = new Dictionary<int, T>();

            foreach (T data in Managers.Resource.LoadData<T>())
                dic.Add(data.GetID(), data);
            
            return dic;
        }
    }
}