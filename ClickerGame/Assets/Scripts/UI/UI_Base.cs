﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UI
{
    public abstract class UI_Base : MonoBehaviour
    {
        private Dictionary<Type, Object[]> _objects = new Dictionary<Type, Object[]>();

        private void Start() => Initialize();
        protected abstract void Initialize();

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

        protected T Get<T>(int index) where T : Object
        {
            Object[] objects = null;
            if (_objects.TryGetValue(typeof(T), out objects) == false)
                return null;

            return objects[index] as T;
        }

        public static void BindEvent(GameObject go,Action action,Define.UIEvent type = Define.UIEvent.Click)
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
        }
    }
}