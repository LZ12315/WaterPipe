using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSourceCell : Cell
{
    protected override void Awake()
    {
        base.Awake();
        containsWater = true;
    }
}
