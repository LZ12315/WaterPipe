using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RenderCell : Cell
{


    private void PlaceCell()
    {
        if (BagManager.instance.ReturnCellOnHand() != null)
        {
            Cell newCell = BagManager.instance.ReturnCellOnHand();
            CellCover(newCell);
        }
    }
}

