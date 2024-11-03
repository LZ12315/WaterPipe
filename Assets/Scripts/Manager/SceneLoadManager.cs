using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

/// <summary>
/// һ��Manager�ű� ���𳡾�ת������߼� 
/// ����ת����������OnLoadScene���� ������ճ���ת������Ϣ �Լ�����ж�ؾɳ����������³��������������ȹ��ܵ���غ��� 
/// ��ѭ�۲���ģʽ��˼ά �ýű�װ���ڳ�����Persistent�������塰SceneLoadManager����
/// Persistent����ʼ�մ��� ���汣�������е�Manager ������Ϸ���ڲ��߼�ʵ��
/// </summary>

public class SceneLoadManager : MonoBehaviour
{
    [Header("����")]
    public GameSceneSO firstLoadScene;

    [Header("��Ϣ����")]
    public SceneLoadEventSO sceneLoadEvent;

    [Header("�㲥")]
    public VoidEventSO beforeUnLoadEvent;
    public VoidEventSO afterLoadEvent;

    [Header("�����л�")]
    private bool isLoading;
    public static GameSceneSO currentScene;
    public static GameSceneSO previousScene;
    private GameSceneSO sceneToLoad;

    [Header("���ض���")]
    private bool fadeScreen;
    private float fadeTime;

    private void Awake()
    {
        if(firstLoadScene != null)
        {
            OnLoadScene(firstLoadScene, false, 0);
        }
    }

    private void Start()
    {
        previousScene = firstLoadScene;
    }

    private void OnEnable()
    {
        sceneLoadEvent.sceneLoadEvent += OnLoadScene;
    }   

    private void OnDisable()
    {
        sceneLoadEvent.sceneLoadEvent -= OnLoadScene;
    }

    private void OnLoadScene(GameSceneSO sceneToLoad, bool fadeScreen, float fadeTime)
    {
        if(isLoading)
            return;

        isLoading = true;
        this.sceneToLoad = sceneToLoad;
        this.fadeScreen = fadeScreen;
        this.fadeTime = fadeTime;

        if(currentScene != null )
            StartCoroutine(UnLoadCurrentScene());
        else
            LoadNewScene();
    }

    private IEnumerator UnLoadCurrentScene()
    {
        if(fadeScreen)
        {
            //fade in
        }
        DataManager.instance.SaveSceneData();
        previousScene = currentScene;
        yield return currentScene.sceneReference.UnLoadScene();
        yield return new WaitForSeconds(fadeTime);
        LoadNewScene() ;
    }

    private void LoadNewScene()
    {
        var loadingOption = sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        loadingOption.Completed += OnLoadCompleted;
    }

    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> handle)
    {
        DataManager.instance.LoadSavedSceneData();
        currentScene = sceneToLoad;
        isLoading = false;
        if (fadeScreen)
        {
            //Fadeout
        }

        Scene sceneToActive = handle.Result.Scene;
        if (sceneToActive.IsValid())
        {
            SceneManager.SetActiveScene(sceneToActive);
        }
        afterLoadEvent.RaiseVoidEvent();
    }

}
