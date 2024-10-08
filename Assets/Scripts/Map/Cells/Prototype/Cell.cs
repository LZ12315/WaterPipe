using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
public class Cell : MonoBehaviour, IInteractable_OBJ
=======
public class Cell : MonoBehaviour
>>>>>>> 4b26caab92ea37db4c76086622a04e9e55c5f812
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

    private void Awake()
    {
        CalculateSide();
    }

<<<<<<< HEAD
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
=======
    public virtual void CellInit(Vector2 pos,Cushion cushion)
>>>>>>> 4b26caab92ea37db4c76086622a04e9e55c5f812
    {
        this.cushion = cushion;
        gameObject.transform.position = pos;
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        boxCollider.size = new Vector2(sideLength, sideLength);
    }

    public void CellCover(Cell newCell)
    {
        Instantiate(newCell);
        newCell.gameObject.SetActive(false);

        if (cushion != null ) 
        {
            cushion.ChangeCell(newCell);
        }

        Destroy(gameObject);
    }

    #region 动画表现

    private void OnMouseEnter()
    {
<<<<<<< HEAD
        if (GetComponent<Transform>() != null)
            scaleTween = transform.DOScale(enlargeScale, duration).SetEase(Ease.OutBack);
=======
        originScale = transform.localScale;
        transform.DOScale(enlargeScale, duration).SetEase(Ease.OutBack);
>>>>>>> 4b26caab92ea37db4c76086622a04e9e55c5f812
    }

    private void OnMouseExit()
    {
        transform.DOScale(originScale, duration).SetEase(Ease.OutBack);
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
