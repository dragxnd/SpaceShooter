using UnityEngine;
using ScriptableObjects;
using System;

public class Ship : MonoBehaviour
{
    public string Name;

    public string BaseStatName;
    public BaseStat BaseStat;

    public string StatMultiplierName;
    public StatMultiplier statMultiplier;

    public float FirstShootPause { get; set; }

    public Action OnDieEvent { get; set; }
    public Action<int> OnHealthUpdate { get; set; }

    public bool ShowHealthBar { get; set; }
    public Weapon CurrentWeapon;
    public GameObject[] shootPoins;

    public float nextFireTime { get; set; }

    public float CurrentShootingSpeed;
    public float CurrentMoveingSpeed;
    public float CurrentMobility;

    public Rigidbody2D rigidbody;

    private DestroyShipSound destroyShipSound;
    private Type OwnerType;

    private string healthBarPath = "Prefabs/UI/HealthBar";
    private GameObject healthBarPrefab;
    private GameObject healthBarGameObject;
    private HealthBar healthBar;

    private string destroyExplosionPath = "Effects/Prefabs/DestroyExplosion";
    private GameObject destroyExplosionGameObject;

    private bool isInit = false;

    private bool enable = false;
    public bool Enable
    {
        get
        {
            return enable;
        }
        set
        {
            if (value) nextFireTime = Time.time + FirstShootPause;
            enable = value;
        }
    }

    private int currentHealth;
    public int CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        set
        {         
            currentHealth = value;
            if (currentHealth < 0) currentHealth = 0;
            OnHealthUpdate?.Invoke(currentHealth);
            healthBar?.UpdateBar(currentHealth);
            if (currentHealth == 0)
            {
                OnDieEvent?.Invoke();
                Destroy();
            }
        }
    }

    public enum Type
    {
        Player,
        Enemy
    }

    public void Init()
    {
        destroyShipSound = new DestroyShipSound();

        rigidbody = GetComponent<Rigidbody2D>();

        BaseStat = Data.BaseStatsData.baseStats.Find(x => x.name == BaseStatName);
        statMultiplier = Data.StatMultiplierData.statMultipliers.Find(x => x.name == StatMultiplierName);

        // Установка всех current полей
        CurrentShootingSpeed = BaseStat.BaseShootingSpeed * statMultiplier.ShootingSpeedMultiplier;
        CurrentMoveingSpeed = BaseStat.BaseMovingSpeed * statMultiplier.MovingSpeedMultiplier;
        CurrentMobility = BaseStat.BaseMobility * statMultiplier.MobilityMultiplier;
        CurrentHealth = BaseStat.BaseHealth * statMultiplier.HealthMultiplier;

        destroyExplosionGameObject = Resources.Load(destroyExplosionPath) as GameObject;

        if (ShowHealthBar)
        {
            healthBarPrefab = Resources.Load(healthBarPath) as GameObject;
            healthBarGameObject = PoolManager.SpawnObject(healthBarPrefab, PoolManager.Type.UI);
            healthBar = healthBarGameObject.GetComponent<HealthBar>();
            healthBar.objectTransform = transform;
            healthBar.StartHealth = CurrentHealth;
            healthBar.UpdateBar(CurrentHealth);
            healthBar.Init();         
        }

        CurrentWeapon.Init(shootPoins);
        isInit = true;
    }

    public void ApplyDamage(int damage,Vector3 damagePosition)
    {
        float percent = 100 - CurrentMobility;
        float chance = UnityEngine.Random.Range(0, 101);
        if (chance < percent)
        {
            CurrentHealth -= damage;
        }
    }

    public void Destroy()
    {      
        Enable = false;
        AudioManager.Instance.PlaySound(destroyShipSound);
        PoolManager.SpawnObject(destroyExplosionGameObject, transform.position, transform.rotation,2f);
        if (gameObject != null) PoolManager.ReleaseObject(gameObject);
        healthBar?.Remove();
    }

    public void OnDisable()
    {
        Enable = false;
        if (gameObject != null) PoolManager.ReleaseObject(gameObject);
        healthBar?.Remove();
    }

}