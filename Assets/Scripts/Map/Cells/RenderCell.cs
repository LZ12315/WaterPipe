using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RenderCell : Cell
{//1
    public override void ExcutiveAction()
    {
        base.ExcutiveAction();
    }

    public override void HandleSelection()
    {
        if (mouseButton == MouseButton.Left)
        {
            PlaceCell();
        }
    }

    private void PlaceCell()
    {
        if (BagManager.instance.ReturnCellOnHand() != null)
        {
            Cell newCell = BagManager.instance.ReturnCellOnHand();
            if(SelectionManager.instance.ReturnRecentSelectedCells() != null)
            {
                Cell formerPlacedCell = SelectionManager.instance.ReturnRecentSelectedCells();
                CellDirection newCellDirection = formerPlacedCell.returnCellDirection();
                CellCover(newCell, newCellDirection);
            }
            else
            {
                CellCover(newCell, CellDirection.North);
            }
        }
    }
}

