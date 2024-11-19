using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWaterRelated
{
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

        foreach (var cell in WaterCells)
        {
            if(cell != connectCell && cell.CanPassInformation(this, type))
                cell.PassInformation(this, type);
        }
    }

    public void ConnectCheck(IWaterRelated cell);

    public void WaterDiversionCheck(IWaterRelated cell);

    public bool CanPassInformation(IWaterRelated cell, WaterInformationType type);
}
