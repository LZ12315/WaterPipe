using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [Header("Õ¯∏Ò Ù–‘")]
    public int height;
    public int width;
    private Cell[,] gridArray;

    public void GridInit(int height, int width)
    {
        this.height = height;
        this.width = width;
        gridArray = new Cell[height,width];
    }
}
