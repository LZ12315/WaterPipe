using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumericalManager : MonoBehaviour
{
    public static NumericalManager instance;

    [Header("局内评价数值")]
    [SerializeField] private double developmentnValue;
    [SerializeField] public double budgetValue = 800000;
    [SerializeField] private double contaminationValue;

    [Header("信息广播")]
    public DoubleValueEventSO developmentChangeEvent;
    public DoubleValueEventSO budgetChangeEvent;
    public DoubleValueEventSO contaminationChangeEvent;

    [Header("信息接收")]
    public VoidEventSO afterSceneLoadInitEvent;

    [Header("关卡解锁状况")]
    public List<string> levelState = new List<string>() { "1", "0", "0" };

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this);
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
        developmentChangeEvent.RaiseEvent(developmentnValue);
        budgetChangeEvent.RaiseEvent(budgetValue);
        developmentChangeEvent.RaiseEvent(contaminationValue);
    }

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
}
