using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridMapSO))]
public class MapEditor : Editor
{
    private int newArrayValue;
    private int newDictKey;
    private GameObject newCellObject;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GridMapSO map = (GridMapSO)target;

        // 显示数组
        EditorGUILayout.LabelField("2D Array:");
        for (int i = 0; i < map.gridMap.Count; i++)
        {
            int newValue = EditorGUILayout.IntField(map.gridMap[i]);
            EditorGUILayout.Space();
        }

        // 添加元素到数组
        EditorGUILayout.LabelField("Add to 2D Array:");
        newArrayValue = EditorGUILayout.IntField("New Value:", newArrayValue);
        if (GUILayout.Button("Add Row"))
        {
            map.gridMap.Add(newArrayValue);


            EditorUtility.SetDirty(map);
        }

        // 显示字典
        EditorGUILayout.LabelField("Dictionary:");
        foreach (var kvp in map.dictionary)
        {
            if (kvp.value != null)
            {
                EditorGUILayout.LabelField($"Key: {kvp.key} | Value: {kvp.value.name}");
            }
            else
            {
                EditorGUILayout.LabelField($"Key: {kvp.key} | Value: null");
            }
        }

        // 添加元素到字典
        EditorGUILayout.LabelField("Add to Dictionary:");
        newDictKey = EditorGUILayout.IntField("Key (int):", newDictKey);
        newCellObject = (GameObject)EditorGUILayout.ObjectField("Referenced Cell:", newCellObject, typeof(GameObject), true);

        if (GUILayout.Button("Add Entry"))
        {
            map.AddToDictionary(newDictKey, newCellObject.GetComponent<Cell>());
            newDictKey = 0;
            newCellObject = new GameObject();

            EditorUtility.SetDirty(map);
        }
    }
}
