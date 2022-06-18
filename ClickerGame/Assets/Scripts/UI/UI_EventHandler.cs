using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UI_EventHandler : MonoBehaviour,IPointerClickHandler,IPointerDownHandler,IPointerUpHandler
    {
        public event Action<PointerEventData> OnClickHandler = null;
        public event Action<PointerEventData> OnPressedHandler = null;
        public event Action<PointerEventData> OnDownHandler = null;
        public event Action<PointerEventData> OnUpHandler = null;
        
        private bool _pressed = false;

        private void Update()
        {
            if(_pressed)
                OnPressedHandler?.Invoke(null);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClickHandler?.Invoke(eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _pressed = true;
            OnDownHandler?.Invoke(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _pressed = false;
            OnUpHandler?.Invoke(eventData);
        }
    }
}