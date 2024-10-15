using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "LevelSO/GridMapSO")]
public class GridMapSO : ScriptableObject
{
    public int width;
    public int height;
    public List<IntListWrapper> gridMap = new List<IntListWrapper>();
    public List<CellEntry> dictionary = new List<CellEntry>();

    public void AddToArray(List<int> values)
    {
        gridMap.Add(new IntListWrapper { values = values });
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

    public Cell ReturnMapCell(int i, int j)
    {
        foreach (CellEntry cellEntry in dictionary)
        {
            if(gridMap[i].values[j] == cellEntry.key)
                return cellEntry.value;
        }
        return null;
    }
}

[System.Serializable]
public class CellEntry
{
    public int key;
    public Cell value;
}

[System.Serializable]
public class IntListWrapper
{
    public List<int> values;
}