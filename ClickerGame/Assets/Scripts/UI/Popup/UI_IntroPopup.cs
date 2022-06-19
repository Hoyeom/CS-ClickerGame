using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using TMPro;
using UI.Popup;
using UnityEngine;

public class UI_IntroPopup : UI_Popup
{
    enum GameObjects
    {
        Background,
        Select,
        NewGame,
    }

    enum InputFields
    {
        NameInput
    }

    private TMP_InputField nameInput;
    
    public override bool Initialize()
    {
        if (base.Initialize() == false)
            return false;
        
        Bind<GameObject>(typeof(GameObjects));
        BindInputField(typeof(InputFields));

        nameInput = GetInputField((int) InputFields.NameInput);
        
        if (Managers.Game.NewGame)
        {
            Get<GameObject>((int) GameObjects.Select)
                .BindEvent(data => SetName());
        }
        else
        {
            Get<GameObject>((int) GameObjects.NewGame).SetActive(false);
            Get<GameObject>((int) GameObjects.Background)
                .BindEvent((pointer) => SkipIntro());
        }
        
        
        return true;
    }

    private void SetName()
    {
        if(String.IsNullOrEmpty(nameInput.text)) return;
        
        Managers.Game.Player.Name = nameInput.text;
        SkipIntro();
    }
    
    private void SkipIntro()
    {
        ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_PlayPopup>();
    }
}
