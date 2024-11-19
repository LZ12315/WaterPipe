using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDemandCell : Cell, INumricalChange, IWaterRelated
{
    [Header("水相关")]
    [SerializeField] private bool containsWater = false;
    [SerializeField] protected List<IWaterRelated> waterCells = new List<IWaterRelated>();

    [Header("发展数值")]
    public NumericalChangeType pollutionType;
    [SerializeField]private bool hasChangedNumerical;
    public float developmentnValue;
    public float contaminationValue;

    public override void CellDisConnect(Cell cellToRemove, Cell interactCell)
    {
        base.CellDisConnect(cellToRemove, interactCell);

        if (containsWater)
        {//等待处理断水的逻辑
            //if (!CheckIfCanGetWater() && hasChangedNumerical)
            //    NumericalValueReChange();

            //foreach (var cell in connectedCells)
            //{
            //    if (cell != interactCell)
            //        cell.CellDisConnect(cellToRemove, this);
            //}
        }
    }

    #region 水相关
    bool IWaterRelated.ContainsWater { get => containsWater; set => containsWater = value; }
    public List<IWaterRelated> WaterCells { get => waterCells; set => waterCells = value; }
    public CellAltitude Altitude { get => altitude; }

    public bool CanPassInformation(IWaterRelated cell, WaterInformationType type)
    {
        switch (type)
        {
            case WaterInformationType.CheckConnect:
                return false;
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
        IWaterRelated thisWaterCell = this;
        WaterDiversionCheck(thisWaterCell);
        thisWaterCell.PassInformation(this, WaterInformationType.Divertion);
    }

    public void WaterDiversionCheck(IWaterRelated waterCell)
    {
        containsWater = true;
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