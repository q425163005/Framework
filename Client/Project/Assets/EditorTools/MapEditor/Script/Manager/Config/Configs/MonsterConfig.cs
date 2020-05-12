using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace MapEditor
{
    /// <summary>怪物设置</summary>
    public class MonsterConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 怪物Id
        /// 
        /// 副本阶段Id*100+序号
        /// 1010100-9999999副本怪
        /// 
        /// 活动关卡Id*100+序号
        /// 10101-999999 活动怪
        /// 
        /// 1-100+指引怪
        /// 
        /// 101-999 泰坦Id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 怪物名称
        /// </summary>
        public Lang name { get; set; }
        /// <summary>
        /// 怪物头像
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// 怪物模型
        /// </summary>
        public string model { get; set; }
        /// <summary>
        /// 怪物等级
        /// </summary>
        public int level { get; set; }
        /// <summary>
        /// 怪物类型
        /// 0-小怪
        /// 1-Boss
        /// 2-泰坦
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 缩放值
        /// 0 自动
        /// 小怪0.5
        /// Boos 1
        /// 
        /// >0 缩放值
        /// </summary>
        public double scale { get; set; }
        /// <summary>
        /// 攻击间隔
        /// </summary>
        public int attackInterval { get; set; }
        /// <summary>
        /// 攻击特效
        /// </summary>
        public int attackEff { get; set; }
        /// <summary>
        /// 攻击命中
        /// 不填写使用默认
        /// </summary>
        public int attackHitEff { get; set; }
        /// <summary>
        /// 英雄元素类型
        /// 0-火焰
        /// 1-寒冰
        /// 2-自然
        /// 3-光明
        /// 4-黑暗
        /// 
        /// </summary>
        public int elemType { get; set; }
        /// <summary>
        /// 能量上限
        /// </summary>
        public int maxMP { get; set; }
        /// <summary>
        /// 同类元素攻击
        /// 抗性值%
        /// 0 无
        /// </summary>
        public int antibody { get; set; }
        /// <summary>
        /// 主动技能id
        /// 被动技能1
        /// 被动技能2
        /// </summary>
        public int[] skills { get; set; }
        /// <summary>
        /// 攻击
        /// 防御
        /// 生命
        /// </summary>
        public int[] attrs { get; set; }
        /// <summary>
        /// 闪避率
        /// (万分比)
        /// </summary>
        public int miss { get; set; }
        /// <summary>
        /// 爆击率
        /// (万分比)
        /// </summary>
        public int crit { get; set; }
    }
}
