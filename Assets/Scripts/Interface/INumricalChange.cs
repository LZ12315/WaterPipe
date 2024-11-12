using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INumricalChange
{
    public float DevelopmentnValue { get;}

    public float ContaminationValue { get;}

    public double BudgetValue { get;}

    public bool isActive { get; set; }

    public NumericalChangeType numericalType { get;}

    public void NumericalValueChange();

    public void NumericalValueReChange();
}
