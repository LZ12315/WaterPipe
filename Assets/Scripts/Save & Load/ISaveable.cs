using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// һ���ӿ� �������ݴ洢��صĲ��� ������Ҫ������Ķ���Ӧ��ʵ������ӿ�
/// ��Щ����������˳������Ϊ��������������Ƿ������ݴ洢��Ҫ����ĺ�������������ע�ᵽDataManager�ĺ��������������DataManagerע���ĺ���
/// �洢�Լ����ݵĺ����������Լ�֮ǰ�ϴ��õ����ݵĺ���
/// </summary>

public interface ISaveable
{
    public DataDefination GetDefination();

    public void RegisterSaveData() => DataManager.instance.savedObjects.Add(this);

    public void UnRegisterSaveData() => DataManager.instance.savedObjects.Remove(this);

    public void Save(SceneData data);

    public void Load(SceneData data);
}
