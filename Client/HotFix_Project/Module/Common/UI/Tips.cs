using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine.Events;
using CSF.Tasks;

namespace HotFix_Project.Common
{
    public partial class Tips : BaseUI
    {
        private static Queue<string> tipsQueueList = new Queue<string>();

        /// <summary>
        /// 缓存Tips，留着下次使用
        /// </summary>
        private static Queue<Tips> cacheTipsList = new Queue<Tips>();

        //private static WaitForSeconds waitAnim = new WaitForSeconds(0.3f);
        private static CTaskHandle taskRun;
        private static string currTips;

        private static async CTask RunShow()
        {
            while (tipsQueueList.Count > 0)
            {
                currTips = tipsQueueList.Dequeue();
                await showAnim(currTips);
            }

            currTips = null;
            taskRun.Stop();
        }

        private static async CTask showAnim(string content)
        {
            Tips tips;
            if (cacheTipsList.Count < 1)
            {
                tips = new Tips();
                await tips._openUI(content);
            }
            else
            {
                tips = cacheTipsList.Dequeue();
                tips.transform.SetParent(tips.transform.parent);
                tips.ResetTipsAnim(content);
            }

            await CTask.WaitForSeconds(0.3f);
        }

        /// <summary>Tips界面</summary>
        public Tips()
        {
            UINode = EUINode.UIMessage; //UI节点
            OpenAnim = EUIAnim.None;      //UI打开效果
            CloseAnim = EUIAnim.None;      //UI关闭效果 
            EnableLoading = false;             //禁用loading
        }

        /// <summary>重置Tips内容,并且重新显示</summary>
        /// <param name="content"></param>
        public void ResetTipsAnim(string content)
        {
            gameObject.SetVisible(true);
            goContent.transform.localPosition = Vector3.up * initContentY;
            goContent.transform.localScale = Vector3.zero;
            txtContent.text = content;

            Sequence sequenc = DOTween.Sequence();
            sequenc.Append(goContent.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack));
            sequenc.Append(goContent.transform.DOLocalMoveY(initContentY + 250, 1f).SetEase(Ease.Linear));
            sequenc.AppendCallback(() =>
            {
                txtContent.text = string.Empty;
                gameObject.SetVisible(false);
                cacheTipsList.Enqueue(this);
            });
        }

        public string GetContent()
        {
            return txtContent.text;
        }

        private float initContentY = 0;

        /// <summary>添加事件监听</summary>
        protected override void Awake()
        {
            initContentY = goContent.transform.localPosition.y;
            ResetTipsAnim(GetArg<string>());
        }

        /// <summary>释放UI引用</summary>
        public override void Dispose()
        {
        }


        /// <summary>
        /// 弹出Tips提示
        /// </summary>
        /// <param name="content">消息内容</param>
        /// <param name="canLastSame">是否允许与上次的Tips相同</param>
        public static void Show(string content, bool canLastSame = false)
        {
            //不充许与队队中最后一个元素的Tips相同
            if (!canLastSame && tipsQueueList.Count > 0 && content == tipsQueueList.Last())
                return;
            tipsQueueList.Enqueue(content);
            if (taskRun.IsDead)
                taskRun = RunShow().Run();
        }

        /// <summary>
        /// 跟据多语言Key值弹出Tips提示
        /// </summary>
        /// <param name="key"></param>
        public static void ShowLang(string key)
        {
            Show(Mgr.Lang.Get(key));
        }
    }
}

