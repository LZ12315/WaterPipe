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
    public Cell straightPipe;
    public Cell elbowPipe;
    public Cell cellOnHand;
    private Cell nowCell;

    [Header("鼠标输入检测")]
    private float scrollInput;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        cellOnHand = straightPipe;
    }

    private void Update()
    {
        GetMouseScroll();
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

    private void GetMouseScroll()
    {
        scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput > 0)
        {
            cellOnHand = straightPipe;
        }
        else if (scrollInput < 0)
        {
            cellOnHand = elbowPipe;
        }
    }

    public Cell ReturnCellOnHand()
    {
        return cellOnHand;
    }
}
