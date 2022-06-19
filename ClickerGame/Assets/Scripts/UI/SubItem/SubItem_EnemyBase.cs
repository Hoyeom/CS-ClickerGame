using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Manager;
using UI;
using UI.SubItem;
using UnityEngine;

public class SubItem_EnemyBase : SubItem_UnitBase
{
    public override bool Initialize()
    {
        if (base.Initialize() == false)
            return false;

        transform.localScale = new Vector3(-1, 1, 1);
        
        AttackSequence = DOTween.Sequence()
            .Pause()
            .Append(transform.DOLocalMove(Vector3.left * Screen.width / 2,Define.AttackSpeed))
            .Insert(Define.AttackSpeed,transform.DOLocalMove(Vector3.zero, Define.AttackDelay))
            .SetAutoKill(false);

        return true;
    }

    public override void SetInfo()
    {
        UnitImage.sprite = Managers.Game.Combat.Enemy.Sprite;
        Managers.Game.Combat.Enemy.OnChangeHealth -= OnChangeHeath;
        Managers.Game.Combat.Enemy.OnChangeHealth += OnChangeHeath;
    }

    public override void Attack(System.Action callback)
    {
        AttackSequence.Play()
            .OnComplete(() =>
            {
                Managers.Sound.Play(Define.Sound.Effect, "Punch",Random.Range(.95f,1.05f));
                Managers.Game.Player.TakeDamage(Managers.Game.Combat.Enemy.AtkPower);
                callback?.Invoke();
            })
            .Restart();
    }
}
