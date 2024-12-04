using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWaterRelated
{
    public WaterNodeType NodeType { get; }

    public bool ContainsWater { get; set; }

    public List<IWaterRelated> WaterCells { get; set; }

    public void SetWaterBreak(WaterNodeManager controller);

    public void WaterDivertion();

}
