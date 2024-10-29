using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    //������������ק�߼�
    public bool isDragging=false;
    public Vector3 offset;
   
 

    private void OnMouseDown()
    {
        isDragging = true;

        offset =gameObject.transform.position-Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
    }

    void OnMouseDrag()
    {
        // ���������ק������������λ��  
        if (isDragging)
        {
            // ��������Ļ����ת��Ϊ�������꣬����ȥƫ�����Ա�����ק��һ����  
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane)) + offset;

            // ���������λ��  
            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        }
       
    }

    void OnMouseUp()
    {
        // ������ͷ�ʱ��ֹͣ��ק  
        isDragging = false;
    }


    //�����ǰ����任������״�߼�
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