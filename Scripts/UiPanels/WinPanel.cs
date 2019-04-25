using System;
using UnityEngine.UI;

public class WinPanel : UiPanel
{

    public Button NextLevelButton;
    private GuiSound guiSound;

    public override void OnStart(Action callback)
    {
        gameObject.SetActive(true);
        guiSound = new GuiSound();

        NextLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySound(guiSound);
            DI.Resolve<LoadLevelState>().NextLevel();
            GameStatesManager.EnableState(DI.Resolve<LoadLevelState>());
        });

        callback?.Invoke();
    }

    public override void OnExit(Action callback)
    {
        NextLevelButton.onClick.RemoveAllListeners();
        gameObject.SetActive(false);
        callback?.Invoke();
    }

}

