using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropCell : Cell, INumricalChange, IPlaceable
{
    [Header("发展数值")]
    public NumericalChangeType pollutionType;
    public float developmentnValue;
    public float contaminationValue;
    public double budgetValue;
    private bool hasChange;

    [Header("放置相关")]
    public bool canPlaceOnWater;

    private void Start()
    {
        canWrite = true;
    }

    public override void CellInit(Vector2 pos, Cushion cushion, CellDirection cellDirection = CellDirection.North)
    {
        base.CellInit(pos, cushion, cellDirection);
        Align();
    }
    protected override void RemoveCell()
    {
        base.RemoveCell();
        NumericalValueReChange();
    }

    public override void HandleSelection()
    {
        base.HandleSelection();

        if (mouseButton == MouseButton.Right)
        {
            CellRotate(1);
        }

        if (mouseButton == MouseButton.Middle)
        {
            RemoveCell();
        }
    }

    #region 发展指数相关

    public virtual void NumericalValueChange()
    {
        NumericalManager.instance.ChangeDevelopment(this, developmentnValue);
        NumericalManager.instance.ChangeContamination(this, contaminationValue);
        NumericalManager.instance.ChangeBudget(this, -budgetValue);
        hasChange = true;
    }

    public virtual void NumericalValueReChange()
    {
        NumericalManager.instance.ChangeDevelopment(this, -developmentnValue);
        NumericalManager.instance.ChangeContamination(this, -contaminationValue);
        NumericalManager.instance.ChangeBudget(this, budgetValue);
        hasChange = false;
    }

    public float DevelopmentnValue { get => 0;}

    public float ContaminationValue { get => 0; }

    public double BudgetValue { get => budgetValue; }

    public bool isActive { get => hasChange; set => hasChange = value; }

    public NumericalChangeType numericalType { get => pollutionType; }

    #endregion

    #region  放置相关

    public void Align()
    {
        if (cellConnectors.Count == 0 || cellConnectors.Count == 4)
            return;

        int index = 0;
        List<CellDirection> directions = new List<CellDirection>();
        int maxConnectedCells = connectedCells.Count;
        directions.Add(direction);
        for (int i = 0; i < 3; i++)
        {
            CellRotate(1);
            directions.Add(direction);
            Debug.Log(connectedCells.Count);
            if (connectedCells.Count > maxConnectedCells)
            {
                Debug.Log(direction);
                index = i + 1;
                maxConnectedCells = connectedCells.Count;
            }
        }
        CellRotate(direction.GetRotateNum(directions[index]));
    }

    public bool CanPlaceOnWater { get => canPlaceOnWater; }

    public bool CanWrite { get => canWrite; set => canWrite = true; }

    #endregion

}
