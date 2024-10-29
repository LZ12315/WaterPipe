using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDemandCell : Cell, INumricalChange
{
    [Header("发展数值")]
    public double developmentnValue;
    public float contaminationValue;

    public override void CellConnect(Cell interactCell)
    {
        base.CellConnect(interactCell);
        if (interactCell.ReturnIfContainsWater())
        {
            containsWater = true;
            if (!waterSources.Contains(interactCell))
                waterSources.Add(interactCell);
            NumericalValueChange();
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
            if (!CheckIfCanGetWater())
                NumericalValueReChange();

            foreach (var cell in connectedCells)
            {
                if (cell != interactCell)
                    cell.CellDisConnect(cellToRemove, this);
            }
        }
    }

    private bool CheckIfCanGetWater()
    {
        if (waterSources.Count == 0)
        {
            containsWater = false;
            return false;
        }

        foreach (var cell in connectedCells)
        {
            containsWater = false;
            if (cell.ReturnIfContainsWater() && waterSources.Contains(cell))
            {
                containsWater = true;
                return true;
            }
            else
                waterSources.Remove(cell);
        }

        return false;
    }

    public void NumericalValueChange()
    {
        NumericalManager.instance.ChangeDevelopment(this, developmentnValue);
        NumericalManager.instance.ChangeContamination(this, contaminationValue);
    }

    public void NumericalValueReChange()
    {
        NumericalManager.instance.ChangeDevelopment(this, -developmentnValue);
        NumericalManager.instance.ChangeContamination(this, -contaminationValue);
    }
}