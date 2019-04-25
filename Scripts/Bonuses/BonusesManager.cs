using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tools;

public class BonusesManager : MonoBehaviourSingleton<BonusesManager>
{
    public List<Bonus> Bonuses = new List<Bonus>();
    private SpawnBonus spawnBonusSound;

    private void Awake()
    {
        spawnBonusSound = new SpawnBonus();
    }

    public void DropRandomBonus(Vector3 dropPosition)
    {
        int randomIndex = UnityEngine.Random.Range(0, Bonuses.Count);
        PoolManager.SpawnObject(Bonuses[randomIndex].gameObject, dropPosition, Quaternion.identity);
        AudioManager.Instance.PlaySound(spawnBonusSound);
    }
}
