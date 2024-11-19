using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSource : Cell, IWaterRelated
{

    [Header("水相关")]
    [SerializeField] private bool containsWater = false;
    [SerializeField] protected List<IWaterRelated> waterCells = new List<IWaterRelated>();

    private void Start()
    {
        containsWater = true;
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
        IWaterRelated thisWaterCell = this;
        WaterDiversionCheck(thisWaterCell);
        thisWaterCell.PassInformation(this, WaterInformationType.Divertion);
    }

    public void WaterDiversionCheck(IWaterRelated cell)
    {
        //等待动画实现
    }

}
