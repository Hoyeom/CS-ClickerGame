﻿using System;
using UnityEngine;

namespace Manager
{
    public class Managers : MonoBehaviour
    {
        private static Managers s_instance = null;
        public static Managers Instance { get => s_instance; }
        
        private static ResourceManager s_resource = new ResourceManager();
        private static SceneManagerEx s_scene = new SceneManagerEx();
        private static SoundManager s_sound = new SoundManager();
        private static UIManager s_ui = new UIManager();

        
        public static ResourceManager Resource { get { Initialize(); return s_resource; } }
        public static SceneManagerEx Scene { get { Initialize(); return s_scene; } }
        public static SoundManager Sound {get { Initialize(); return s_sound; }}
        public static UIManager UI { get { Initialize(); return s_ui; } }
        
        
        private static readonly string Name = "@Managers";

        private void Awake() => name = Name;
        private void Start() => Initialize();
        
        private static void Initialize()
        {
            if(s_instance != null) return;
            
            GameObject go = GameObject.Find(Name);
            
            if (go == null)
                go = new GameObject {name = Name};

            s_instance = go.GetOrAddComponent<Managers>();
            
            DontDestroyOnLoad(go);
            
            ManagersInitialize();
        }
        
        private static void ManagersInitialize()
        {
            s_scene.Initialize();
            s_sound.Initialize();
            s_ui.Initialize();
        }

        public void ManagersClear()
        {
            UI.Clear();
            Sound.Clear();
        }
        
    }
}