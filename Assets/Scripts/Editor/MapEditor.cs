using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridMapSO))]
public class MapEditor : Editor
{
    private string newArrayValue;
    private int newDictKey;
    private GameObject newCellObject;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GridMapSO mapSO = (GridMapSO)target;

        // 显示数组
        EditorGUILayout.LabelField("2D Array:");
        for (int i = 0; i < mapSO.gridMap.Count; i++)
        {
            EditorGUILayout.LabelField($"Row {i}:");
            for (int j = 0; j < mapSO.gridMap[i].values.Count; j++)
            {   //确保元素被添加到数组里
                mapSO.gridMap[i].values[j] = EditorGUILayout.IntField(mapSO.gridMap[i].values[j]);
                EditorUtility.SetDirty(mapSO);
            }
            EditorGUILayout.Space();
        }

        // 添加元素到数组
        EditorGUILayout.LabelField("Add to 2D Array:");
        newArrayValue = EditorGUILayout.TextField("New Value:", newArrayValue);
        if (GUILayout.Button("Add Row"))
        {
            string[] values = newArrayValue.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
            List<int> valuesList = new List<int>();
            foreach (string str in values)
            {
                // 尝试解析整数值
                if (int.TryParse(str.Trim(), out int value) && valuesList.Count < mapSO.width)
                {
                    valuesList.Add(value);
                }
            }
            if (valuesList.Count > 0 && mapSO.gridMap.Count < mapSO.height) // 只在有有效值并且高度未超出的情况下添加
            {
                mapSO.gridMap.Add(new IntListWrapper { values = valuesList });
                newArrayValue = "";
                EditorUtility.SetDirty(mapSO);
            }
            else
            {
                Debug.LogWarning("No valid integer values were provided or off the map");
            }
        }

        // 显示字典
        EditorGUILayout.LabelField("Dictionary:");
        foreach (var kvp in mapSO.dictionary)
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
            mapSO.AddToDictionary(newDictKey, newCellObject.GetComponent<Cell>());
            newDictKey = 0;
            newCellObject = new GameObject();

            EditorUtility.SetDirty(mapSO);
        }
    }
}
