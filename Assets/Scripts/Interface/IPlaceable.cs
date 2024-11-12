using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlaceable
{
    public bool CanWrite {  get; set; }
    public void Align();
}
