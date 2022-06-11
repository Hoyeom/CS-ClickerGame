using System.Collections;
using System.Collections.Generic;
using Manager;
using TMPro;
using UI.Popup;
using UnityEngine;
using UnityEngine.UI;

public class UI_TitlePopup : UI_Popup
{
    public enum GameObjects
    {
        StartButton,
    }

    public enum Texts
    {
        TitleName,
        TouchToPlay,
        StartButtonText
    }
    

    private GameObject startButton;
    private TextMeshProUGUI touchToPlay;


    protected override void Initialize()
    {
        base.Initialize();
        
        Bind<GameObject>(typeof(GameObjects));
        BindText(typeof(Texts));
        
        startButton = Get<GameObject>((int) GameObjects.StartButton)
            .BindEvent(GameStart, Define.UIEvent.Up)
            .SetActiveChain(false);
        
        GetText((int)Texts.StartButtonText).text = 
            Managers.Data.GetText((int) Define.UITextID.StartGame);

        touchToPlay = GetText((int) Texts.TouchToPlay);
        touchToPlay.text = Managers.Data.GetText((int) Define.UITextID.TouchToPlay);
        touchToPlay.gameObject.BindEvent(PopButtons);
    }

    void PopButtons()
    {
        touchToPlay.gameObject.SetActive(false);
        startButton.SetActive(true);
    }

    private void GameStart()
    {
        ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_IntroPopup>();
    }
}
