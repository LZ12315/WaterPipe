using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// һ�������ص��κ������ϵĽű� ����дһЩ����������������ʹ��
/// �����ǣ�Direction��������ֵ����չ���� �Ƚ��б����Ƿ�������һ����ͬԪ�صĺ���
/// </summary>


public static class DirectionExtensions
{
    public static CellDirection Rotate(this CellDirection direction, int num)
    {
        return (CellDirection)(((int)direction + num) % Enum.GetValues(typeof(CellDirection)).Length);
    }

    public static CellDirection GetOppositeDirection(this CellDirection direction)
    {
        return direction.Rotate(2);
    }

    public static int GetRotateNum(this CellDirection direction, CellDirection targetDirection)
    {
        int value = Enum.GetValues(typeof(CellDirection)).Length;
        return (targetDirection > direction) ? targetDirection - direction : value - (direction - targetDirection);
    }
}

public class ListComparison
{
    public static bool HaveCommonElements<T>(List<T> list1, List<T> list2)
    {
        return list1.Any(item => list2.Contains(item));
    }
}

