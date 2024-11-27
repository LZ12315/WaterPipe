using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumericalManager : MonoBehaviour
{
    public static NumericalManager instance;

    [Header("����������ֵ")]
    [SerializeField] private double developmentnValue;
    [SerializeField] public double budgetValue;
    [SerializeField] private double contaminationValue;

    [Header("��Ϣ�㲥")]
    public DoubleValueEventSO developmentChangeEvent;
    public DoubleValueEventSO budgetChangeEvent;
    public DoubleValueEventSO contaminationChangeEvent;

    [Header("��Ϣ����")]
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

    #region ��ֵ�ı�ӿ�

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

    #region ֵ����

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
