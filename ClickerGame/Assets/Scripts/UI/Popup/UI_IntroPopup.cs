using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UI.Popup;
using UnityEngine;

public class UI_IntroPopup : UI_Popup
{
    enum GameObjects
    {
        Background,
    }
    
    protected override void Initialize()
    {
        base.Initialize();
        Bind<GameObject>(typeof(GameObjects));

        Get<GameObject>((int) GameObjects.Background)
            .BindEvent(SkipIntro);
    }

    private void SkipIntro()
    {
        ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_PlayPopup>();
    }
}
