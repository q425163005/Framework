using System;
using System.Collections.Generic;
using System.Linq;
using HotFix_Project.Module.Login.DataMgr;
using HotFix_Project.Module.Login.DataMgr.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HotFix_Project.Login
{
    /// <summary>
    /// 服务器列表Item
    /// </summary>
    public partial class SelServerItem : BaseItem
    {
        public  Action<SelServerItem> onClick;
        public ServerItemData Data;

        /// <summary>添加事件监听</summary>
        protected override void Awake()
        {
            //在滚动区内Item加点击事件，只能在Button上加
            gameObject.GetComponent<Button>().AddClick(self_Click);
        }

        /// <summary>当前对象点击事件</summary>
        void self_Click()
        {
            onClick?.Invoke(this);
        }

        /// <summary>设置数据</summary><param name="data"></param>
        public void SetData(ServerItemData data)
        {
            Data = data;
            Refresh();
        }

        /// <summary>刷新Item</summary>
        public override void Refresh()
        {
            if (!isInstance) return;
            txtServerName.text = Data.ServerName;
            texNum.text        = Data.ServerId.ToString();
            switch (Data.Flag) //服务器标记
            {
                case 1: //新服 暂时只一个标记
                    imgFlag.SetSprite("Icon_New", UIAtlas.Login).Run();
                    imgFlag.gameObject.SetActive(true);
                    break;
                default:
                    imgFlag.gameObject.SetActive(false);
                    break;
            }
            
            if (Data.URL == ServerListMgr.I.ServerURL)
            {
                imgFlag.SetSprite("Icon_Last", UIAtlas.Login).Run();
                imgFlag.gameObject.SetActive(true);
            }
        }

        public override void Dispose()
        {
            Data    = null;
            onClick = null;
        }
    }
}