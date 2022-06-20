using UnityEngine;

namespace UI.Popup
{
    public class UI_SettingPopup : UI_Popup
    {
        public override bool Initialize()
        {
            if (base.Initialize() == false)
                return false;
            
            return true;
        }
    }
}