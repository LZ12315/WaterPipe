using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCell : Cell, INumricalChange, IPlaceable, IWaterRelated
{
    [Header("水相关")]
    [SerializeField] private bool containsWater = false;
    [SerializeField] protected List<IWaterRelated> waterCells = new List<IWaterRelated>();

    [Header("发展数值")]
    public NumericalChangeType pollutionType;
    public double budgetValue;
    private bool hasChange;

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
            IWaterRelated thisWaterCell = this;
            thisWaterCell.PassInformation(this, WaterInformationType.CheckConnect);
        }

        if (mouseButton == MouseButton.Middle)
        {
            RemoveCell();
            IWaterRelated thisWaterCell = this;
            thisWaterCell.PassInformation(this, WaterInformationType.CheckConnect);
        }
    }

    public override void CellInit(Vector2 pos, Cushion cushion, CellDirection cellDirection = CellDirection.North)
    {
        base.CellInit(pos, cushion, cellDirection);
        CellAlign();
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

        waterCells.Clear();
        foreach (var cell in connectedCells)
        {
            if (cell.GetComponent<IWaterRelated>() != null)
            {
                IWaterRelated waterCell = cell.GetComponent<IWaterRelated>();
                waterCells.Add(waterCell);
            }
                
        }
    }

    #region 引水相关

    bool IWaterRelated.ContainsWater { get => containsWater; set => containsWater = value; }
    public List<IWaterRelated> WaterCells { get => waterCells; set => waterCells = value; }
    public CellAltitude Altitude { get => altitude; }


    public bool CanPassInformation(IWaterRelated cell, WaterInformationType type)
    {
        switch (type)
        {
            case WaterInformationType.CheckConnect:
                return true;
            case WaterInformationType.Divertion:
                CellAltitude alt = cell.Altitude;
                if (alt < altitude)
                    return false;
                return true;
            default:
                return true;
        }
    }


    public void ConnectCheck(IWaterRelated cell)
    {
        containsWater = false;
    }


    public void WaterDiversionCheck(IWaterRelated cell)
    {
        containsWater = true;
    }

    //private void WaterDiversion()
    //{
    //    foreach (var cell in connectedCells)
    //    {
    //        if (cell.ReturnIfContainsWater())
    //        {
    //            containsWater = true;
    //            if(!waterSources.Contains(cell))
    //                waterSources.Add(cell);
    //        }
    //    }
    //}

    //private bool CheckIfCanGetWater()
    //{
    //    if(waterSources.Count == 0)
    //    {
    //        containsWater = false;
    //        return false;
    //    }

    //    foreach (var cell in connectedCells)
    //    {
    //        containsWater = false;
    //        if (cell.ReturnIfContainsWater() && waterSources.Contains(cell))
    //        {
    //            containsWater = true;
    //            return true;
    //        }
    //        else
    //            waterSources.Remove(cell);
    //    }

    //    return false;
    //}

    #endregion

    #region 发展指数相关

    public void NumericalValueChange()
    {
        NumericalManager.instance.ChangeBudget(this,-budgetValue);
        hasChange = true;
    }

    public void NumericalValueReChange()
    {
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

    public bool CanWrite { get => canWrite;}

    public bool CanPlaceOnWater { get => true; }

    public void CellAlign()
    {
        if (cellConnectors.Count == 0 || cellConnectors.Count == 4)
            return;

        int index = 0;
        List<CellDirection> directions = new List<CellDirection>();
        int maxConnectedCells = connectedCells.Count;
        directions.Add(direction);
        for(int i=0; i<3; i++)
        {
            CellRotate(1);
            directions.Add(direction);
            if (connectedCells.Count > maxConnectedCells)
            {
                index = i + 1;
                maxConnectedCells = connectedCells.Count;
            }
        }
        CellRotate(direction.GetRotateNum(directions[index]));
    }

    #endregion

}
