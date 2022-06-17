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
        NextText,
        LevelText
    }

    enum Images
    {
        UpgradeIcon,
        PriceButton,
    }

    private Image upgradeIcon;
    private Image priceButton;
    private TextMeshProUGUI priceText;
    private TextMeshProUGUI currentText;
    private TextMeshProUGUI nextText;
    private TextMeshProUGUI levelText;
    
    public override bool Initialize()
    {
        if (base.Initialize() == false)
            return false;
        
        BindText(typeof(Texts));
        BindImage(typeof(Images));

        upgradeIcon = GetImage((int) Images.UpgradeIcon);
        priceButton = GetImage((int) Images.PriceButton);
        priceText = GetText((int) Texts.CostText);
        currentText = GetText((int) Texts.CurrentText);
        nextText = GetText((int) Texts.NextText);
        levelText = GetText((int) Texts.LevelText);


        return true;
    }

    public void SetInfo(UpgradeData data)
    {
        priceText.text = data.Price.ToString();
        nextText.text = data.IncreaseStat.ToString();
        levelText.text = data.Level.ToString();
        upgradeIcon.sprite = Managers.Data.LoadPathData<Sprite>(data.IconID);
        
        switch (data.UpgradeType)
        {
            case Define.UpgradeType.Health:
                priceButton.gameObject.BindEvent(() => Managers.Game.UpgradeShop.Health.Upgrade());
                Managers.Game.UpgradeShop.Health.OnChangePrice += SetPrice;
                Managers.Game.Player.OnChangeHealth += SetCurrentStatus;
                break;
            case Define.UpgradeType.Defence:
                priceButton.gameObject.BindEvent(() => Managers.Game.UpgradeShop.DefPower.Upgrade());
                Managers.Game.UpgradeShop.DefPower.OnChangePrice += SetPrice;
                Managers.Game.Player.OnChangeDefPower += SetCurrentStatus;
                break;
            case Define.UpgradeType.Attack:
                priceButton.gameObject.BindEvent(() => Managers.Game.UpgradeShop.AtkPower.Upgrade());
                Managers.Game.UpgradeShop.AtkPower.OnChangePrice += SetPrice;
                Managers.Game.Player.OnChangeAtkPower += SetCurrentStatus;
                break;
        }
    }

    public void SetPrice(int value)
    {
        priceText.text = value.ToString();
    }
    
    public void SetCurrentStatus(int value)
    {
        currentText.text = value.ToString();
    }
}
