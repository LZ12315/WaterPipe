using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// һ��SO�ļ� ������洢���� ����װ����һ��Action ����װ�������SO�ļ������嶼����ע�����Action �Ӷ���������Action���㲥��Ϣ Ҳ���Խ���Action�㲥����Ϣ
/// sceneLoadEvent ��һ��Action ���𳡾��л� ����װ�������SO�ļ��Ľű�������ʹ��һ������ע�ᵽAction�����չ㲥��Ϣ
/// RaiseSceneLoadEvent���� ��������sceneLoadEvent���㲥��Ϣ
/// </summary>

[CreateAssetMenu(menuName = "EventSO/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    public Action<GameSceneSO,bool,float> sceneLoadEvent;

    public void RaiseSceneLoadEvent(GameSceneSO sceneToLoad, bool fadeScreen, float fadeTime)
    {
        sceneLoadEvent?.Invoke(sceneToLoad,fadeScreen,fadeTime);
    }
}
