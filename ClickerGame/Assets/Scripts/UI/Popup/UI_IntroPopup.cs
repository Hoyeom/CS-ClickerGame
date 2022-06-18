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
    
    public override bool Initialize()
    {
        if (base.Initialize() == false)
            return false;
        
        Bind<GameObject>(typeof(GameObjects));

        Get<GameObject>((int) GameObjects.Background)
            .BindEvent((pointer) => SkipIntro());

        return true;
    }

    private void SkipIntro()
    {
        ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_PlayPopup>();
    }
}
