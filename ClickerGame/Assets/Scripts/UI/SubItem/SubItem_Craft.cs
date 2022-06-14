using System.Collections;
using System.Collections.Generic;
using Data;
using Manager;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class SubItem_Craft : UI_Base
{
    enum Images
    {
        ItemIcon,
        NotifyNew,
        LevelBorder,
        LockIcon
    }

    enum Texts
    {
        LevelText
    }

    private Image notifyIcon;
    private Image lockIcon;
    private Image itemIcon;
    private Image levelBorder;
    private Image border;

    private TextMeshProUGUI levelText;

    public override bool Initialize()
    {
        if (base.Initialize() == false)
            return false;
        
        BindImage(typeof(Images));
        BindText(typeof(Texts));

        notifyIcon = GetImage((int) Images.NotifyNew);
        lockIcon = GetImage((int) Images.LockIcon);
        itemIcon = GetImage((int) Images.ItemIcon);
        levelBorder = GetImage((int) Images.LevelBorder);
        levelText = GetText((int) Texts.LevelText);
        border = GetComponent<Image>();
        
        lockIcon.SetActive(true);
        levelBorder.SetActive(false);
        levelText.SetActive(false);
        notifyIcon.SetActive(false);
        itemIcon.SetActive(false);
        
        return true;
    }

    public void SetInfo(ItemData data)
    {
        border.sprite = Managers.Data.LoadPathData<Sprite>(data.IconBorderID);
        lockIcon.SetActive(data.Lock);
        
        if (data.IconID == 0)
        {
            levelBorder.SetActive(false);
            levelText.SetActive(false);
            notifyIcon.SetActive(false);
            itemIcon.SetActive(false);
        }
        else
        {
            levelBorder.SetActive(true);
            levelText.SetActive(true);
            itemIcon.SetActive(true);

            levelText.text = data.Level.ToString();
            itemIcon.sprite = Managers.Data.LoadPathData<Sprite>(data.IconID);
        }
        
       
        
    }
}
