using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "LevelSO/GridMapSO")]
public class GridMapSO : ScriptableObject
{
    public int width;
    public int height;
    public List<int> gridMap = new List<int>();
    public List<CellEntry> dictionary = new List<CellEntry>();

    public void AddToArray(int value)
    {
        gridMap.Add(value);
        EditorUtility.SetDirty(this);
    }

    public void AddToDictionary(int key, Cell value)
    {
        if (!dictionary.Exists(entry => entry.key == key))
        {
            dictionary.Add(new CellEntry { key = key, value = value });
            EditorUtility.SetDirty(this);
        }
    }

    public int GetValue(int x, int y)
    {
        return gridMap[y * width + x];
    }

    public void SetValue(int x, int y, int value)
    {
        gridMap[y * width + x] = value;
    }
}

[System.Serializable]
public class CellEntry
{
    public int key;
    public Cell value;
}