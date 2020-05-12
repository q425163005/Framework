using CSF.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace HotFix_Project.Common
{
    public partial class Confirm : BaseUI
    {
        private Action confirmCB;
        private Action cancelCB;

        /// <summary>提示确认界面</summary>
        public Confirm()
        {
            UINode        = EUINode.UIAlert;  //UI节点
            OpenAnim      = EUIAnim.ScaleIn;  //UI打开效果
            CloseAnim     = EUIAnim.ScaleOut; //UI关闭效果 
            EnableLoading = false;
        }

        /// <summary>添加事件监听</summary>
        protected override void Awake()
        {
            confirmCB                                      = GetArg<Action>(0);
            cancelCB                                       = GetArg<Action>(1);
            txtContent.text                                = GetArg<string>(2);
            txtTitle.text                                  = GetArg<string>(3);
            btnConfirm.GetComponentInChildren<Text>().text = GetArg<string>(4);
            btnCancel.GetComponentInChildren<Text>().text  = GetArg<string>(5);
            bool isShowClose = GetArg<bool>(6);
            bool isAlert     = GetArg<bool>(7);
            btnCancel.gameObject.SetActive(!isAlert); //Alert 不显示取消
            btnClose.gameObject.SetActive(isShowClose);
            btnClose.AddClick(CloseSelf);
            btnConfirm.AddClick(btnConfirm_Click);
            btnCancel.AddClick(btnCancel_Click);
        }

        /// <summary>确认</summary>
        void btnConfirm_Click()
        {           
            confirmCB?.Invoke();
            CloseSelf();
        }

        /// <summary>取消</summary>
        void btnCancel_Click()
        {            
            cancelCB?.Invoke();
            CloseSelf();
        }


        public void ResetBtnCanvas()
        {
            Canvas can = btnConfirm.GetComponent<Canvas>();
            if (can!=null)
            {
                can.sortingLayerName = "UIMessage";
                can.sortingOrder = 61;
            }
                              
        }


        /// <summary>释放UI引用</summary>
        public override void Dispose()
        {
            confirmCB = null;
            cancelCB  = null;
        }

        public void ShowNetErr(Action confirmCB,
            string                    content,
            string                    title,
            string                    confirmTex)
        {
            btnCancel.gameObject.SetActive(false);
            btnClose.gameObject.SetActive(false);
            this.confirmCB                                 = confirmCB;
            txtContent.text                                = content;
            txtTitle.text                                  = title;
            btnConfirm.GetComponentInChildren<Text>().text = confirmTex;
            Mgr.UI.UIAnim(gameObject, OpenAnim).Run();
        }


        /// <summary>
        /// 显示确认框
        /// </summary>
        /// <param name="confirmCB">确认回调</param>
        /// <param name="cancelCB">取消回调</param>
        /// <param name="content">消息内容</param>
        /// <param name="title">标题,传null使用默认标题</param>
        /// <param name="showClose">显示Close按钮，默认不显示</param>
        public static void Show(
            Action confirmCB,
            Action cancelCB,
            string content,
            string title,
            string confirmTex = null,
            string cancleTex  = null,
            bool   showClose  = false)
        {
            title      = title      ?? Mgr.Lang.Get("Com_ConfirmTitle");
            confirmTex = confirmTex ?? Mgr.Lang.Get("Com_Confirm");
            cancleTex  = cancleTex  ?? Mgr.Lang.Get("Com_Cancel");
            Mgr.UI.Show<Confirm>(confirmCB, cancelCB, content, title, confirmTex, cancleTex, showClose, false);
        }

        /// <summary>
        /// 显示确认框
        /// </summary>
        /// <param name="confirmCB">确认回调</param>
        /// <param name="cancelCB">取消回调</param>
        /// <param name="content">消息内容(语言Key)</param>
        /// <param name="title">标题,传null使用默认标题(语言Key)</param>
        /// <param name="showClose">显示Close按钮，默认不显示</param>
        public static void ShowLang(
            Action confirmCB,
            Action cancelCB,
            string content,
            string title,
            string confirmTex = null,
            string cancleTex  = null,
            bool   showClose  = false)
        {
            title      = title      == null ? Mgr.Lang.Get("Com_ConfirmTitle") : Mgr.Lang.Get(title);
            confirmTex = confirmTex == null ? Mgr.Lang.Get("Com_Confirm") : Mgr.Lang.Get(confirmTex);
            cancleTex  = cancleTex  == null ? Mgr.Lang.Get("Com_Cancel") : Mgr.Lang.Get(cancleTex);
            Mgr.UI.Show<Confirm>(confirmCB, cancelCB, Mgr.Lang.Get(content), title, confirmTex, cancleTex, showClose,
                false);
        }

        /// <summary>
        /// 警告框（只有一个确定按钮）
        /// </summary>
        /// <param name="confirmCB">确认回调</param>
        /// <param name="content">消息内容</param>
        /// <param name="title">标题,传null使用默认标题</param>
        ///  <param name="showClose">显示Close按钮，默认不显示</param>
        public static void Alert(
            Action confirmCB,
            string content,
            string title,
            string confirmTex = null,
            bool   showClose  = false)
        {
            title      = title      ?? Mgr.Lang.Get("Com_AlertTitle");
            confirmTex = confirmTex ?? Mgr.Lang.Get("Com_Confirm");
            Mgr.UI.Show<Confirm>(confirmCB, null, content, title, confirmTex, Mgr.Lang.Get("Com_Cancel"), showClose,
                true);
        }

        /// <summary>
        /// 警告框（只有一个确定按钮）
        /// </summary>
        /// <param name="confirmCB">确认回调</param>
        /// <param name="content">消息内容(语言Key)</param>
        /// <param name="title">标题,传null使用默认标题(语言Key)</param>
        ///  <param name="showClose">显示Close按钮，默认不显示</param>
        public static void AlertLang(
            Action confirmCB,
            string content,
            string title,
            string confirmTex = null,
            bool   showClose  = false)
        {
            title      = title      == null ? Mgr.Lang.Get("Com_AlertTitle") : Mgr.Lang.Get(title);
            confirmTex = confirmTex == null ? Mgr.Lang.Get("Com_Confirm") : Mgr.Lang.Get(confirmTex);
            Mgr.UI.Show<Confirm>(confirmCB, null, Mgr.Lang.Get(content), title, confirmTex, Mgr.Lang.Get("Com_Cancel"),
                showClose, true);
        }

        /// <summary>
        /// 警告框（只有一个确定按钮）(显示关闭按钮并绑定事件)
        /// </summary>
        /// <param name="confirmCB">确认回调</param>
        /// <param name="content">消息内容(语言Key)</param>
        /// <param name="title">标题,传null使用默认标题(语言Key)</param>
        /// <param name="showClose">显示Close按钮，默认不显示</param>
        /// <param name="closeAction">Close按钮事件绑定，默认不显示</param>
        public static async CTask AlertWithCloseAction(
            Action confirmCB,
            string content,
            string title,
            string confirmTex = null,
            bool showClose = true,
            Action closeAction= null)
        {
            title = title == null ? Mgr.Lang.Get("Com_AlertTitle") : Mgr.Lang.Get(title);
            confirmTex = confirmTex == null ? Mgr.Lang.Get("Com_Confirm") : Mgr.Lang.Get(confirmTex);
            Confirm ui=Mgr.UI.Show<Confirm>(confirmCB, null, Mgr.Lang.Get(content), title, confirmTex, Mgr.Lang.Get("Com_Cancel"),
                showClose, true);
            await ui.Await();
            ui.CloseAction = closeAction;
        }


        /// <summary>
        /// 断线提示 层级要调到最上面
        /// </summary>
        /// <param name="confirmCB">确认回调</param>
        /// <param name="content">消息内容(语言Key)</param>
        /// <param name="title">标题,传null使用默认标题(语言Key)</param>
        ///  <param name="showClose">显示Close按钮，默认不显示</param>
        public static async CTask AlertLangTop(
            Action confirmCB,
            string content,
            string title,
            string confirmTex = null,
            bool   showClose  = false)
        {
            title      = title == null ? Mgr.Lang.Get("Com_AlertTitle") : Mgr.Lang.Get(title);
            confirmTex = confirmTex ?? Mgr.Lang.Get("Com_Confirm");

            Confirm confirm = Mgr.UI.GetUI<Confirm>();
            if (confirm != null)  //强制关，N次切网有时弹不出来
            {
                confirm.CloseSelf();
                confirm = null;
            }

            if (confirm == null)
            {
                confirm = Mgr.UI.Show<Confirm>(confirmCB, null, Mgr.Lang.Get(content), title, confirmTex,
                    Mgr.Lang.Get("Com_Cancel"), showClose,
                    true);
                await confirm.Await();
            }
            else
            {
                confirm.ShowNetErr(confirmCB, Mgr.Lang.Get(content), title, confirmTex);
            }

            Canvas can = confirm.gameObject.GetComponent<Canvas>();
            if (can == null)
            {
                can                  = confirm.gameObject.AddComponent<Canvas>();
              
                confirm.gameObject.AddComponent<GraphicRaycaster>();
            }
            can.overrideSorting  = true;
            can.sortingLayerName = "UIMessage";
            can.sortingOrder     = 60;
            confirm.ResetBtnCanvas();
        }

        /// <summary>
        /// 交换按钮颜色
        /// </summary>
        public void SwopBtnSprite()
        {
            Color color1 = btnConfirm.GetComponentInChildren<Text>().color;
            Color color2 = btnCancel.GetComponentInChildren<Text>().color;
            btnConfirm.GetComponent<Image>().SetSprite("Btn_3_1_red", UIAtlas.PublicBKNew).Run();
            btnConfirm.GetComponentInChildren<Text>().color = color2;
            btnCancel.GetComponent<Image>().SetSprite("Btn_3_1", UIAtlas.PublicBKNew).Run();
            btnCancel.GetComponentInChildren<Text>().color = color1;
        }
    }
}