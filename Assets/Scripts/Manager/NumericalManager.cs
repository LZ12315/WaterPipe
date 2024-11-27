using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumericalManager : MonoBehaviour
{
    public static NumericalManager instance;

    [Header("局内评价数值")]
    [SerializeField] private double developmentnValue;
    [SerializeField] public double budgetValue;
    [SerializeField] private double contaminationValue;

    [Header("信息广播")]
    public DoubleValueEventSO developmentChangeEvent;
    public DoubleValueEventSO budgetChangeEvent;
    public DoubleValueEventSO contaminationChangeEvent;

    [Header("信息接收")]
    public LevelNumricalSO levelNumricalSO;
    public VoidEventSO afterSceneLoadInitEvent;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(instance);
            instance = this;
        }
    }

    private void OnEnable()
    {
        afterSceneLoadInitEvent.voidEvent += NumericalInit;
    }

    private void OnDisable()
    {
        afterSceneLoadInitEvent.voidEvent -= NumericalInit;
    }

    private void NumericalInit()
    {
        developmentnValue = levelNumricalSO.developmentnValue;
        budgetValue = levelNumricalSO.budgetValue;
        contaminationValue = levelNumricalSO.contaminationValue;
        UpdatePanel();
    }

    private void UpdatePanel()
    {
        developmentChangeEvent.RaiseEvent(developmentnValue);
        budgetChangeEvent.RaiseEvent(budgetValue);
        developmentChangeEvent.RaiseEvent(contaminationValue);
    }

    #region 数值改变接口

    public void ChangeDevelopment(INumricalChange sender,double value)
    {
        developmentnValue += value;
        developmentChangeEvent.RaiseEvent(developmentnValue);
    }

    public void ChangeBudget(INumricalChange sender, double value)
    {
        budgetValue += value;
        budgetChangeEvent.RaiseEvent(budgetValue);
    }

    public void ChangeContamination(INumricalChange sender, double value)
    {
        contaminationValue += value;
        contaminationChangeEvent.RaiseEvent(contaminationValue);
    }

    #endregion

    #region 值函数

    public double ReturnDevelopment()
    {
        return developmentnValue;
    }

    public double ReturnBudget()
    {
        return budgetValue;
    }

    public double ReturnContamination()
    {
        return contaminationValue;
    }

    #endregion
}
