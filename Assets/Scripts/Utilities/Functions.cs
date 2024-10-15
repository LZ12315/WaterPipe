using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// 一个不搭载到任何物体上的脚本 负责写一些函数方法供开发者使用
/// 依次是：Direction类型字面值的扩展函数 比较列表中是否至少有一个相同元素的函数
/// </summary>


public static class DirectionExtensions
{
    public static CellDirection Rotate(this CellDirection direction, int num)
    {
        return (CellDirection)(((int)direction + num) % Enum.GetValues(typeof(CellDirection)).Length);
    }
}

public class ListComparison
{
    public static bool HaveCommonElements<T>(List<T> list1, List<T> list2)
    {
        return list1.Any(item => list2.Contains(item));
    }
}

