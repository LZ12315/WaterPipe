using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDemandCell : Cell, INumricalChange, IWaterRelated
{
    [Header("水相关")]
    public WaterNodeType waterNodeType;
    [SerializeField] private bool containsWater = false;
    [SerializeField] protected List<IWaterRelated> waterCells = new List<IWaterRelated>();

    [Header("发展数值")]
    public NumericalChangeType pollutionType;
    [SerializeField]private bool hasChangedNumerical;
    public float developmentnValue;
    public float contaminationValue;

    private void Start()
    {
        OnCellConnectChange += (value) =>
        {
            List<IWaterRelated> list = new List<IWaterRelated>();
            foreach (var cell in value)
            {
                IWaterRelated waterCell = cell.GetComponent<IWaterRelated>();
                if (!containsWater && waterCell.ContainsWater)
                    WaterDivertion();
                list.Add(waterCell);
            }
            WaterCells = list;
            WaterNodeManager.Instance.NodeChange(this, waterCells);
        };
    }

    public void InvokeTeacing(List<Cell> cells)
    {
        WaterNodeManager.Instance.NodeChange(this, waterCells);
    }

    public override void CellInit(Vector2 pos, Cushion cushion, CellDirection cellDirection = CellDirection.North)
    {
        base.CellInit(pos, cushion, cellDirection);
        WaterNodeManager.Instance.AddNode(this, WaterCells);
    }

    #region 水相关

    public WaterNodeType NodeType { get => waterNodeType; }

    bool IWaterRelated.ContainsWater { get => containsWater; set => containsWater = value; }

    public List<IWaterRelated> WaterCells { get => waterCells; set => waterCells = value; }

    public void SetWaterBreak(WaterNodeManager controller)
    {
        if(containsWater)
            NumericalValueReChange();
        containsWater = false;
    }

    public void WaterDivertion()
    {
        if (!containsWater)
            NumericalValueChange();
        containsWater = true;
        OnInteractAnim();
    }

    #endregion

    #region 发展数值

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

    #endregion
}