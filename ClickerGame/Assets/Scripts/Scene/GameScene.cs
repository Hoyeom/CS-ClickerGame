using Manager;
using UI.Popup;

namespace Scene
{
    public class GameScene : BaseScene
    {
        public override void Clear()
        {
            
        }

        protected override void Initialize()
        {
            _scene = Define.Scene.Game;

            Managers.UI.ShowPopupUI<UI_LoadingPopup>().Initialize();

            // Managers.UI.ShowPopupUI<UI_TitlePopup>().Initialize();
        }
    }
}