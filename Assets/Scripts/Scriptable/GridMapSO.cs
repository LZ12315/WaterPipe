using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = ("LevelSO/GridMapSO"))]
[System.Serializable]
public class GridMapSO : ScriptableObject
{
    [SerializeField]
    public int[,] gridMap;
    public Dictionary<int, Cell> instruction;
}
