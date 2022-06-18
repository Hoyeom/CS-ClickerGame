﻿using Data;
using Manager;
using UnityEngine;

namespace Content
{
    public class Combat
    {
        enum Turn
        {
            PlayerTurn,
            EnemyTurn,
        }

        public Enemy Enemy { get; set; } = new Enemy();
        public Player Player => Managers.Game.Player;
        private Turn _turn;

        public void Initialize()
        {
            Enemy = new Enemy();
            _turn = Turn.PlayerTurn;
        }

        public void OnStart()
        {
            Managers.Game.Player.Attack(TurnEnd);
        }

        private void TurnEnd()
        {
            if (Enemy.IsDead)
            {
                Managers.Resource.Destroy(Enemy.View.gameObject);
                Enemy.SetView(null);

                return;
            }
            else if (Player.IsDead)
            {
                Managers.Resource.Destroy(Enemy.View.gameObject);
                Enemy.SetView(null);

                Player.IsDead = false;
                _turn = Turn.PlayerTurn;
                return;
            }

            if (_turn == Turn.PlayerTurn)
            {
                _turn = Turn.EnemyTurn;

                Enemy.Attack(TurnEnd);
            }
            else if (_turn == Turn.EnemyTurn)
            {
                _turn = Turn.PlayerTurn;

                Managers.Game.Player.Attack(TurnEnd);
            }
        }

        public void SpawnEnemy(EnemyData data)
        {
            SubItem_EnemyBase temp = null;

            if (Enemy.View != null)
            {
                temp = Enemy.View;
                if (Enemy.EnemyData == data)
                    return;
            }

            Enemy = new Enemy
            {
                EnemyData = data,
                Turn = false,
            };

            Enemy.SetView(temp == null
                ? Managers.UI.MakeSubItem<SubItem_EnemyBase>(Managers.Game.EnemySpawnArea)
                : temp);

            Enemy.View.SetInfo();

            OnStart();
        }
    }
}