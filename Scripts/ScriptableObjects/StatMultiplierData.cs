using UnityEngine;
using System.Collections.Generic;

namespace ScriptableObjects
{
    //[CreateAssetMenu(fileName = "StatMultiplierData", menuName = "Data/Create StatMultiplierData")]
    public class StatMultiplierData : ScriptableObject
    {
        public List<StatMultiplier> statMultipliers = new List<StatMultiplier>();
    }
}