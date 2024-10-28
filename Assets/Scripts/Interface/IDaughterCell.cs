using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDaughterCell
{
    public Collider2D collider2D { get; set; }

    public void GetAdhibition(MultiCell parentCell);
}
