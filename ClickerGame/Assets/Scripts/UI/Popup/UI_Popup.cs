using Manager;

namespace UI.Popup
{
    public abstract class UI_Popup : UI_Base
    {
        public override bool Initialize()
        {
            if (base.Initialize() == false)
                return false;
            
            Managers.UI.SetCanvas(gameObject, true);

            return true;
        }

        public virtual void ClosePopupUI()
        {
            Managers.UI.ClosePopupUI(this);
        }
    }
}