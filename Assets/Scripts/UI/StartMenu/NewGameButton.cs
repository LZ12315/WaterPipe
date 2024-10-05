using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameButton : UIButton
{
    [Header("Ŀ�곡��������")]
    public string TargetSceneName="";

    private void Start()
    {
        SceneManager.LoadScene(TargetSceneName);
    }
}
