//工具生成不要修改
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace HotFix_Project.Common
{
    public partial class ItemTipUI : BaseUI
    {
        private Text texTitle;
        private GameObject StarContent;
        private GameObject imgStar;
        private Text texDes;
        private ContentSizeFitter imgBG;
        private GameObject posObj;
        private GameObject DowerNeed;
        private GameObject DowerStudy;
        private Button btnDowerStudy;
        private Image imgDowerFood;
        private Image imgDowerStone;
        private Image imgDowerItem;
 
        /// <summary>初始化UI控件</summary>
        protected override void InitializeComponent()
        {
            texTitle = Get<Text>("texTitle");
            StarContent = Get("StarContent");
            imgStar = Get("imgStar");
            texDes = Get<Text>("texDes");
            imgBG = Get<ContentSizeFitter>("imgBG");
            posObj = Get("posObj");
            DowerNeed = Get("DowerNeed");
            DowerStudy = Get("DowerStudy");
            btnDowerStudy = Get<Button>("btnDowerStudy");
            imgDowerFood = Get<Image>("imgDowerFood");
            imgDowerStone = Get<Image>("imgDowerStone");
            imgDowerItem = Get<Image>("imgDowerItem");

        }
    }
}

