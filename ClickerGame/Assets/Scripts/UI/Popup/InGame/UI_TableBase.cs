﻿using System.Collections.Generic;
using DG.Tweening;
using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup.InGame
{
    public abstract class UI_TableBase : UI_Popup
    {
        enum GameObjects
        {
            Content
        }
        
        private Canvas _canvas;

        private Transform _content;
        protected Transform Content => _content;

        protected virtual void SetLayoutGroup() { }

        public override bool Initialize()
        {
            if (base.Initialize() == false)
                return false;
            
            _canvas = GetComponent<Canvas>();

            Bind<GameObject>(typeof(GameObjects));
            
            _content = Get<GameObject>((int) GameObjects.Content).transform;

            RemoveAllContent();

            return true;
        }

        private void RemoveAllContent()
        {
            for (int i = 0; i < _content.childCount; i++)
                Destroy(_content.GetChild(i).gameObject);
        }
        
        public virtual void SetActive(bool value)
        {
            _canvas.enabled = value;
        }
    }
}