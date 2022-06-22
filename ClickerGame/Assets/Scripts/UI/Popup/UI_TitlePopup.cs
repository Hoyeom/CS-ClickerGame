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
    private TextMeshProUGUI touchToPlayText;
    private TextMeshProUGUI startButtonText;


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

        startButtonText = GetText((int) Texts.StartButtonText);

        startButtonText.text = Managers.Data.GetText((int) Define.UITextID.StartGame);
        touchToPlayText = GetText((int) Texts.TouchToPlay);
        touchToPlayText.text = Managers.Data.GetText((int) Define.UITextID.TouchToPlay);
        touchToPlayText.gameObject.BindEvent((pointer) => PopButtons());
        
        RefreshUI();
        
        return true;
    }

    void PopButtons()
    {
        touchToPlayText.gameObject.SetActive(false);
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

    public override void RefreshUI()
    {
        startButtonText.text = Managers.Data.GetText((int) Define.UITextID.StartGame);
        touchToPlayText = GetText((int) Texts.TouchToPlay);
        touchToPlayText.text = Managers.Data.GetText((int) Define.UITextID.TouchToPlay);
        touchToPlayText.gameObject.BindEvent((pointer) => PopButtons());
    }
}
