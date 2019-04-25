using System;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundPanel : UiPanel
{
    public Image BackgroundSprite;

    public void SetImage(Sprite image)
    {
        BackgroundSprite.sprite = image;
    }

    public override void OnStart(Action callback)
    {
        gameObject.SetActive(true);
        callback?.Invoke();
    }
}

