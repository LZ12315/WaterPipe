using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropPanel : MonoBehaviour
{
    public Image image;
    public VoidEventSO workCellSwitchEventSO;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        workCellSwitchEventSO.voidEvent += ChangePorpImage;
    }

    private void OnDisable()
    {
        workCellSwitchEventSO.voidEvent -= ChangePorpImage;
    }

    private void ChangePorpImage()
    {
        Sprite newImage = BagManager.instance.ReturnCellOnHand().ReturnCellSprite();
        image.sprite = newImage;
    }
}
