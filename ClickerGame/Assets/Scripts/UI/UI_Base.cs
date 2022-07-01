using System;
using System.Collections.Generic;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace UI
{
    public class UI_Base : MonoBehaviour
    {
        private Dictionary<Type, Object[]> _objects = new Dictionary<Type, Object[]>();

        protected bool _init = false;
        
        private void Start()
        {
            Initialize();
        }

        public virtual bool Initialize()
        {
            if(_init) return false;

            Managers.UI.AddUI(this);
            
            return _init = true;
        }

        public virtual void RefreshUI() { }

        protected void BindText(Type type) => Bind<TextMeshProUGUI>(type);
        protected void BindImage(Type type) => Bind<Image>(type);
        protected void BindInputField(Type type) => Bind<TMP_InputField>(type);

        protected void Bind<T>(Type type) where T : Object
        {
            string[] names = Enum.GetNames(type);
            Object[] objects = new Object[names.Length];
            _objects.Add(typeof(T),objects);

            for (int i = 0; i < names.Length; i++)
            {
                if (typeof(T) == typeof(GameObject))
                    objects[i] = Utils.FindChild(gameObject, names[i], true);
                else
                    objects[i] = Utils.FindChild<T>(gameObject, names[i], true);
            }
            
        }

        
        protected TextMeshProUGUI GetText(int index) => Get<TextMeshProUGUI>(index);
        protected Image GetImage(int index) => Get<Image>(index);
        protected TMP_InputField GetInputField(int index) => Get<TMP_InputField>(index);
        
        protected T Get<T>(int index) where T : Object
        {
            Object[] objects = null;
            if (_objects.TryGetValue(typeof(T), out objects) == false)
                return null;

            return objects[index] as T;
        }
        
        public static GameObject BindEvent(GameObject go,Action<PointerEventData> action,Define.UIEvent type = Define.UIEvent.Click)
        {
            UI_EventHandler evt = Utils.GetOrAddComponent<UI_EventHandler>(go);

            switch (type)
            {
                case Define.UIEvent.Click:
                    evt.OnClickHandler -= action;
                    evt.OnClickHandler += action;
                    break;
                case Define.UIEvent.Pressed:
                    evt.OnPressedHandler -= action;
                    evt.OnPressedHandler += action;
                    break;
                case Define.UIEvent.Down:
                    evt.OnDownHandler -= action;
                    evt.OnDownHandler += action;
                    break;
                case Define.UIEvent.Up:
                    evt.OnUpHandler -= action;
                    evt.OnUpHandler += action;
                    break;
            }

            return go;
        }
    }
}