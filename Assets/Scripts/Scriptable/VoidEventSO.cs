using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// һ��SO�ļ� ������洢���� ����װ����һ��Action ����װ�������SO�ļ������嶼����ע�����Action �Ӷ���������Action���㲥��Ϣ Ҳ���Խ���Action�㲥����Ϣ
/// voidEvent ��һ��Action ���������κβ��� Ҳ����˵������Ϊ�˹㲥��Ϣ ����װ�������SO�ļ��Ľű�������ʹ��һ������ע�ᵽAction�����չ㲥��Ϣ
/// RaiseVoidEvent���� ��������voidEvent���㲥��Ϣ
/// </summary>

[CreateAssetMenu(menuName = "EventSO/VoidEventSO")]
public class VoidEventSO : ScriptableObject
{
    public Action voidEvent;

    public void RaiseVoidEvent()
    {
        voidEvent?.Invoke();
    }
}
