using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Cell : MonoBehaviour, IInteractable_OBJ
{
    public BoxCollider2D boxCollider;

    [Header("��������")]
    public Sprite cellSprite;
    public int resolution = 312;
    private float sideLength;
    protected Cushion cushion;

    public CellDirection direction = CellDirection.North;
    public List<CellDirection> cellConnectors = new List<CellDirection>();

    [Header("��������")]
    public Vector3 enlargeScale = new Vector3(1.2f, 1.2f, 1.2f);
    public Vector3 shrinkScale = new Vector3(0.8f, 0.8f, 0.8f);
    public float duration = 0.2f;
    protected Vector3 originScale;
    protected Tween scaleTween;

    [Header("�������")]
    protected MouseButton mouseButton;
    public float rotateAngle = 90;

    [Header("��ˮ")]
    protected List<Cell> connectedCells = new List<Cell>();
    [SerializeField] protected bool containsWater = false;

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

    public virtual void CellInit(Vector2 pos, Cushion cushion, CellDirection newCellDirection = CellDirection.North)
    {
        this.cushion = cushion;
        gameObject.transform.position = pos;
        direction = newCellDirection;

        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        boxCollider.size = new Vector2(sideLength, sideLength);
        if (newCellDirection != CellDirection.North)
            transform.Rotate(0, 0, rotateAngle * (newCellDirection - CellDirection.North));

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

    protected virtual void CellRotate(int num)
    {
        direction = direction.Rotate(num);
        transform.Rotate(0, 0, num * rotateAngle);
        if(cellConnectors.Count > 0)
        {
            for (int i = 0; i < cellConnectors.Count; i++)
                cellConnectors[i].Rotate(num);
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
            if(ListComparison.HaveCommonElements(cu.ReturnCell().ReturnCellConnectors(),cellConnectors))
                connectedCells.Add(cu.ReturnCell());
        }
    }

    public virtual void CellInteract(Cell interactCell)
    {

    }

    #region �������

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

    #region ��������

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

    #region ֵ����

    public void CalculateSide()
    {
        sideLength = resolution / cellSprite.pixelsPerUnit;
    }

    public List<CellDirection> ReturnCellConnectors()
    {
        return cellConnectors;
    }

    public float ReturnSideLength()
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
