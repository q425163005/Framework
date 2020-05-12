using UnityEngine;
using System.Collections;

public static class ExtensionMethods
{

    //Even though they are used like normal methods, extension
    //methods must be declared static. Notice that the first
    //parameter has the 'this' keyword followed by a Transform
    //variable. This variable denotes which class the extension
    //method becomes a part of.
    public static void ResetTransformation(this Transform trans)
    {
        trans.position = Vector3.zero;
        trans.localRotation = Quaternion.identity;
        trans.localScale = new Vector3(1, 1, 1);
    }

    //visible
    public static void SetVisible(this GameObject obj, bool isVisible)
    {
        bool useActive = false;
        var trans = obj.transform;
        if (useActive)
        {
            if (Vector3.zero == trans.localScale)
                trans.localScale = Vector3.one;
            obj.SetActive(isVisible);
        }
        else
        {
            if (!obj.activeSelf)
                obj.SetActive(true);
            if (isVisible)
            {
                trans.localScale = Vector3.one;
            }
            else
            {
                trans.localScale = Vector3.zero;
            }
        }
    }
    ///// <summary>
    ///// 把Z轴移到相机外，进行隐藏(慎用,某些子对象使用时Drawcall会变高)
    ///// </summary>
    //public static void SetVisibleZ(this GameObject obj, bool isVisible)
    //{
    //    var trans = obj.transform;
    //    if (isVisible)
    //    {
    //        trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y, 0);
    //    }
    //    else
    //    {
    //        trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y, -100000);
    //    }
    //}
    //public static bool IsVisibleZ(this GameObject obj)
    //{
    //    return obj.transform.localPosition.z != -100000;
    //}

    public static bool IsVisible(this GameObject obj)
    {
        return obj.transform.localScale != Vector3.zero;
    }

}
