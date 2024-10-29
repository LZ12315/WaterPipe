using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : UIButton
{
    public override void ExcutiveAction()
    {
        base.ExcutiveAction();
        Quit();
    }

    public void Quit()
    {
        Application.Quit();

        // ÔÚ±à¼­Æ÷ÖÐÍ£Ö¹²¥·Å
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

}
