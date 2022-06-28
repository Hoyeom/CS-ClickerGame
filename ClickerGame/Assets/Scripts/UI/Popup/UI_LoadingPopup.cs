using System;
using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup
{
    public class UI_LoadingPopup : UI_Popup
    {
        enum Sliders
        {
            LoadingSlider
        }

        private Slider _loadingBar;
        
        public override bool Initialize()
        {
            if (base.Initialize() == false)
                return false;
            
            Bind<Slider>(typeof(Sliders));

            _loadingBar = Get<Slider>((int) Sliders.LoadingSlider);
            _loadingBar.value = 0;
            
            return false;
        }

        private void Update()
        {
            _loadingBar.value = Managers.Resource.GetLoadProgress();

            if (Managers.Resource.CompleteLoad)
            {
                ClosePopupUI();
                Managers.UI.ShowPopupUI<UI_TitlePopup>();
            }
            
        }
    }
}