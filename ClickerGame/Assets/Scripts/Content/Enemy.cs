using System;
using Data;
using Manager;
using UnityEngine;

namespace Content
{
    public class Enemy : Unit
    {
        public EnemyData EnemyData { get; set; }
        
        public override int AtkPower => EnemyData.AttackPower;
        public override int DefPower  => EnemyData.DefencePower;
        public override int MaxHealth  => EnemyData.Health;
        public override Sprite Sprite => Managers.Data.PathIDToData<Sprite>(EnemyData.IconID);

        private SubItem_EnemyBase _view;
        public SubItem_EnemyBase View => _view;

        public bool IsDead { get; private set; } = false;
        
        public void SetView(SubItem_EnemyBase enemy)
        {
            Health = MaxHealth;
            _view = enemy;
        }

        public override void OnDead()
        {
            Managers.Game.Player.Coin += EnemyData.DropCoin;
            Managers.Game.Player.AddExp(EnemyData.DropExp);
            IsDead = true;
        }

        public override void Attack(Action callback)
        {
            View.Attack(callback);
        }
    }
}