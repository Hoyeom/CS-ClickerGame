using System.Collections;
using System.Collections.Generic;
using Data;
using Manager;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class SubItem_Upgrade : UI_Base
{
    enum Texts
    {
        CostText,
        CurrentText,
        NextText
    }

    enum Images
    {
        UpgradeIcon
    }

    private Image upgradeIcon;
    private TextMeshProUGUI costText;
    private TextMeshProUGUI currentText;
    private TextMeshProUGUI nextText;
    
    public override bool Initialize()
    {
        if (base.Initialize() == false)
            return false;
        
        BindText(typeof(Texts));
        BindImage(typeof(Images));

        upgradeIcon = GetImage((int) Images.UpgradeIcon);
        costText = GetText((int) Texts.CostText);
        currentText = GetText((int) Texts.CurrentText);
        nextText = GetText((int) Texts.NextText);
        
        return true;
    }
    
    public void SetInfo(UpgradeData data)
    {
        costText.text = data.Cost.ToString();
        currentText.text = data.CurValue.ToString();
        nextText.text = data.UpgradeValue.ToString();
        upgradeIcon.sprite = Managers.Data.LoadPathData<Sprite>(data.IconID);
    }
    
    public void RefreshUI()
    {
        
    }
}
