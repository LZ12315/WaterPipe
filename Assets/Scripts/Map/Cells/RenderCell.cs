using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RenderCell : Cell
{
    public override void ExcutiveAction()
    {
        base.ExcutiveAction();
    }

    public override void HandleSelection()
    {
        if(mouseButton == MouseButton.Left)
        {
            PlaceCell();
        }
    }

    private void PlaceCell()
    {
        if (BagManager.instance.ReturnCellOnHand() != null)
        {
            Cell newCell = BagManager.instance.ReturnCellOnHand();
            CellCover(newCell);
        }
    }
}

