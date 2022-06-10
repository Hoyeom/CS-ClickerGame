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
         Enemy,
         Upgrade,
         Craft,
         Shop
     }

     public static readonly Color ActiveTabColor = new Color(0.3098039f, 0.454902f, 0.9647059f);
     public static readonly Color DefaultTabColor = new Color(0.7686275f, 0.7921569f, 0.8196079f);
}
