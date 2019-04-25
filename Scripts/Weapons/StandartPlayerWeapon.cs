using UnityEngine;
using System.Collections;

public class StandartPlayerWeapon : Weapon
{
    public override void Init(GameObject[] pos)
    {
        path = "Prefabs/Weapons/Missile 1";
        launchSound = new RocketLaunch1Sound();
        Damage = 3;
        bulletSpeed = 550;
        OwnerType = Ship.Type.Player;

        bulletPrefab = Resources.Load(path) as GameObject;
        this.positions = pos;
    }
}
