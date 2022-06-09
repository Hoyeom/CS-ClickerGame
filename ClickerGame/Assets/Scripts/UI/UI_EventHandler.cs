using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UI_EventHandler : MonoBehaviour,IPointerClickHandler,IPointerDownHandler,IPointerUpHandler
    {
        public event Action OnClickHandler = null;
        public event Action OnPressedHandler = null;
        public event Action OnDownHandler = null;
        public event Action OnUpHandler = null;
        
        private bool _pressed = false;

        private void Update()
        {
            if(_pressed)
                OnPressedHandler?.Invoke();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClickHandler?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _pressed = true;
            OnDownHandler?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _pressed = false;
            OnUpHandler?.Invoke();
        }
    }
}