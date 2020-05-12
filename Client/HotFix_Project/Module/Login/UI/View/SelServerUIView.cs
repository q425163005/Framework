//工具生成不要修改
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace HotFix_Project.Login
{
    public partial class SelServerUI : BaseUI
    {
        private ContentSizeFitter gridContent;
        private UIOutlet prefabSelServerItem;
        private Button btnClose;
 
        /// <summary>初始化UI控件</summary>
        protected override void InitializeComponent()
        {
            gridContent = Get<ContentSizeFitter>("gridContent");
            prefabSelServerItem = Get<UIOutlet>("prefabSelServerItem");
            btnClose = Get<Button>("btnClose");

        }
    }
}

