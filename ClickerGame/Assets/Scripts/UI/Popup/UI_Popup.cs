using Manager;

namespace UI.Popup
{
    public abstract class UI_Popup : UI_Base
    {
        protected override void Initialize()
        {
            Managers.UI.SetCanvas(gameObject, true);
        }

        public virtual void ClosePopupUI()
        {
            Managers.UI.ClosePopupUI(this);
        }
    }
}