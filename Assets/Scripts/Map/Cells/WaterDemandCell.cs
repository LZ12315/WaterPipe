using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDemandCell : Cell
{
    public override void CellInteract(Cell interactCell)
    {
        base.CellInteract(interactCell);
        if (interactCell.ReturnIfContainsWater())
            GetWater();
    }
}
