using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
    public List<LevelControlItem> levelLists;
    public Transform LevelTran;
    public List<string> levelState =new List<string>() { "1", "0", "0" };
    public void Start()
    {
        InitLevel();
       

    }

    public void InitLevel()
    {
       for(var i=0;i<levelLists.Count;i++)
        {
            levelLists[i].InitLevelItem(levelState[i]);
        }
        Debug.Log("初始化完成");
    }

    public void ChooseLevel(int levelid)
    {
        print("Goto Level" + levelid);
        SceneManager.LoadScene(levelid);

    }

    public void BackHome()
    {
        SceneManager.LoadScene(0);
    }
}
