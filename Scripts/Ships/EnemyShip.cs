using UnityEngine;

public class EnemyShip : Ship
{
    public EnemyShip()
    {
        FirstShootPause = 3f;
    }

    public void Update()
    {
        if (!Enable) return;
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + CurrentShootingSpeed;
            this.CurrentWeapon.Shoot(statMultiplier.WeaponDamageMultiplier);
        }
    }
}

