using System;
using UnityEngine;
using UnityEngine.UI;

public class BackToMainMenuPanel : UiPanel
{

    public Button ExitButton;
    private GuiSound guiSound;

    public override void OnStart(Action callback)
    {
        gameObject.SetActive(true);
        guiSound = new GuiSound();

        ExitButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySound(guiSound);
            GameStatesManager.EnableState(DI.Resolve<MainMenuState>());
        });

        callback?.Invoke();
    }

    public override void OnExit(Action callback)
    {
        ExitButton.onClick.RemoveAllListeners();
        callback?.Invoke();
    }

}

