using UnityEngine;

public class Game : MonoBehaviour
{
    [Header("Pool parents")]
    public Transform UiPoolParent;

    // Игровые состояния
    private MainMenuState MainMenuState = new MainMenuState();
    private GameplayState GameplayState = new GameplayState();
    private LoadLevelState LoadLevelState = new LoadLevelState();

    void Start()
    {
        // Настройка пула
        PoolManager.Parents.Add(PoolManager.Type.UI, UiPoolParent);

        GameStatesManager.EnableState(MainMenuState);
    }

    public void Update()
    {
        if (Input.GetKey(InputKeyCodes.Exit))
        {
            Application.Quit();
        }
    }
}
