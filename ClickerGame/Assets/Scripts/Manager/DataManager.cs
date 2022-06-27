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
        public Dictionary<int, StartStatusData> StartStatus = new Dictionary<int, StartStatusData>();
        public Dictionary<int, StringData> String = new Dictionary<int, StringData>();
        public Dictionary<int, PathData> Path = new Dictionary<int, PathData>();
        
        private Dictionary<int, Coroutine> _dicCoroutine = new Dictionary<int, Coroutine>();
        
        public Define.Language Language { get =>  Managers.Game.SaveData.Language; set { Managers.Game.SaveData.Language = value; Managers.UI.RefreshUI(); } }

        
        public void Initialize()
        {
            _dicCoroutine.Clear();
            
            // Managers.Resource.AsyncLoadData(Monster);
            Managers.Resource.AsyncDataLoad<EnemyData>(data =>
            {
                Monster = data;
                Boss = Monster.Values.Where(data => data.EnemyType == Define.EnemyType.Boss).ToList();
            });

            Managers.Resource.AsyncDataLoad<UpgradeData>(data => Upgrade = data);
            Managers.Resource.AsyncDataLoad<ItemData>(data => Item = data);
            Managers.Resource.AsyncDataLoad<ShopData>(data => Shop = data);
            Managers.Resource.AsyncDataLoad<StartStatusData>(data => StartStatus = data);
            Managers.Resource.AsyncDataLoad<StringData>(data => String = data);
            Managers.Resource.AsyncDataLoad<PathData>(data => Path = data);

            // Monster = LoadData<int,EnemyData>();
            // Boss = Monster.Values.Where(data => data.EnemyType == Define.EnemyType.Boss).ToList();
            // Upgrade = LoadData<int,UpgradeData>();
            // Item = LoadData<int,ItemData>();
            // Shop = LoadData<int,ShopData>();
            // StartStatus = LoadData<int, StartStatusData>();
            // String = LoadData<int,StringData>();
            // Path = LoadData<int,PathData>();
        }

        public string GetText(int key,Action<string> callback = null)
        {
            string text = System.String.Empty;
            String.TryGetValue(key, out StringData data);

            if (data == null)
            {
                if (!_dicCoroutine.ContainsKey(key))
                {
                    Debug.Log($"{key}사용");
                    _dicCoroutine.Add(key, Managers.Instance.StartCoroutine(CoTryGetText(key, callback)));
                }
                
                // throw new Exception($"Fail GetValue, Key: {key.ToString()}, Language: {Language}");
                return null;
            }

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
        
        IEnumerator CoTryGetText(int key,Action<string> callback)
        {
            string temp = null;
            while (string.IsNullOrEmpty(temp))
            {
                if (String.TryGetValue(key, out StringData data))
                    SelectLanguage(data, out temp);

                yield return null;
            }
            
            callback.Invoke(temp);
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