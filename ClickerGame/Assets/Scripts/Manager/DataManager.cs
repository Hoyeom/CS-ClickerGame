using System;
using System.Collections.Generic;
using Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Manager
{
    public class DataManager
    {
        public Dictionary<int, StatusData> Status =new Dictionary<int, StatusData>();
        public Dictionary<int, MonsterData> Monster = new Dictionary<int, MonsterData>();
        public Dictionary<int, UpgradeData> Upgrade = new Dictionary<int, UpgradeData>();
        public Dictionary<int, CraftData> Craft = new Dictionary<int, CraftData>();
        public Dictionary<int, ShopData> Shop = new Dictionary<int, ShopData>();
        public Dictionary<int, StartStatus> StartStatus = new Dictionary<int, StartStatus>();
        public Dictionary<int, StringData> String = new Dictionary<int, StringData>();
        public Dictionary<int, PathData> Path = new Dictionary<int, PathData>();

        public Define.Language Language = Define.Language.Eng;
        
        public void Initialize()
        {
            Status = LoadData<int,StatusData>();
            Monster = LoadData<int,MonsterData>();
            Upgrade = LoadData<int,UpgradeData>();
            Craft = LoadData<int,CraftData>();
            Shop = LoadData<int,ShopData>();
            StartStatus = LoadData<int, StartStatus>();
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

        public T LoadPathData<T>(int id) where T : Object
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