using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI;
using UI.Popup;
using UI.Scene;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Manager
{
    public class UIManager
    {
        private int _order = -20;
        private Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();

        public UI_Scene SceneUI { get; private set; }

        private static readonly string Name = "@UI_Root";
        
        public GameObject Root
        {
            get
            {
                GameObject root = GameObject.Find(Name);
                if (root == null)
                    root = new GameObject {name = Name};
                
                return root;
            }
        }
        
        public void Initialize()
        {
            InitEventSystem();
        }

        public void SetCanvas(GameObject go, bool sort = true)
        {
            Canvas canvas = Utils.GetOrAddComponent<Canvas>(go);
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.overrideSorting = true;

            if (sort)
            {
                canvas.sortingOrder = _order;
                _order++;
            }
            else
            {
                canvas.sortingOrder = 0;
            }
        }
        
        public T MakeSubItem<T>(Transform parent = null, string name = null) where T : UI_Base
        {
            if (string.IsNullOrEmpty(name))
                name = typeof(T).Name;

            GameObject prefab = Managers.Resource.Load<GameObject>($"Prefabs/UI/SubItem/{name}");

            GameObject go = Managers.Resource.Instantiate(prefab);
            if (parent != null)
                go.transform.SetParent(parent);

            go.transform.localScale = Vector3.one;
            go.transform.localPosition = prefab.transform.position;

            return Utils.GetOrAddComponent<T>(go);
        }
        
        
        public T ShowSceneUI<T>(string name = null) where T : UI_Scene
        {
            if (string.IsNullOrEmpty(name))
                name = typeof(T).Name;

            GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");
            T sceneUI = Utils.GetOrAddComponent<T>(go);
            SceneUI = sceneUI;

            go.transform.SetParent(Root.transform);

            return sceneUI;
        }

        public T ShowPopupUI<T>(string name = null, Transform parent = null) where T : UI_Popup
        {
            if (string.IsNullOrEmpty(name))
                name = typeof(T).Name;

            GameObject prefab = Managers.Resource.Load<GameObject>($"Prefabs/UI/Popup/{name}");

            GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");
            T popup = Utils.GetOrAddComponent<T>(go);
            _popupStack.Push(popup);

            if (parent != null)
                go.transform.SetParent(parent);
            else if (SceneUI != null)
                go.transform.SetParent(SceneUI.transform);
            else
                go.transform.SetParent(Root.transform);

            go.transform.localScale = Vector3.one;
            go.transform.localPosition = prefab.transform.position;

            return popup;
        }
        
        public T FindPopup<T>() where T : UI_Popup
        {
            return _popupStack.Where(x => x.GetType() == typeof(T)).FirstOrDefault() as T;
        }
        
        private void InitEventSystem()
        {
            EventSystem eventSystem = GameObject.FindObjectOfType<EventSystem>();
            GameObject go = null;
            if (eventSystem == null)
                go = Managers.Resource.Instantiate("UI/EventSystem");
            else
                go = eventSystem.gameObject;
            go.name = "@EventSystem";
        }

        public void ClosePopupUI(UI_Popup popup)
        {
            if(_popupStack.Count == 0)
                return;

            if (_popupStack.Peek() != popup)
            {
                Debug.Log("Close PopupFailed");
                return;
            }
            
            ClosePopupUI();
        }
        
        public void ClosePopupUI()
        {
            if (_popupStack.Count == 0)
                return;

            UI_Popup popup = _popupStack.Pop();
            Managers.Resource.Destroy(popup.gameObject);
            popup = null;
            _order--;
        }
        
        public void CloseAllPopupUI()
        {
            while (_popupStack.Count > 0)
                ClosePopupUI();
        }

        public void Clear()
        {
            CloseAllPopupUI();
            SceneUI = null;
        }
    }
}