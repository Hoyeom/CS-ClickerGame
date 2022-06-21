using System.Collections.Generic;
using Manager;
using UnityEngine;

namespace UI.Popup.InGame
{
    public abstract class UI_TableBase : UI_Popup
    {
        enum GameObjects
        {
            Content
        }

        private Transform _content;
        protected Transform Content => _content;
        
        public override bool Initialize()
        {
            if (base.Initialize() == false)
                return false;
            
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
    }
}