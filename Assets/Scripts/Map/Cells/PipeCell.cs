using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCell : Cell, INumricalChange
{
    [Header("发展数值")]
    public double budgetValue;

    private void Start()
    {
        canWrite = true;
    }

    public override void HandleSelection()
    {
        base.HandleSelection();

        if (mouseButton == MouseButton.Right)
        {
            CellRotate(1);
        }
    }

    public override void CellInit(Vector2 pos, Cushion cushion, CellDirection cellDirection = CellDirection.North)
    {
        base.CellInit(pos, cushion, cellDirection);
        NumericalValueChange();
    }

    protected override void RemoveCell()
    {
        base.RemoveCell();
        NumericalValueReChange();
    }

    protected override void TeaseConnectedCells()
    {
        base.TeaseConnectedCells();
        WaterDiversion();
        foreach (var cell in connectedCells)
        {
            cell.CellConnect(this);
        }
    }

    public override void CellConnect(Cell interactCell)
    {
        base.CellConnect(interactCell);

        if(interactCell.ReturnIfContainsWater())
        {
            if(!containsWater)
            {
                containsWater = true;
                foreach (var cell in connectedCells)
                {
                    if (cell != interactCell)
                        cell.CellConnect(this);
                }
            }

            containsWater = true;
            if(!waterSources.Contains(interactCell))
                waterSources.Add(interactCell);
        }
    }

    public override void CellDisConnect(Cell cellToRemove ,Cell interactCell)
    {
        base.CellDisConnect(cellToRemove,interactCell);

        if(waterSources.Contains(cellToRemove))
            waterSources.Remove(cellToRemove);

        if (containsWater)
        {
            CheckIfCanGetWater();

            foreach (var cell in connectedCells)
            {
                if (cell != interactCell)
                    cell.CellDisConnect(cellToRemove,this);
            }
        }
    }

    private void WaterDiversion()
    {
        foreach (var cell in connectedCells)
        {
            if (cell.ReturnIfContainsWater())
            {
                containsWater = true;
                waterSources.Add(cell);
            }
        }
    }

    private bool CheckIfCanGetWater()
    {
        foreach (var cell in connectedCells)
        {
            containsWater = false;
            if (cell.ReturnIfContainsWater() && waterSources.Contains(cell))
            {
                containsWater = true;
                return true;
            }
        }

        return false;
    }

    public void NumericalValueChange()
    {
        NumericalManager.instance.ChangeBudget(this,-budgetValue);
    }

    public void NumericalValueReChange()
    {
        NumericalManager.instance.ChangeBudget(this, -budgetValue);
    }
}
