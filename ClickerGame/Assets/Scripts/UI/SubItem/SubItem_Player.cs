using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Manager;
using UI;
using UI.SubItem;
using UnityEngine;

public class SubItem_Player : SubItem_UnitBase
{
    public override bool Initialize()
    {
        if (base.Initialize() == false)
            return false;
        
        
        
        return true;
    }

    public override void Attack()
    {
        transform.DOLocalMove(Vector3.right * 200, Managers.Game.Player.AttackSpeed)
            .OnComplete(() => transform.DOLocalMove(Vector3.zero, Managers.Game.Player.AttackDelay));
    }
}
