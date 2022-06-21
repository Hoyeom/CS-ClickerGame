using Manager;
using TMPro;
using UnityEngine.UI;

namespace UI.Popup.InGame
{
    public class UI_UserInfoPopup : UI_Popup
    {
        enum Texts
        {
            SliderHpText,
            CoinText,
            InfoLevelText,
            UserName
        }

        enum Sliders
        {
            SliderExp,
            SliderHp,
        }
        
        private TextMeshProUGUI _levelText;
        private TextMeshProUGUI _userNameText;
        private TextMeshProUGUI _coinText;
        
        private Slider _expSlider;
        private Slider _hpSlider;
        private TextMeshProUGUI _hpText;
        
        public override bool Initialize()
        {
            if (base.Initialize() == false)
                return false;
            
            BindText(typeof(Texts));
            Bind<Slider>(typeof(Sliders));
            
            _hpText = GetText((int) Texts.SliderHpText);
            _coinText = GetText((int) Texts.CoinText);
            _levelText = GetText((int) Texts.InfoLevelText);
            _userNameText = GetText((int) Texts.UserName);
            _coinText.text = 0.ToString();
            
            _expSlider = Get<Slider>((int) Sliders.SliderExp);
            _hpSlider = Get<Slider>((int) Sliders.SliderHp);
            
            Managers.Game.Player.OnChangeName += OnChangeName;
            Managers.Game.Player.OnChangePlayerLevel += OnChangeLevel;
            Managers.Game.Player.OnChangeCoin += OnChangeCoin;
            Managers.Game.Player.OnChangeExp += SetExpSlider;
            Managers.Game.Player.OnChangeHealth += SetHealthSlider;

            return true;
        }
        
        private void SetExpSlider(int cur,int max)
        {
            _expSlider.value = (float) cur / max;
        }
        private void SetHealthSlider(int cur,int max)
        {
            _hpSlider.value = (float) cur / max;
            _hpText.text = $"{cur.ToString()}/{max.ToString()}";
        }
        private void OnChangeName(string name)
        {
            _userNameText.text = name;
        }
        private void OnChangeLevel(int cur)
        {
            _levelText.text = cur.ToString();
        }
        private void OnChangeCoin(int value)
        {
            _coinText.text = value.ToString();
        }
    }
}