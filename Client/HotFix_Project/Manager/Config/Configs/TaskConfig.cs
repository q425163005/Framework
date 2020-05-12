using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace HotFix_Project
{
    /// <summary>任务</summary>
    public class TaskConfig : BaseConfig,ITaskConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        ///  任务Id
        /// 任务线*1000+编号
        /// </summary>
        public int id;
        /// <summary>
        /// 任务类型名
        /// </summary>
        public Lang typename;
        /// <summary>
        /// 任务名称
        /// </summary>
        public Lang name;
        /// <summary>
        /// 任务类型
        /// ETaskType
        /// </summary>
        public int type;
        /// <summary>
        /// 是否禁用
        /// (某些功能未完成可以先禁用)
        /// 1 禁用
        /// </summary>
        public bool disable;
        /// <summary>
        /// 领取任务奖励后是否自动关闭任务界面
        /// 1自动关闭
        /// </summary>
        public bool autoClose;
        /// <summary>
        /// 任务线
        /// 0主线任务
        /// 其它支线任务
        /// 
        /// </summary>
        public int line;
        /// <summary>
        /// 任务开启等级
        /// </summary>
        public int level;
        /// <summary>
        /// 任务完成开启任务线
        /// 如果需要有多个任务完成开启同一条线,任务的openLine设为一样即可
        /// </summary>
        public int openLine;
        /// <summary>
        /// 任务图标
        /// </summary>
        public string icon;
        /// <summary>
        /// 完成条件参数0
        /// 完成条件参数1
        /// 完成条件参数2
        /// </summary>
        public int[] condition;
        /// <summary>
        /// 任务奖励
        /// </summary>
        public List<int[]> award;
        /// <summary>
        /// 前往类型
        /// 0无
        /// 1:打开UI
        /// </summary>
        public int guideGoType;
        /// <summary>
        /// 前往字符串参数
        /// 1:UI名
        /// </summary>
        public string guideGoStrArg;
        /// <summary>
        /// 前往Int参数1
        /// 前往Int参数2
        /// </summary>
        public int[] guideGoIntArg;
        /// <summary>
        /// 任务描述
        /// </summary>
        public Lang des;
       
    }        
}
