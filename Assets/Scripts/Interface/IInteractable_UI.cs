using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// һ���ӿ� ������ҽ�����صĲ��� ���г������塢UI�����ȶ�Ӧ��ʵ������ӿ�
/// ������������˳������Ϊ�������⽻���¼��ĺ���������Ƿ���Խ����ĺ���������������Ϊ�ĺ���
/// </summary>

public interface IInteractable_UI
{
    public void ReceiveInteraction();

    public bool CheckIfInteractable();

    public void ExcutiveAction();
}
