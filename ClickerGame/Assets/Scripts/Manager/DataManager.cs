using System;
using System.Collections;
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
        public Dictionary<int, StatusData> Status = new Dictionary<int, StatusData>();
        public Dictionary<int, StringData> String = new Dictionary<int, StringData>();
        public Dictionary<int, PathData> Path = new Dictionary<int, PathData>();
        
        public Define.Language Language { get =>  Managers.Game.SaveData.Language; set { Managers.Game.SaveData.Language = value; Managers.UI.RefreshUI(); } }

        
        public void Initialize()
        {
            ResourceManager resource = Managers.Resource;
            
            resource.AsyncDataLoad<EnemyData>(completeCallback: (Dictionary<int, EnemyData> callbackData) =>
            {
                Monster = callbackData;
                Boss = Monster.Values.Where(data => data.EnemyType == Define.EnemyType.Boss).ToList();
            });

            resource.AsyncDataLoad<UpgradeData>(completeCallback: (Dictionary<int, UpgradeData> callbackData) => Upgrade = callbackData);
            resource.AsyncDataLoad<ItemData>(completeCallback: (Dictionary<int, ItemData> callbackData) => Item = callbackData);
            resource.AsyncDataLoad<ShopData>(completeCallback: (Dictionary<int, ShopData> callbackData) => Shop = callbackData);
            resource.AsyncDataLoad<StatusData>(completeCallback: (Dictionary<int, StatusData> callbackData) => Status = callbackData);
            resource.AsyncDataLoad<StringData>(completeCallback: (Dictionary<int, StringData> callbackData) => String = callbackData);
            resource.AsyncDataLoad<PathData>(completeCallback: (Dictionary<int, PathData> callbackData) => Path = callbackData);

            // Monster = LoadData<int,EnemyData>();
            // Boss = Monster.Values.Where(data => data.EnemyType == Define.EnemyType.Boss).ToList();
            // Upgrade = LoadData<int,UpgradeData>();
            // Item = LoadData<int,ItemData>();
            // Shop = LoadData<int,ShopData>();
            // Status = LoadData<int, StatusData>();
            // String = LoadData<int,StringData>();
            // Path = LoadData<int,PathData>();
        }

        public string GetText(int key,Action<string> callback = null)
        {
            string text = System.String.Empty;
            String.TryGetValue(key, out StringData data);

            if (data == null)
                throw new Exception($"Fail GetValue, Key: {key.ToString()}, Language: {Language}");
            
            SelectLanguage(data, out text);
            
            return text;
        }

        private void SelectLanguage(StringData data,out string text)
        {
            text = Language switch
            {
                Define.Language.Kor => data.Kor,
                Define.Language.Eng => data.Eng,
                _ => throw new ArgumentOutOfRangeException()
            };
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