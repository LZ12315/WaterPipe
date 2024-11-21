using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCell : Cell, INumricalChange, IPlaceable, IWaterRelated
{
    [Header("水相关")]
    public WaterNodeType waterNodeType;
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
            List<IWaterRelated> tmpList = new List<IWaterRelated>();
            foreach (var cell in waterCells)
                tmpList.Add(cell);

            CellRotate(1);

            IWaterRelated thisWaterCell = this;
            thisWaterCell.PassInformation(this, WaterInformationType.CheckConnect);
            foreach (var cell in tmpList)
                cell.PassInformation(this, WaterInformationType.CheckConnect);
            tmpList.Clear();
        }

        if (mouseButton == MouseButton.Middle)
        {
            //RemoveCell();
            //IWaterRelated thisWaterCell = this;
            //thisWaterCell.PassInformation(this, WaterInformationType.CheckConnect);
            List<IWaterRelated> tmpList = new List<IWaterRelated>();
            foreach (var cell in waterCells)
                tmpList.Add(cell);

            RemoveCell();

            IWaterRelated thisWaterCell = this;
            thisWaterCell.PassInformation(this, WaterInformationType.CheckConnect);
            foreach (var cell in tmpList)
                cell.PassInformation(this, WaterInformationType.CheckConnect);
            tmpList.Clear();
        }
    }

    public override void CellInit(Vector2 pos, Cushion cushion, CellDirection cellDirection = CellDirection.North)
    {
        base.CellInit(pos, cushion, cellDirection);
        CellAlign();
        NumericalValueChange();

        IWaterRelated thisWaterCell = this;
        thisWaterCell.PassInformation(this, WaterInformationType.CheckConnect);
    }

    protected override void RemoveCell()
    {
        base.RemoveCell();
        NumericalValueReChange();
    }

    protected override void TeaseConnectedCells()
    {
        base.TeaseConnectedCells();
        UpdateWaterCells();
    }

    public override void CellConnect(Cell interactCell)
    {
        base.CellConnect(interactCell);
        UpdateWaterCells();
    }

    #region 水相关

    public WaterNodeType WaterCellType { get => waterNodeType; }
    bool IWaterRelated.ContainsWater { get => containsWater; set => containsWater = value; }
    public List<IWaterRelated> WaterCells { get => waterCells; set => waterCells = value; }
    public CellAltitude Altitude { get => altitude; }

    private void UpdateWaterCells()
    {
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

    public bool CanPassInformation(IWaterRelated connectCell, WaterInformationType type)
    {
        if (connectCell.WaterCellType == WaterNodeType.Demand)
            return false;
        return true;
    }

    public bool CanCheck(IWaterRelated cell, WaterInformationType type)
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
        OnInteractAnim();
    }

    #endregion

    #region 发展指数

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
