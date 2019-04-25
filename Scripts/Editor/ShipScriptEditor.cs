using UnityEngine;
using System.Collections;
using UnityEditor;
using ScriptableObjects;

[CustomEditor(typeof(Ship),true)]
[CanEditMultipleObjects]
public class ShipScriptEditor : Editor
{
    private int offset = 10;
    public override void OnInspectorGUI()
    {
        Ship shipTarget = (Ship)target;

        GUILayout.Space(offset);

        GUILayout.Label("Ship name : ");
        shipTarget.Name = GUILayout.TextField(shipTarget.Name);

        GUILayout.Space(offset);

        GUILayout.Label("Base stat : " + shipTarget.BaseStatName);
        if (GUILayout.Button("Select base stat"))
        {
            GenericMenu menu = new GenericMenu();
            foreach (var stat in Data.BaseStatsData.baseStats)
            {
                menu.AddItem(new GUIContent(stat.name), false, (obj) => 
                {
                    shipTarget.BaseStatName = stat.name;
                    EditorUtility.SetDirty(shipTarget);
                    AssetDatabase.SaveAssets();
                }, stat);
            }
            menu.ShowAsContext();
        }

        GUILayout.Space(offset);

        GUILayout.Label("Stat multiplier : " + shipTarget.StatMultiplierName);
        if (GUILayout.Button("Select stat multiplier"))
        {
            GenericMenu menu = new GenericMenu();
            foreach (var stat in Data.StatMultiplierData.statMultipliers)
            {
                menu.AddItem(new GUIContent(stat.name), false, (obj) =>
                {
                    shipTarget.StatMultiplierName = stat.name;
                    EditorUtility.SetDirty(shipTarget);
                    AssetDatabase.SaveAssets();
                }, stat);
            }
            menu.ShowAsContext();
        }

        GUILayout.Space(offset);

        SerializedObject serializedObject = new SerializedObject(target);
        SerializedProperty property = serializedObject.FindProperty("shootPoins");
        serializedObject.Update();
        EditorGUILayout.PropertyField(property, true);
        serializedObject.ApplyModifiedProperties();

    }
}