using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    //以下是物体拖拽逻辑
    public bool isDragging=false;
    public Vector3 offset;
   
 

    private void OnMouseDown()
    {
        isDragging = true;

        offset =gameObject.transform.position-Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
    }

    void OnMouseDrag()
    {
        // 如果正在拖拽，则更新物体的位置  
        if (isDragging)
        {
            // 将鼠标的屏幕坐标转换为世界坐标，并减去偏移量以保持拖拽的一致性  
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane)) + offset;

            // 更新物体的位置  
            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        }
       
    }

    void OnMouseUp()
    {
        // 当鼠标释放时，停止拖拽  
        isDragging = false;
    }


    //以下是按键变换物体形状逻辑
    void Update()
    {
        if (isDragging&&(Input.GetKeyDown(KeyCode.RightArrow)))
        {
            transform.Rotate(Vector3.forward, 90f);
        }
        if(isDragging&&(Input.GetKeyDown(KeyCode.LeftArrow))) 
        {
            transform.Rotate(Vector3.forward, -90f);
                
        }
    }

}