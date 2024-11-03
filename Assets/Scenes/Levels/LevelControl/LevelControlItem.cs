using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControlItem : MonoBehaviour
{
    public GameObject LockTip;
    public GameObject MonIco;
    public LevelControl LevelControl;

    public int LevelID;
    
    public void InitLevelItem(string state)
    {
        LockTip.SetActive(false);
        MonIco.SetActive(false);
        if (state == "0")
        {
            LockTip.SetActive(true);
        }
        else
        {
            MonIco.SetActive(true) ;
        }
    }

    public void ChoseLevel()
    {
        if (LevelControl == null)
        {
            LevelControl=GameObject.Find("MainController").gameObject.GetComponent<LevelControl>();
        }
        LevelControl.ChooseLevel(LevelID);
    }
 
}
