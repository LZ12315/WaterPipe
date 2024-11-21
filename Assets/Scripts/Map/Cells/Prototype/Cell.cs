using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Cell : MonoBehaviour, IInteractable_OBJ
{
    public BoxCollider2D boxCollider;
    public PolygonCollider2D polygonCollider;

    [Header("λ������")]
    protected Cushion cushion;
    public CellDirection direction = CellDirection.North;
    public CellAltitude altitude;
    public List<CellDirection> cellConnectors = new List<CellDirection>();
    [SerializeField] protected List<Cell> connectedCells = new List<Cell>();

    [Header("��ͼ���")]
    public Sprite cellSprite;
    public Vector2 resolution = new Vector2(100,100);
    private float sideLengthMean;
    private Vector2 sideLength;

    [Header("��������")]
    public Vector3 enlargeScale = new Vector3(1.2f, 1.2f, 1.2f);
    public Vector3 shrinkScale = new Vector3(0.8f, 0.8f, 0.8f);
    public float duration = 0.2f;
    protected Vector3 originScale;
    protected Tween scaleTween;

    [Header("�������")]
    public bool canRotate = true;
    public bool canWrite;
    protected MouseButton mouseButton;
    public float rotateAngle = 90;

    //[Header("��ˮ���")] //�ȴ��ع� ��Щ���Բ�Ӧ���ڸ�����
    //[SerializeField] protected List<Cell> waterSources = new List<Cell>();
    //[SerializeField] protected bool containsWater = false;

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

    //public virtual void CellInit(Vector2 pos, Cushion cushion)
    //{
    //    this.cushion = cushion;
    //    gameObject.transform.position = pos;
    //    sideLengthMean = Mathf.Sqrt(sideLength.x * sideLength.y);

    //    boxCollider = gameObject?.GetComponent<BoxCollider2D>();
    //    polygonCollider = gameObject?.GetComponent<PolygonCollider2D>();
    //    if (boxCollider != null)
    //        boxCollider.size = new Vector2(sideLengthMean, sideLengthMean);

    //    InitialCellRotate(direction);
    //    TeaseConnectedCells();
    //}

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

    protected virtual void CellCover(Cell newCell, CellDirection cellDirection = CellDirection.North)
    {
        Cell instantiatedCell = Instantiate(newCell, cushion.corePos, Quaternion.identity);
        SelectionManager.instance.SetFormerPlacedCell(instantiatedCell);
        instantiatedCell.gameObject.SetActive(false);

        if (cushion != null)
        {
            cushion.ChangeCell(instantiatedCell,cellDirection);
        }
    }

    protected virtual void RemoveCell()
    {
        cushion.RemoveCell();
        foreach (var cell in connectedCells)
            cell.CellDisConnect(this, this);
        Destroy(gameObject);
    }

    private void InitialCellRotate(CellDirection initialCellDirection)
    {
        int rotateNum = 0;
        if (initialCellDirection != CellDirection.North)
            rotateNum = initialCellDirection - CellDirection.North;
        transform.Rotate(0, 0, rotateAngle * -rotateNum);
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
        if (!canRotate)
            return;
        direction = direction.Rotate(num);
        transform.Rotate(0, 0, num * -rotateAngle);
        if(cellConnectors.Count > 0)
        {
            for (int i = 0; i < cellConnectors.Count; i++)
            {
                cellConnectors[i] = cellConnectors[i].Rotate(num);
            }
        }

        foreach (var cell in connectedCells)
            cell.CellDisConnect(this, this);

        TeaseConnectedCells();
    }

    protected virtual void TeaseConnectedCells()
    {
        if(cushion.ReturnNearCushions().Count == 0 || cellConnectors.Count == 0)
            return;

        connectedCells.Clear();
        foreach (var cu in cushion.ReturnNearCushions())
        {
            Cell nearCell = cu.Value.ReturnWorkCell();
            CellDirection nearCellDir = cu.Key;
            if(nearCell.ReturnCellConnectors().Count == 0 || !cellConnectors.Contains(nearCellDir) || connectedCells.Contains(nearCell))
                continue;

            foreach (CellDirection dir in nearCell.ReturnCellConnectors())
            {
                if (dir == nearCellDir.GetOppositeDirection())
                {
                    connectedCells.Add(nearCell);
                    nearCell.CellConnect(this);
                    break;
                }
            }
        }
    }

    public virtual void CellConnect(Cell interactCell)
    {
        if(!connectedCells.Contains(interactCell))
            connectedCells.Add(interactCell);
    }

    public virtual void CellDisConnect(Cell cellToRemove, Cell interactCell)
    {
        if(connectedCells.Contains(cellToRemove))
            connectedCells.Remove(cellToRemove);
        TeaseConnectedCells();
    }

    #region �������

    public void ReceiveInteraction(MouseButton mouseButton)
    {
        this.mouseButton = mouseButton;

        if(CheckIfInteractable())
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

    public virtual void CellInteract(Cell interactCell)
    {

    }

    #endregion

    #region ��������

    protected virtual void OnMouseEnter()
    {
        if (GetComponent<Transform>() != null)
            scaleTween = transform.DOScale(enlargeScale, duration).SetEase(Ease.OutBack);
    }

    private void OnMouseExit()
    {
        if (GetComponent<Transform>() != null)
            scaleTween = transform.DOScale(originScale, duration).SetEase(Ease.OutBack);
    }

    protected void OnInteractAnim()
    {
        if (GetComponent<Transform>() != null)
        {
            Sequence scaleSequence = DOTween.Sequence();
            scaleSequence.Append(transform.DOScale(shrinkScale, duration).SetEase(Ease.OutBack));
            scaleSequence.Append(transform.DOScale(originScale, duration).SetEase(Ease.OutBack));
            scaleSequence.Play();
        }
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

    public Vector2 ReturnSideLength()
    {
        CalculateSide();
        return sideLength;
    }

    public CellDirection returnCellDirection()
    {
        return direction;
    }

    public Sprite ReturnCellSprite()
    {
        return cellSprite;
    }

    public CellAltitude ReturnCellAltitude()
    {
        return altitude;
    }

    #endregion
}
