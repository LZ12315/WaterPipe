using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCell : Cell
{

    public override void HandleSelection()
    {
        if (mouseButton == MouseButton.Right)
        {
            CellRotate(1);
        }
    }

    protected override void TeaseConnectedCells()
    {
        base.TeaseConnectedCells();
        WaterDiversion();
    }

    public override void CellInteract(Cell interactCell)
    {
        base.CellInteract(interactCell);
        if(interactCell.ReturnIfContainsWater() && !containsWater)
        {
            containsWater = true;
            foreach (var cell in connectedCells)
            {
                if(cell != interactCell)
                    cell.CellInteract(this);
            }
        }
    }

    private void WaterDiversion()
    {
        foreach(var cell in connectedCells)
        {
            if (cell.ReturnIfContainsWater())
            {
                containsWater = true;
                break;
            }
        }
        foreach (var cell in connectedCells)
        {
            cell.CellInteract(this);
        }

    }
}
