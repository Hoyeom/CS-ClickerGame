using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Manager;
using UnityEngine;

namespace Content
{
    public class Inventory
    {
        private readonly List<ItemData> _items = new List<ItemData>();

        public event Action<int, ItemData> OnChangeItem;
        
        public int Slot = 4; // TODO StartData 연동

        public void InitAddList(ItemData item)
        {
            _items.Add(item);
            if (Slot >= _items.Count)
            {
                Upgrade(_items.Count - 1);
                return;
            }
            
            OnChangeItem?.Invoke(_items.Count - 1, item);
        }


        public bool AddItem(ItemData data)
        {
            int? index = FoundEmptySlot();
            
            if (index != null)
            {
                _items[(int) index] = data;
                OnChangeItem?.Invoke((int) index, data);
                return true;
            }

            return false;
        }
        
        
        public bool Craft(int slotIndex1, int slotIndex2)
        {
            bool value = _items[slotIndex1].Level == _items[slotIndex2].Level;

            if (value)
            {
                if (Upgrade(slotIndex2))
                {
                    RemoveItem(slotIndex1);
                    Debug.Log("Upgrade Complete");
                }
            }
            
            return value;
        }

        public void RemoveItem(int slotIndex)
        {
            _items[slotIndex] = Managers.Data.Item.First().Value;
            OnChangeItem?.Invoke(slotIndex, _items[slotIndex]);
        }


        private bool Upgrade(int slotIndex)
        {
            if (Managers.Data.Item.TryGetValue(_items[slotIndex].ID + 1, out ItemData item))
            {
                _items[slotIndex] = item;
                OnChangeItem?.Invoke(slotIndex, item);
                return true;
            }
            
            return false;
        }
        
        private int? FoundEmptySlot()
        {
            int? value = null;
            
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Level == 0 && _items[i].Lock == false)
                {
                    value = i;
                    break;
                }
            }

            return value;
        }
    }
}