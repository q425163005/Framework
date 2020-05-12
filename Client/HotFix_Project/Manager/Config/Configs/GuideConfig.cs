using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace HotFix_Project
{
    /// <summary>新手指引</summary>
    public class GuideConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 指引Id
        /// </summary>
        public int id;
        /// <summary>
        /// 开启条件,需要玩家等级
        /// </summary>
        public int needLv;
        /// <summary>
        /// 开启条件，需要完成任务Id
        /// </summary>
        public int needTask;
        /// <summary>
        /// 需要建筑类型;建筑等级
        /// 
        /// 类型说明
        /// 主城堡1 农场2 矿场3 房屋4
        /// 仓库5 角斗场6 工坊7 训练场8
        /// 铁匠铺9
        /// </summary>
        public int[] needHouseLv;
        /// <summary>
        /// 开启条件,需要完成指定指引
        /// </summary>
        public int[] needIds;
        /// <summary>
        /// 开启条件
        /// 需要通关副本卡关
        /// FBLevelStage Id
        /// </summary>
        public int needFBStageId;
        /// <summary>
        /// 是否服务器进行保存
        /// 默认客户端通知
        /// (几个建造，服务器建完后保存)
        /// </summary>
        public bool serverSave;
       
    }        
}
