using System;
using Manager;
using UnityEngine;

namespace Scene
{
    public abstract class BaseScene : MonoBehaviour
    {
        [SerializeField]
        protected Define.Scene _scene = Define.Scene.Unknown;
        public Define.Scene SceneType => _scene;

        private void Awake() => name = $"@{GetType().Name}";

        private void Start()
        {
            Initialize();
        }

        public abstract void Clear();
        
        protected abstract void Initialize();
    }
}