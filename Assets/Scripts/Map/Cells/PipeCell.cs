using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCell : Cell
{

    public override void HandleSelection()
    {
        if (mouseButton == MouseButton.Right)
        {
            CellRotate(1);
        }
    }

    protected override void TeaseConnectedCells()
    {
        if (cushion.ReturnNearCushions().Count == 0)
            return;

        connectedCells.Clear();
        foreach (var cu in cushion.ReturnNearCushions())
        {
            if (ListComparison.HaveCommonElements(cu.ReturnCell().ReturnCellConnectors(), cellConnectors))
                connectedCells.Add(cu.ReturnCell());
        }

        WaterDiversion();
    }

    public override void CellInteract(Cell interactCell)
    {
        base.CellInteract(interactCell);
        if(!connectedCells.Contains(interactCell))
            TeaseConnectedCells();
        else
            WaterDiversion();
    }

    private void WaterDiversion()
    {
        foreach(var cell in connectedCells)
        {
            if (cell.ReturnIfContainsWater())
                OpenWater();
        }
        if (containsWater)
        {
            foreach (var cell in connectedCells)
            {
                if (!cell.ReturnIfContainsWater())
                    cell.CellInteract(this);
            }
        }

    }

    public void OpenWater()
    {
        containsWater = true;
        if (GetComponent<Transform>() != null)
        {
            Sequence scaleSequence = DOTween.Sequence();
            scaleSequence.Append(transform.DOScale(shrinkScale, duration).SetEase(Ease.OutBack));
            scaleSequence.Append(transform.DOScale(originScale, duration).SetEase(Ease.OutBack));
            scaleSequence.Play();
        }
    }
}
