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
    public List<WorkCells> workCells = new List<WorkCells>();
    public List<CellEntry> dictionary = new List<CellEntry>();

    public void AddToWorkCells(WorkCells newWorkCell)
    {
        workCells.Add(newWorkCell);
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

    public List<WorkCells> ReturnWorkCells()
    {
        return workCells;
    }

    public Cell ReturnDicCell(int key)
    {
        foreach (CellEntry cellEntry in dictionary)
        {
            if (key == cellEntry.key)
                return cellEntry.value;
        }
        return null;
    }
}

[System.Serializable]
public class IntListWrapper
{
    public List<int> values;
}

[System.Serializable]
public class WorkCells
{
    public Vector2 position;
    public int value;

    public WorkCells(Vector2 position, int value)
    {
        this.position = position;
        this.value = value;
    }
}

[System.Serializable]
public class CellEntry
{
    public int key;
    public Cell value;
}