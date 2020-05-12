using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEffectSort : MonoBehaviour
{
    public bool IsAutoOrder = true;
    public int Order = 0;
    public void Start()
    {
        //SetOrder(Order);
        sort();
    }
    /// <summary>
    /// 重新设置排序
    /// </summary>
    /// <param name="order"></param>
    public void SetOrder(int order)
    {
        IsAutoOrder = false;        
        Order = order;
        sort();
    }
    public void sort()
    {
        if (IsAutoOrder) //自动跟据父对象的Canvas层级进行设置
        {
            Canvas canvas = GetComponentInParent<Canvas>();
            if (canvas != null)
                Order = canvas.sortingOrder + 1;
        }
        Renderer[] renders = GetComponentsInChildren<Renderer>();
        foreach (Renderer render in renders)
            render.sortingOrder = Order;
    }
#if UNITY_EDITOR
    void OnValidate()
    {
        sort();
    }
#endif
}