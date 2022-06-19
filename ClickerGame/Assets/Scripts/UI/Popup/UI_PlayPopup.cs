using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data;
using DG.Tweening;
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
        Background,
        EnemyTabButton,
        UpgradeTabButton,
        CraftTabButton,
        ShopTabButton,
        CraftButton,
    }

    enum Texts
    {
        TableText,
        CoinText,
        InfoLevelText,
        SliderHpText,
        UserName
    }

    enum Transforms
    {
        EnemyContent,
        UpgradeContent,
        CraftContent,
        ShopContent,
        PlayerArea,
        EnemyArea,
        Equip,
    }

    enum Sliders
    {
        SliderExp,
        SliderHp,
        ChargeSlider
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
    
    public Slider expSlider;
    public Slider hpSlider;
    public Slider chargeSlider;

    private RectTransform _focus;
    private TextMeshProUGUI _tableText;
    private TextMeshProUGUI _coinText;
    private TextMeshProUGUI _levelText;
    private TextMeshProUGUI _hpText;
    private TextMeshProUGUI _userNameText;

    private Image _craftButton;
    private Image _background;

    private bool availableCraft = false;
    private Sequence chargeSequence;

    private Dictionary<Define.Tab, Tab> tabs = new Dictionary<Define.Tab, Tab>();

    private List<SubItem_Boss> _bosses = new List<SubItem_Boss>();
    private List<SubItem_Craft> _craft = new List<SubItem_Craft>();
    private List<SubItem_Shop> _shops = new List<SubItem_Shop>();
    private List<SubItem_Upgrade> _upgrades = new List<SubItem_Upgrade>();

    private Transform equipContent;
    
    public override bool Initialize()
    {
        if (base.Initialize() == false)
            return false;

        Managers.Sound.Play(Define.Sound.Bgm, "InGameBgm");
        
        _bosses = new List<SubItem_Boss>();
        _craft = new List<SubItem_Craft>();
        _shops = new List<SubItem_Shop>();
        _upgrades = new List<SubItem_Upgrade>();
        
        Bind<GameObject>(typeof(GameObjects));
        BindImage(typeof(Images));
        BindText(typeof(Texts));
        Bind<Slider>(typeof(Sliders));
        Bind<Transform>(typeof(Transforms));

        _background = GetImage((int) Images.Background);
        _coinText = GetText((int) Texts.CoinText);
        _levelText = GetText((int) Texts.InfoLevelText);
        _hpText = GetText((int) Texts.SliderHpText);
        _userNameText = GetText((int) Texts.UserName);
        _coinText.text = 0.ToString();

        Managers.Game.Player.OnChangeName += OnChangeName;
        Managers.Game.Player.OnChangePlayerLevel += OnChangeLevel;
        Managers.Game.Player.OnChangeCoin += OnChangeCoin;
        _background.gameObject.BindEvent((data) => Managers.Game.Player.TabToAddCoin());
        
        _craftButton = GetImage((int) Images.CraftButton);

        expSlider = Get<Slider>((int) Sliders.SliderExp);
        hpSlider = Get<Slider>((int) Sliders.SliderHp);
        chargeSlider = Get<Slider>((int) Sliders.ChargeSlider);

        chargeSlider.value = 0;
        
        chargeSequence = DOTween.Sequence()
            .InsertCallback(0, () => chargeSlider.value = 0)
            .Insert(0, chargeSlider.DOValue(1, Managers.Game.Player.ChargeSpeed)
                .OnComplete(() => availableCraft = true))
            .SetAutoKill(false);

        Managers.Game.Player.OnChangeExp += SetExpSlider;
        Managers.Game.Player.OnChangeHealth += SetHealthSlider;
        
        _craftButton.gameObject.BindEvent((data) => CraftItem());

        Managers.Game.SetPlayerArea(Get<Transform>((int) Transforms.PlayerArea));
        Managers.Game.SetEnemyArea(Get<Transform>((int) Transforms.EnemyArea));

        Managers.Game.Player.SetView(Managers.UI.MakeSubItem<SubItem_Player>(Managers.Game.PlayerSpawnArea));
        
        TabInit();
        RemoveAllTabContent();
        AddTabContents();
        SelectTab();

        StopAllCoroutines();
        StartCoroutine(CoAutoSave());
        
        return true;
    }

    private void OnChangeName(string name)
    {
        _userNameText.text = name;
    }
    private void OnChangeLevel(int cur)
    {
        _levelText.text = cur.ToString();
    }
    private void OnChangeCoin(int value)
    {
        _coinText.text = value.ToString();
    }
    private void CraftItem()
    {
        if(!availableCraft) return;

        availableCraft = false;
        
        chargeSequence.Restart();
        
        ItemData item = Managers.Data.
            Item.
            Values.
            FirstOrDefault(data => data.Level == Managers.Game.Player.CraftLevel);
        
        if (item != null)
        {
            bool temp = Managers.Game.Player.Inventory.AddItem(item);
            
            if(temp)
            {
                Managers.Sound.Play(Define.Sound.Effect, "Craft");
                Debug.Log("CraftComplete");
                return;
            }
        }

        Debug.Log("CraftFailed");
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
                foreach (EnemyData data in Managers.Data.Boss)
                {
                    SubItem_Boss subItem = 
                        Managers.UI.MakeSubItem<SubItem_Boss>(root);
                    _bosses.Add(subItem);
                    
                    

                    subItem.SetInfo(data);
                }
                break;
            case Define.Tab.Craft:
                _craft.Clear();
                
                Managers.Game.Player.Inventory.OnChangeItem -= RefreshInventory;
                Managers.Game.Player.Inventory.OnChangeItem += RefreshInventory;
                
                ItemData item = Managers.Data.Item.First().Value;
                
                for (int i = 0; i < Define.MaxInventorySlot; i++)
                {
                    SubItem_Craft subItem = Managers.UI.MakeSubItem<SubItem_Craft>(root);
                    subItem.SlotIndex = i;
                    
                    _craft.Add(subItem);
                    
                    Managers.Game.Player.Inventory.InitAddList(item);
                }

                SubItem_Craft craft = Managers.UI.MakeSubItem<SubItem_Craft>(equipContent);
                Managers.Game.Player.Inventory.OnChangeEquip += craft.SetInfo;
                
                break;
            case Define.Tab.Shop:
                // _shops.Clear();
                // foreach (ShopData data in Managers.Data.Shop.Values)
                // {
                //     SubItem_Shop subItem = 
                //         Managers.UI.MakeSubItem<SubItem_Shop>(root);
                //     _shops.Add(subItem);
                //     subItem.SetInfo(data);
                // }
                break;
            case Define.Tab.Upgrade:
                _upgrades.Clear();

                foreach (UpgradeData data in Managers.Data.Upgrade.Values.Where((data => data.Level == 0)))
                {
                    SubItem_Upgrade subItem = 
                        Managers.UI.MakeSubItem<SubItem_Upgrade>(root);
                    _upgrades.Add(subItem);
                    subItem.SetInfo(data);
                }
                break;
        }
        
        Managers.UI.RefreshUI();
        Managers.Game.Player.RefreshUIData(); ;
    }
    private void RefreshInventory(int index,ItemData item)
    {
        _craft[index].SetInfo(item);
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

            equipContent = Get<Transform>((int) Transforms.Equip);
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
            _ => throw new ArgumentOutOfRangeException(nameof(tab), tab, null)
        };
        tabs[tab].SetActive(true, _focus);
    }
    private void SetExpSlider(int cur,int max)
    {
        expSlider.value = (float) cur / max;
    }
    private void SetHealthSlider(int cur,int max)
    {
        hpSlider.value = (float) cur / max;
        _hpText.text = $"{cur.ToString()}/{max.ToString()}";
    }
    
    IEnumerator CoAutoSave()
    {
        while (true)
        {
            Managers.Game.SaveGame();
            yield return new WaitForSeconds(3);
        }
    }
}
