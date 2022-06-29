using System;
using Manager;
using TMPro;
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

        enum Texts
        {
            LoadingText
        }

        private Slider _loadingBar;
        private TextMeshProUGUI _loadingText;
        
        public override bool Initialize()
        {
            if (base.Initialize() == false)
                return false;
            
            Bind<Slider>(typeof(Sliders));
            BindText(typeof(Texts));

            _loadingBar = Get<Slider>((int) Sliders.LoadingSlider);
            _loadingBar.value = 0;

            _loadingText = GetText((int) Texts.LoadingText);
            _loadingText.text = "";
            
            Managers.Resource.OnCompleteDataLoad += RefreshUI;
            
            return false;
        }

        public override void RefreshUI()
        {
            ResourceManager resource = Managers.Resource;
            
            _loadingBar.value = resource.GetLoadProgress();
            _loadingText.text =
                $"Data Load({resource.LoadCurCount.ToString()}/{resource.LoadMaxCount.ToString()}) {(resource.GetLoadProgress() * 100) :F1}%";
            
            if (Managers.Resource.CompleteLoad)
            {
                ClosePopupUI();
                Managers.UI.ShowPopupUI<UI_TitlePopup>();
            }
        }
    }
}