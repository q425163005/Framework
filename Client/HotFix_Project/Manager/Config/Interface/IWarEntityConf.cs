/// <summary>
/// 战斗对象配置接口
/// </summary>
namespace HotFix_Project
{
    public interface IWarEntityConf
    {
        /// <summary>
        /// 英雄元素类型
        /// 0-火焰
        /// 1-寒冰
        /// 2-自然
        /// 3-光明
        /// 4-黑暗
        /// </summary>
        int elemType { get; set; }

        /// <summary>
        /// 能量上限
        /// </summary>
        int maxMP { get; set; }

        /// <summary>
        /// 同类元素攻击
        /// 抗性值(万分比)
        /// 0 无
        /// </summary>
        int antibody { get; set; }
        
        /// <summary>
        /// [主动技能id 被动技能1 被动技能2]
        /// </summary>
        int[] skills { get; set; }

        /// <summary>
        /// [攻击,防御,生命,闪避率(万分比),爆击率(万分比)
        /// </summary>
        int[] attrs { get; set; }
    }
}
