using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.SubItem
{
    public abstract class SubItem_UnitBase : UI_Base
    {
        protected enum Images
        {
            UnitImage,
            HealthImage,
        }
        
        private Image unitImage;
        private Image healthImage;
        public Image UnitImage => unitImage;
        public Image HealthImage => healthImage;
        protected Sequence AttackSequence { get; set; }
        
        public override bool Initialize()
        {
            if (base.Initialize() == false)
                return false;

            BindImage(typeof(Images));
            
            unitImage = GetImage((int) Images.UnitImage);
            healthImage = GetImage((int) Images.HealthImage);

            SetInfo();
             
            return true;
        }

        public void SetHealthSlider(int cur, int max)
        {
            HealthImage.fillAmount = (float) cur / max;
        }

        public abstract void SetInfo();

        public abstract void Attack(Action callback);
        
    }
}