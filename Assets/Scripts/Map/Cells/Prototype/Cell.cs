using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

[System.Serializable]
public class Cell : MonoBehaviour, IInteractable_OBJ
{
    public BoxCollider2D boxCollider;
    public PolygonCollider2D polygonCollider;

    [Header("物体属性")]
    public Sprite cellSprite;
    public Vector2 resolution = new Vector2(100,100);
    private float sideLengthMean;
    private Vector2 sideLength;
    protected Cushion cushion;
    public CellDirection direction = CellDirection.North;
    public List<CellDirection> cellConnectors = new List<CellDirection>();

    [Header("动画表现")]
    public Vector3 enlargeScale = new Vector3(1.2f, 1.2f, 1.2f);
    public Vector3 shrinkScale = new Vector3(0.8f, 0.8f, 0.8f);
    public float duration = 0.2f;
    protected Vector3 originScale;
    protected Tween scaleTween;

    [Header("物体操作")]
    protected MouseButton mouseButton;
    public float rotateAngle = 90;

    [Header("引水相关")]
    public List<Cell> connectedCells = new List<Cell>();
    protected bool containsWater = false;

    protected virtual void Awake()
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

    public virtual void CellInit(Vector2 pos, Cushion cushion, CellDirection cellDirection = CellDirection.North)
    {
        this.cushion = cushion;
        gameObject.transform.position = pos;
        direction = cellDirection;
        sideLengthMean = Mathf.Sqrt(sideLength.x * sideLength.y);

        boxCollider = gameObject?.GetComponent<BoxCollider2D>();
        polygonCollider = gameObject?.GetComponent<PolygonCollider2D>();
        if(boxCollider != null)
            boxCollider.size = new Vector2(sideLengthMean, sideLengthMean);

        InitialCellRotate(cellDirection);
        TeaseConnectedCells();
    }

    protected virtual void CellCover(Cell newCell, CellDirection cellDirection)
    {
        Cell instantiatedCell = Instantiate(newCell, cushion.corePos, Quaternion.identity);
        SelectionManager.instance.SetFormerPlacedCell(instantiatedCell);
        instantiatedCell.gameObject.SetActive(false);

        if (cushion != null)
        {
            cushion.ChangeCell(instantiatedCell,cellDirection);
        }
    }

    private void InitialCellRotate(CellDirection initialCellDirection)
    {
        int rotateNum = 0;
        if (initialCellDirection != CellDirection.North)
            rotateNum = initialCellDirection - CellDirection.North;
        transform.Rotate(0, 0, rotateAngle * rotateNum);
        if (cellConnectors.Count > 0)
        {
            for (int i = 0; i < cellConnectors.Count; i++)
            {
                cellConnectors[i] = cellConnectors[i].Rotate(rotateNum);
            }
        }
    }

    protected virtual void CellRotate(int num)
    {
        direction = direction.Rotate(num);
        transform.Rotate(0, 0, num * rotateAngle);
        if(cellConnectors.Count > 0)
        {
            for (int i = 0; i < cellConnectors.Count; i++)
            {
                cellConnectors[i] = cellConnectors[i].Rotate(num);
            }
        }

        TeaseConnectedCells();
    }

    protected virtual void TeaseConnectedCells()
    {
        if(cushion.ReturnNearCushions().Count == 0 || cellConnectors.Count == 0)
            return;

        connectedCells.Clear();
        foreach (var cu in cushion.ReturnNearCushions())
        {
            Cell nearCell = cu.Value.ReturnCell();
            if(nearCell.ReturnCellConnectors().Count == 0)
                continue;

            CellDirection nearCellDir = cu.Key;
            if (!cellConnectors.Contains(nearCellDir))
                continue;

            foreach (CellDirection dir in nearCell.ReturnCellConnectors())
            {
                if (dir == nearCellDir.GetOppositeDirection())
                    connectedCells.Add(cu.Value.ReturnCell());
            }
        }
    }

    public virtual void CellInteract(Cell interactCell)
    {

    }

    #region 交互相关

    public void ReceiveInteraction(MouseButton mouseButton)
    {
        this.mouseButton = mouseButton;

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

    #endregion

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

    public List<CellDirection> ReturnCellConnectors()
    {
        return cellConnectors;
    }

    public Vector2 ReturnSideLength()
    {
        CalculateSide();
        return sideLength;
    }

    public CellDirection returnCellDirection()
    {
        return direction;
    }

    public bool ReturnIfContainsWater()
    {
        return containsWater;
    }

    #endregion
}
