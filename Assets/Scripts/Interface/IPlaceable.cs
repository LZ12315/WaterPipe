using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlaceable
{
    public bool CanWrite {  get;}

    public bool CanPlaceOnWater {  get;}

    public void CellAlign();
}
