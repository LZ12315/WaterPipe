using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWaterRelated
{
    public WaterNodeType WaterCellType { get; }

    public CellAltitude Altitude { get;}

    public bool ContainsWater { get; set; }

    public List<IWaterRelated> WaterCells { get; set; }

    public void PassInformation(IWaterRelated connectCell, WaterInformationType type)
    {
        switch (type)
        {
            case WaterInformationType.CheckConnect:
                ConnectCheck(connectCell);
                break;
            case WaterInformationType.Divertion:
                WaterDiversionCheck(connectCell);
                break;
        }

        if (!CanPassInformation(connectCell, type))
            return;

        foreach (var cell in WaterCells)
        {
            if(cell != connectCell && cell.CanCheck(this, type))
                cell.PassInformation(this, type);
        }
    }

    public void ConnectCheck(IWaterRelated cell);

    public void WaterDiversionCheck(IWaterRelated cell);

    public bool CanCheck(IWaterRelated cell, WaterInformationType type);

    public bool CanPassInformation(IWaterRelated connectCell, WaterInformationType type);

}
