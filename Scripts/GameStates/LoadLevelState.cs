using System;
using System.Collections;
using UnityEngine;

public class LoadLevelState : State
{
    private int loadingLevelIndex;
    private PlayerShip selectedShip;
    private LoadedLevelPanel loadingLevelPanel;
    private readonly string backgroundPanelPath = "Textures/Backgrounds/6";
    private BackgroundPanel backgroundPanel;
    private readonly string starsSkyPath = "Prefabs/Backgrounds/Stars";
    private GameplayState gameplayState;

    public void NextLevel()
    {
        loadingLevelIndex++;
        if (loadingLevelIndex == LevelsManager.Instance.Levels.Length) loadingLevelIndex = 0;
    }

    // Установка индекса уровня
    public void SetLevel(int levelIndex)
    {
        loadingLevelIndex = levelIndex;
    }

    // Установка текущего корабля игрока
    public void SetPlayerShip(PlayerShip playerShip)
    {
        selectedShip = playerShip;
    }

    // Ожидание нажатия клавиши SPACE для входа в игру
    private IEnumerator StartGame()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        GameStatesManager.EnableState(DI.Resolve<GameplayState>());
    }

    // Загрузка уровня
    public override void OnStart(Action callback)
    {
        PoolManager.SpawnObject(Resources.Load(starsSkyPath) as GameObject);

        loadingLevelPanel = UiManager.EnablePanel<LoadedLevelPanel>();
        loadingLevelPanel.StatusText.text = StringKeys.Loading;

        gameplayState = DI.Resolve<GameplayState>();

        gameplayState.LevelMusic = LevelsManager.Instance.Levels[loadingLevelIndex].music;

        gameplayState.Player = new Player();
        gameplayState.Player.Init(selectedShip, gameplayState.Lose);

        gameplayState.EnemyManager = new EnemyManager();
        gameplayState.EnemyManager.Init(LevelsManager.Instance.Levels[loadingLevelIndex].LevelData.levelObjects.ToArray());

        gameplayState.BackgoundPrefab = LevelsManager.Instance.Levels[loadingLevelIndex].Background;

        backgroundPanel = UiManager.EnablePanel<BackgroundPanel>();
        backgroundPanel.SetImage(Resources.Load<Sprite>(backgroundPanelPath));

        loadingLevelPanel.StatusText.text = StringKeys.PressToStart;
        MonoBehaviourTools.Instance.StartCor(StartGame());
        callback?.Invoke();
    }

    public override void OnExit(Action callback)
    {
        UiManager.DisablePanel(loadingLevelPanel);
        UiManager.DisablePanel(backgroundPanel);
        callback?.Invoke();
    }


}
