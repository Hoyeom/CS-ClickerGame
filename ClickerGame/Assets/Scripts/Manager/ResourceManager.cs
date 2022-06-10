using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Manager
{
    public class ResourceManager
    {
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
        
    }
}