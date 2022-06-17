using UnityEngine;

namespace UI.SubItem
{
    public abstract class SubItem_UnitBase : UI_Base
    {
        public override bool Initialize()
        {
            if (base.Initialize() == false)
                return false;

            InvokeRepeating(nameof(Attack), 0, 2);
            
            
            return true;
        }

        public abstract void Attack();

    }
}