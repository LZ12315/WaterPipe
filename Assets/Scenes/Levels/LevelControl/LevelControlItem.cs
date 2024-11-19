using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControlItem : MonoBehaviour
{
    public GameObject LockTip;
    public GameObject MonIco;
    public LevelControl LevelControl;

    public GameSceneSO ConnectedScene;
    
    public void InitLevelItem(string state)
    {
        if (LevelControl == null)
        {
            LevelControl=FindObjectOfType<LevelControl>();
        }
        LockTip.SetActive(false);
        MonIco.SetActive(false);
        if (state == "0")
        {
            LockTip.SetActive(true);
            MonIco.SetActive(false);
        }
        else
        {
            LockTip.SetActive(false);
            MonIco.SetActive(true) ;
        }
    }

    public void ChoseLevel()
    {
        if (LevelControl == null)
        {
            LevelControl=GameObject.Find("MainController").gameObject.GetComponent<LevelControl>();
        }
        LevelControl.ChooseLevel(ConnectedScene);
    }
 
}
