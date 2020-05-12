using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace CSF
{

    public enum EAdaptiveType
    {
        None,
        /// <summary>全屏背景</summary>
        FullBackground,
        /// <summary>流海缩进时自动向上移</summary>
        Top,
        /// <summary>流海缩进时自动向下移</summary>
        Botton,
        /// <summary>居中</summary>
        Center,
        /// <summary>高度<=最小高度时，往上移100像素</summary>
        Top100,
    }


    /// <summary>
    /// UI背景适配,自动补满流海屏缩进的背景
    /// </summary>
    public class UIAdaptive : MonoBehaviour
    {
        RectTransform rectTransform;
        public EAdaptiveType AdaptiveType;

        Vector2 anchoredPosition;
        Vector2 offsetMax;
        Vector2 offsetMin;
        private void Awake()
        {
            rectTransform = transform as RectTransform;
            anchoredPosition = rectTransform.anchoredPosition; //初始位置
            offsetMax = rectTransform.offsetMax;
            offsetMin = rectTransform.offsetMin;
            Reset();
        }

        public void Reset()
        {
            if (rectTransform == null) return;
            int cutoutsHeight = Mgr.UI.canvasAdaptive.CutoutsHeight;
            int cutoutsBottonHeight = Mgr.UI.canvasAdaptive.CutoutsBottonHeight;
            switch (AdaptiveType)
            {
                case EAdaptiveType.FullBackground:
                    rectTransform.offsetMax = new Vector2(0, cutoutsHeight);
                    rectTransform.offsetMin = new Vector2(0, -cutoutsBottonHeight);
                    break;
                case EAdaptiveType.Top:
                    rectTransform.anchoredPosition = anchoredPosition + Vector2.up * cutoutsHeight;
                    break;
                case EAdaptiveType.Top100: //特殊需求!!!!!!
                    if (Screen.width / (float)Screen.height >= 1080 / 1920f)
                        rectTransform.anchoredPosition = anchoredPosition + Vector2.up * (cutoutsHeight + 100);
                    else
                        rectTransform.anchoredPosition = anchoredPosition + Vector2.up * cutoutsHeight;
                    break;
                case EAdaptiveType.Botton:
                    //rectTransform.anchoredPosition = anchoredPosition - Vector2.up * cutoutsBottonHeight;
                    rectTransform.offsetMax = offsetMax;
                    rectTransform.offsetMin = offsetMin - Vector2.up * cutoutsBottonHeight;
                    break;
                case EAdaptiveType.Center:
                    rectTransform.anchoredPosition = anchoredPosition + Vector2.up * (cutoutsHeight - cutoutsBottonHeight)/2;
                    break;
            }
        }
#if UNITY_EDITOR
        void OnValidate()
        {           
            if (Mgr.UI != null)
                Reset();
        }
#endif
    }
}
