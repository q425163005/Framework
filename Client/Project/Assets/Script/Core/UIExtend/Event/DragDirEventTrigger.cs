using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using CSF;
using UnityEngine.Events;
using System;
/// <summary>
/// 方位拖动事件
/// </summary>
public class DragDirEventTrigger : UIBehaviour, IPointerDownHandler, IPointerUpHandler,  IPointerClickHandler, IDragHandler
{

    private bool isPointerDown = false;
    private bool isDragTriggered = false;

    public float DragMinDis = 12;
    public Action<Vector2Int> onDrag;
    public Action onClick;
    private Vector2 startPostion;
    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
        startPostion = eventData.position;
        isDragTriggered = false;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        //CLog.Error((startPostion - eventData.position).magnitude);
        if (!isPointerDown) return;
        Vector2 dir = eventData.position - startPostion;
        if (dir.sqrMagnitude > DragMinDis * DragMinDis)
        {
            var angle = Vector2.SignedAngle(Vector2.up, dir);
            Vector2Int dragDir = Vector2Int.up;  
            if (angle >= -45 && angle < 45) //上
            {
                //左上角为起点，所以上下对换了一下
                dragDir = Vector2Int.down;
            }
            else if (angle >= 45 && angle <= 135)//左
            {
                dragDir = Vector2Int.left;
            }
            else if (angle > -135 && angle <= -45)//右
            {
                dragDir = Vector2Int.right;
            }
            onDrag?.Invoke(dragDir);
            isPointerDown = false;
            isDragTriggered = true;
        }
    }


    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    isPointerDown = false;
    //    isDragTriggered = false;
    //}

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isDragTriggered)
        {
            onClick?.Invoke();
            isPointerDown = false;
            isDragTriggered = false;
        }
    }
}