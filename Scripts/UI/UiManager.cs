using UnityEngine;

public static class UiManager
{
    [HideInInspector]
    public static string uiPanelsPath = "Prefabs/UI/";

    public static T EnablePanel<T>() where T : Component
    {
        GameObject prefab = Resources.Load(uiPanelsPath + typeof(T).ToString()) as GameObject;
        GameObject panelGameObject = PoolManager.SpawnObject(prefab, PoolManager.Type.UI);
        UiPanel uiPanel = panelGameObject.GetComponent<UiPanel>();
        uiPanel.State = UiPanel.state.Start;
        uiPanel.OnStart(delegate {
            uiPanel.State = UiPanel.state.Idle;
            uiPanel.OnIdle(null);
        });
        return panelGameObject.GetComponent<T>();
    }

    public static void DisablePanel(UiPanel panel)
    {
        PoolManager.ReleaseObject(panel.gameObject);
        panel.State = UiPanel.state.Exit;
        panel.OnExit(delegate {
            panel.State = UiPanel.state.Hide;
        });
    }

}

