using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDemandCell : Cell
{
    public override void CellConnect(Cell interactCell)
    {
        base.CellConnect(interactCell);
        if (interactCell.ReturnIfContainsWater())
        {
            containsWater = true;
            if (!waterSources.Contains(interactCell))
                waterSources.Add(interactCell);
            WaterRunning(this);
        }
    }

    public override void CellDisConnect(Cell cellToRemove, Cell interactCell)
    {
        base.CellDisConnect(cellToRemove, interactCell);

        if (waterSources.Contains(cellToRemove))
            waterSources.Remove(cellToRemove);

        if (containsWater)
        {
            CheckIfCanGetWater();

            foreach (var cell in connectedCells)
            {
                if (cell != interactCell)
                    cell.CellDisConnect(cellToRemove, this);
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

}
