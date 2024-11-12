using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDemandCell : Cell, INumricalChange
{
    [Header("发展数值")]
    public NumericalChangeType pollutionType;
    [SerializeField]private bool hasChangedNumerical;
    public float developmentnValue;
    public float contaminationValue;

    public override void CellConnect(Cell interactCell)
    {
        base.CellConnect(interactCell);
        if (interactCell.ReturnIfContainsWater())
        {
            containsWater = true;
            if (!waterSources.Contains(interactCell))
                waterSources.Add(interactCell);
            if (!hasChangedNumerical)
            {
                NumericalValueChange();
                foreach(var cell in connectedCells)
                {
                    if(cell.GetComponent<INumricalChange>() != null)
                    {
                        INumricalChange cellToInteract = cell.GetComponent<INumricalChange>();
                        if(cellToInteract.numericalType == NumericalChangeType.Purify)
                            cell.CellInteract(this);
                    }
                }
            }
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
            if (!CheckIfCanGetWater() && hasChangedNumerical)
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
        hasChangedNumerical = true;
    }

    public void NumericalValueReChange()
    {
        NumericalManager.instance.ChangeDevelopment(this, -developmentnValue);
        NumericalManager.instance.ChangeContamination(this, -contaminationValue);
        hasChangedNumerical = false;
    }

    public float DevelopmentnValue { get => developmentnValue; }

    public float ContaminationValue { get => contaminationValue; }

    public double BudgetValue { get => 0; }

    public bool isActive { get => hasChangedNumerical; set => hasChangedNumerical = value; }

    public NumericalChangeType numericalType { get => pollutionType; }
}