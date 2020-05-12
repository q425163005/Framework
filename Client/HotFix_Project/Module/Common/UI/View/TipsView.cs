//工具生成不要修改
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace HotFix_Project.Common
{
    public partial class Tips : BaseUI
    {
        private GameObject goContent;
        private Text txtContent;
 
        /// <summary>初始化UI控件</summary>
        override protected void InitializeComponent()
        {
            goContent = Get("goContent");
            txtContent = Get<Text>("txtContent");

        }
    }
}

