using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Canvas自适应
/// </summary>
[ExecuteInEditMode]
public class CanvasAdaptive : MonoBehaviour {

    // Use this for initialization
    private AspectRatioFitter aspect;
    private CanvasScaler canvas;

    //流海切割高度
    public int CutoutsHeight = 0;
    //底部割高度
    public int CutoutsBottonHeight = 0;
    //固定1280高度的缩放值,把1280固定高度缩放满屏
    [HideInInspector]
    public float HightScale = 1;
    void Awake()
    {
        canvas = gameObject.GetComponent<CanvasScaler>();
        aspect = gameObject.GetComponent<AspectRatioFitter>();
        if (aspect == null)
            aspect = gameObject.AddComponent<AspectRatioFitter>();
        adaptive();

#if !UNITY_EDITOR    
        var cutouts = Screen.cutouts;   
        if (cutouts.Length > 0)
        {
            foreach (var c in cutouts)
                CutoutsHeight = (int)Mathf.Max(CutoutsHeight, c.height);
        }
        if (CutoutsHeight > 100) CutoutsHeight = 100;

        CutoutsBottonHeight = CutoutsHeight/2;
#endif
    }

    void adaptive()
    {
        float whRatio = Screen.width / (float)Screen.height;
        if (whRatio < 0.5625f)  //宽高比小于720*1280
        {
            canvas.matchWidthOrHeight = 0;
            aspect.aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
            //if (whRatio < 0.5)
            //    aspect.aspectRatio = 0.5f;
            //else
            aspect.aspectRatio = whRatio;
            HightScale = 0.5625f / whRatio;
        }
        else
        {
            canvas.matchWidthOrHeight = 1;
            aspect.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
            aspect.aspectRatio = whRatio;
            HightScale = 1;
        }
    }
#if UNITY_EDITOR
    void Update ()
    {       
        adaptive();
    }
#endif
}


/*
       void OnGUI()
       {
           var res = Screen.currentResolution;
           var safeArea = Screen.safeArea;
           var cutouts = Screen.cutouts;
           StringBuilder safeAreaInfo = new StringBuilder($"\n\n\n\n\n\n\n\n\n\nResolution (Width × Height): {res.width}x{res.height}\n");
           safeAreaInfo.Append($"safeArea (X x Y / Width × Height):\n");
           safeAreaInfo.Append($"{safeArea.x} x {safeArea.y} / {safeArea.width} x {safeArea.height} \n");

           if (cutouts.Length > 0)
           {
               foreach (var c in cutouts)
               {
                   GUI.Box(c, "a");
                   safeAreaInfo.Append($"cutout (X x Y / Width × Height):\n");
                   safeAreaInfo.Append($"{c.x} x {c.y} / {c.width} x {c.height}\n");
               }
           }
           else
           {
               safeAreaInfo.Append("No cutouts detected");               
           }
           //GUIDrawRect(safeArea, Color.red, safeAreaInfo.ToString());
           GUI.Box(safeArea, safeAreaInfo.ToString());
       }*/
