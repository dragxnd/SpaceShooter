using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHudPanel : UiPanel
{
    public Text Health;
    public void UpdateHealth(int newHp)
    {
        Health.text = newHp.ToString();
    }

    public override void OnStart(Action callback)
    {
        gameObject.SetActive(true);
        callback?.Invoke();
    }

}

