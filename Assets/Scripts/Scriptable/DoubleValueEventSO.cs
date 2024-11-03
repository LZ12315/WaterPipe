using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "EventSO/DoubleEventSO")]
public class DoubleValueEventSO : ScriptableObject
{
    public UnityAction<double> doubleValueEvent;

    public void RaiseEvent(double value)
    {
        doubleValueEvent?.Invoke(value);
    }
}
