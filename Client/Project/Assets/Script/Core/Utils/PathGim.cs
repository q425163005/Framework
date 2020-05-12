using System;
using System.Collections;
using System.Collections.Generic;
using CSF;
using DG.Tweening;
using UnityEngine;
using Random = System.Random;

public class PathGim : MonoBehaviour
{
    //private List<Transform> list = new List<Transform>();

    public void OnDrawGizmos()
    {
        List<Transform> list = new List<Transform>();
        foreach (Transform variable in this.transform)
        {
            list.Add(variable);
        }

        if (list.Count < 2)
            return;
        Gizmos.color = Color.red;
        for (int i = 1; i < list.Count; i++)
        {
            Gizmos.DrawLine(list[i - 1].position, list[i].position);
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(list[0].position, .6f * GetHandleSize(list[0].position));
        for (int i = 0; i < list.Count; i++)
        {
            if (i > 0)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(list[i].position, .3f * GetHandleSize(list[i].position));
            }
        }
    }

    public virtual float GetHandleSize(Vector3 pos)
    {
        float handleSize = 1f;
#if UNITY_EDITOR
        handleSize = UnityEditor.HandleUtility.GetHandleSize(pos) * 0.4f;
        handleSize = Mathf.Clamp(handleSize, 0, 1.2f);
#endif
        return handleSize;
    }

    public  Tweener        tween;
    private Transform      soldier;
    private SpriteRenderer spRender;
    private Animator       animator;


    public bool reverse = false;

    private Vector3[] WayPoint;

    private float MoveTime;

    /// <summary>运动类型</summary>
    public MoveType moveType;

    /// <summary>每一段路径方向</summary>
    public AniType[] aniTypeArry;

    private bool isStart = true;

    public int nowIndex = 0;

    public AniType testType;


    public void SetData(bool _reverse, Vector3[] _WayPoint, float _MoveTime, MoveType _moveType, AniType[] _aniTypeArry)
    {
        reverse     = _reverse;
        WayPoint    = _WayPoint;
        MoveTime    = _MoveTime;
        moveType    = _moveType;
        aniTypeArry = _aniTypeArry;
    }

    public void SetAni(Transform obj)
    {
        soldier  = obj;
        spRender = soldier.GetComponent<SpriteRenderer>();
        animator = soldier.GetComponent<Animator>();

        TweenParams parms = new TweenParams();
        parms.SetEase(Ease.Linear);
        parms.SetLoops(-1, LoopType.Yoyo);
        parms.OnStepComplete(() =>
        {
            switch (moveType)
            {
                case MoveType.Back:

                    SetMoveAni(!reverse ? aniTypeArry[nowIndex - 1] : aniTypeArry[nowIndex], !reverse);

                    //SetMoveAni(aniTypeArry[nowIndex], !reverse);
                    break;
                case MoveType.Wait:
                    SetIdleState(aniTypeArry[nowIndex], reverse);
                    Pause(UnityEngine.Random.Range(3, 5));
                    break;
            }

            reverse = !reverse;
        });
        parms.OnWaypointChange(OnWaypointChange);
        tween = soldier.DOLocalPath(WayPoint, MoveTime, PathType.Linear, PathMode.Ignore)
            .SetAs(parms);
    }

    private void OnWaypointChange(int index)
    {
        nowIndex = index;
        if (isStart)
        {
            isStart = false;
            SetMoveAni(aniTypeArry[index]);
        }
        else
        {
            if (index > 0 && index < WayPoint.Length - 1)
            {
                SetMoveAni(reverse ? aniTypeArry[index - 1] : aniTypeArry[index], reverse);
            }
        }
    }

    private void Pause(float seconds = 0f)
    {
        StopCoroutine(Wait());

        if (!gameObject.activeInHierarchy) return;//禁用了会报错
        tween?.Pause();
        if (seconds > 0)
            StartCoroutine(Wait(seconds));
    }

    private IEnumerator Wait(float secs = 0f)
    {
        yield return new WaitForSeconds(secs);
        Resume();
    }

    public void Resume()
    {
        StopCoroutine(Wait());

        SetMoveAni(reverse ? aniTypeArry[nowIndex - 1] : aniTypeArry[nowIndex], reverse);
        //SetMoveAni(aniTypeArry[nowIndex], reverse);
        tween?.Play();
    }


    void SetIdleState(AniType type, bool back = false)
    {
        int val = 0;
        switch (type)
        {
            case AniType.LeftDown:
                val            = back ? 3 : 2;
                spRender.flipX = false;
                break;
            case AniType.LeftUp:
                val            = back ? 2 : 3;
                spRender.flipX = true;
                break;
            case AniType.RightDown:
                val            = back ? 3 : 2;
                spRender.flipX = true;
                break;
            case AniType.RightUp:
                val            = back ? 2 : 3;
                spRender.flipX = false;
                break;
        }

        animator.SetInteger("Move", val);
    }

    void SetMoveAni(AniType type, bool back = false)
    {
        int val = 0;
        switch (type)
        {
            case AniType.LeftDown:
                val            = back ? 1 : 0;
                spRender.flipX = false;
                testType       = back ? AniType.RightUp : type;
                break;
            case AniType.LeftUp:
                val            = back ? 0 : 1;
                spRender.flipX = true;
                testType       = back ? AniType.RightDown : type;
                break;
            case AniType.RightDown:
                val            = back ? 1 : 0;
                spRender.flipX = true;
                testType       = back ? AniType.LeftUp : type;
                break;
            case AniType.RightUp:
                val            = back ? 0 : 1;
                spRender.flipX = false;
                testType       = back ? AniType.LeftDown : type;
                break;
        }

        animator.SetInteger("Move", val);
    }
}

public enum MoveType
{
    Back = 0, //走到尽头立刻回头
    Wait = 1, //走到尽头等待几秒，回头
}

public enum AniType
{
    LeftDown = 0, //左下
    LeftUp,       //左上
    RightDown,    //右下
    RightUp,      //右上
}