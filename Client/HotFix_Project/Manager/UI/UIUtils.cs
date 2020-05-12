using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using CSF.Tasks;

namespace HotFix_Project
{
    public class UIUtils
    {
        public static async CTask ObjectAnim(GameObject target, EUIAnim anim,float time=0.5f)
        {
            if (anim == EUIAnim.None || target == null) return;
            //UI淡入淡出效果
            if (anim == EUIAnim.FadeIn || anim == EUIAnim.FadeOut)
            {
                Graphic[] comps = target.GetComponentsInChildren<Graphic>();
                for (int i = comps.Length; --i >= 0;)
                {
                    if (anim == EUIAnim.FadeIn)
                        comps[i].DOFade(0, time).From();
                    else
                        comps[i].DOFade(0, time);
                }
                await CTask.WaitForSeconds(time);
            }
            else if (anim == EUIAnim.ScaleIn || anim == EUIAnim.ScaleOut)
            {
                if (anim == EUIAnim.ScaleIn)
                {
                    target.transform.DOScale(0, time).SetEase(Ease.OutBack).From();
                    await CTask.WaitForSeconds(time);
                }
                else
                {
                    target.transform.DOScale(0, time).SetEase(Ease.InBack);
                    await CTask.WaitForSeconds(time);
                }
            }
        }

        /// <summary>
        /// UI背景适配,适用于自适应的全屏背景
        /// 填充流海缩进的大小
        /// </summary>
        /// <param name="rect"></param>
        public static void UIBGAdaptive(RectTransform rect)
        {
            CSF.Mgr.UI.SetUIBgPadding(rect);
        }

        /// <summary>
        /// UI背景缩放,适用于1280的固定高度，跟据场景大小缩放至全屏
        /// </summary>
        /// <param name="rect"></param>
        public static void UIBGScale(Transform trans)
        {
            trans.localScale = new Vector3(1, CSF.Mgr.UI.canvasAdaptive.HightScale, 1);
        }
    }
}
