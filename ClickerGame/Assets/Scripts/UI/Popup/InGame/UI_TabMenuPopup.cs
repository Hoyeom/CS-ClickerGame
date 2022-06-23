using System;
using System.Collections.Generic;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup.InGame
{
    public class UI_TabMenuPopup : UI_Popup
    {
        enum RectTransforms
        {
            TabFocus,
        }

        enum Images
        {
            EnemyTabButton,
            UpgradeTabButton,
            CraftTabButton,
            ShopTabButton,
        }

        enum Texts
        {
            TableText,
        }

        
        
        private RectTransform _focus;
        
        private Image _enemyTabButton;
        private Image _upgradeTabButton;
        private Image _craftTabButton;
        private Image _shopTabButton;

        private TextMeshProUGUI _tableText;
        
        private Define.Tab _tab = Define.Tab.Boss;
        private Define.Tab Tab { get => _tab; set {
        {
            Define.Tab temp = _tab;
            _tab = value;
            if (_tab != temp) 
                Managers.UI.RefreshUI();
        } } }

        private Dictionary<Define.Tab, UI_TableBase> _tableBases = new Dictionary<Define.Tab, UI_TableBase>();

        public override bool Initialize()
        {
            if (base.Initialize() == false)
                return false;

            Bind<RectTransform>(typeof(RectTransforms));
            BindImage(typeof(Images));
            BindText(typeof(Texts));

            _focus = Get<RectTransform>((int) RectTransforms.TabFocus);
            _enemyTabButton = GetImage((int) Images.EnemyTabButton);
            _upgradeTabButton = GetImage((int) Images.UpgradeTabButton);
            _craftTabButton = GetImage((int) Images.CraftTabButton);
            _shopTabButton = GetImage((int) Images.ShopTabButton);

            _tableText = GetText((int) Texts.TableText);
            
            _enemyTabButton.gameObject.BindEvent(data => SelectTab(Define.Tab.Boss));
            _upgradeTabButton.gameObject.BindEvent(data => SelectTab(Define.Tab.Upgrade));
            _craftTabButton.gameObject.BindEvent(data => SelectTab(Define.Tab.Craft));
            _shopTabButton.gameObject.BindEvent(data => SelectTab(Define.Tab.Shop));
            
            _tableBases.Clear();
            
            _tableBases.Add(Define.Tab.Boss, Managers.UI.ShowPopupUI<UI_EnemyTablePopup>());
            _tableBases.Add(Define.Tab.Upgrade, Managers.UI.ShowPopupUI<UI_UpgradeTablePopup>());
            _tableBases.Add(Define.Tab.Craft, Managers.UI.ShowPopupUI<UI_CraftTablePopup>());
            _tableBases.Add(Define.Tab.Shop, Managers.UI.ShowPopupUI<UI_ShopTablePopup>());

            foreach (UI_TableBase table in _tableBases.Values)
            {
                table.Initialize();
            }
            
            SelectTab(Define.Tab.Boss);
            
            return true;
        }

        private void SelectTab(Define.Tab tab)
        {
            Tab = tab;
        }

        public override void RefreshUI()
        {
            foreach (UI_TableBase table in _tableBases.Values) 
                table.SetActive(false);
            
            _tableBases[Tab].SetActive(true);
            
            _enemyTabButton.color = Define.DefaultTabColor;
            _upgradeTabButton.color = Define.DefaultTabColor;
            _craftTabButton.color = Define.DefaultTabColor;
            _shopTabButton.color = Define.DefaultTabColor;
            
            Transform parent = null;
            
            switch (Tab)
            {
                case Define.Tab.Boss:
                    parent = _enemyTabButton.transform;
                    _enemyTabButton.color = Define.ActiveTabColor;
                    _tableText.text = Managers.Data.GetText((int) Define.UITextID.Boss);
                    break;
                case Define.Tab.Upgrade:
                    parent = _upgradeTabButton.transform;
                    _upgradeTabButton.color = Define.ActiveTabColor;
                    _tableText.text = Managers.Data.GetText((int) Define.UITextID.Upgrade);
                    break;
                case Define.Tab.Craft:
                    parent = _craftTabButton.transform;
                    _craftTabButton.color = Define.ActiveTabColor;
                    _tableText.text = Managers.Data.GetText((int) Define.UITextID.Craft);
                    break;
                case Define.Tab.Shop:
                    parent = _shopTabButton.transform;
                    _shopTabButton.color = Define.ActiveTabColor;
                    _tableText.text = Managers.Data.GetText((int) Define.UITextID.Shop);
                    break;
            }
            
            _focus.SetParent(parent);
            _focus.anchoredPosition = Vector2.zero;
        }


    }
}