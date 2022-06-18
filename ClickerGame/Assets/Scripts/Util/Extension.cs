using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class Extension
{
    public static T GetOrAddComponent<T>(this GameObject go) where T : Component 
        => Utils.GetOrAddComponent<T>(go);

    public static GameObject BindEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
        => UI_Base.BindEvent(go, action, type);

    public static void SetActive(this Image image, bool value)
        => image.gameObject.SetActive(value);
    
    public static void SetActive(this TextMeshProUGUI text, bool value)
        => text.gameObject.SetActive(value);
    
    public static GameObject SetActiveChain(this GameObject go, bool value)
    {
        go.SetActive(value);
        return go;
    }
}
