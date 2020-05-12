using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

namespace HotFix_Project.Common
{
    public partial class Loading : BaseUI
    {
        private static Loading self;
        private float value = 0;

        /// <summary>Loading界面</summary>
        public Loading()
        {
            UINode = EUINode.UIMessage; //UI节点
            OpenAnim = EUIAnim.None;      //UI打开效果
            CloseAnim = EUIAnim.FadeOut;   //UI关闭效果 
            EnableLoading = false;
        }

        /// <summary>添加事件监听</summary>
        protected override void Awake()
        {
            sliderProg.value = 0;
            if (value > 0)
                SetValue(value);
            txtInfo.text = GetArg<string>();
            Logo.SetSprite(Mgr.Lang.GetLogoName(), UIAtlas.Login).Run();
        }

        public override void Refresh()
        {
           
        }

        /// <summary>释放UI引用</summary>
        public override void Dispose()
        {
            self = null;
        }

        private static float startTime = 0;

        public static Loading Show(string defTitle = null)
        {
            startTime = Time.realtimeSinceStartup;
            defTitle = defTitle ?? Mgr.Lang.Get("Loading.Default");
            self = Mgr.UI.Show<Loading>(defTitle);
            return self;
        }

        /// <summary>
        /// 设置标题
        /// </summary>
        public static void SetTitle(string title)
        {
            if (self == null || !self.isInstance) return;
            self.txtInfo.text = title;
        }

        /// <summary>
        /// 设置进度(0-1)
        /// </summary>
        public static void SetValue(float val)
        {
            if (self == null) return;
            self.value = val;
            if (!self.isInstance) return;
            self.sliderProg.DOKill(false);
            self.sliderProg.DOValue(val, val - self.sliderProg.value);
        }

        /// <summary>
        /// 设置标题和进度
        /// </summary>
        /// <param name="title"></param>
        /// <param name="val"></param>
        public void SetInfo(string title, float val)
        {
            SetTitle(title);
            SetValue(val);
        }

        public static void Close()
        {
            float waitTime = 1 - Time.realtimeSinceStartup - startTime;
            SetValue(1f);
            if (waitTime < 0.3f)
                waitTime = 0.3f;
            //Mgr.Timer.Once(waitTime,() => {
            //    Mgr.UI.Close<Loading>();
            //});
            //await CTask.WaitForSeconds(waitTime);
            
        }
    }
}

