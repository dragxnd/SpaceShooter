using System;
using UnityEngine;

public class Player
{
    private PlayerShip playerShip;
    private GameObject playerShipGameObject;

    private PlayerHudPanel playerHudPanel;

    private Vector3 playerStartPosition = new Vector3(0, -6.33f, 0);

    private bool enable;
    public bool Enable
    {
        get
        {
            return enable;
        }
        set
        {
            playerHudPanel?.gameObject.SetActive(value);
            enable = value;
            playerShip.Enable = value;
        }
    }


    public void Init(PlayerShip PlayerShip, Action OnDieEvent)
    {
        playerShipGameObject = PoolManager.SpawnObject(PlayerShip.gameObject, PoolManager.Type.Root);
        playerShipGameObject.transform.position = playerStartPosition;
        playerShipGameObject.tag = "Player";

        playerHudPanel = UiManager.EnablePanel<PlayerHudPanel>();
        playerHudPanel.gameObject.SetActive(false);

        this.playerShip = playerShipGameObject.GetComponent<PlayerShip>();
        this.playerShip.ShowHealthBar = false;
        this.playerShip.CurrentWeapon = new StandartPlayerWeapon();

        this.playerShip.OnDieEvent = OnDieEvent;
        this.playerShip.OnHealthUpdate = playerHudPanel.UpdateHealth;
        this.playerShip.CurrentWeapon.Init(PlayerShip.shootPoins);
        this.playerShip.Init();
    }

    public void RemovePlayer()
    {
        if (playerHudPanel != null) UiManager.DisablePanel(playerHudPanel);
        PoolManager.ReleaseObject(playerShipGameObject);
    }
}

