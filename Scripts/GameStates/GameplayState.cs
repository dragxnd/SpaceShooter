using System;
using UnityEngine;

public class GameplayState : State
{
    public Player Player { get; set; }
    public EnemyManager EnemyManager { get; set; }
    public GameObject BackgoundPrefab { get; set; }
    public AudioClip LevelMusic { get; set; }

    private Sound themeMusic;
    private UiPanel backToMainMenuPanel;
    private LosePanel losePanel;
    private WinPanel winPanel;
    private GameObject spawnedBackground;

    public override void OnStart(Action callback)
    {
        themeMusic = new Sound(LevelMusic, true);
        AudioManager.Instance.PlayMusic(themeMusic);
        spawnedBackground = PoolManager.SpawnObject(BackgoundPrefab, BackgoundPrefab.transform.position, BackgoundPrefab.transform.rotation);
        backToMainMenuPanel = UiManager.EnablePanel<BackToMainMenuPanel>();
        
        Player.Enable = true;       
        EnemyManager.Enable = true;

        callback?.Invoke();
    }

    public override void OnExit(Action callback)
    {
        PoolManager.ReleaseObject(DI.Resolve<Stars>().gameObject);

        AudioManager.Instance.StopMusic(themeMusic);
        UiManager.DisablePanel(backToMainMenuPanel);
        if (losePanel != null) UiManager.DisablePanel(losePanel);
        if (winPanel != null) UiManager.DisablePanel(winPanel);
     
        Player.Enable = false;
        Player.RemovePlayer();
        Player = null;

        EnemyManager.Enable = false;
        EnemyManager.RemoveAll();
        EnemyManager = null;

        PoolManager.ReleaseObject(spawnedBackground);

        callback?.Invoke();
    }

    // Игрок успешно завершил уровень
    public void Win()
    {
        Player.Enable = false;
        EnemyManager.Enable = false;
        winPanel = UiManager.EnablePanel<WinPanel>();
    }

    // Игрока уничтожили
    public void Lose()
    {
        Player.Enable = false;
        EnemyManager.Enable = false;
        losePanel = UiManager.EnablePanel<LosePanel>();
    }

}
