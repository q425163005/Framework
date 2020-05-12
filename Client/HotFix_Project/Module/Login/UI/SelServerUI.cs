using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using HotFix_Project.Common;
using HotFix_Project.Module.Login.DataMgr;
using HotFix_Project.Module.Login.DataMgr.Data;
using UnityEngine.Events;

namespace HotFix_Project.Login
{
    public partial class SelServerUI : BaseUI
    {
        /// <summary>服务器列表</summary>
        private List<SelServerItem> itemList = new List<SelServerItem>();

        private string SelSeverURL = string.Empty;

        /// <summary>选服界面</summary>
        public SelServerUI()
        {
            UINode    = EUINode.UIPopup;  //UI节点
            OpenAnim  = EUIAnim.ScaleIn;  //UI打开效果
            CloseAnim = EUIAnim.ScaleOut; //UI关闭效果 
        }

        /// <summary>添加事件监听</summary>
        protected override void Awake()
        {
            //btnConfirm.AddClick(btnConfirm_Click); //确定
            //btnCancel.AddClick(btnCancel_Click);   //
            btnClose.AddClick(CloseSelf);
            prefabSelServerItem.gameObject.SetActive(false);
            setServerList();
            //btnTest.AddClick(btnTest_Click);
        }

        //设置服务器列表
        void setServerList()
        {
            SelServerItem item;
            int           i = 0;
            foreach (ServerItemData data in ServerListMgr.I.dicServerList.Values)
            {
                if (itemList.Count == i)
                {
                    item = new SelServerItem();
                    item.Instantiate(prefabSelServerItem, gridContent.transform);
                    item.onClick = selectServerItem;
                    itemList.Add(item);
                }
                else
                    item = itemList[i];

                item.SetData(data);

                i++;
            }
        }

        /// <summary>
        /// 服务器列表Item点击回调事件
        /// </summary>
        /// <param name="item"></param>
        void selectServerItem(SelServerItem item)
        {
            SelSeverURL = item.Data.URL;
            ServerListMgr.I.SetServerId(SelSeverURL);
            CloseSelf();
        }

        /// <summary>释放UI引用</summary>
        public override void Dispose()
        {
            itemList.Dispose();
            itemList = null;
        }
    }
}