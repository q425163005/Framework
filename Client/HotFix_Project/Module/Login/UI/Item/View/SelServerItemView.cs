//工具生成不要修改
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace HotFix_Project.Login
{
    public partial class SelServerItem : BaseItem
    {
        private Text texNum;
        private Text txtServerName;
        private Image imgFlag;
        private Image imgState;
 
 
        /// <summary>初始化UI控件</summary>
        override protected void InitializeComponent()
        {
            texNum = Get<Text>("texNum");
            txtServerName = Get<Text>("txtServerName");
            imgFlag = Get<Image>("imgFlag");
            imgState = Get<Image>("imgState");

        }
        /// <summary>初始化皮肤设置</summary>
        protected override void InitializeSkin()
        {

            
        }
    }
}

