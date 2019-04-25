using ScriptableObjects;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : UiPanel
{

    public Text ShipName;
    public Button LeftButton;
    public Button RightButton;
    public Button StartButton;

    private int selectedIndex = 0;
    private PlayerShip selectedShip;
    private GuiSound guiSound;
    private PlayerShip[] playerShips;

    public PlayerShip SelectedShip
    {
        get
        {
            return selectedShip;
        }
        set
        {
            selectedShip = value;
            ShipName.text = selectedShip.Name;
        }
    }



    public override void OnStart(Action callback)
    {
        guiSound = new GuiSound();
        playerShips = Resources.LoadAll<PlayerShip>("");
        if(selectedIndex==0) SelectedShip = playerShips[selectedIndex];


        LeftButton.onClick.AddListener(() => {
            AudioManager.Instance.PlaySound(guiSound);
            if (selectedIndex == 0)
            {
                selectedIndex = playerShips.Length - 1;
            }
            else
            {
                selectedIndex--;
            }
            SelectedShip = playerShips[selectedIndex];
        });


        RightButton.onClick.AddListener(() => {
            AudioManager.Instance.PlaySound(guiSound);
            if (selectedIndex == playerShips.Length-1)
            {
                selectedIndex = 0;
            }
            else
            {
                selectedIndex++;
            }
            SelectedShip = playerShips[selectedIndex];
        });


        StartButton.onClick.AddListener(() => {
            AudioManager.Instance.PlaySound(guiSound);
            DI.Resolve<LoadLevelState>().SetLevel(0);
            DI.Resolve<LoadLevelState>().SetPlayerShip(selectedShip);
            GameStatesManager.EnableState(DI.Resolve<LoadLevelState>());
        });


        gameObject.SetActive(true);
        callback?.Invoke();
    }

    public override void OnExit(Action callback)
    {
        LeftButton.onClick.RemoveAllListeners();
        RightButton.onClick.RemoveAllListeners();
        StartButton.onClick.RemoveAllListeners();
        callback?.Invoke();
    }
}

