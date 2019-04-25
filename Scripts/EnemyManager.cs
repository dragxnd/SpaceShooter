using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager
{

    private List<EnemyShip> enemyShips = new List<EnemyShip>();
    private List<Tweener> tweeners = new List<Tweener>();
    private float moveDuration = 5f;
    private float offset = 1f;

    private bool enable;
    public bool Enable
    {
        get
        {
            return enable;
        }
        set
        {
            enable = value;
            SetActiveEnemies(value);
            if (value) EnableMove();
        }
    }


    public void Init(LevelObject[] enemyObjects)
    {
        for (int i = 0; i < enemyObjects.Length; i++)
        {        
            GameObject spawnObject = PoolManager.SpawnObject(enemyObjects[i].prefab, enemyObjects[i].position, enemyObjects[i].rotation);
            spawnObject.transform.position = enemyObjects[i].position;
            spawnObject.transform.rotation = enemyObjects[i].rotation;

            spawnObject.tag = "Enemy";
            EnemyShip EnemyShip = spawnObject.GetComponent<EnemyShip>();
            EnemyShip.CurrentWeapon = new StandartEnemyWeapon();
            EnemyShip.ShowHealthBar = true;
            EnemyShip.OnDieEvent = delegate
            {
                BonusesManager.Instance.DropRandomBonus(EnemyShip.transform.position);
                Remove(EnemyShip);
            };
            EnemyShip.CurrentWeapon.Init(EnemyShip.shootPoins);

            EnemyShip.Init();
            this.enemyShips.Add(EnemyShip);
        }
    }

    private void EnableMove()
    {
        MoveLeft();
    }

    private GameObject GetMaxLeftElement(List<EnemyShip> elements)
    {
        GameObject gameObj = elements.Find(y => y.gameObject.transform.position.x == (elements.Min(x => x.gameObject.transform.position.x))).gameObject;
        return gameObj;
    }

    private GameObject GetMaxRightElement(List<EnemyShip> elements)
    {
        GameObject gameObj = elements.Find(y => y.gameObject.transform.position.x == (elements.Max(x => x.gameObject.transform.position.x))).gameObject;
        return gameObj;
    }

    private void MoveLeft()
    {
        KillAllTweens();
        float posx = Extensions.OrthographicBounds().min.x + offset - GetMaxLeftElement(enemyShips).transform.position.x;
        GameObject leftObj = GetMaxLeftElement(enemyShips);

        for (int i = 0; i < enemyShips.Count; i++)
        {
            Tweener tweener = enemyShips[i].transform.DOMove(
                new Vector3(enemyShips[i].transform.position.x + posx, enemyShips[i].transform.position.y, enemyShips[i].transform.position.z),
                moveDuration).SetEase(Ease.InOutSine);

            if (enemyShips[i].gameObject == leftObj) tweener.OnComplete(MoveRight);
            tweeners.Add(tweener);
        }
    }


    private void MoveRight()
    {
        KillAllTweens();
        float posx = GetMaxRightElement(enemyShips).transform.position.x - Extensions.OrthographicBounds().max.x + offset;
        GameObject leftObj = GetMaxRightElement(enemyShips);

        for (int i = 0; i < enemyShips.Count; i++)
        {
            Tweener tweener = enemyShips[i].transform.DOMove(
                new Vector3(enemyShips[i].transform.position.x - posx, enemyShips[i].transform.position.y, enemyShips[i].transform.position.z),
                moveDuration).SetEase(Ease.InOutSine);

            if (enemyShips[i].gameObject == leftObj) tweener.OnComplete(MoveLeft);
            tweeners.Add(tweener);
        }
    }

    public void RemoveAll()
    {
        KillAllTweens();
        Enable = false;
        for (int i = 0; i < enemyShips.Count; i++)
        {
            PoolManager.ReleaseObject(enemyShips[i].gameObject);
        }
        enemyShips.Clear();
    }

    private void Remove(EnemyShip enemyShip)
    {
        enemyShips.Remove(enemyShip);
        if (enemyShips.Count==0)
        {
            DI.Resolve<GameplayState>().Win();
        }
    }

    private void KillAllTweens()
    {
        for (int i = 0; i < tweeners.Count; i++)
        {
            tweeners[i].Kill();
        }
        tweeners.Clear();
    }

    private void SetActiveEnemies(bool active)
    {
        for (int i = 0; i < enemyShips.Count; i++)
        {
            enemyShips[i].Enable = active;
        }
    }
}

