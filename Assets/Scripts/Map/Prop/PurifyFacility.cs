using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurifyFacility : PropCell
{
    [SerializeField]List<Cell> clearedCells = new List<Cell>();

    public override void CellInit(Vector2 pos, Cushion cushion, CellDirection cellDirection = CellDirection.North)
    {
        base.CellInit(pos, cushion, cellDirection);
        NumericalValueChange();
        ContaminationClear();
    }

    public override void CellConnect(Cell interactCell)
    {
        base.CellConnect(interactCell);
        ContaminationClear();
    }

    public override void CellDisConnect(Cell cellToRemove, Cell interactCell)
    {
        base.CellDisConnect(cellToRemove, interactCell);
        ReContaminationClear(interactCell);
    }

    public override void CellInteract(Cell interactCell)
    {
        base.CellInteract(interactCell);
        ContaminationClear();
    }

    protected override void RemoveCell()
    {
        base.RemoveCell();
        ReContaminationClear();
    }

    private void ContaminationClear()
    {
        foreach (var cell in connectedCells)
        {
            if (cell.GetComponent<INumricalChange>() != null && !clearedCells.Contains(cell))
            {
                INumricalChange numricalChange = cell.GetComponent<INumricalChange>();
                if(numricalChange.numericalType == NumericalChangeType.Sewage && numricalChange.isActive)
                {
                    float contaminationValue = numricalChange.ContaminationValue;
                    NumericalManager.instance.ChangeContamination(this, -contaminationValue);
                    clearedCells.Add(cell);
                }
            }
        }
    }

    private void ReContaminationClear(Cell clearedCell)
    {
        if (!clearedCells.Contains(clearedCell))
            return;

        INumricalChange numricalChange = clearedCell.GetComponent<INumricalChange>();
        float contaminationValue = numricalChange.ContaminationValue;
        NumericalManager.instance.ChangeContamination(this, contaminationValue);
        clearedCells.Remove(clearedCell);
    }

    private void ReContaminationClear()
    {
        foreach (var cell in clearedCells)
        {
            if (cell.GetComponent<INumricalChange>() != null)
            {
                INumricalChange numricalChange = cell.GetComponent<INumricalChange>();
                float contaminationValue = numricalChange.ContaminationValue;
                NumericalManager.instance.ChangeContamination(this, contaminationValue);
            }
        }
        clearedCells.Clear();
    }

}
