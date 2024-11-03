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

        // �ڱ༭����ֹͣ����
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

}
