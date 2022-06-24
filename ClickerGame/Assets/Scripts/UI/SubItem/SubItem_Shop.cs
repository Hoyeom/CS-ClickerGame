using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using Manager;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class SubItem_Shop : UI_Base
{
    enum Texts
    {
        Title,
        Price
    }

    enum Images
    {
        Icon,
    }

    enum Buttons
    {
        PriceButton
    }

    private ShopData _data;
    
    private TextMeshProUGUI _titleText;
    private TextMeshProUGUI _priceText;

    private Image _icon;

    private Button _priceButton;

    private Action _costAction;
    private Action _rewardAction;
    
    public override bool Initialize()
    {
        if (base.Initialize() == false)
            return false;
        
        BindText(typeof(Texts));
        BindImage(typeof(Images));
        Bind<Button>(typeof(Buttons));

        _titleText = GetText((int) Texts.Title);
        _priceText = GetText((int) Texts.Price);

        _icon = GetImage((int) Images.Icon);

        _priceButton = Get<Button>((int) Buttons.PriceButton);
        // _priceButton.gameObject.BindEvent(evt => Managers.Ads.LoadAd());

        return true;
    }
    
    public void SetInfo(ShopData data)
    {
        _data = data;
        
        _titleText.text = Managers.Data.GetText(data.TitleTextID);
        _priceText.text = Managers.Data.GetText(data.PriceTextID);

        _icon.sprite = Managers.Data.PathIDToData<Sprite>(data.IconID);

        SetCostAction();
        SetRewardAction();
        
        _priceButton.gameObject.BindEvent(data => _costAction.Invoke());
    }

    void SetCostAction()
    {
        switch (_data.CostType)
        {
            case Define.Cost.Ads:
                _costAction = AdAction;
                break;
            case Define.Cost.Cash:
                _costAction = CashAction;
                break;
            case Define.Cost.Coin:
                _costAction = CoinAction;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(_data.CostType));
        }
    }
    void SetRewardAction()
    {
        switch (_data.RewardType)
        {
            case Define.Cost.Ads:
                _rewardAction = RemoveAds;

                break;
            case Define.Cost.Cash:
                _rewardAction = GetCash;
                break;
            case Define.Cost.Coin:
                _rewardAction = GetCoin;
                break;
        }
    }

    private void GetCoin()
    {
        Managers.Game.Player.Coin += _data.Reward;
    }

    private void RemoveAds()
    {
        Debug.Log("광고 제거");
    }

    private void GetCash()
    {
        Debug.Log("현금 주는거 안되요!!!");
    }

    private void AdAction()
    {
        Managers.Ads.LoadAd(((sender, reward) => _rewardAction.Invoke()));
    }

    private void CoinAction()
    {
        if (Managers.Game.Player.Coin < _data.Price) return;
        
        Managers.Game.Player.Coin -= _data.Price;
        // TODO Price Action
    }

    private void CashAction()
    {
        
    }
}
