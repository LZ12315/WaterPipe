using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWaterRelated
{
    public List<IWaterRelated> ConnectedWaterCells { get; set; }

    public List<IWaterRelated> WaterSources { get; set; }

    public bool ContainsWater { get; set; }

    public void CellInteract(IWaterRelated relatedWaterCell);
}
