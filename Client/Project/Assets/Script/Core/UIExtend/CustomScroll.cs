using System;
using System.Collections;
using System.Collections.Generic;
using CSF;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomScroll : ScrollRect
{

    public bool IsLock = false;

    bool _enableDrag = true; //拖拽

    /// <summary>拖拽</summary>
    public bool EnableDrag
    {
        get => _enableDrag;
        set
        {
            _enableDrag = value;
            horizontal  = value;
            vertical    = value;
        }
    }

    [HideInInspector] public Vector2 screenSize;


    bool _enableZoom = true; //拖拽

    /// <summary>缩放</summary>
    public bool EnableZoom
    {
        get { return _enableZoom; }
        set { _enableZoom = value; }
    }

    [HideInInspector] public Action BGMoveAction;
    [HideInInspector] public Action DragAction;
    [HideInInspector] public Action ZoomAction;
    [HideInInspector] public Action UpdateAction;

    public RectTransform imgFarBG;
    public RectTransform imgCloseBG;

    protected override void Awake()
    {
        if (!Application.isPlaying)
            return;
        base.Awake();


        CanvasScaler canvasScaler = Mgr.UI.canvas.GetComponent<CanvasScaler>();
        screenSize.x = canvasScaler.referenceResolution.x;
        screenSize.y = screenSize.x * Screen.height / Screen.width;
        
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        //if (!IsEnabledDrag || IsLookAt) return; //禁用拖动
        //FrozenScrollRect(true);
        //if (Input.touchCount > 1)
        //{
        //    return;
        //}
        base.OnBeginDrag(eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        //if (!IsEnabledDrag || IsLookAt) return; //禁用拖动
        //if (Input.touchCount > 1)
        //{
        //    touchNum = Input.touchCount;
        //    return;
        //}
        //else if (Input.touchCount == 1 && touchNum > 1)
        //{
        //    touchNum = Input.touchCount;
        //    base.OnBeginDrag(eventData);
        //    return;
        //}
        base.OnDrag(eventData);
    }

    void Update()
    {
        if (IsLock)
        {
            return;
        }
        //拖拽
        if (EnableDrag)
        {
            DragAction?.Invoke();
        }

        //缩放
        if (EnableZoom)
        {
            ZoomAction?.Invoke();
        }
        UpdateAction?.Invoke();
    }


    protected override void LateUpdate()
    {
        base.LateUpdate();
        BGMoveAction?.Invoke(); //背景错位移动
    }
}