using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace HotFix_Project
{
    /// <summary>功能开放</summary>
    public class FunOpenConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 功能名称
        /// </summary>
        public string id;
        /// <summary>
        /// 开启条件类型
        /// 0:玩家等级
        /// 1:完成指引开放
        /// 2:任务领奖时开放
        /// 3：主城等级
        /// 99:暂未开放
        /// </summary>
        public int openType;
        /// <summary>
        /// 开启条件参数
        /// 0:玩家等级
        /// 1:指引Id
        /// 2:任务Id
        /// 3：主城等级
        /// </summary>
        public int openArg;
       
    }        
}
