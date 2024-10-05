using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameButton : UIButton
{
    [Header("目标场景的名字")]
    public string TargetSceneName="";

    private void Start()
    {
        SceneManager.LoadScene(TargetSceneName);
    }
}
