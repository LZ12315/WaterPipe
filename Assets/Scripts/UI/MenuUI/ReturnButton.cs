using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnButton : SceneLoadButton
{
    public override void ExcutiveAction()
    {
        sceneToLoad = SceneLoadManager.previousScene;
        Debug.Log(SceneLoadManager.previousScene.name);
        sceneLoadEvent.RaiseSceneLoadEvent(sceneToLoad, fadeScreen, fadeTime);
    }
}
