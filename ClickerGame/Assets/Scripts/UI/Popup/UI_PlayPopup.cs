using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data;
using Manager;
using TMPro;
using UI;
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
        TableText,
        CoinText
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
    private TextMeshProUGUI _coinText;

    private Dictionary<Define.Tab, Tab> tabs = new Dictionary<Define.Tab, Tab>();

    private List<SubItem_Boss> _bosses = new List<SubItem_Boss>();
    private List<SubItem_Craft> _craft = new List<SubItem_Craft>();
    private List<SubItem_Shop> _shops = new List<SubItem_Shop>();
    private List<SubItem_Upgrade> _upgrades = new List<SubItem_Upgrade>();

    public override bool Initialize()
    {
        if (base.Initialize() == false)
            return false;
        
        Bind<GameObject>(typeof(GameObjects));
        BindImage(typeof(Images));
        BindText(typeof(Texts));
        Bind<Transform>(typeof(Transforms));

        _coinText = GetText((int) Texts.CoinText);
        _coinText.text = 0.ToString();
        // TODO
        
        TabInit();
        RemoveAllTabContent();
        AddTabContents();
        SelectTab();

        return true;
    }

    private void AddTabContents()
    {
        MakeSubItem(Define.Tab.Boss);
        MakeSubItem(Define.Tab.Craft);
        MakeSubItem(Define.Tab.Upgrade);
        MakeSubItem(Define.Tab.Shop);
    }
    private void MakeSubItem(Define.Tab tab)
    {
        Transform root = tabs[tab].Content;;
        switch (tab)
        {
            case Define.Tab.Boss:
                _bosses.Clear();
                foreach (MonsterData data in Managers.Data.Monster.Values.Where(data => data.MonsterType == Define.MonsterType.Boss))
                {
                    SubItem_Boss subItem = 
                        Managers.UI.MakeSubItem<SubItem_Boss>(root);
                    _bosses.Add(subItem);
                    subItem.SetInfo(data);
                }
                break;
            case Define.Tab.Craft:
                _craft.Clear();
                for (int i = 0; i < Define.MaxInventorySlot; i++)
                {
                    SubItem_Craft subItem = 
                        Managers.UI.MakeSubItem<SubItem_Craft>(root);
                    subItem.SetInfo(Managers.Data.Craft.First().Value);
                    _craft.Add(subItem);
                }
                break;
            case Define.Tab.Shop:
                _shops.Clear();
                foreach (ShopData data in Managers.Data.Shop.Values)
                {
                    SubItem_Shop subItem = 
                        Managers.UI.MakeSubItem<SubItem_Shop>(root);
                    _shops.Add(subItem);
                    subItem.SetInfo(data);
                }
                break;
            case Define.Tab.Upgrade:
                _upgrades.Clear();
                foreach (UpgradeData data in Managers.Data.Upgrade.Values)
                {
                    SubItem_Upgrade subItem = 
                        Managers.UI.MakeSubItem<SubItem_Upgrade>(root);
                    _upgrades.Add(subItem);
                    subItem.SetInfo(data);
                }
                break;


        }
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
                case Define.Tab.Boss:
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

            tabs[tabType].ButtonImage.gameObject.BindEvent(delegate { SelectTab(tabType); });
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
    private void SelectTab(Define.Tab tab = Define.Tab.Boss)
    {
        foreach (Tab t in tabs.Values)
            t.SetActive(false);
        _tableText.text = tab switch
        {
            Define.Tab.Boss => Managers.Data.GetText((int)Define.UITextID.Boss),
            Define.Tab.Upgrade => Managers.Data.GetText((int)Define.UITextID.Upgrade),
            Define.Tab.Craft => Managers.Data.GetText((int)Define.UITextID.Craft),
            Define.Tab.Shop => Managers.Data.GetText((int)Define.UITextID.Shop),
        };
        tabs[tab].SetActive(true, _focus);
    }
}
