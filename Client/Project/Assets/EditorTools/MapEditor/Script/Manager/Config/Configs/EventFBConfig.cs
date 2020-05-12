using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace MapEditor
{
    /// <summary>活动副本</summary>
    public class EventFBConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 活动id
        /// (1-99)
        /// </summary>
        public int id;
        /// <summary>
        /// 关卡名称
        /// 语言表id
        /// </summary>
        public Lang name;
        /// <summary>
        /// 活动类型
        /// 0正常
        /// 1特殊
        /// </summary>
        public int type;
        /// <summary>
        /// 活动类型
        /// 0普通活动
        /// 1挑战活动
        /// 2节日活动
        /// 3英雄试练
        /// </summary>
        public int eventType;
        /// <summary>
        /// 活动图标
        /// </summary>
        public string Icon;
        /// <summary>
        /// 活动稀有度
        /// 普通 白色
        /// 罕见 橙色
        /// </summary>
        public Lang quality;
        /// <summary>
        /// 难度级别
        /// 难度1
        /// 难度2
        /// 难度3
        /// 活动有几个难度填几个
        /// 关卡里面相应的配几个难度对应的关卡
        /// </summary>
        public int[] difficulty;
        /// <summary>
        /// 开放时间
        /// 开始时间
        /// </summary>
        public string startTime;
        /// <summary>
        /// 结束时间
        /// </summary>
        public string endTime;
        /// <summary>
        /// 坐标位
        /// 客户端定几组坐标
        /// 1-3显示普通活动(最多3个)
        /// 下面3个位置固定
        /// 11挑战活动
        /// 12节日活动
        /// 13英雄试炼
        /// </summary>
        public int location;
        /// <summary>
        /// 对话背景图片
        /// </summary>
        public string aniBgImg;
        /// <summary>
        /// 对话角色图片
        /// </summary>
        public string aniHeroImg;

    }
}
