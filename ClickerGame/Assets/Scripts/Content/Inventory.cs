using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Manager;
using UnityEngine;

namespace Content
{
    [Serializable]
    public class Inventory
    {
        private const int LockID = 500000;
        private const int UnLockID = 500001;
        
        [NonSerialized] public List<ItemData> Items = new List<ItemData>();
        public List<int> SaveData = new List<int>();

        private ItemData _equip;
        public event Action<ItemData> OnChangeEquip;
        public ItemData Equip
        {
            get=>_equip;
            set
            {
                _equip = value;
                EquipSaveData = _equip.GetID();
                OnChangeEquip?.Invoke(_equip);
            }
        }

        public int EquipSaveData;
        
        public event Action<int, ItemData> OnChangeItem;

        [SerializeField] private int _slot;
        public int Slot
        {
            get => Managers.Game.Player.StatusData.InventorySlot;
        }

        private bool _init = false;
        public void InitAddList(ItemData item)
        {
            if(_init) return;
            
            Items.Add(item);
            SaveData.Add(item.GetID());
            OnChangeItem?.Invoke(Items.Count - 1, item);
            
            if (Items.Count + 1 == Define.MaxInventorySlot)
                InitLock();
        }
        
        
        public void LoadData()
        {
            Items = new List<ItemData>();
            for (int i = 0; i < SaveData.Count; i++)
                Items.Add(Managers.Data.Item[SaveData[i]]);

            if (Managers.Data.Item.TryGetValue(EquipSaveData, out ItemData itemData))
                Equip = itemData;
            
            InitLock();
            _init = true;
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


        public void EquipWeapon(int slotIndex)
        {
            if (Equip != null)
                (Equip, Items[slotIndex]) = (Items[slotIndex], Equip);
            else
            {
                Equip = Items[slotIndex];
                RemoveItem(slotIndex);
            }
            
            RefreshUIData();
        }
        
        public bool Craft(int slotIndex1, int slotIndex2)
        {
            bool value = Items[slotIndex1].Level == Items[slotIndex2].Level
                         && slotIndex1 != slotIndex2;

            if (value && Upgrade(slotIndex2))
            {
                RemoveItem(slotIndex1);
                Debug.Log("Upgrade Complete");
            }
            
            return value;
        }

        public void ChangeSlot(int slotIndex1, int slotIndex2)
        {
            Debug.Log("ChangeSlot");
            if (!Items[slotIndex1].Lock || !Items[slotIndex2])
                (Items[slotIndex1], Items[slotIndex2]) = (Items[slotIndex2], Items[slotIndex1]);
            
            RefreshUIData();
        }
        
        private void ChangeItem(int index, ItemData item)
        {
            Items[index] = item;
            SaveData[index] = item.GetID();
            OnChangeItem?.Invoke((int) index, item);
        }

        public void RemoveItem(int slotIndex)
        { 
            ChangeItem(slotIndex, Managers.Data.Item[UnLockID]);
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

        private bool EquipUpgrade()
        {
            if (Managers.Data.Item.TryGetValue(Equip.ID + 1, out ItemData item))
            {
                Equip = item;
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

        public void RefreshUIData()
        {
            for (int i = 0; i < Items.Count; i++)
                OnChangeItem?.Invoke(i, Items[i]);
            if(Equip != null)
                OnChangeEquip?.Invoke(Equip);
            InitLock();
        }

        public void UpgradeItems(int level)
        {
            Debug.Log($"Player{level} Item {Equip.Level}");
            
            if (Equip.Level <= level)
                EquipUpgrade();
            
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Level <= level)
                    Upgrade(i);
            }
        }
    }
}