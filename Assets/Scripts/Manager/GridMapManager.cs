using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Pipeline;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Cushion
{
    private Cushion[,] gridMap;
    public Cell cell;
    public Vector2 corePos;

    public int column;
    public int row;
    private Dictionary<CellDirection, Cushion> nearCushions = new Dictionary<CellDirection, Cushion>();

    public void CushionInit(Cushion[,] gridMap, Cell cell, Vector2 pos, int column, int row)
    {
        this.gridMap = gridMap;
        this.cell = cell;
        corePos = pos;
        cell.CellInit(pos, this);
        this.column = column;
        this.row = row;
    }

    public void ChangeCell(Cell newCell, CellDirection newCellDirection)
    {
        if (cell != null && cell.boxCollider != null)
        {
            cell.boxCollider.enabled = false;
        }

        cell = newCell;
        newCell.gameObject.SetActive(true);
        newCell.CellInit(corePos, this, newCellDirection);
    }

    public void SetNearCushion(Cushion cushion, CellDirection direction)
    {
        nearCushions[direction] = cushion;
    }

    public Cell ReturnCell()
    {
        return cell;
    }

    public Dictionary<CellDirection, Cushion> ReturnNearCushions()
    {
        return nearCushions;
    }

    //public void SetCushionEast(Cushion cushion)
    //{
    //    eastCushion = cushion;
    //    nearCushions[(int)CellDirection.East] = cushion;
    //    //nearCushions.Add(cushion);
    //}

    //public void SetCushionSouth(Cushion cushion)
    //{
    //    southCushion = cushion;
    //    nearCushions[(int)CellDirection.South] = cushion;
    //}

    //public void SetCushionWest(Cushion cushion)
    //{
    //    westCushion = cushion;
    //    nearCushions[(int)CellDirection.West] = cushion;
    //}

    //public void SetCushionNorth(Cushion cushion)
    //{
    //    northCushion = cushion;
    //    nearCushions[(int)CellDirection.North] = cushion;
    //}
}

public class GridMapManager : MonoBehaviour
{
    public GridMapSO gridMapSO;
    private Cushion[,] Map;
    private List<Line> lineList;

    [Header("网格属性")]
    public int height;
    public int width;
    public float sideLength;
    public GameObject leftTopPoint;
    public Vector2 leftTopPos;

    [Header("填充物")]
    public GameObject emptyCell;
    public GameObject line;

    [Header("事件广播")]
    public VoidEventSO afterInitializedEvent;

    private void Start()
    {
        height = gridMapSO.height;
        width = gridMapSO.width;
        GridInit(height, width);
        CreateGrid();
    }

    private void GridInit(int height, int width)
    {
        this.height = height;
        this.width = width;
        leftTopPos = leftTopPoint.transform.position;
        sideLength = GetNowSideLength();
        Map = new Cushion[height, width];
        lineList = new List<Line>();
    }

    private void CreateGrid()
    {
        CreateGridCell();
        CreateLine();
        afterInitializedEvent.RaiseVoidEvent();    
    }

    private void CreateGridCell()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Cushion newCushion = new Cushion();
                Map[i, j] = newCushion;

                GameObject newCell = Instantiate(gridMapSO.ReturnMapCell(i,j).gameObject);
                newCell.gameObject.SetActive(true);
                Cell newCellComp = newCell.GetComponent<Cell>();

                if (i > 0 && Map[i - 1, j] != null)
                {
                    newCushion.SetNearCushion(Map[i - 1, j], CellDirection.North);
                    Map[i - 1, j].SetNearCushion(newCushion,CellDirection.South);
                }
                if (j > 0 && Map[i, j - 1] != null)
                {
                    newCushion.SetNearCushion(Map[i, j - 1],CellDirection.West);
                    Map[i, j - 1].SetNearCushion(newCushion, CellDirection.East);
                }

                Map[i, j].CushionInit(Map, newCellComp, CalculateCellPos(i, j), i, j);
            }
        }
    }

    private void CreateLine()
    {
        for (int i = 0; i <= height; i++)
        {
            GameObject newLine = Instantiate(line);
            Line newLineComp = newLine?.GetComponent<Line>();

            Vector2 startPos = new Vector2(leftTopPos.x, leftTopPos.y - sideLength * i);
            Vector2 endPos = new Vector2(startPos.x + sideLength * width, startPos.y);

            newLineComp.LineInit(startPos, endPos);
            newLineComp.DrawLine();
            //newLineComp.ChangeLineAnimeState(LineAnimeState.DisAppear);
            lineList.Add(newLineComp);
        }
        for (int i = 0; i <= width; i++)
        {
            GameObject newLine = Instantiate(line);
            Line newLineComp = newLine.GetComponent<Line>();

            Vector2 startPos = new Vector2(leftTopPos.x + i * sideLength, leftTopPos.y);
            Vector2 endPos = new Vector2(startPos.x, startPos.y - sideLength * height);

            newLineComp.LineInit(startPos, endPos);
            newLineComp.DrawLine();
            //newLineComp.ChangeLineAnimeState(LineAnimeState.DisAppear);
            lineList.Add(newLineComp);
        }
    }

    public void CellCover(Cushion cushion, Cell newCell)
    {

    }

    private Vector2 CalculateCellPos(int i, int j)
    {
        float x = leftTopPos.x + j * sideLength + sideLength / 2;
        float y = leftTopPos.y - i * sideLength - sideLength / 2;
        return new Vector2(x, y);
    }

    private float GetNowSideLength()
    {
        GameObject cell = Instantiate(emptyCell.gameObject);
        float side = Mathf.Sqrt(emptyCell.GetComponent<Cell>().ReturnSideLength().x * emptyCell.GetComponent<Cell>().ReturnSideLength().y);
        Destroy(cell.gameObject);
        return side;
    }
}
