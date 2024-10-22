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

        // ��ʾ����
        EditorGUILayout.LabelField("2D Array:");
        for (int i = 0; i < mapSO.gridMap.Count; i++)
        {
            EditorGUILayout.LabelField($"Row {i}:");
            for (int j = 0; j < mapSO.gridMap[i].values.Count; j++)
            {   //ȷ��Ԫ�ر���ӵ�������
                mapSO.gridMap[i].values[j] = EditorGUILayout.IntField(mapSO.gridMap[i].values[j]);
                EditorUtility.SetDirty(mapSO);
            }
            EditorGUILayout.Space();
        }

        // ���Ԫ�ص�����
        EditorGUILayout.LabelField("Add to 2D Array:");
        newArrayValue = EditorGUILayout.TextField("New Value:", newArrayValue);
        if (GUILayout.Button("Add Row"))
        {
            string[] values = newArrayValue.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
            List<int> valuesList = new List<int>();
            foreach (string str in values)
            {
                // ���Խ�������ֵ
                if (int.TryParse(str.Trim(), out int value) && valuesList.Count < mapSO.width)
                {
                    valuesList.Add(value);
                }
            }
            if (valuesList.Count > 0 && mapSO.gridMap.Count < mapSO.height) // ֻ������Чֵ���Ҹ߶�δ��������������
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

        // ��ʾ�ֵ�
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

        // ���Ԫ�ص��ֵ�
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
