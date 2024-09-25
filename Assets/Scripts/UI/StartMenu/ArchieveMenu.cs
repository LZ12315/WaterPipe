using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArchieveMenu : MonoBehaviour
{
    [Header("����ƶ�")]
    public Vector2 targetPos;
    public float moveDuration;
    private RectTransform rectTransform;

    private void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.DOAnchorPos(targetPos, moveDuration).SetEase(Ease.OutCubic);
    }
}
