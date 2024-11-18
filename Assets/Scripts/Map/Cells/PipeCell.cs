using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCell : Cell, INumricalChange, IPlaceable, IWaterRelated
{
    [Header("发展数值")]
    public NumericalChangeType pollutionType;
    public double budgetValue;
    private bool hasChange;

    [Header("引水相关")]
    [SerializeField] protected List<IWaterRelated> waterSourcesList = new List<IWaterRelated>();
    [SerializeField] protected List<IWaterRelated> connectedWaterCells = new List<IWaterRelated>();

    private void Start()
    {
        canWrite = true;
    }

    public override void HandleSelection()
    {
        base.HandleSelection();

        if (mouseButton == MouseButton.Right)
        {
            CellRotate(1);
        }

        if (mouseButton == MouseButton.Middle)
        {
            RemoveCell();
        }
    }

    public override void CellInit(Vector2 pos, Cushion cushion, CellDirection cellDirection = CellDirection.North)
    {
        base.CellInit(pos, cushion, cellDirection);
        CellAlign();
        NumericalValueChange();
    }

    protected override void RemoveCell()
    {
        base.RemoveCell();
        NumericalValueReChange();
    }

    protected override void TeaseConnectedCells()
    {
        if (cushion.ReturnNearCushions().Count == 0 || cellConnectors.Count == 0)
            return;

        connectedCells.Clear();
        foreach (var cu in cushion.ReturnNearCushions())
        {
            Cell nearCell = cu.Value.ReturnWorkCell();
            if (nearCell.ReturnCellConnectors().Count == 0)
                continue;

            CellDirection nearCellDir = cu.Key;
            if (!cellConnectors.Contains(nearCellDir))
                continue;

            foreach (CellDirection dir in nearCell.ReturnCellConnectors())
            {
                if (dir == nearCellDir.GetOppositeDirection())
                {
                    connectedCells.Add(nearCell);
                }
            }
        }

        WaterDiversion();

        foreach (var cell in connectedCells)
        {
            cell.CellConnect(this);
        }
    }

    public override void CellConnect(Cell interactCell)
    {
        base.CellConnect(interactCell);

        if(interactCell.ReturnIfContainsWater())
        {
            if(!containsWater)
            {
                containsWater = true;
                if (!waterSources.Contains(interactCell))
                    waterSources.Add(interactCell);
                foreach (var cell in connectedCells)
                {
                    if (cell != interactCell)
                        cell.CellConnect(this);
                }
            }

            containsWater = true;
        }
    }

    public override void CellDisConnect(Cell cellToRemove ,Cell interactCell)
    {
        base.CellDisConnect(cellToRemove,interactCell);

        if(waterSources.Contains(cellToRemove))
            waterSources.Remove(cellToRemove);

        if (containsWater)
        {
            CheckIfCanGetWater();

            foreach (var cell in connectedCells)
            {
                if (cell != interactCell)
                    cell.CellDisConnect(cellToRemove,this);
            }
        }
    }

    public void CellInteract(IWaterRelated relatedWaterCell)
    {
        if(containsWater)
        {
            if(!relatedWaterCell.ContainsWater)
            {
                if(waterSourcesList.Contains(relatedWaterCell))
                    waterSourcesList.Remove(relatedWaterCell);

                if (waterSourcesList.Count == 0)
                    containsWater = false;

                foreach (var cell in connectedWaterCells)
                {
                    containsWater = false;
                    if (cell.ContainsWater && waterSourcesList.Contains(cell))
                        containsWater = true;
                    else
                        waterSourcesList.Remove(cell);
                }
            }
        }
    }

    #region 引水相关

    bool IWaterRelated.ContainsWater { get => containsWater; set => containsWater = value; }

    public List<IWaterRelated> WaterSources { get => waterSourcesList; set => waterSourcesList = value; }

    public List<IWaterRelated> ConnectedWaterCells { get => connectedWaterCells; set => connectedWaterCells = value; }

    private void WaterDiversion()
    {
        foreach (var cell in connectedCells)
        {
            if (cell.ReturnIfContainsWater())
            {
                containsWater = true;
                if(!waterSources.Contains(cell))
                    waterSources.Add(cell);
            }
        }
    }

    private bool CheckIfCanGetWater()
    {
        if(waterSources.Count == 0)
        {
            containsWater = false;
            return false;
        }

        foreach (var cell in connectedCells)
        {
            containsWater = false;
            if (cell.ReturnIfContainsWater() && waterSources.Contains(cell))
            {
                containsWater = true;
                return true;
            }
            else
                waterSources.Remove(cell);
        }

        return false;
    }

    #endregion

    #region 发展指数相关

    public void NumericalValueChange()
    {
        NumericalManager.instance.ChangeBudget(this,-budgetValue);
        hasChange = true;
    }

    public void NumericalValueReChange()
    {
        NumericalManager.instance.ChangeBudget(this, budgetValue);
        hasChange = false;
    }


    public float DevelopmentnValue { get => 0;}

    public float ContaminationValue { get => 0; }

    public double BudgetValue { get => budgetValue; }

    public bool isActive { get => hasChange; set => hasChange = value; }

    public NumericalChangeType numericalType { get => pollutionType; }

    #endregion

    #region  放置相关

    public void CellAlign()
    {
        if (cellConnectors.Count == 0 || cellConnectors.Count == 4)
            return;

        int index = 0;
        List<CellDirection> directions = new List<CellDirection>();
        int maxConnectedCells = connectedCells.Count;
        directions.Add(direction);
        for(int i=0; i<3; i++)
        {
            CellRotate(1);
            directions.Add(direction);
            if (connectedCells.Count > maxConnectedCells)
            {
                index = i + 1;
                maxConnectedCells = connectedCells.Count;
            }
        }
        CellRotate(direction.GetRotateNum(directions[index]));
    }

    public bool CanWrite { get => canWrite;}

    public bool CanPlaceOnWater { get => true; }

    #endregion

}
