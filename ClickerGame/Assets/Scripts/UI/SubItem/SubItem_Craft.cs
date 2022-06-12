using System.Collections;
using System.Collections.Generic;
using Data;
using UI;
using UnityEngine;

public class SubItem_Craft : UI_Base
{
    public override bool Initialize()
    {
        if (base.Initialize() == false)
            return false;

        return true;
    }
    
    public void SetInfo(WeaponData data)
    {
        
    }
}
