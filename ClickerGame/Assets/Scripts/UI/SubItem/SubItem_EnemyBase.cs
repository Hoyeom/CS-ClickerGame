using System.Collections;
using System.Collections.Generic;
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

        return true;
    }

    public override void Attack()
    {
        
    }
}
