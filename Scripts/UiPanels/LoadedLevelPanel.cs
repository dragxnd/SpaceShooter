using System;
using UnityEngine.UI;

public class LoadedLevelPanel : UiPanel
{

    public Text StatusText;

    public override void OnStart(Action callback)
    {
        gameObject.SetActive(true);
        callback?.Invoke();
    }

}

