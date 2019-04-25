using System;
using UnityEngine.UI;

public class LosePanel : UiPanel
{

    public Button BackToMainMenuButton;
    private GuiSound guiSound;

    public override void OnStart(Action callback)
    {
        gameObject.SetActive(true);
        guiSound = new GuiSound();

        BackToMainMenuButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySound(guiSound);
            GameStatesManager.EnableState(DI.Resolve<MainMenuState>());
        });

        callback?.Invoke();
    }


    public override void OnExit(Action callback)
    {
        BackToMainMenuButton.onClick.RemoveAllListeners();
        callback?.Invoke();
    }

}

