using DG.Tweening;
using System;
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

    protected override void Awake()
    {
        base.Awake();
        canWrite = true;
        OnCellConnectChange += (value) =>
        {
            List<IWaterRelated> list = new List<IWaterRelated>();
            foreach (var cell in value)
            {
                if (cell.GetComponent<IWaterRelated>() != null)
                {
                    IWaterRelated waterCell = cell.GetComponent<IWaterRelated>();
                    if (waterCell.ContainsWater)
                        WaterDivertion();
                    list.Add(waterCell);
                }
            }
            WaterCells = list;
            WaterNodeManager.Instance.NodeChange(this, WaterCells);
        };
    }

    public override void HandleSelection()
    {
        base.HandleSelection();

        if (mouseButton == MouseButton.Right)
        {
            CellRotate(1);
            WaterNodeManager.Instance.WaterContainsCheck(this);
        }

        if (mouseButton == MouseButton.Middle)
            RemoveCell();
    }

    public override void CellInit(Vector2 pos, Cushion cushion, CellDirection cellDirection = CellDirection.North)
    {
        base.CellInit(pos, cushion, cellDirection);
        CellAlign();
        NumericalValueChange();
        WaterNodeManager.Instance.AddNode(this, WaterCells);
    }

    protected override void RemoveCell()
    {
        base.RemoveCell();
        NumericalValueReChange();
        WaterNodeManager.Instance.DeleteNode(this);
    }

    #region 水相关

    public WaterNodeType NodeType { get => waterNodeType; }

    bool IWaterRelated.ContainsWater { get => containsWater; set => containsWater = value; }

    public List<IWaterRelated> WaterCells { get => waterCells; set => waterCells = value; }

    public void SetWaterBreak(WaterNodeManager controller)
    {
        containsWater = false;
    }

    public void WaterDivertion()
    {
        containsWater = true;
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
