using System;
using UnityEngine;

public class MainMenuState : State
{

    private readonly string mainMenuBackgroundPath = "Textures/Backgrounds/6";
    private BackgroundPanel mainMenuBackgroundPanel;
    private UiPanel mainMenuSelectShipPanel;
    private MainMenuMusic mainMenuMusic;


    public override void OnStart(Action callback)
    {
        mainMenuBackgroundPanel = UiManager.EnablePanel<BackgroundPanel>();
        mainMenuBackgroundPanel.SetImage(Resources.Load<Sprite>(mainMenuBackgroundPath));
        mainMenuSelectShipPanel = UiManager.EnablePanel<MainMenuPanel>();

        mainMenuMusic = new MainMenuMusic();
        AudioManager.Instance.PlayMusic(mainMenuMusic);

        callback?.Invoke();
    }


    public override void OnExit(Action callback)
    {
        AudioManager.Instance.StopMusic(mainMenuMusic);
        UiManager.DisablePanel(mainMenuSelectShipPanel);
        UiManager.DisablePanel(mainMenuBackgroundPanel);

        callback?.Invoke();
    }

}
