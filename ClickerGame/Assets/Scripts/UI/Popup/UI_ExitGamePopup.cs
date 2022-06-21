using System;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup
{
    public class UI_ExitGamePopup : UI_Popup
    {
        enum Texts
        {
            TitleText,
            InfoText,
            ButtonText,
        }

        enum Buttons
        {
            ExitButton,
        }

        private TextMeshProUGUI _titleText;
        private TextMeshProUGUI _infoText;
        private TextMeshProUGUI _buttonText;

        private Button _exitButton;
        
        public override bool Initialize()
        {
            if (base.Initialize() == false)
                return false;

            BindText(typeof(Texts));
            Bind<Button>(typeof(Buttons));

            _titleText = GetText((int) Texts.TitleText);
            _infoText = GetText((int) Texts.InfoText);
            _buttonText = GetText((int) Texts.ButtonText);

            _exitButton = Get<Button>((int) Buttons.ExitButton);

            _exitButton.gameObject.BindEvent(data => ExitGame());

            RefreshUI();
            
            return true;
        }

        private void ExitGame()
        {
            Application.Quit();
        }
        
        public override void RefreshUI()
        {
            _titleText.text = Managers.Data.GetText((int) Define.UITextID.ExitGame);
            _infoText.text = Managers.Data.GetText((int) Define.UITextID.Really);
            _buttonText.text = Managers.Data.GetText((int) Define.UITextID.Exit);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Managers.Game.TryExitGame = false;
                ClosePopupUI();
            }
        }
    }
}