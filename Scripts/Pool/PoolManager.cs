using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class PoolManager : MonoBehaviour
{
    private bool logStatus = false;
    public Transform root { get; set; }

    private Dictionary<GameObject, ObjectPool<GameObject>> prefabLookup;
    private Dictionary<GameObject, ObjectPool<GameObject>> instanceLookup;

    public enum Type
    {
        Root,
        UI,
        Ship,
        Bullet
    }

    static Dictionary<Type, Transform> _parents = new Dictionary<Type, Transform>();
    public static Dictionary<Type, Transform> Parents
    {
        get
        {
            return _parents;
        }
        set
        {
            _parents = value;
        }
    }

    private bool dirty = false;

    void Awake()
    {
        prefabLookup = new Dictionary<GameObject, ObjectPool<GameObject>>();
        instanceLookup = new Dictionary<GameObject, ObjectPool<GameObject>>();
    }


    public void warmPool(GameObject prefab, int size)
    {
        if (prefabLookup.ContainsKey(prefab))
        {
            throw new Exception("Pool for prefab " + prefab.name + " has already been created");
        }
        var pool = new ObjectPool<GameObject>(() => { return InstantiatePrefab(prefab); }, size);
        prefabLookup[prefab] = pool;

        dirty = true;
    }

    public GameObject spawnObject(GameObject prefab, Type type = Type.Root)
    {
        return spawnObject(prefab, prefab.transform.position, prefab.transform.rotation, type);
    }

    public GameObject spawnObject(GameObject prefab, Vector3 position, Quaternion rotation, Type type = Type.Root)
    {
        if (!prefabLookup.ContainsKey(prefab))
        {
            WarmPool(prefab, 1);
        }

        var pool = prefabLookup[prefab];

        var clone = pool.GetItem();

        if (type != Type.UI)
        {
            clone.transform.position = position;
            clone.transform.rotation = rotation;
        }


        if (type != Type.Root)
        {
            if (type == Type.UI)
            {
                clone.transform.SetParent(Parents[type], false);
            }
            else
            {
                clone.transform.parent = Parents[type];
            }
        }
        clone.SetActive(true);

        instanceLookup.Add(clone, pool);
        dirty = true;
        return clone;
    }


    public void releaseObject(GameObject clone)
    {
        clone.SetActive(false);

        if (instanceLookup.ContainsKey(clone))
        {
            instanceLookup[clone].ReleaseItem(clone);
            instanceLookup.Remove(clone);
            dirty = true;
        }
    }


    private GameObject InstantiatePrefab(GameObject prefab)
    {
        GameObject go = Instantiate(prefab) as GameObject;
        if (root != null) go.transform.parent = root;
        return go;
    }

    #region Static API

    public static void WarmPool(GameObject prefab, int size)
    {
        DI.Resolve<PoolManager>().warmPool(prefab, size);
    }

    public static GameObject SpawnObject(GameObject prefab, Type type = Type.Root)
    {
        return DI.Resolve<PoolManager>().spawnObject(prefab, type);
    }

    public static GameObject SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation, Type type = Type.Root)
    {
        return DI.Resolve<PoolManager>().spawnObject(prefab, position, rotation, type);
    }

    public static GameObject SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        return DI.Resolve<PoolManager>().spawnObject(prefab, position, rotation);
    }

    public static GameObject SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation, float time)
    {
        GameObject gameObj = DI.Resolve<PoolManager>().spawnObject(prefab, position, rotation);
        ReleaseObject(gameObj, time);
        return gameObj;
    }

    public static void ReleaseObject(GameObject clone)
    {
        DI.Resolve<PoolManager>().releaseObject(clone);
    }

    public static void ReleaseObject(GameObject clone, float time)
    {
        MonoBehaviourTools.Instance.StartCor(DI.Resolve<PoolManager>().ReleaseObjectCor(clone, time));
    }

    public IEnumerator ReleaseObjectCor(GameObject clone, float time)
    {
        yield return new WaitForSeconds(time);
        ReleaseObject(clone);
    }

    #endregion
}


