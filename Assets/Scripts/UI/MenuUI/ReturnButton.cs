using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnButton : SceneLoadButton
{
    public override void ExcutiveAction()
    {
        sceneToLoad = SceneLoadManager.previousScene;
        sceneLoadEvent.RaiseSceneLoadEvent(sceneToLoad, fadeScreen, fadeTime);
    }
}
