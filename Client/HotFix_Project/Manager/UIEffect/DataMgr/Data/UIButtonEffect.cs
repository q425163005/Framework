using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace HotFix_Project.UIEffect
{
    /// <summary>
    /// UIButton 按钮效果
    /// </summary>
    public class UIButtonEffect : UIItemEffectBase
    {
        private Transform myTransform;
        private Button myBtn;
        private Image[] myImage;
        private float coefficient;

        Vector3 transformScaler;
        
        public UIButtonEffect(UIItemEffectEnum type, Button btn,float officient)
        {
            target = btn.gameObject;
            EffectType = type;
            coefficient = officient;
            myTransform = target.transform;
            transformScaler = myTransform.localScale;
            myBtn = btn;
            myImage = myBtn.GetComponentsInChildren<Image>();
            //myImage = myBtn.targetGraphic as Image;

            switch (type)
            {
                case UIItemEffectEnum.Smaller:
                case UIItemEffectEnum.Bigger:
                case UIItemEffectEnum.BtnSmallerAndBrighten:
                case UIItemEffectEnum.BtnBiggerAndBrighten:
                case UIItemEffectEnum.Brighten:
                    ShieldButtonEffect(myBtn);
                    break;
                case UIItemEffectEnum.BtnSmallerAndDarken:
                case UIItemEffectEnum.BtnBiggerAndDarken:
                default:
                    break;
            }
        }
        public override void ResetEffect()
        {
            if (myTransform != null)
                myTransform.localScale = transformScaler;
            Brighten(myImage, false);
        }

        public override void TriggerEffect()
        {
            switch (EffectType)
            {
                case UIItemEffectEnum.Smaller:
                    SclaleTtamsform(myTransform, coefficient, true);
                    break;
                case UIItemEffectEnum.Bigger:
                    SclaleTtamsform(myTransform, coefficient, false);
                    break;
                case UIItemEffectEnum.BtnSmallerAndBrighten:
                    SclaleTtamsform(myTransform, coefficient, true);
                    Brighten(myImage, true);
                    break;
                case UIItemEffectEnum.BtnBiggerAndBrighten:
                    SclaleTtamsform(myTransform, coefficient, false);
                    Brighten(myImage, true);
                    break;
                case UIItemEffectEnum.BtnSmallerAndDarken:
                    SclaleTtamsform(myTransform, coefficient, true);
                    break;
                case UIItemEffectEnum.BtnBiggerAndDarken:
                    SclaleTtamsform(myTransform, coefficient, false);
                    break;
                case UIItemEffectEnum.Brighten:
                    Brighten(myImage, true);
                    break;
                default:
                    break;
            }
        }
        
        //变亮
        void Brighten(Image[] img,bool isBrighten)
        {
            if (img == null)
                return;
            if (isBrighten)
            {
                for (int i=0;i<img.Length;i++)
                {
                    img[i].material = Mgr.UIItemEffect.BrightenMat;
                }
            }
            else
            {
                for (int i = 0; i < img.Length; i++)
                {
                    img[i].material = null;
                }
            }
        }

        //缩放 变大
        void SclaleTtamsform(Transform t,float x,bool isSclale)
        {
            if (t == null)
                return;
            if (isSclale)
                t.localScale -= new Vector3(x, x, x);
            else
                t.localScale += new Vector3(x, x, x);

        }

        //屏蔽按钮变暗效果
        void ShieldButtonEffect(Button btn)
        {
            if (btn == null)
                return;
            ColorBlock colorBlock = ColorBlock.defaultColorBlock;
            colorBlock.pressedColor = colorBlock.normalColor;
            btn.colors = colorBlock;
        }
    }
}
