using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "LevelName", menuName = "Level/Save Level")]
    public class Level : ScriptableObject
    {
        public List<LevelObject> levelObjects = new List<LevelObject>();

        private void Awake()
        {
            Ship[] sceneShips = GameObject.FindObjectsOfType<Ship>();
            List<Ship> assetShips = Resources.LoadAll<Ship>("Prefabs/").ToList();

            foreach (var s in sceneShips)
            {
                try
                {
                    LevelObject newLevelObject = new LevelObject();
                    newLevelObject.prefab = assetShips.Find(x => x.Name == s.Name).gameObject;
                    newLevelObject.position = s.transform.position;
                    newLevelObject.rotation = s.transform.rotation;
                    levelObjects.Add(newLevelObject);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }     
    }
}