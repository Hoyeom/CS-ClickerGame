using Manager;

namespace UI.Scene
{
    public abstract class UI_Scene : UI_Base
    {
        protected override void Initialize()
        {
            Managers.UI.SetCanvas(gameObject, false);
        }
    }
}