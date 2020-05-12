//工具生成不要修改

using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace HotFix_Project.Common
{
    public partial class Confirm : BaseUI
    {
        private Button btnClose;
        private Text   txtTitle;
        private Text   txtContent;
        private Button btnConfirm;
        private Button btnCancel;
        private Image  imgMask;

        /// <summary>初始化UI控件</summary>
        override protected void InitializeComponent()
        {
            btnClose   = Get<Button>("btnClose");
            txtTitle   = Get<Text>("txtTitle");
            txtContent = Get<Text>("txtContent");
            btnConfirm = Get<Button>("btnConfirm");
            btnCancel  = Get<Button>("btnCancel");
            imgMask    = Get<Image>("imgMask");
        }
    }
}