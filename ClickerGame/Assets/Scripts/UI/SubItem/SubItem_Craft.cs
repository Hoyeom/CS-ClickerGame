using System.Collections;
using System.Collections.Generic;
using Data;
using Manager;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
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

    public int SlotIndex { get; set; }

    private Image notifyIcon;
    private Image lockIcon;
    private Image itemIcon;
    private Image levelBorder;
    private Image border;

    public ItemData Item { get; private set; }
    
    private TextMeshProUGUI levelText;

    private ScrollRect _scrollRect;

    public override bool Initialize()
    {
        if (base.Initialize() == false)
            return false;
        
        BindImage(typeof(Images));
        BindText(typeof(Texts));

        notifyIcon = GetImage((int) Images.NotifyNew);
        lockIcon = GetImage((int) Images.LockIcon);
        levelBorder = GetImage((int) Images.LevelBorder);
        levelText = GetText((int) Texts.LevelText);
        itemIcon = GetImage((int) Images.ItemIcon);
        border = GetComponent<Image>();

        gameObject.BindEvent(DragIcon, Define.UIEvent.Pressed);
        gameObject.BindEvent(DownIcon, Define.UIEvent.Down);
        gameObject.BindEvent(UpIcon, Define.UIEvent.Up);
        
        lockIcon.SetActive(true);
        levelBorder.SetActive(false);
        levelText.SetActive(false);
        notifyIcon.SetActive(false);
        itemIcon.SetActive(false);
        
        return true;
    }

    
    public void SetScrollRect(ScrollRect scrollRect)
    {
        _scrollRect = scrollRect;
    }
    
    private void DownIcon(PointerEventData pointer)
    {
        _scrollRect.enabled = false;
        
        itemIcon.transform.SetParent(Managers.UI.HighOrderCanvas);
    }
    
    private void DragIcon(PointerEventData pointer)
    {
        itemIcon.transform.position = Input.mousePosition;
    }

    private void UpIcon(PointerEventData pointer)
    {
        _scrollRect.enabled = true;
        
        itemIcon.transform.SetParent(transform);
        itemIcon.transform.SetAsFirstSibling();
        itemIcon.transform.localPosition = Vector3.zero;

        if(pointer == null) return;
        
        if (pointer.pointerCurrentRaycast.gameObject.TryGetComponent<SubItem_Craft>(out SubItem_Craft pointerItem))
        {
            if(pointerItem.Item.Lock) return;
            
            if (this == pointerItem)
                Managers.Game.Player.Inventory.EquipWeapon(SlotIndex);
            else if (!Managers.Game.Player.Inventory.Craft(SlotIndex, pointerItem.SlotIndex))
            {
                Managers.Game.Player.Inventory.ChangeSlot(SlotIndex, pointerItem.SlotIndex);
            }
        }
    }
    
    public void SetInfo(ItemData data)
    {
        Item = data;
        border.sprite = Managers.Data.PathIDToData<Sprite>(data.IconBorderID);
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
            itemIcon.sprite = Managers.Data.PathIDToData<Sprite>(data.IconID);
        }
        
       
        
    }
}
