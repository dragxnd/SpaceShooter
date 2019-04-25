using UnityEditor;
using UnityEngine;
namespace ScriptableObjects
{
    public static class Data
    {
        private static readonly string baseStatsDataPath = "Assets/Resources/Data/BaseStatsData.asset";
        private static readonly string statMultiplierDataPath = "Assets/Resources/Data/StatMultiplierData.asset";
        private static readonly string baseStatsResourcesPath = "Data/BaseStatsData";
        private static readonly string statMultiplierResourcesPath = "Data/StatMultiplierData";

        private static BaseStatsData baseStatsData;
        private static StatMultiplierData statMultiplierData;

        public static BaseStatsData BaseStatsData
        {
            get
            {
                if (baseStatsData == null)
                {
                    baseStatsData = Resources.Load(baseStatsResourcesPath) as BaseStatsData;
#if UNITY_EDITOR
                    if (baseStatsData == null)
                    {

                        baseStatsData = ScriptableObject.CreateInstance<BaseStatsData>();
                        AssetDatabase.CreateAsset(baseStatsData, baseStatsDataPath);
                        AssetDatabase.SaveAssets();
                        return baseStatsData;
                    }
#endif
                    return baseStatsData;
                }
                else
                {
                    return baseStatsData;
                }
            }
            private set { }
        }



        public static StatMultiplierData StatMultiplierData
        {
            get
            {
                if (statMultiplierData == null)
                {
                    statMultiplierData = Resources.Load(statMultiplierResourcesPath) as StatMultiplierData;
#if UNITY_EDITOR
                    if (statMultiplierData == null)
                    {
                        statMultiplierData = ScriptableObject.CreateInstance<StatMultiplierData>();
                        AssetDatabase.CreateAsset(statMultiplierData, statMultiplierDataPath);
                        AssetDatabase.SaveAssets();
                        return statMultiplierData;
                    }
#endif
                    return statMultiplierData;
                }
                else
                {
                    return statMultiplierData;
                }
            }
            private set { }
        }

    }
}