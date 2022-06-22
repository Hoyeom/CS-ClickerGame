using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public const int MaxInventorySlot = 20;
    public const float AttackSpeed = .4f;
    public const float AttackDelay = .2f;
    
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
         Boss = 0,
         Upgrade = 1,
         Craft = 2,
         Shop = 3,
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
         Enemy = 2,
         _ = 3,
         Upgrade = 4,
         Item = 5,
         Shop = 6,
         StartStatus = 7,
         String = 8,
         Path = 9
     }
     
     public enum EnemyType
     {
         Unknown,
         Boss
     }
     
     public enum Language
     {
         Eng = 0,
         Kor = 1
     }

     public enum UpgradeType
     {
         Attack,
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
         Language = 810008,
         YouName = 810009,
         Select = 810010,
         UserName = 810011,
         Fight = 810012,
         ExitGame = 810013, 
         Really = 810014,
         Exit = 810015,

     }
     
     public enum ItemType
     {
         None,
         Weapon,
     }

     public static readonly Color ActiveTabColor = new Color(0.3098039f, 0.454902f, 0.9647059f);
     public static readonly Color DefaultTabColor = new Color(0.7686275f, 0.7921569f, 0.8196079f);
}
