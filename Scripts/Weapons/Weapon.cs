using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    public string path { get; set; }
    public int Damage { get; set; }

    public Ship.Type OwnerType { get; set; }
    public float bulletSpeed { get; set; }
    public Sound launchSound;

    public GameObject bulletPrefab { get; set; }
    public GameObject[] positions { get; set; }

    private Dictionary<Ship.Type, Vector3> directions = new Dictionary<Ship.Type, Vector3>()
    {
        { Ship.Type.Player, new Vector3(0,0,90) },
        { Ship.Type.Enemy, new Vector3(0,0,-90) }
    };

    public virtual void Init(GameObject[] pos = null) { }

    public virtual void Shoot(int damageMultiplier)
    {
        AudioManager.Instance.PlaySound(launchSound);
        foreach (var transf in positions)
        {
            GameObject gameObj = PoolManager.SpawnObject(bulletPrefab, transf.transform.position, Quaternion.Euler(directions[OwnerType]));
            Bullet bullet = gameObj.GetComponent<Bullet>();
            bullet.OwnerType = OwnerType;
            bullet.BulletSpeed = bulletSpeed;
            bullet.Damage = Damage * damageMultiplier;
            bullet.Init();
        }
    }

}
