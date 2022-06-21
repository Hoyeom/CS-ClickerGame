using System.Collections.Generic;
using System.Linq;
using Data;
using DG.Tweening;
using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup.InGame
{
    public class UI_CraftTablePopup : UI_TableBase
    {
        enum Transforms
        {
            Equip
        }

        enum Sliders
        {
            ChargeSlider
        }

        enum Images
        {
            CraftButton
        }

        private ScrollRect _scrollRect;
        
        private List<SubItem_Craft> _subItems = new List<SubItem_Craft>();
        private Transform _equipContent;
        private Slider _chargeSlider;

        private bool _availableCraft = false;
        private Sequence _chargeSequence;
        private Image _craftButton;

        public override bool Initialize()
        {
            if (base.Initialize() == false)
                return false;

            _scrollRect = GetComponentInChildren<ScrollRect>();
            
            Bind<Transform>(typeof(Transforms));
            Bind<Slider>((typeof(Sliders)));
            BindImage(typeof(Images));
            
            InitInventory();
            
            _chargeSlider = Get<Slider>((int) Sliders.ChargeSlider);
            _craftButton = GetImage((int) Images.CraftButton);
            
            _chargeSlider.value = 0;
            
            _chargeSequence = DOTween.Sequence()
                .InsertCallback(0, () => _chargeSlider.value = 0)
                .Insert(0, _chargeSlider.DOValue(1, Managers.Game.Player.ChargeSpeed)
                    .OnComplete(() => _availableCraft = true))
                .SetAutoKill(false);
            

            _craftButton.gameObject.BindEvent((data) => CraftItem());
            
            return true;
        }

        private void InitInventory()
        {
            _equipContent = Get<Transform>((int) Transforms.Equip);
            
            Managers.Game.Player.Inventory.OnChangeItem -= RefreshInventory;
            Managers.Game.Player.Inventory.OnChangeItem += RefreshInventory;
            
            _subItems.Clear();

            ItemData item = Managers.Data.Item.First().Value;

            for (int i = 0; i < Define.MaxInventorySlot; i++)
            {
                SubItem_Craft subItem = Managers.UI.MakeSubItem<SubItem_Craft>(Content);
                subItem.SetScrollRect(_scrollRect);
                subItem.Initialize();
                subItem.SlotIndex = i;
                    
                _subItems.Add(subItem);
                    
                Managers.Game.Player.Inventory.InitAddList(item);
            }
            
            SubItem_Craft craft = Managers.UI.MakeSubItem<SubItem_Craft>(_equipContent);
            Managers.Game.Player.Inventory.OnChangeEquip += craft.SetInfo;
        }
        private void CraftItem()
        {
            if(!_availableCraft) return;

            _availableCraft = false;
        
            _chargeSequence.Restart();
        
            ItemData item = Managers.Data.
                Item.
                Values.
                FirstOrDefault(data => data.Level == Managers.Game.Player.CraftLevel);
        
            if (item != null)
            {
                bool temp = Managers.Game.Player.Inventory.AddItem(item);
            
                if(temp)
                {
                    Managers.Sound.Play(Define.Sound.Effect, "Craft");
                    Debug.Log("CraftComplete");
                    return;
                }
            }

            Debug.Log("CraftFailed");
        }
        private void RefreshInventory(int index,ItemData item)
        {
            _subItems[index].SetInfo(item);
        }
    }
}