using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSource : Cell, IWaterRelated
{

    [Header("Ë®Ïà¹Ø")]
    public WaterNodeType waterNodeType;
    [SerializeField] private bool containsWater = false;
    [SerializeField] protected List<IWaterRelated> waterCells = new List<IWaterRelated>();

    private void Start()
    {
        containsWater = true;
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
        if (type == WaterInformationType.Divertion)
            return true;
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
                return true;
        }
    }

    public void ConnectCheck(IWaterRelated cell)
    {
        IWaterRelated thisWaterCell = this;
        WaterDiversionCheck(thisWaterCell);
        cell.PassInformation(this, WaterInformationType.Divertion);
    }

    public void WaterDiversionCheck(IWaterRelated cell)
    {

    }

}
