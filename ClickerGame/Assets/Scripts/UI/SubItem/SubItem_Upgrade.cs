using System;
using System.Collections;
using System.Collections.Generic;
using Content;
using Data;
using DG.Tweening;
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

    private DOTweenAnimation _doTweenAnimation;
    
    private UpgradeData _data;

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

        _doTweenAnimation = GetComponent<DOTweenAnimation>();
        
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
        _data = data;
        priceText.text = data.Price.ToString();
        nextText.text = data.IncreaseStat.ToString();
        levelText.text = data.Level.ToString();
        upgradeIcon.sprite = Managers.Data.PathIDToData<Sprite>(data.IconID);


        switch (data.UpgradeType)
        {
            case Define.UpgradeType.Health:
                priceButton.gameObject.BindEvent((pointer) => Managers.Game.UpgradeShop.Health.Upgrade());
                break;
            case Define.UpgradeType.Defence:
                priceButton.gameObject.BindEvent((pointer) => Managers.Game.UpgradeShop.DefPower.Upgrade());
                break;
            case Define.UpgradeType.Attack:
                priceButton.gameObject.BindEvent((pointer) => Managers.Game.UpgradeShop.AtkPower.Upgrade());
                break;
        }
    }

    public override void RefreshUI()
    {
        switch (_data.UpgradeType)
        {
            case Define.UpgradeType.Attack:
                Status atk = Managers.Game.UpgradeShop.AtkPower;
                levelText.text = atk.Level.ToString();
                priceText.text =atk.Price.ToString();
                nextText.text = atk.IncreaseStat.ToString();
                currentText.text = Managers.Game.Player.AtkPower.ToString();
                break;
            case Define.UpgradeType.Defence:
                Status def = Managers.Game.UpgradeShop.DefPower;
                levelText.text = def.Level.ToString();
                priceText.text = def.Price.ToString();
                nextText.text = def.IncreaseStat.ToString();
                currentText.text = Managers.Game.Player.DefPower.ToString();
                break;
            case Define.UpgradeType.Health:
                Status health = Managers.Game.UpgradeShop.Health;
                levelText.text = health.Level.ToString();
                priceText.text = health.Price.ToString();
                nextText.text = health.IncreaseStat.ToString();
                currentText.text = Managers.Game.Player.MaxHealth.ToString();
                break;
        }
    }
    
    public void TweenRestart()
    {
        _doTweenAnimation.DORestart();
    }
}
