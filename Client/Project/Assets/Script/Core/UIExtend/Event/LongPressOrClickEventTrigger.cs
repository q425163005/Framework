using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LongPressOrClickEventTrigger : UIBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerClickHandler
{
    [Tooltip("How long must pointer be down on this object to trigger a long press")]
    public float durationThreshold = 0.7f;
    public float moveDisCancel = 10;


    public float longPressMinTime = 0.15f;  
    private bool isClick = true;

    public UnityEvent onLongPress = new UnityEvent();
    public UnityEvent onClick = new UnityEvent();

    public UnityEvent onDown = new UnityEvent();
    public UnityEvent onUp = new UnityEvent();

    private bool isPointerDown = false;
    private bool longPressTriggered = false;
    private float timePressStarted;

    private bool longDownTriggered = false;

    private void Update()
    {
        if (isPointerDown && !longPressTriggered)
        {
            if (Time.time - timePressStarted > durationThreshold)
            {
                longPressTriggered = true;
                onLongPress.Invoke();
            }
            if (Time.time - timePressStarted > longPressMinTime && !longDownTriggered)
            {
                longDownTriggered = true;
                onDown.Invoke();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        timePressStarted = Time.time;
        isPointerDown = true;
        longPressTriggered = false;
        longDownTriggered = false;
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Vector2.Distance(eventData.delta, Vector2.zero) > moveDisCancel)
        {
            isPointerDown = false;
            onUp.Invoke();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerDown = false;
        onUp.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!longPressTriggered)
        {
            onClick.Invoke();
            isPointerDown = false;
        }
    }
}