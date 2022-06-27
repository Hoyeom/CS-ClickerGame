using System;
using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Manager
{
    public class ResourceManager
    {
        private AsyncOperationHandle _loadHandle;

        public void AsyncDataLoad<T>(Action<List<T>> completeCallback) where T : ScriptableObject, ITableSetter
        {
            GetDataPath<T>(out string path);
            AsyncOperationHandle<IList<T>> f = Addressables.LoadAssetsAsync<T>(path, (result) => { });
            
            _loadHandle = Addressables.DownloadDependenciesAsync(path);
            _loadHandle.Completed += Addressables.Release;
        }
        public Object Load(string path)
        {
            PathNullException(path);
            return Resources.Load(path);
        }
        public T Load<T>(string path) where T : Object
        {
            PathNullException(path);
            
            if (typeof(T) == typeof(GameObject))
            {
                if(!path.Contains("Prefabs/"))
                    path = $"Prefabs/{path}";
            }

            return Resources.Load<T>(path);
        }
        public AsyncOperationHandle<IList<T>> AsyncLoadData<T>(Dictionary<int,T> dataDic) where T : ScriptableObject, ITableSetter
        {
            string dataName = typeof(T).Name;

            int index = dataName.IndexOf("Data");

            if (index > 0)
                dataName = dataName.Remove(index, 4);

            dataDic = new Dictionary<int, T>();

            return Addressables.LoadAssetsAsync<T>($"Data/{dataName}", sO => dataDic.Add(sO.GetID(), sO));
        }
        public T[] LoadData<T>() where T : ScriptableObject,ITableSetter
        {
            GetDataPath<T>(out string path);
            return Resources.LoadAll<T>(path);
        }
        private void PathNullException(string path)
        {
            if (String.IsNullOrEmpty(path))
                throw new Exception($"Load Failed: Empty Path");
        }
        public GameObject Instantiate(string path)
        {
            if(!path.Contains("Prefabs/"))
                path = $"Prefabs/{path}";

            GameObject obj = Load<GameObject>(path);

            if (obj == null)
                throw new Exception($"Load Failed: {path}");
            
            return Instantiate(obj);
        }
        public GameObject Instantiate(GameObject obj)
        {
            return Object.Instantiate(obj);
        }
        public void Destroy(Object obj)
        {
            if(obj == null)
                return;
            Object.Destroy(obj);
        }
        private void GetDataPath<T>(out string path) where T : ScriptableObject, ITableSetter
        {
            path = typeof(T).Name;

            int index = path.IndexOf("Data");

            if (index > 0)
                path = path.Remove(index, 4);

            path = $"Data/{path}";
        }
    }
}