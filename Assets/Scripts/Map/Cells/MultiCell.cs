using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCell : Cell
{
    [Header("多方块管理")]
    public VoidEventSO afterMapInitializedEvent;
    [SerializeField]protected List<IDaughterCell> daughterCells = new List<IDaughterCell>();

    protected override void Awake()
    {
        base.Awake();
        afterMapInitializedEvent.voidEvent += AcceptCells;
    }

    public void AcceptCells()
    {
        List<Collider2D> overlappingColliders = new List<Collider2D>();
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.NoFilter();

        if (polygonCollider != null)
            polygonCollider.OverlapCollider(contactFilter, overlappingColliders);
        else if(boxCollider != null)
            boxCollider.OverlapCollider(contactFilter, overlappingColliders);

        foreach (var collider in overlappingColliders)
        {
            if(collider.GetComponent<IDaughterCell>() != null)
            {
                IDaughterCell newDaughterCell = collider.GetComponent<IDaughterCell>();
                newDaughterCell.GetAdhibition(this);
                daughterCells.Add(newDaughterCell);
            }
        }
    }
}
