using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using TMPro;
using UI.Popup;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayPopup : UI_Popup
{
    enum GameObjects
    {
        EnemyList,
        UpgradeList,
        CraftList,
        ShopList,
        TabFocus
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
        TableText
    }

    enum Transforms
    {
        EnemyContent,
        UpgradeContent,
        CraftContent,
        ShopContent,
    }

    private class Tab
    {
        public Transform Content;
        public GameObject Scroll;
        public Image ButtonImage;
        private Button button;

        public Button Button
        {
            get
            {
                button ??= ButtonImage.GetComponent<Button>();
                return button;
            }
        }

        public void SetActive(bool value,RectTransform focus = default)
        {
            Scroll.SetActive(value);
            
            if (value)
            {
                focus.SetParent(ButtonImage.transform);
                focus.anchoredPosition = Vector3.zero;
                ButtonImage.color = Define.ActiveTabColor;
            }
            else
            {
                ButtonImage.color = Define.DefaultTabColor;
            }

        }
    }


    private RectTransform _focus;
    private TextMeshProUGUI _tableText;

    private Dictionary<Define.Tab, Tab> tabs = new Dictionary<Define.Tab, Tab>();

    protected override void Initialize()
    {
        base.Initialize();

        Bind<GameObject>(typeof(GameObjects));
        BindImage(typeof(Images));
        BindText(typeof(Texts));
        Bind<Transform>(typeof(Transforms));
        
        TabInit();
        
        RemoveAllTabContent();
        
        SelectTab();
    }

    private void TabInit()
    {
        _focus = Get<GameObject>((int) GameObjects.TabFocus).transform as RectTransform;
        
        _tableText = GetText((int) Texts.TableText);

        foreach (Define.Tab tabType in Enum.GetValues(typeof(Define.Tab)))
        {
            tabs.Add(tabType, new Tab());

            Tab tab = tabs[tabType];
            switch (tabType)
            {
                case Define.Tab.Enemy:
                    tab.Scroll = Get<GameObject>((int) GameObjects.EnemyList);
                    tab.ButtonImage = GetImage((int) Images.EnemyTabButton);
                    tab.Content = Get<Transform>((int) Transforms.EnemyContent);
                    break;
                case Define.Tab.Upgrade:
                    tab.Scroll = Get<GameObject>((int) GameObjects.UpgradeList);
                    tab.ButtonImage = GetImage((int) Images.UpgradeTabButton);
                    tab.Content = Get<Transform>((int) Transforms.UpgradeContent);
                    break;
                case Define.Tab.Craft:
                    tab.Scroll = Get<GameObject>((int) GameObjects.CraftList);
                    tab.ButtonImage = GetImage((int) Images.CraftTabButton);
                    tab.Content = Get<Transform>((int) Transforms.CraftContent);
                    break;
                case Define.Tab.Shop:
                    tab.Scroll = Get<GameObject>((int) GameObjects.ShopList);
                    tab.ButtonImage = GetImage((int) Images.ShopTabButton);
                    tab.Content = Get<Transform>((int) Transforms.ShopContent);
                    break;
            }
            
            tabs[tabType].Button.onClick.AddListener((delegate { SelectTab(tabType); }));
        }
    }

    private void RemoveAllTabContent()
    {
        foreach (Tab tab in tabs.Values)
        {
            for (int i = 0; i < tab.Content.childCount; i++)
                Managers.Resource.Destroy(tab.Content.GetChild(i).gameObject);   
        }
    }
    
    private void SelectTab(Define.Tab tab = Define.Tab.Enemy)
    {
        foreach (Tab t in tabs.Values)
            t.SetActive(false);
        _tableText.text = tab.ToString();
        tabs[tab].SetActive(true, _focus);
    }
}
