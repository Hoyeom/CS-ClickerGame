using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
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
         Shop
     }
     
     public enum AssetType
     {
         Sprite
     }
     
     public enum CostType
     {
         Cash,
         Coin
     }
     
     public enum Table
     {
         None = 0,
         Status = 1,
         Boss = 2,
         Monster = 3,
         Upgrade = 4,
         Weapon = 5,
         Shop = 6,
         Null = 7,
         String = 8,
         Path = 9
     }
     
     public enum Language
     {
         Eng,
         Kor
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
