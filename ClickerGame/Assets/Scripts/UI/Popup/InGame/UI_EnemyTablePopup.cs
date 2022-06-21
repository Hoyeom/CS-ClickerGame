using System.Collections.Generic;
using Data;
using Manager;
using UnityEngine;

namespace UI.Popup.InGame
{
    public class UI_EnemyTablePopup : UI_TableBase
    {
        
        private List<SubItem_Boss> _subItems = new List<SubItem_Boss>();
        
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

            return true;
        }

    }
}