using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour, IInteractable_OBJ
{
    public BoxCollider2D boxCollider;

    [Header("物体属性")]
    public Sprite cellSprite;
    public int resolution = 312;
    private float sideLength;
    private Cushion cushion;

    [Header("动画表现")]
    public Vector3 enlargeScale = new Vector3(1.2f, 1.2f, 1.2f);
    public Vector3 shrinkScale = new Vector3(0.8f, 0.8f, 0.8f);
    public float duration = 0.2f;
    private Vector3 originScale;
    private Tween scaleTween;

    [Header("物体操作")]
    protected MouseButton mouseButton;

    private void Awake()
    {
        CalculateSide();
    }

    private void OnEnable()
    {
        originScale = transform.localScale;
    }

    protected virtual void OnDestroy()
    {
        if (scaleTween != null && scaleTween.IsActive())
            scaleTween.Kill();
    }

    public virtual void CellInit(Vector2 pos, Cushion cushion)
    {
        this.cushion = cushion;
        gameObject.transform.position = pos;
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        boxCollider.size = new Vector2(sideLength, sideLength);
    }

    protected virtual void CellCover(Cell newCell)
    {
        Cell instantiatedCell = Instantiate(newCell, cushion.corePos, Quaternion.identity);
        instantiatedCell.gameObject.SetActive(false);

        if (cushion != null)
        {
            cushion.ChangeCell(instantiatedCell);
        }
    }

    public void ReceiveInteraction(MouseButton mouseButton)
    {
        if (CheckIfInteractable())
            ExcutiveAction();
    }

    public virtual bool CheckIfInteractable()
    {
        return true;
    }

    public virtual void ExcutiveAction()
    {
        HandleSelection();
    }

    public virtual void HandleSelection()
    {

    }

    #region 动画表现

    private void OnMouseEnter()
    {
        if (GetComponent<Transform>() != null)
            scaleTween = transform.DOScale(enlargeScale, duration).SetEase(Ease.OutBack);
    }

    private void OnMouseExit()
    {
        if (GetComponent<Transform>() != null)
            scaleTween = transform.DOScale(originScale, duration).SetEase(Ease.OutBack);
    }

    #endregion

    #region 值函数

    public void CalculateSide()
    {
        sideLength = resolution / cellSprite.pixelsPerUnit;
    }

    public float ReturnSideLength()
    {
        CalculateSide();
        return sideLength;
    }

    #endregion
}
