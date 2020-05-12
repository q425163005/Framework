//工具生成不要修改
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace HotFix_Project.Common
{
    public partial class Loading : BaseUI
    {
        private Slider sliderProg;
        private Text txtInfo;
        private Text texTip;
        private Image Logo;
        /// <summary>初始化UI控件</summary>
        override protected void InitializeComponent()
        {
            sliderProg = Get<Slider>("sliderProg");
            txtInfo = Get<Text>("txtInfo");
            texTip = Get<Text>("texTip");
            Logo = Get<Image>("Logo");
        }
    }
}

