using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadButton : UIButton
{
    [Header("Ŀ�곡��")]
    public SceneLoadEventSO sceneLoadEvent;
    public GameSceneSO sceneToLoad;
    public bool fadeScreen;
    public float fadeTime;

    public override void ExcutiveAction()
    {
        base.ExcutiveAction();
        sceneLoadEvent.RaiseSceneLoadEvent(sceneToLoad, fadeScreen, fadeTime);
    }
}
