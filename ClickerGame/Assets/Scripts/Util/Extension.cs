using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public static class Extension
{
    public static T GetOrAddComponent<T>(this GameObject go) where T : Component 
        => Utils.GetOrAddComponent<T>(go);

    public static GameObject BindEvent(this GameObject go, Action action, Define.UIEvent type = Define.UIEvent.Click)
        => UI_Base.BindEvent(go, action, type);

    public static GameObject SetActiveChain(this GameObject go, bool value)
    {
        go.SetActive(value);
        return go;
    }
}
