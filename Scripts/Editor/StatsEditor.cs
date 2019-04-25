using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using ScriptableObjects;

[CustomEditor(typeof(StatsEditor))]
public class StatsEditor : EditorWindow
{

    public List<BaseStat> baseStats;
    public List<StatMultiplier> statMultipliers;

    private int deleteButtonWidth = 30;

    [MenuItem("Editors/Stats Editor")]
    static void Init()
    {
        var window = (StatsEditor)EditorWindow.GetWindow(typeof(StatsEditor));
        window.Show();      
    }

    Vector2 scrollPos;
    void OnGUI()
    {
            baseStats = Data.BaseStatsData.baseStats;
            statMultipliers = Data.StatMultiplierData.statMultipliers;

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            if (GUILayout.Button(StringKeys.e_SaveAll))
            {
                EditorUtility.SetDirty(Data.BaseStatsData);
                EditorUtility.SetDirty(Data.StatMultiplierData);
                AssetDatabase.SaveAssets();
            }

            GUILayout.Label(StringKeys.e_BaseShipStatsTitle, EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(StringKeys.e_Add))
            {
                Data.BaseStatsData.baseStats.Add(new BaseStat());
            }

            if (GUILayout.Button(StringKeys.e_DeleteAll))
            {
                Data.BaseStatsData.baseStats.Clear();
            }


            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < Data.BaseStatsData.baseStats.Count; i++)
            {

                EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
                ScriptableObject target = this;
                SerializedObject so = new SerializedObject(target);
                SerializedProperty stringsProperty = so.FindProperty($"baseStats.Array.data[{i}]");

                EditorGUILayout.PropertyField(stringsProperty, true);
                so.ApplyModifiedProperties();

                if (GUILayout.Button("X", GUILayout.Width(deleteButtonWidth)))
                {
                    Data.BaseStatsData.baseStats.Remove(baseStats[i]);
                }

                EditorGUILayout.EndHorizontal();
            }



            GUILayout.Label(StringKeys.e_ShipStatsMultiplierTitle, EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(StringKeys.e_Add))
            {
                statMultipliers.Add(new StatMultiplier());
            }

            if (GUILayout.Button(StringKeys.e_DeleteAll))
            {
                statMultipliers.Clear();
            }
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < statMultipliers.Count; i++)
            {
                EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
                ScriptableObject target = this;
                SerializedObject so = new SerializedObject(target);
            
                SerializedProperty stringsProperty = so.FindProperty($"statMultipliers.Array.data[{i}]");
                EditorGUILayout.PropertyField(stringsProperty, true);
                so.ApplyModifiedProperties();

                if (GUILayout.Button("X", GUILayout.Width(deleteButtonWidth)))
                {
                    statMultipliers.Remove(statMultipliers[i]);
                }
                EditorGUILayout.EndHorizontal();
            }

        EditorGUILayout.EndScrollView();
    }

}
