using System.Collections.Generic;
using Data;
using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup.InGame
{
    public class UI_EnemyTablePopup : UI_TableBase
    {
        private List<SubItem_Boss> _subItems = new List<SubItem_Boss>();
        private VerticalLayoutGroup _layoutGroup;
        
        public override bool Initialize()
        {
            if (base.Initialize() == false)
                return false;

            _subItems.Clear();
            
            foreach (EnemyData data in Managers.Data.Boss)
            {
                _subItems.Add(Managers
                    .UI
                    .MakeSubItem<SubItem_Boss>(Content)
                    .SetInfo(data));
            }

            _layoutGroup = Content.GetComponent<VerticalLayoutGroup>();

            Invoke(nameof(SetLayoutGroup), 1);
            
            return true;
        }
        
        protected override void SetLayoutGroup()
        {
            _layoutGroup.enabled = true;
            _layoutGroup.enabled = false;
        }

        public override void SetActive(bool value)
        {
            base.SetActive(value);
            if (!value) return;
            
            foreach (SubItem_Boss subItem in _subItems)
                subItem.TweenRestart();
        }
    }
}