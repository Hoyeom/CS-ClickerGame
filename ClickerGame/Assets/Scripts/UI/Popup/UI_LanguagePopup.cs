using System.Collections;
using System.Collections.Generic;
using Manager;
using TMPro;
using UI.Popup;
using UnityEngine;

public class UI_LanguagePopup : UI_Popup
{
    enum GameObjects
    {
        ScreenDim,
        CloseButton,
        EngFlag,
        KorFlag,
        Focus,
    }

    enum Texts
    {
        TitleText
    }

    private GameObject focus;
    private GameObject engFlag;
    private GameObject korFlag;
    private GameObject closeButton;
    private GameObject screenDim;
    private TextMeshProUGUI titleText;
    
    public override bool Initialize()
    {
        if (base.Initialize() == false)
            return false;
        
        BindText(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        titleText = GetText((int) Texts.TitleText);
            
        focus = Get<GameObject>((int) GameObjects.Focus);
        engFlag = Get<GameObject>((int) GameObjects.EngFlag);
        korFlag = Get<GameObject>((int) GameObjects.KorFlag);
        closeButton = Get<GameObject>((int) GameObjects.CloseButton);
        screenDim = Get<GameObject>((int) GameObjects.ScreenDim);

        engFlag.BindEvent((evt) => SetFocus(Define.Language.Eng));
        korFlag.BindEvent((evt) => SetFocus(Define.Language.Kor));

        closeButton.BindEvent((evt) => ClosePopupUI());
        screenDim.BindEvent((evt) => ClosePopupUI());

        titleText.text = Managers.Data.GetText((int) Define.UITextID.Language);
        
        titleText = GetText((int) Texts.TitleText);
        
        SetFocus(Managers.Data.Language);
        
        RefreshUI();
        
        return true;
    }

    public override void RefreshUI()
    {
        switch (Managers.Data.Language)
        {
            case Define.Language.Eng:
                focus.transform.SetParent(engFlag.transform);
                break;
            case Define.Language.Kor:
                focus.transform.SetParent(korFlag.transform);
                break;
        }
        
        titleText.text = Managers.Data.GetText((int) Define.UITextID.Language);
        focus.transform.localPosition = Vector3.zero;
    }

    private void SetFocus(Define.Language language)
    {
        Managers.Data.Language = language;
    }
}
