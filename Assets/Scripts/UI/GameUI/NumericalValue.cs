using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumericalValue : MonoBehaviour
{
    public Text textMeshPro;
    public DoubleValueEventSO doubleEventSO;

    private void Awake()
    {
        textMeshPro = GetComponent<Text>();
    }

    private void OnEnable()
    {
        doubleEventSO.doubleValueEvent += ValueChange;
    }

    private void OnDisable()
    {
        doubleEventSO.doubleValueEvent -= ValueChange;
    }

    private void ValueChange(double value)
    {
        textMeshPro.text = value.ToString();
    }
}
