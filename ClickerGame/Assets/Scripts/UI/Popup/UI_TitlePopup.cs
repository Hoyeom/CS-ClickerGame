using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using TMPro;
using UI.Popup;
using UnityEngine;
using UnityEngine.UI;

public class UI_TitlePopup : UI_Popup
{
    enum GameObjects
    {
        StartButton,
        SettingButton
    }

    enum Texts
    {
        TitleName,
        TouchToPlay,
        StartButtonText
    }
    
    

    private GameObject startButton;
    private GameObject settingButton;
    private TextMeshProUGUI touchToPlay;


    public override bool Initialize()
    {
        if (base.Initialize() == false)
            return false;

        Managers.Sound.Play(Define.Sound.Bgm, "TitleBgm");
        
        Bind<GameObject>(typeof(GameObjects));
        BindText(typeof(Texts));
        
        startButton = Get<GameObject>((int) GameObjects.StartButton)
            .BindEvent((pointer)=>GameStart(), Define.UIEvent.Up)
            .SetActiveChain(false);

        settingButton = Get<GameObject>((int) GameObjects.SettingButton)
            .BindEvent((pointer) => Managers.UI.ShowPopupUI<UI_LanguagePopup>());
        
        GetText((int)Texts.StartButtonText).text = 
            Managers.Data.GetText((int) Define.UITextID.StartGame);

        touchToPlay = GetText((int) Texts.TouchToPlay);
        touchToPlay.text = Managers.Data.GetText((int) Define.UITextID.TouchToPlay);
        touchToPlay.gameObject.BindEvent((pointer) => PopButtons());

        return true;
    }

    void PopButtons()
    {
        touchToPlay.gameObject.SetActive(false);
        startButton.SetActive(true);
    }

    private void GameStart()
    {
        if (String.IsNullOrEmpty(Managers.Game.Player.Name))
        {
            Managers.UI.ShowPopupUI<UI_NameChangePopup>();
            return;
        }
        
        ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_IntroPopup>();
    }
}
