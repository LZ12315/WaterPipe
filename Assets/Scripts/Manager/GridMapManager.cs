using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Cushion
{
    private GridMapManager gridMap;
    public Cell cell;
    public Vector2 corePos;

    private int column;
    private int row;
    private Cushion westCushion;
    private Cushion eastCushion;
    private Cushion northCushion;
    private Cushion southCushion;

    public void CushionInit(GridMapManager gridMap, Cell cell, Vector2 pos, int column, int row)
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
        if (cell != null)
        {
            GameObject.Destroy(cell.gameObject);
        }

        this.cell = newCell;
        newCell.gameObject.SetActive(true);
        newCell.CellInit(corePos, this, newCellDirection);
    }

    public void SetCushionEast(Cushion cushion)
    {
        eastCushion = cushion;
    }

    public void SetCushionSouth(Cushion cushion)
    {
        southCushion = cushion;
    }

    public void SetCushionWest(Cushion cushion)
    {
        westCushion = cushion;
    }

    public void SetCushionNorth(Cushion cushion)
    {
        northCushion = cushion;
    }
}

public class GridMapManager : MonoBehaviour
{
    private Cushion[,] gridArray;
    private List<Line> lineList;

    [Header("Õ¯∏Ò Ù–‘")]
    public int height;
    public int width;
    public float sideLength;
    public GameObject leftTopPoint;
    public Vector2 leftTopPos;

    [Header("ÃÓ≥‰ŒÔ")]
    public GameObject emptyCell;
    public GameObject line;

    private void Start()
    {
        GridInit(height, width);
        CreateGrid();
    }

    private void GridInit(int height, int width)
    {
        this.height = height;
        this.width = width;
        leftTopPos = leftTopPoint.transform.position;
        sideLength = GetNowSideLength();
        gridArray = new Cushion[height, width];
        lineList = new List<Line>();
    }

    private void CreateGrid()
    {
        CreateGridCell();
        CreateLine();
    }

    private void CreateGridCell()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Cushion newCushion = new Cushion();
                gridArray[i, j] = newCushion;

                GameObject newCell = Instantiate(emptyCell.gameObject);
                newCell.gameObject.SetActive(true);
                Cell newCellComp = newCell.GetComponent<Cell>();

                if (i > 0 && gridArray[i - 1,j] != null)
                {
                    newCushion.SetCushionNorth(gridArray[i - 1, j]);
                    gridArray[i - 1, j].SetCushionSouth(newCushion);
                }
                if (j > 0 && gridArray[i, j - 1] != null)
                {
                    newCushion.SetCushionWest(gridArray[i, j - 1]);
                    gridArray[i, j - 1].SetCushionEast(newCushion);
                }


                gridArray[i, j].CushionInit(this, newCellComp, CalculateCellPos(i, j), i, j);
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
        float side = emptyCell.GetComponent<Cell>().ReturnSideLength();
        Destroy(cell.gameObject);
        return side;
    }
}
