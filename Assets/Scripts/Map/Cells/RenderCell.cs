using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RenderCell : Cell,IDaughterCell
{
    public override void HandleSelection()
    {
        base.HandleSelection();
        if (mouseButton == MouseButton.Left)
        {
            PlacePipeCell();
        }
    }

    public override void CellInteract(Cell interactCell)
    {
        base.CellInteract(interactCell);
        PlaceCell(interactCell);
    }

    private void PlacePipeCell()
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

    public void PlaceCell(Cell newCell)
    {
        CellCover(newCell, CellDirection.North);
    }

    #region 子物体设置

    Collider2D IDaughterCell.collider2D { get => boxCollider; set => boxCollider = (BoxCollider2D)value; }

    public void GetAdhibition(MultiCell parentCell)
    {
        boxCollider.enabled = false;
        containsWater = true;
        for (int i = 0; i < 4; i++)
        {
            cellConnectors.Add((CellDirection)i);
        }
    }

    #endregion
}

