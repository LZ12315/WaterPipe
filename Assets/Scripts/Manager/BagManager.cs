using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagManager : MonoBehaviour
{
    public static BagManager instance;

    [Header("鼠标输入检测")]
    private float scrollInput;

    [Header("信息接收")]
    public VoidEventSO afterGridInitEventSO;
    public DoubleValueEventSO budgetChangeEvent;

    [Header("消息广播")]
    public VoidEventSO workCellSwitchEventSO;

    [Header("物品使用")]
    public List<Cell> workCells = new List<Cell>();
    private int index = 0;
    public Cell nowCell;
    private bool canPlace = true;
    private double nowBudgetNum;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        nowCell = workCells[index];
    }

    private void OnEnable()
    {
        afterGridInitEventSO.voidEvent += UpdateWorkCellImage;
        budgetChangeEvent.doubleValueEvent += GetBudgetUpdate;
    }

    private void OnDisable()
    {
        afterGridInitEventSO.voidEvent -= UpdateWorkCellImage;
        budgetChangeEvent.doubleValueEvent -= GetBudgetUpdate;
    }

    private void Update()
    {
        GetMouseScroll();
    }

    #region 物体放置

    private void GetMouseScroll()
    {
        scrollInput = Input.GetAxis("Mouse ScrollWheel");
        SwitchCell(scrollInput);
    }

    private void SwitchCell(float changeDir)
    {
        if (changeDir == 0)
            return;

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

        UpdateWorkCellImage();
    }

    private void UpdateWorkCellImage()
    {
        workCellSwitchEventSO.RaiseVoidEvent();
    }

    public Cell PlaceCell()
    {
        double cellBudget = nowCell.GetComponent<INumricalChange>().BudgetValue;
        if(cellBudget <= nowBudgetNum && canPlace)
            return nowCell;
        else
            return null;
    }

    #endregion

    #region 信息接收&值函数

    private void GetBudgetUpdate(double value)
    {
        nowBudgetNum = value;
    }

    public Cell ReturnNowCell()
    {
        return nowCell;
    }

    #endregion

}
