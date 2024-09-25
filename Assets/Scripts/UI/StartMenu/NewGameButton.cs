using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameButton : UIButton
{
    [Header("������")]
    public GameObject menuCanvas;

    public override void ExcutiveAction()
    {
        base.ExcutiveAction();
        menuCanvas.SetActive(true);
    }
}
