using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCell : Cell
{
    public override void HandleSelection()
    {
        if (mouseButton == MouseButton.Right)
        {
            CellRotate(1);
        }
    }
}
