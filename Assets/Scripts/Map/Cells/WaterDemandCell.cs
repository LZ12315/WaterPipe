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
        return false;
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
                return false;
        }
    }

    public void ConnectCheck(IWaterRelated cell)
    {
        foreach (var item in waterCells)
        {
            if (item.ContainsWater)
                return;
        }

        if(containsWater)
        {
            NumericalValueReChange();
            containsWater = false;
        }
    }

    public void WaterDiversionCheck(IWaterRelated waterCell)
    {
        if (containsWater)
            return;

        containsWater = true;
        OnInteractAnim();
        if (!hasChangedNumerical)
        {
            NumericalValueChange();
            foreach (var cell in connectedCells)
            {
                if (cell.GetComponent<INumricalChange>() != null)
                {
                    INumricalChange cellToInteract = cell.GetComponent<INumricalChange>();
                    if (cellToInteract.numericalType == NumericalChangeType.Purify)
                        cell.CellInteract(this);
                }
            }
        }
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