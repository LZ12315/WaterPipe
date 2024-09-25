using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// һ��Manager�ű� �������ݱ�����ز���
/// ������SaveSceneData��LoadSavedSceneData���� ǰ�߸������л�����ǰ�洢�ɳ��������� ���߸�������³����洢�õ�����
/// ��ѭ�۲���ģʽ��˼ά �ýű�װ���ڳ�����Persistent�������塰DataManager����
/// Persistent����ʼ�մ��� ���汣�������е�Manager ������Ϸ���ڲ��߼�ʵ��
/// </summary>

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    [Header("���ݱ���")]
    public List<ISaveable> savedObjects = new List<ISaveable>();
    public SceneData NowSceneData;


    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    public void SaveSceneData()
    {
        if (savedObjects.Count <= 0)
            return;

        foreach(var saveables in savedObjects)
        {
            if(saveables.GetDefination().persistentType == PersistentType.ReadWrite)
                saveables.Save(NowSceneData);
        }
    }

    public void LoadSavedSceneData()
    {
        if (savedObjects.Count <= 0)
            return;

        foreach (var saveables in savedObjects)
        {
            if (saveables.GetDefination().persistentType == PersistentType.ReadWrite)
                saveables.Load(NowSceneData);
        }
    }

    public void SaveArchiveData()
    {

    }

    public void LoadSavedArchiveData()
    {

    }
}
