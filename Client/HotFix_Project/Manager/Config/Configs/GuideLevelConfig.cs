using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace HotFix_Project
{
    /// <summary>指引战斗关</summary>
    public class GuideLevelConfig : BaseConfig,IWarSceneConf
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 关卡Id
        /// </summary>
        public int id{ get; set; }
        /// <summary>
        /// 场景地图
        /// </summary>
        public string mapScene{ get; set; }
        /// <summary>
        /// 战斗背景音乐
        /// </summary>
        public string bgMusic;
        /// <summary>
        /// 关卡最大回合数
        /// 0无限制
        /// </summary>
        public int roundMax{ get; set; }
        /// <summary>
        /// 战斗中获得
        /// 食物基数
        /// 
        /// (金币符号)
        /// </summary>
        public int warFood{ get; set; }
        /// <summary>
        /// 战斗中获得
        /// 石头基数
        /// 
        /// (石头符号)
        /// </summary>
        public int warStone{ get; set; }
        /// <summary>
        /// 战斗Box配置Id
        /// WarBox表(id)
        /// 
        /// (Box符号)
        /// (0不出现Box)
        /// </summary>
        public int warBoxId{ get; set; }
        /// <summary>
        /// 战斗几率配置Id
        /// WarSymbolProb表(probId)
        /// </summary>
        public int warProbId{ get; set; }
        /// <summary>
        /// 战斗连线规则Id
        /// WarLineRule表(ruleId)
        /// 1 25线
        /// </summary>
        public int warRuleId{ get; set; }
        /// <summary>
        /// 特殊玩法Id
        /// WarSpecial表
        /// (0普通)
        /// </summary>
        public int warSpecialId{ get; set; }
        /// <summary>
        /// 元素队例
        /// 队列用完从随机里选取
        /// 0 火焰
        /// 1 寒冰
        /// 2 自然
        /// 3 光明
        /// 4 黑暗
        /// 
        /// </summary>
        public int[] symbolsQueue;
        /// <summary>
        /// 五元素出现权重
        /// 0火焰;1寒冰;2自然;3光明;4黑暗
        /// (不填权重都一样)
        /// 如要填5个都要配
        /// </summary>
        public int[] symbolWeight;
        /// <summary>
        /// 出战英雄模板
        /// </summary>
        public int[] heros;
       
    }        
}
