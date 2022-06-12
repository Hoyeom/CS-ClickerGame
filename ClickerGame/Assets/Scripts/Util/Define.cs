using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public const int MaxInventorySlot = 20;
    
    public enum Scene
    {
        Unknown,
        Game
    }

     public enum UIEvent
     {
         Click,
         Pressed,
         Down,
         Up
     }

     public enum Sound
     {
         Bgm,
         Effect,
         Speech
     }
     
     public enum Tab
     {
         Boss,
         Upgrade,
         Craft,
         Shop,
     }
     
     public enum AssetType
     {
         Sprite
     }
     
     public enum Cost
     {
         Cash,
         Coin,
         Ads
     }
     
     public enum Table
     {
         None = 0,
         Status = 1,
         Monster = 2,
         _ = 3,
         Upgrade = 4,
         Weapon = 5,
         Shop = 6,
         StartStatus = 7,
         String = 8,
         Path = 9
     }
     
     public enum MonsterType
     {
         Unknown,
         Boss
     }
     
     public enum Language
     {
         Eng,
         Kor
     }

     public enum UpgradeType
     {
         Weapon,
         Defence,
         Health
     }
     
     public enum UITextID
     {
         TouchToPlay = 810001,
         StartGame = 810002,
         Boss = 810003,
         Upgrade = 810004,
         Craft = 810005,
         Shop = 810006,
         AdRemove = 810007,
         AdRemovePrice = 810008
     }

     public static readonly Color ActiveTabColor = new Color(0.3098039f, 0.454902f, 0.9647059f);
     public static readonly Color DefaultTabColor = new Color(0.7686275f, 0.7921569f, 0.8196079f);
}
