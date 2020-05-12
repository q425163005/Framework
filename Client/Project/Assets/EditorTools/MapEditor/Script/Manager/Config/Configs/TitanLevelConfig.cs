using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace MapEditor
{
    /// <summary>泰坦战斗关</summary>
    public class TitanLevelConfig : BaseConfig, IWarSceneConf
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 关卡Id
        /// 一个关卡对应一个泰坦
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 场景地图
        /// </summary>
        public string mapScene { get; set; }
        /// <summary>
        /// 地图类型
        /// 0 草地
        /// 1 沼泽
        /// 2 沙地
        /// 3 熔岩
        /// 4 雪地
        /// </summary>
        public int sceneType { get; set; }
        /// <summary>
        /// 战斗背景音乐
        /// </summary>
        public string bgMusic { get; set; }
        /// <summary>
        /// 关卡最大回合数
        /// 0无限制
        /// </summary>
        public int roundMax { get; set; }
        /// <summary>
        /// 战斗中获得
        /// 食物基数
        /// 
        /// (金币符号)
        /// </summary>
        public int warFood { get; set; }
        /// <summary>
        /// 战斗中获得
        /// 石头基数
        /// 
        /// (石头符号)
        /// </summary>
        public int warStone { get; set; }
        /// <summary>
        /// 战斗Box配置Id
        /// WarBox表(id)
        /// 
        /// (Box符号)
        /// (0不出现Box)
        /// </summary>
        public int warBoxId { get; set; }
        /// <summary>
        /// 战斗几率配置Id
        /// WarSymbolProb表(probId)
        /// </summary>
        public int warProbId { get; set; }
        /// <summary>
        /// 战斗连线规则Id
        /// WarLineRule表(ruleId)
        /// 1 25线
        /// </summary>
        public int warRuleId { get; set; }
        /// <summary>
        /// 特殊玩法Id
        /// WarSpecial表
        /// (0普通)
        /// </summary>
        public int warSpecialId { get; set; }
    }
}
