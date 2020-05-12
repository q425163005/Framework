using System;
using System.Collections;
using System.Collections.Generic;
using CSF;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MapScrollRect : ScrollRect
{
    //public RectTransform myRectTransform;
    /// <summary>
    /// 锚点
    /// </summary>
    public RectTransform anchorsPoint;

    /// <summary>
    /// 缩放因子,控制缩放速度
    /// </summary>
    public float scaleUnit = 0.005f;

    public float MaxScale = 2.0f; //缩放最大值
    float        MaxScaleOffces;
    public float MinScale = 1f; //缩放最小值
    float        MinScaleOffces;
    public float scaleOffset = 0.2f;


    private float   distance     = 0;
    private float   NewDistance  = 0;
    bool            IsStartScale = false;
    bool            IsInitScale  = false;
    private Vector3 WorldPos;
    float           autoInitSpeed = 5;

    public bool IsEnabledDrag = true; //是否启用拖动

    /// <summary>
    /// 屏蔽滑动缩放
    /// </summary>
    public bool IsLock = false;

    public bool IsLookAt = false;

    /// <summary>
    /// 看向物体前 的位置
    /// </summary>
    Vector3 LookAtBeforeConnetPos;

    /// <summary>
    /// 看向物体前 的位置
    /// </summary>
    Vector3 LookAtBeforeConnetScale;

    int touchNum;

    /// <summary>
    /// 可视区域
    /// </summary>
    public RectTransform visualArea;

    /// <summary>
    /// 可视区域2个对角点的世界坐标
    /// </summary>
    Vector2[] WorldPosVisualArea = {new Vector2(-2.7f, -5.1f), new Vector2(2.6f, 4.8f)};

    bool IsReadVisualAreaPos = false;


    /// <summary>
    /// 时间控制0移动到建筑时间，副本定点关卡时间1
    /// </summary>
    public float[] times;


    public delegate void ZoomDelegate(Vector3 value);

    public ZoomDelegate _zoomCallBack = null;

    /*========远景错位模块=======*/
    public RectTransform Vista;
    Vector2              VistaPos;
    public RectTransform CloseShot;
    Vector2              CloseShotPos;
    public float         VistaMoveSpeed     = 0.3f;
    public float         CloseShotMoveSpeed = 3.3f;

    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (!IsEnabledDrag || IsLookAt) return; //禁用拖动
        FrozenScrollRect(true);
        if (Input.touchCount > 1)
        {
            return;
        }

        base.OnBeginDrag(eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (!IsEnabledDrag || IsLookAt) return; //禁用拖动
        if (Input.touchCount > 1)
        {
            touchNum = Input.touchCount;
            return;
        }
        else if (Input.touchCount == 1 && touchNum > 1)
        {
            touchNum = Input.touchCount;
            base.OnBeginDrag(eventData);
            return;
        }

        base.OnDrag(eventData);
    }

    protected override void Awake()
    {
        if (!Application.isPlaying)
            return;
        base.Awake();
        MaxScaleOffces = MaxScale + scaleOffset;
        MinScaleOffces = MinScale - scaleOffset;
        if (content.Find("anchorsPoint") == null)
        {
            Debug.LogError("请设置锚点");
            this.enabled = false;
            return;
        }

        anchorsPoint = content.Find("anchorsPoint").GetComponent<RectTransform>();
        if (Vista != null)
            VistaPos = Vista.anchoredPosition;
        if (CloseShot != null)
            CloseShotPos = CloseShot.anchoredPosition;
    }

    void Update()
    {
        //测试
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    UnLookAt();
        //}

        //手指缩放
        //if (Input.touchCount == 2 && !IsInitScale && !IsLock)
        //{
        //    return;
        //    Touch   t1    = Input.GetTouch(0);
        //    Touch   t2    = Input.GetTouch(1);
        //    Vector2 t1Pos = t1.position;
        //    Vector2 t2Pos = t2.position;
        //    NewDistance = Vector2.Distance(t1Pos, t2Pos);
        //    if (!IsStartScale)
        //        GetTargetPos(t1Pos, t2Pos);

        //    float OffsetDistance = NewDistance - distance;
        //    content.localScale += new Vector3(OffsetDistance * scaleUnit, OffsetDistance * scaleUnit,
        //        OffsetDistance                               * scaleUnit);
        //    if (content.localScale.x >= MaxScaleOffces)
        //        content.localScale = new Vector3(MaxScaleOffces, MaxScaleOffces, MaxScaleOffces);
        //    if (content.localScale.x <= MinScaleOffces)
        //        content.localScale = new Vector3(MinScaleOffces, MinScaleOffces, MinScaleOffces);
        //    distance = NewDistance;
        //    MoveContent();
        //    _zoomCallBack(content.localScale);
        //}
        //else
        //{
        //    IsStartScale  = false;
        //    autoInitSpeed = 0;
        //    //判断是否需要重置到临界值
        //    if (content.localScale.x > MaxScale)
        //    {
        //        IsInitScale   = true;
        //        autoInitSpeed = -5;
        //    }
        //    else if (content.localScale.x < MinScale)
        //    {
        //        IsInitScale   = true;
        //        autoInitSpeed = 5;
        //    }
        //}

        autoInitSpeed = 0;
        //判断是否需要重置到临界值
        if (content.localScale.x > MaxScale)
        {
            IsInitScale   = true;
            autoInitSpeed = -5;
        }
        else if (content.localScale.x < MinScale)
        {
            IsInitScale   = true;
            autoInitSpeed = 5;
        }

        //还原到最大 最小缩放
        if (IsInitScale)
        {
            content.localScale += new Vector3(autoInitSpeed * scaleUnit, autoInitSpeed * scaleUnit,
                autoInitSpeed                               * scaleUnit);
            if (content.localScale.x >= MinScale && autoInitSpeed > 0)
            {
                IsInitScale        = false;
                content.localScale = new Vector3(MinScale, MinScale, MinScale);
            }

            if (content.localScale.x <= MaxScale && autoInitSpeed < 0)
            {
                IsInitScale        = false;
                content.localScale = new Vector3(MaxScale, MaxScale, MaxScale);
            }

            _zoomCallBack(content.localScale);
            MoveContent();
        }

        MisplacedMovement();
    }

    /// <summary>
    /// 错位移动
    /// </summary>
    void MisplacedMovement()
    {
        if (!Application.isPlaying)
            return;
        if (Vista == null || CloseShot == null)
            return;
        //获取当前移动的距离
        float ConPosX  = Math.Abs(content.anchoredPosition.x);
        float ConPosY  = Math.Abs(content.anchoredPosition.y);
        float ConScale = content.localScale.x;
        Vista.anchoredPosition = new Vector2((content.anchoredPosition.x + 654) / 2.84f,
            VistaPos.y - (ConPosY * (VistaMoveSpeed / ConScale)) / 2.0f);

        CloseShot.anchoredPosition = new Vector2((content.anchoredPosition.x + 654) / 5.28f,
            CloseShotPos.y - (ConPosY * (CloseShotMoveSpeed / ConScale)) / 2.0f);
    }

    /// <summary>
    /// 冻结滑条
    /// </summary>
    /// <param name="i"></param>
    void FrozenScrollRect(bool i)
    {
        vertical   = i;
        horizontal = i;
    }

    void MoveContent()
    {
        //计算出锚点现在的世界坐标
        Vector3 CurrWorldPos = anchorsPoint.position;
        Vector3 Offces       = CurrWorldPos - WorldPos;
        content.position -= Offces;
    }

    /// <summary>
    /// 确定两点之间的中间点
    /// </summary>
    /// <param name="Pos01"></param>
    /// <param name="Pos02"></param>
    void GetTargetPos(Vector2 Pos01, Vector2 Pos02)
    {
        Vector2 v2        = Pos01     - Pos02;
        Vector2 InputPos  = v2 * 0.5f + Pos02;
        Vector2 TargetPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(content, InputPos, Camera.main, out TargetPos);
        anchorsPoint.anchoredPosition = TargetPos;
        WorldPos                      = anchorsPoint.position;
        IsStartScale                  = true;

        distance = Vector2.Distance(Pos01, Pos02);
    }

    /// <summary>
    /// 看向某个物体 Rect物体的可视区域
    /// </summary>
    public void MoveToTarget(RectTransform target, Action moveEndAction = null)
    {
        //最佳可视区域
        if (visualArea != null && !IsReadVisualAreaPos)
        {
            IsReadVisualAreaPos = true;
            Rect visualRect = visualArea.rect;
            // Debug.Log("visualRect" + visualRect);
            WorldPosVisualArea[0] = new Vector2(-visualRect.width / 2, -visualRect.height / 2);
            WorldPosVisualArea[1] = new Vector2(visualRect.width  / 2, visualRect.height  / 2);
            TransformPoint(WorldPosVisualArea, visualArea);
        }

        FrozenScrollRect(false);
        //将目标的可视区域的4个点转换成世界坐标
        Rect RectTatget = target.rect;
        //由于target的中心点位于坐下 左下  右上
        Vector2[] WorldPosTarget = new Vector2[2];
        WorldPosTarget[0] = new Vector2(0, 0);
        WorldPosTarget[1] = new Vector2(RectTatget.width, RectTatget.height);
        TransformPoint(WorldPosTarget, target);
        //与可视区域的4个点对比
        //判断X轴是否需要移动
        Vector2 MovePos = Vector2.zero;
        if (WorldPosTarget[0].x < WorldPosVisualArea[0].x)
            MovePos.x += WorldPosVisualArea[0].x - WorldPosTarget[0].x; //X轴的左
        if (WorldPosTarget[1].x > WorldPosVisualArea[1].x)
            MovePos.x -= WorldPosTarget[1].x - WorldPosVisualArea[1].x; //X轴的右
        //Y轴
        if (WorldPosTarget[0].y < WorldPosVisualArea[0].y)
            MovePos.y += WorldPosVisualArea[0].y - WorldPosTarget[0].y; //Y轴的下
        if (WorldPosTarget[1].y > WorldPosVisualArea[1].y)
            MovePos.y -= WorldPosTarget[1].y - WorldPosVisualArea[1].y; //Y轴的上
        //content.
        if (new Vector3(MovePos.x, MovePos.y, 0) == Vector3.zero)
        {
            moveEndAction?.Invoke();
        }
        else
        {
            Vector2 screen = Mgr.UI.canvas.GetComponent<RectTransform>().sizeDelta;

            content.DOMove(content.position + new Vector3(MovePos.x, MovePos.y, 0), times[0])
                .OnComplete(() => moveEndAction?.Invoke());
        }

        //content.position += new Vector3(MovePos.x, MovePos.y,0);
    }

    /// <summary>
    /// 看向某个点 缩放同时移动到屏幕中央
    /// </summary>
    public void LookAt(RectTransform target)
    {
        IsLookAt = true;
        FrozenScrollRect(true);
        //计算出需要移动的距离
        anchorsPoint.position = target.position;
        Vector3 currScale = content.localScale;
        LookAtBeforeConnetScale = content.localScale;

        content.localScale = new Vector3(MaxScale, MaxScale, MaxScale);
        Vector3 TargetPos = anchorsPoint.position;
        content.localScale = currScale;

        LookAtBeforeConnetPos = content.position;
        content.DOScale(Vector3.one * MaxScale, times[1]);
        //CLog.Error(content.position, TargetPos, content.position - TargetPos);
        var movePosition = (content.position - TargetPos) + new Vector3(Screen.width/2, Screen.height/2, 0);
        content.DOMove(movePosition,
            times[1]).OnComplete(() => { MMove(content.anchoredPosition);});
    }

    async void MMove(Vector2 pos)
    {
        
        int index = 0;
        while (index<10)
        {
            index++;
            content.anchoredPosition = pos;
            await new WaitForUpdate();
        }
       
    }


    /// <summary>
    /// 解除锁定
    /// </summary>
    public void UnLookAt()
    {
        if (!IsLookAt)
            return;
        IsLookAt = false;
        content.DOMove(LookAtBeforeConnetPos, times[1]);
        content.DOScale(LookAtBeforeConnetScale, times[1]);
    }

    public void TestLookAtOnClickBtn(RectTransform rect)
    {
        LookAt(rect);
    }


    public void TestOnClickBtn(RectTransform rect)
    {
        MoveToTarget(rect);
    }

    void TransformPoint(Vector2[] Points, Transform transform)
    {
        for (int i = 0; i < Points.Length; i++)
        {
            Points[i] = transform.TransformPoint(Points[i]);
        }
    }
}