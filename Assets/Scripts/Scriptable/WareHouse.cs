using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelSO/WareHouseSO")]


public class WareHouse : ScriptableObject
{
    [SerializeField]
    private List<CellInThisLevel> storedCells;

    public List<CellInThisLevel> ReturnStoredCells()
    {
        return storedCells;
    }
}

[System.Serializable]
public struct CellInThisLevel
{
    public Cell cell;
    public int num;
}