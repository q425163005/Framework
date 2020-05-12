using UnityEngine;
using HotFix_Project.Common;
using HotFix_Project.Module.Login.DataMgr;
using HotFix_Project.Module.Login.DataMgr.Data;
using CSF;
using CSF.Tasks;

namespace HotFix_Project.Login
{
    public partial class LoginUI : BaseUI
    {
        bool isConnectIng = false;

        /// <summary>登录界面</summary>
        public LoginUI()
        {
            EnableLoading = false;
            UINode        = EUINode.UIWindow; //UI节点
            OpenAnim      = EUIAnim.FadeIn;   //UI打开效果
            CloseAnim     = EUIAnim.FadeOut;  //UI关闭效果 
        }
        private GameObject debugServer;
        /// <summary>添加事件监听</summary>
        protected override void Awake()
        {
          
        }



        /// <summary>释放UI引用</summary>
        public override void Dispose()
        {
        }
    }
}