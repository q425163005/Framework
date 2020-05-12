using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

[ExecuteInEditMode]
public class RichTextAlphaUpdater : MonoBehaviour
{
    public Text Txt;

    /// <summary>
    /// 匹配颜色值
    /// </summary>
    public static readonly Regex RichColorReg = new Regex("<color=#([a-f0-9]{8})>", RegexOptions.IgnoreCase);
    public const int ColorMax = 255;

    private UnityAction _vertDirtyAction;
    private UnityAction VertDirtyAction
    {
        get
        {
            if (null == _vertDirtyAction)
            {
                _vertDirtyAction = _OnVertDirty;
            }
            return _vertDirtyAction;
        }
    }

    /// <summary>
    /// 文字顶点变化的事件
    /// </summary>
    private void _OnVertDirty()
    {
        string alpha = _GetHexAlpha();
        string txt = Txt.text;
        Match match = RichColorReg.Match(txt);
        Group group = null;
        while (match.Success)
        {
            group = match.Groups[1];
            _ReplaceAlpha(txt, group.Index, alpha);
            match = match.NextMatch();
        }
    }

    /// <summary>
    /// 缓存数据，降低处理频率
    /// </summary>
    private int _prevAlpha = 0;
    private string _hexAlpha = null;

    /// <summary>
    /// 获取当前Alpha的Hex值
    /// </summary>
    private string _GetHexAlpha()
    {
        int alpha = Mathf.Clamp((int)(Txt.color.a * ColorMax), 0, ColorMax);
        if (null != _hexAlpha && alpha == _prevAlpha)
        {
            return _hexAlpha;
        }

        string hexAlpha = Convert.ToString(alpha, 16);
        if (hexAlpha.Length == 1)
        {
            return "0" + hexAlpha;
        }
        return hexAlpha;
    }

    private void _ReplaceAlpha(string txt, int colorIdx, string alpha)
    {
        unsafe
        {
            fixed (char* hexPtr = txt)
            {
                hexPtr[colorIdx + 6] = alpha[0];
                hexPtr[colorIdx + 7] = alpha[1];
            }
        }
    }

    void OnEnable()
    {
        if (null == Txt)
        {
            Txt = GetComponent<Text>();
        }
        if (null != Txt)
        {
            Txt.RegisterDirtyVerticesCallback(VertDirtyAction);
        }
    }

    void OnDisable()
    {
        if (null == Txt) return;
        Txt.UnregisterDirtyVerticesCallback(VertDirtyAction);
    }
}
