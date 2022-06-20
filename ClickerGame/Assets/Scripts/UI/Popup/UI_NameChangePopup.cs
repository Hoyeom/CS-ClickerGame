using System;
using Manager;
using TMPro;
using UnityEngine;

namespace UI.Popup
{
    public class UI_NameChangePopup : UI_Popup
    {
        enum GameObjects
        {
            Select,
        }

        enum InputFields
        {
            NameInput
        }

        enum Texts
        {
            Placeholder,
            SelectText,
            TitleText,
        }
        
        private TMP_InputField nameInput;
        
        public override bool Initialize()
        {
            if (base.Initialize() == false)
                return false;
            
            Bind<GameObject>(typeof(GameObjects));
            BindText(typeof(Texts));
            BindInputField(typeof(InputFields));
            
            nameInput = GetInputField((int) InputFields.NameInput);

            GetText((int) Texts.Placeholder).text = Managers.Data.GetText((int) Define.UITextID.UserName);
            GetText((int) Texts.SelectText).text = Managers.Data.GetText((int) Define.UITextID.Select);
            GetText((int) Texts.TitleText).text = Managers.Data.GetText((int) Define.UITextID.YouName);
            
            Get<GameObject>((int) GameObjects.Select)
                .BindEvent(data => SetName());
            
            return true;
        }
        
        private void SetName()
        {
            if(String.IsNullOrEmpty(nameInput.text)) return;
            
            Managers.Game.Player.Name = nameInput.text;
            
            ClosePopupUI();
        }
    }
}