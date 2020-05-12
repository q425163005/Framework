using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace MapEditor
{
    /// <summary>活动关卡</summary>
    public class EventFBLevelConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 关卡Id
        /// 副本id*100+关卡序号
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 所属活动ID
        /// </summary>
        public int eventId { get; set; }
        /// <summary>
        /// 关卡号
        /// </summary>
        public int levelId { get; set; }
        /// <summary>
        /// 难度
        /// 1简单
        /// 2中等的
        /// 3挑战性的
        /// 4困难的
        /// 5非常困难
        /// </summary>
        public int complexity { get; set; }
        /// <summary>
        /// 参考战力
        /// 不填不显示
        /// 
        /// </summary>
        public int FC { get; set; }
        /// <summary>
        /// 消耗行动点
        /// </summary>
        public int acPoint { get; set; }
        /// <summary>
        /// 通关奖励
        /// 发奖用
        /// </summary>
        public List<int[]> award { get; set; }
        /// <summary>
        /// 奖励预览
        /// 给客户端显示用
        /// 只配2,3个
        /// </summary>
        public List<int[]> awardPreview { get; set; }
    }
}
