using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIOutlet))]
/// <summary>
/// UI界面风格控制组件
/// </summary>
public class UISkin : MonoBehaviour
{
    public List<SkinInfo> dicSkinInfo=new List<SkinInfo>();

    public int currSkinId=0;
    [SerializeField]
    private SkinInfo currSkin;

    public SkinInfo CurrSkin
    {
        get
        {
            foreach (var item in dicSkinInfo)
            {
                if (item.id == currSkinId)
                    return item;
            }
            return null;
        }
        set => currSkin = value; }
}
[System.Serializable]
/// <summary>皮肤信息 </summary>
public class SkinInfo
{
    public int id;
    public int skinName;
    public List<RectInfo> objRectInfo =new List<RectInfo>();
    
}
[System.Serializable]
/// <summary>
/// 对象的矩阵信息
/// </summary>
public class RectInfo
{
    /// <summary> 对象 </summary>
    public GameObject gameObject;
    public Vector3 anchoredPosition;
    public Vector2 anchorMax;
    public Vector2 anchorMin;
    public Vector2 sizeDelta;
    public Vector2 pivot;
    public bool IsShow = false;
    public RectInfo(GameObject obj)
    {
        gameObject = obj;
        RefreshTramsform();
    }
    public void RefreshTramsform()
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError($"请添加正确的对象[{gameObject.name}]");
            return;
        }

        anchoredPosition = rectTransform.anchoredPosition;
        anchorMax = rectTransform.anchorMax;
        anchorMin = rectTransform.anchorMin;
        sizeDelta = rectTransform.sizeDelta;
        pivot = rectTransform.pivot;

    }

    public void ApplyRectTramsform()
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();

        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.anchorMax = anchorMax;
        rectTransform.anchorMin =anchorMin;
        rectTransform.sizeDelta = sizeDelta;
        rectTransform.pivot = pivot;
    }

}
