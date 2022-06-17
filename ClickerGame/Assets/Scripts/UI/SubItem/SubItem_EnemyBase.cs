using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class SubItem_EnemyBase : UI_Base
{
    public override bool Initialize()
    {
        if (base.Initialize() == false)
            return false;

        transform.localScale = new Vector3(-1, 1, 1);

        return true;
    }
}
