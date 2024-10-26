using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{//0
    public static SelectionManager instance;
    private Camera mainCamera;

    private Queue<IInteractable_OBJ> selectedObjects = new Queue<IInteractable_OBJ>();
    private Queue<Cell> recentSelectedCells = new Queue<Cell>();
    public Cell formerPlacedCell = null;

    [Header("鼠标选择参数")]
    private MouseButton mouseButton = MouseButton.None;
    public bool isMousePressing = false;
    private float minMouseDragTime = 0.5f;
    private float mouseDragCounter;
    private Vector2 mouseOriginPos;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        mainCamera = Camera.main;
    }

    void Update()
    {
        HandleMouseAction();
    }

    private void HandleMouseAction()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
            if (Input.GetMouseButtonDown(0) && mouseButton == MouseButton.None)
            {
                mouseButton = MouseButton.Left;
                HandleMouseClick();
            }

            if (Input.GetMouseButtonDown(1) && mouseButton == MouseButton.None)
            {
                mouseButton = MouseButton.Right;
                HandleMouseClick();
            }

            if (Input.GetMouseButtonDown(2) && mouseButton == MouseButton.None)
            {
                mouseButton = MouseButton.Middle;
                HandleMouseClick();
            }

            if (mouseDragCounter < minMouseDragTime)
                mouseDragCounter += Time.deltaTime;
            else
                isMousePressing = true;

            mouseOriginPos = Input.mousePosition;
        }

        if ((Input.GetMouseButtonUp(0) && mouseButton == MouseButton.Left) || 
            (Input.GetMouseButtonUp(1) && mouseButton == MouseButton.Right) || 
            (Input.GetMouseButtonUp(2) && mouseButton == MouseButton.Middle))
        {
            mouseButton = MouseButton.None;

            isMousePressing = false;
            mouseDragCounter = 0;

            mouseOriginPos = Input.mousePosition;
        }

        if (mouseButton != MouseButton.None && isMousePressing)
        {
            HandleMousePress();
        }
    }

    private void HandleMouseClick()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            GameObject selectedObject = hit.collider.gameObject;
            Cell cellObject = selectedObject?.GetComponent<Cell>();
            if (cellObject != null)
            {
                SelectObject(cellObject);
            }
            else
            {
                //这里用来写取消选中的逻辑 
            }
        }
    }

    private void HandleMousePress()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            GameObject selectedObject = hit.collider.gameObject;
            IInteractable_OBJ interactableObject = selectedObject?.GetComponent<IInteractable_OBJ>();
            Cell cellObject = selectedObject?.GetComponent<Cell>();
            if (interactableObject != null)
            {
                if (!selectedObjects.Contains(interactableObject))
                {
                    if(cellObject != null)
                    {
                        SelectObject(cellObject);
                    }
                    selectedObjects.Enqueue(interactableObject);
                }
                else
                {
                    //这里用来写已经被选中的物体被重新选中时的逻辑 比如取消物体被选中的状态
                }
            }
            else
            {
                //这里用来写取消选中的逻辑
            }
        }
        else
        {
            if(selectedObjects.Count != 0)
            {
                selectedObjects.Clear();
            }
        }
    }

    private void SelectObject(Cell objectToSelect)
    {
        objectToSelect.ReceiveInteraction(mouseButton);
        //if(recentSelectedCells.Count >= 2)
        //{
        //    recentSelectedCells.Clear();
        //    recentSelectedCells.Enqueue(objectToSelect);
        //}
        //else
        //    recentSelectedCells.Enqueue(objectToSelect);
    }

    public void SetFormerPlacedCell(Cell formerCell)
    {
        formerPlacedCell = formerCell;
    }

    public Cell ReturnRecentSelectedCells()
    {
        return formerPlacedCell;
    }

    public Queue<IInteractable_OBJ> ReturnSelectedObjects(int nums)
    {
        Queue<IInteractable_OBJ> queue = new Queue<IInteractable_OBJ>();
        IInteractable_OBJ obj = null;
        for (int i = 0; i < nums; i++)
        {
            obj = selectedObjects.Dequeue();
            queue.Enqueue(obj);
        }
        return queue;
    }
}

