using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageScrol : MonoBehaviour, IDragHandler
{
    public Transform Target;
    public float speed = 1;
    public bool Horizontal;
    public bool Vertical;
    public void OnDrag(PointerEventData eventData)
    {
        //拖拽旋转图片
        SetDraggedRotation(eventData);
    }

    private void SetDraggedRotation(PointerEventData eventData)
    {
        if (Horizontal)
        {
            Target.localRotation = Quaternion.Euler(0, -eventData.delta.x * speed, 0) * Target.localRotation;
        }
        if (Vertical)
        {
            Target.localRotation = Quaternion.Euler(eventData.delta.y * speed, 0, 0) * Target.localRotation;
        }
    }
}