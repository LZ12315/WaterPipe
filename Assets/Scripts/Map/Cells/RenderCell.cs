using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RenderCell : Cell,IDaughterCell
{
    [Header("放置相关")]
    public RenderCellType renderCellType;


    public override void HandleSelection()
    {
        base.HandleSelection();
        if (mouseButton == MouseButton.Left)
        {
            PlacePropCell();
        }
    }

    public override void CellInteract(Cell interactCell)
    {
        base.CellInteract(interactCell);
        PlaceCell(interactCell);
    }

    private void PlacePropCell()
    {
        if (BagManager.instance.PlaceCell() != null)
        {
            bool canPlace = false;
            Cell newCell = BagManager.instance.PlaceCell();
            switch(renderCellType)
            {
                case RenderCellType.Water:
                    if(newCell.GetComponent<IPlaceable>().CanPlaceOnWater)
                        canPlace = true;
                    break;
                case RenderCellType.None:
                    canPlace = true;
                    break;
            }
            if(canPlace)
                CellCover(newCell);
        }
    }

    public void PlaceCell(Cell newCell)
    {
        CellCover(newCell);
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

