using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// һ��SO�ļ� ����洢��Ϸ��������Ҫ������������
/// sceneDataNumber�����������Լ������ĸ��浵������ ���������ֵ为��洢����
/// </summary>

[CreateAssetMenu (menuName = "DataSO/SceneData")]
public class SceneData : ScriptableObject
{
    public ArchieveNumber sceneDataNumber;

    public Dictionary<string, string> stringDictionary = new Dictionary<string, string>();
    public Dictionary<string, bool> boolDictionary = new Dictionary<string, bool>();
}
