using System;
using UnityEngine;

namespace Content
{
    public abstract class Unit
    {
        private bool _turn;
        public bool Turn
        {
            get => _turn;
            set => _turn = value;
        }
        public abstract int AtkPower { get; }
        public abstract int DefPower { get; }
        public abstract int MaxHealth { get; }
        private int _health;
        protected int Health
        {
            get => _health;
            set
            {
                _health = value;
                OnChangeHealth?.Invoke(_health, MaxHealth);
            }
        }

        public event Action<int,int> OnChangeHealth;

        public virtual void RefreshUIData()
        {
            OnChangeHealth?.Invoke(_health, MaxHealth);
        }
        
        public abstract Sprite Sprite { get; }
        public abstract void OnDead();
        public abstract void Attack(Action callback);

        public virtual void TakeDamage(int damage)
        {
            if(Health <= 0) return;

            damage -= DefPower;
            
            Health -= damage;

            Debug.Log($"남은 체력: {Health} 데미지: {damage}");

            if (Health <= 0)
            {
                Health = 0;
                OnDead();
            }
        }
    }
}