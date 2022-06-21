using System.Collections.Generic;
using System.Linq;
using Data;
using Manager;
using UnityEngine;

namespace UI.Popup.InGame
{
    public class UI_UpgradeTablePopup : UI_TableBase
    {
        private List<SubItem_Upgrade> _subItems = new List<SubItem_Upgrade>();

        public override bool Initialize()
        {
            if (base.Initialize() == false)
                return false;

            _subItems.Clear();
            
            foreach (UpgradeData data in Managers.Data.Upgrade.Values.Where((data => data.Level == 0)))
            {
                SubItem_Upgrade subItem = Managers.UI.MakeSubItem<SubItem_Upgrade>(Content);
                _subItems.Add(subItem);
                
                subItem.SetInfo(data);
            }
            
            
            return true;
        }
    }
}