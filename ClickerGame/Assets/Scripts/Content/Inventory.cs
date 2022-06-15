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
        private const int LockID = 500000;
        private const int UnLockID = 500001;
        
        public List<ItemData> Items = new List<ItemData>();

        public event Action<int, ItemData> OnChangeItem;
        
        public int Slot = 4; // TODO StartData 연동
        
        
        public void InitAddList(ItemData item)
        {
            Items.Add(item);
            OnChangeItem?.Invoke(Items.Count - 1, item);
            
            if (Items.Count + 1 == Define.MaxInventorySlot)
            {
                
                InitLock();
            }
        }

        public bool AddItem(ItemData data)
        {
            int? index = FindIndexEmptySlot();
            
            if (index != null)
            {
                ChangeItem((int) index, data);
                return true;
            }

            return false;
        }


        
        public bool Craft(int slotIndex1, int slotIndex2)
        {
            bool value = Items[slotIndex1].Level == Items[slotIndex2].Level;

            if (value && Upgrade(slotIndex2))
            {
                RemoveItem(slotIndex1);
                Debug.Log("Upgrade Complete");
            }
            
            return value;
        }

        private void ChangeItem(int index, ItemData item)
        {
            Items[index] = item;
            OnChangeItem?.Invoke((int) index, item);
        }
        
        public void RemoveItem(int slotIndex)
        { 
            ChangeItem(slotIndex,Managers.Data.Item.First().Value);
        }

        private void InitLock()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Slot <= i)
                    ChangeItem(i, Managers.Data.Item[LockID]);
                else if(IsLockOrEmpty(i))
                    ChangeItem(i, Managers.Data.Item[UnLockID]);
            }
        }

        private bool IsLockOrEmpty(int slotIndex)
            => Items[slotIndex].Level == 0;
        private bool Upgrade(int slotIndex)
        {
            if (Managers.Data.Item.TryGetValue(Items[slotIndex].ID + 1, out ItemData item))
            {
                ChangeItem(slotIndex,item);
                return true;
            }
            
            return false;
        }
        
        private int? FindIndexEmptySlot()
        {
            int? value = null;
            
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Level == 0 && Items[i].Lock == false)
                {
                    value = i;
                    break;
                }
            }

            return value;
        }
        
    }
}