using UnityEngine;
using System.Collections.Generic;

namespace ScriptableObjects
{
    //[CreateAssetMenu(fileName = "BaseStatsData", menuName = "Data/Create BaseStatsData")]
    public class BaseStatsData : ScriptableObject
    {
        public List<BaseStat> baseStats = new List<BaseStat>();
    }
}

