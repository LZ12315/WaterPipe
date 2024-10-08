using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager instance;
    private Camera mainCamera;

    private List<IInteractable_OBJ> selectedObjects = new List<IInteractable_OBJ>();

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

            if (Input.GetMouseButtonDown(2) && mouseButton == MouseButton.None)
            {
                mouseButton = MouseButton.Right;
                HandleMouseClick();
            }

            if (mouseDragCounter < minMouseDragTime)
                mouseDragCounter += Time.deltaTime;
            else
                isMousePressing = true;

            mouseOriginPos = Input.mousePosition;
        }

        if ((Input.GetMouseButtonUp(0) && mouseButton == MouseButton.Left) || (Input.GetMouseButtonUp(2) && mouseButton == MouseButton.Right))
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
            IInteractable_OBJ obj = selectedObject.GetComponent<IInteractable_OBJ>();
            if (obj != null)
            {
                obj.ReceiveInteraction(mouseButton);
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
            IInteractable_OBJ obj = selectedObject?.GetComponent<IInteractable_OBJ>();
            if (obj != null)
            {
                if (!selectedObjects.Contains(obj))
                {
                    obj.ReceiveInteraction(mouseButton);
                    selectedObjects.Add(obj);
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
    }

    public void AddToSelectedObjects(IInteractable_OBJ newObject)
    {
        selectedObjects.Add(newObject);
    }
}

