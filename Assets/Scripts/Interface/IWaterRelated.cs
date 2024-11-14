using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWaterRelated
{
    public List<IWaterRelated> WaterSources { get; set; }

    protected bool ContainsWater { get; set; }
}
