using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable_OBJ
{
    public void ReceiveInteraction(MouseButton mouseButton);

    public bool CheckIfInteractable();

    public void ExcutiveAction();
}
