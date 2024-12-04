using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSource : Cell, IWaterRelated
{

    [Header("Ë®Ïà¹Ø")]
    public WaterNodeType waterNodeType;
    [SerializeField] private bool containsWater = true;
    [SerializeField] protected List<IWaterRelated> waterCells = new List<IWaterRelated>();

    private void Start()
    {
        containsWater = true;
        OnCellConnectChange += (value) =>
        {
            List<IWaterRelated> list = new List<IWaterRelated>();
            foreach (var cell in value)
            {
                IWaterRelated waterCell = cell.GetComponent<IWaterRelated>();
                list.Add(waterCell);
            }
            WaterCells = list;
            WaterNodeManager.Instance.NodeChange(this, waterCells);
        };
    }

    public override void CellInit(Vector2 pos, Cushion cushion, CellDirection cellDirection = CellDirection.North)
    {
        base.CellInit(pos, cushion, cellDirection);
        WaterNodeManager.Instance.AddNode(this, WaterCells);
    }

    public WaterNodeType NodeType { get => waterNodeType; }

    bool IWaterRelated.ContainsWater { get => containsWater; set => containsWater = value; }

    public List<IWaterRelated> WaterCells { get => waterCells; set => waterCells = value; }

    public void SetWaterBreak(WaterNodeManager controller)
    {
    }

    public void WaterDivertion()
    {
    }

}
