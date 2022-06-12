using Manager;

namespace UI.Scene
{
    public abstract class UI_Scene : UI_Base
    {
        public override bool Initialize()
        {
            if (base.Initialize() == false)
                return false;
            Managers.UI.SetCanvas(gameObject, false);

            return true;
        }
    }
}