using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReTryButton : SceneLoadButton
{
    public override void ExcutiveAction()
    {
        sceneToLoad = SceneLoadManager.currentScene;
        base.ExcutiveAction();
    }
}
