using System;
using Data;
using Manager;
using UnityEngine;

namespace Content
{
    [Serializable]
    public class Player : Unit
    {
        [SerializeField] private int _levelID = 0;

        [SerializeField] private int _exp;
        public event Action<int,int> OnChangeExp;
        public int Exp
        {
            get => _exp;
            set
            {
                _exp = value;
                OnChangeExp?.Invoke(_exp, TargetExp);
            }
        }

        public int LevelID
        {
            get=>_levelID;
            set
            {
                _levelID = value;
                OnChangePlayerLevel?.Invoke(Level);
            }
        }
        public StartStatusData StatusData => Managers.Data.StartStatus[LevelID];
        private SubItem_Player _view;
        public SubItem_Player View => _view;
        public int Level => StatusData.Level;
        public int AddCoin => StatusData.AddCoin;
        public int TargetExp => StatusData.TargetExp;
        public float ChargeSpeed => StatusData.ChargeSpeed;
        public override int AtkPower => StatusData.AtkPower + Managers.Game.UpgradeShop.AtkPower.Stat +
                                        (Inventory.Equip != null ? Inventory.Equip.AttackPower : 0);
        public override int DefPower => StatusData.DefPower + Managers.Game.UpgradeShop.DefPower.Stat;
        public override int MaxHealth => StatusData.Health + Managers.Game.UpgradeShop.Health.Stat;

        public override Sprite Sprite =>  Managers.Data.PathIDToData<Sprite>(StatusData.IconID); 

        public override void OnDead()
        {
            IsDead = true;
            Coin -= Level * 1000;
            Health = MaxHealth;
        }
        
        public override void Attack(Action callback) => View.Attack(callback);
        
        public int CraftLevel => StatusData.CraftLevel;
        public event Action<int> OnChangePlayerLevel;

        [SerializeField] private int _coin;
        public int Coin
        {
            get => _coin;
            set
            {
                _coin = Mathf.Max(value, 0);
                OnChangeCoin?.Invoke(_coin);
            }
        }
        public event Action<int> OnChangeCoin;
        public override void RefreshUIData()
        {
            base.RefreshUIData();
            
            OnChangeCoin?.Invoke(Coin);
            OnChangePlayerLevel?.Invoke(Level);
            OnChangeExp?.Invoke(_exp, TargetExp);
            Inventory.RefreshUIData();
        }
        
        public void SetView(SubItem_Player player)
        {
            _view = player;
            Health = MaxHealth;
            player.SetInfo();
        }

        public void TabToAddCoin()
        {
            Coin += AddCoin;
            Managers.Sound.Play(Define.Sound.Effect, "Coin");
        }

        public void AddExp(int exp)
        {
            Exp += exp;
            if (TargetExp <= Exp)
                LevelUp();
        }

        private void LevelUp()
        {
            Exp = 0;
            if (Managers.Data.StartStatus.ContainsKey(_levelID + 1))
            {
                LevelID += 1;
                Inventory.RefreshUIData();
            }
            else
            {
                Debug.Log("만렙");
            }
        }
        
        public Inventory Inventory
        {
            get => Managers.Game.SaveData.Inventory;
            set => Managers.Game.SaveData.Inventory = value;
        }

        public bool IsDead { get; set; } = false;
    }
}