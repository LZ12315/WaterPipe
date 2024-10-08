using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagManager : MonoBehaviour
{
    public static BagManager instance;

    [Header("物品存储")]
    private WareHouse wareHouse;
    private List<CellInThisLevel> storedCells;
    private List<Cell> usedCells;

    [Header("物品使用")]
    public Cell cell;
    public Cell cellOnHand;
    private Cell nowCell;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        cellOnHand = cell;
    }

    public void SetWareHouse(WareHouse wareHouse)
    {
        this.wareHouse = wareHouse;
        this.storedCells = wareHouse.ReturnStoredCells();
    }

    public void StoreCell(List<Cell> newCells)
    {
        foreach (Cell cell in newCells)
        {
            usedCells.Add(cell);
        }
    }

    public void StoreCell(Cell newCell)
    {
        usedCells.Add(newCell);
    }

    public Cell ReturnCellOnHand()
    {
        return cellOnHand;
    }
}
