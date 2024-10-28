using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagManager : MonoBehaviour
{
    public static BagManager instance;

    [Header("物品使用")]
    public List<Cell> workCells = new List<Cell>();
    private int index = 0;
    public Cell nowCell;

    [Header("鼠标输入检测")]
    private float scrollInput;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        nowCell = workCells[index];
    }

    private void Update()
    {
        GetMouseScroll();
    }


    private void GetMouseScroll()
    {
        scrollInput = Input.GetAxis("Mouse ScrollWheel");
        SwitchCell(scrollInput);
    }

    private void SwitchCell(float changeDir)
    {
        if(changeDir < 0)
        {
            if (index == workCells.Count - 1)
                index = 0;
            else
                index++;

            nowCell = workCells[index];
        }
        else if (changeDir > 0)
        {
            if (index == 0)
                index = workCells.Count - 1;
            else
                index--;

            nowCell = workCells[index];
        }
    }

    public Cell ReturnCellOnHand()
    {
        return nowCell;
    }
}
