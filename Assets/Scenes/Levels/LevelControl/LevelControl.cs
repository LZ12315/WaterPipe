using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
    public static SceneLoadManager sceneLoadManager;
    public static NumericalManager numericalManager;

    public List<LevelControlItem> levelLists;
    public Transform LevelTran;
    public void Start()
    {
        if (sceneLoadManager == null)
        {
            sceneLoadManager = FindObjectOfType<SceneLoadManager>();
        }
        numericalManager = FindObjectOfType<NumericalManager>();
        InitLevel();
    }

    public void InitLevel()
    {
       for(var i=0;i<levelLists.Count;i++)
        {
            levelLists[i].InitLevelItem(numericalManager.levelState[i]);
        }
       
        Debug.Log("初始化完成");
    }

    public void ChooseLevel(GameSceneSO sceneToLoad)
    {
        sceneLoadManager.OnLoadScene(sceneToLoad, false, 0);

    }

    public void BackHome()
    {
        SceneManager.LoadScene(0);
    }
}
