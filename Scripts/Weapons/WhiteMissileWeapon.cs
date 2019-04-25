using UnityEngine;
using System.Collections;

public class WhiteMissileWeapon : Weapon
{
    public override void Init(GameObject[] pos)
    {
        path = "Prefabs/Weapons/Missile 2";
        launchSound = new WhiteMissleSound();
        Damage = 5;
        bulletSpeed = 750;
        OwnerType = Ship.Type.Player;

        bulletPrefab = Resources.Load(path) as GameObject;
        this.positions = pos;
    }
}
