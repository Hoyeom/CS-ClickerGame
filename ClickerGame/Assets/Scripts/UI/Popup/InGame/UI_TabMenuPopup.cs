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

        enum Buttons
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
        
        private Button _enemyTabButton;
        private Button _upgradeTabButton;
        private Button _craftTabButton;
        private Button _shopTabButton;

        private TextMeshProUGUI _tableText;
        
        private Define.Tab _tab = Define.Tab.Boss;
        private Define.Tab Tab { get => _tab; set {
        {
            Define.Tab temp = _tab;
            _tab = value; 
            if (_tab != temp) Managers.UI.RefreshUI();
        } } }

        private Dictionary<Define.Tab, UI_TableBase> _tableBases = new Dictionary<Define.Tab, UI_TableBase>();

        public override bool Initialize()
        {
            if (base.Initialize() == false)
                return false;
            
            Bind<RectTransform>(typeof(RectTransforms));
            Bind<Button>(typeof(Buttons));
            BindText(typeof(Texts));

            _focus = Get<RectTransform>((int) RectTransforms.TabFocus);
            _enemyTabButton = Get<Button>((int) Buttons.EnemyTabButton);
            _upgradeTabButton = Get<Button>((int) Buttons.UpgradeTabButton);
            _craftTabButton = Get<Button>((int) Buttons.CraftTabButton);
            _shopTabButton = Get<Button>((int) Buttons.ShopTabButton);

            _tableText = GetText((int) Texts.TableText);
            
            _enemyTabButton.gameObject.BindEvent(data => SelectTab(Define.Tab.Boss));
            _upgradeTabButton.gameObject.BindEvent(data => SelectTab(Define.Tab.Upgrade));
            _craftTabButton.gameObject.BindEvent(data => SelectTab(Define.Tab.Craft));
            _shopTabButton.gameObject.BindEvent(data => SelectTab(Define.Tab.Shop));
            
            _tableBases.Clear();
            
            _tableBases.Add(Define.Tab.Boss,Managers.UI.ShowPopupUI<UI_EnemyTablePopup>());
            _tableBases.Add(Define.Tab.Upgrade,Managers.UI.ShowPopupUI<UI_UpgradeTablePopup>());
            _tableBases.Add(Define.Tab.Craft,Managers.UI.ShowPopupUI<UI_CraftTablePopup>());
            _tableBases.Add(Define.Tab.Shop,Managers.UI.ShowPopupUI<UI_ShopTablePopup>());
            
            foreach (UI_TableBase table in _tableBases.Values)
                table.Initialize();
            
            
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
                table.gameObject.SetActive(false);
            
            _enemyTabButton.image.color = Define.DefaultTabColor;
            _upgradeTabButton.image.color = Define.DefaultTabColor;
            _craftTabButton.image.color = Define.DefaultTabColor;
            _shopTabButton.image.color = Define.DefaultTabColor;
            
            Transform parent = null;
            
            switch (Tab)
            {
                case Define.Tab.Boss:
                    parent = _enemyTabButton.transform;
                    _enemyTabButton.image.color = Define.ActiveTabColor;
                    _tableText.text = Managers.Data.GetText((int) Define.UITextID.Boss);
                    break;
                case Define.Tab.Upgrade:
                    parent = _upgradeTabButton.transform;
                    _upgradeTabButton.image.color = Define.ActiveTabColor;
                    _tableText.text = Managers.Data.GetText((int) Define.UITextID.Upgrade);
                    break;
                case Define.Tab.Craft:
                    parent = _craftTabButton.transform;
                    _craftTabButton.image.color = Define.ActiveTabColor;
                    _tableText.text = Managers.Data.GetText((int) Define.UITextID.Craft);
                    break;
                case Define.Tab.Shop:
                    parent = _shopTabButton.transform;
                    _shopTabButton.image.color = Define.ActiveTabColor;
                    _tableText.text = Managers.Data.GetText((int) Define.UITextID.Shop);
                    break;
            }
            
            _tableBases[Tab].gameObject.SetActive(true);
            
            _focus.SetParent(parent);
            _focus.anchoredPosition = Vector2.zero;
        }


        /*private void TabInit()
        {
            _focus = Get<GameObject>((int) GameObjects.TabFocus).transform as RectTransform;

            _tableText = GetText((int) Texts.TableText);

            enemyContent = Get<Transform>((int) Transforms.EnemyContent);
            upgradeContent = Get<Transform>((int) Transforms.UpgradeContent);
            craftContent =  Get<Transform>((int) Transforms.CraftContent);
            shopContent = Get<Transform>((int) Transforms.ShopContent);

            foreach (Define.Tab tabType in Enum.GetValues(typeof(Define.Tab)))
            {
                tabs.Add(tabType, new Tab());
            
                Tab tab = tabs[tabType];
                switch (tabType)
                {
                    case Define.Tab.Boss:
                        tab.Scroll = Get<GameObject>((int) GameObjects.EnemyList);
                        tab.ButtonImage = GetImage((int) Images.EnemyTabButton);
                        tab.Content = enemyContent;
                        break;
                    case Define.Tab.Upgrade:
                        tab.Scroll = Get<GameObject>((int) GameObjects.UpgradeList);
                        tab.ButtonImage = GetImage((int) Images.UpgradeTabButton);
                        tab.Content = upgradeContent;
                        break;
                    case Define.Tab.Craft:
                        tab.Scroll = Get<GameObject>((int) GameObjects.CraftList);
                        tab.ButtonImage = GetImage((int) Images.CraftTabButton);
                        tab.Content = craftContent;
                        break;
                    case Define.Tab.Shop:
                        tab.Scroll = Get<GameObject>((int) GameObjects.ShopList);
                        tab.ButtonImage = GetImage((int) Images.ShopTabButton);
                        tab.Content = shopContent;
                        break;
                }
                tabs[tabType].ButtonImage.gameObject.BindEvent(delegate { SelectTab(tabType); });
            }

            equipContent = Get<Transform>((int) Transforms.Equip);
        }*/
    }
}