using System.Collections;
using System.Collections.Generic;
using Data;
using Manager;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class SubItem_Boss : UI_Base
{
    enum Images
    {
        MonsterIcon,
        FightButton
    }

    enum Texts
    {
        FightText,
        HpText,
        AtkText,
        DefText
    }

    private TextMeshProUGUI _hpText;
    private TextMeshProUGUI _atkText;
    private TextMeshProUGUI _defText;
    private Image mosterIcon;
    
    public override bool Initialize()
    {
        if (base.Initialize() == false)
            return false;
        
        BindImage(typeof(Images));
        BindText(typeof(Texts));

        _hpText = GetText((int) Texts.HpText);
        _atkText = GetText((int) Texts.AtkText);
        _defText = GetText((int) Texts.DefText);

        mosterIcon = GetImage((int) Images.MonsterIcon);
        
        return true;
    }

    public void SetInfo(MonsterData data)
    {
        _hpText.text = data.Health.ToString();
        _atkText.text = data.AttackPower.ToString();
        _defText.text = data.DefencePower.ToString();
        
        mosterIcon.sprite = Managers.Data.LoadPathData<Sprite>(data.IconID);
    }
}
