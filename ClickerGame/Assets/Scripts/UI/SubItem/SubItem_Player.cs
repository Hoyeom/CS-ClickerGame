using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Manager;
using UI;
using UI.SubItem;
using UnityEngine;
using Random = UnityEngine.Random;

public class SubItem_Player : SubItem_UnitBase
{
    public override bool Initialize()
    {
        if (base.Initialize() == false)
            return false;

        AttackSequence = DOTween.Sequence()
            .Pause()
            .Append(transform.DOLocalMove(Vector3.right * Screen.width / 2,Define.AttackSpeed))
            .Insert(Define.AttackSpeed,transform.DOLocalMove(Vector3.zero, Define.AttackDelay))
            .SetAutoKill(false);
        
        return true;
    }

    public override void SetInfo()
    {
        UnitImage.sprite = Managers.Game.Player.Sprite;
        Managers.Game.Player.OnChangeHealth -= OnChangeHeath;
        Managers.Game.Player.OnChangeHealth += OnChangeHeath;
    }

    public override void Attack(Action callback)
    {
        AttackSequence.Play()
            .OnComplete(() =>
            {
                Managers.Sound.Play(Define.Sound.Effect, "Punch",Random.Range(.95f,1.05f));
                Managers.Game.Combat.Enemy.TakeDamage(Managers.Game.Player.AtkPower);
                callback?.Invoke();
            })
            .Restart();
    }
}
