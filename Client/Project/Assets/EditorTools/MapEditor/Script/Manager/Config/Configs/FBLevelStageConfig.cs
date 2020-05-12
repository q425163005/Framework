using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace MapEditor
{
    /// <summary>关卡阶段</summary>
    public class FBLevelStageConfig : BaseConfig, IWarSceneConf
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 关卡阶段
        /// 副本id*100+关卡序号
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 所属关卡ID
        /// </summary>
        public int levelId { get; set; }
        /// <summary>
        /// 关卡阶段号
        /// </summary>
        public int stage { get; set; }
        /// <summary>
        /// 关卡名称
        /// 语言表id
        /// </summary>
        public Lang name { get; set; }
        /// <summary>
        /// 阶段地图(地图的png缩略图)
        /// </summary>
        public string mapIcon { get; set; }
        /// <summary>
        /// 阶段场景地图
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
        /// 大地图上关卡图标
        /// 0-100表示玩法图标
        /// 100以上表示Boss图标
        /// </summary>
        public string stageMapIcon { get; set; }
        /// <summary>
        /// 战斗背景音乐
        /// </summary>
        public string bgMusic { get; set; }
        /// <summary>
        /// 消耗行动点
        /// </summary>
        public int acPoint { get; set; }
        /// <summary>
        /// 关卡最大回合数
        /// 0无限制
        /// </summary>
        public int roundMax { get; set; }
        /// <summary>
        /// 阶段通关条件类型
        /// 0-固定回合杀死所有怪物
        /// 1-中奖元素次数
        /// </summary>
        public int overStageType { get; set; }
        /// <summary>
        /// 通关条件参数1
        /// 通关条件参数2
        /// </summary>
        public int[] overStageArgs { get; set; }
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
        /// 战斗Bonus配置Id
        /// WarBonus表(id)
        /// 
        /// (Bonus符号)
        /// </summary>
        public int warBonusId { get; set; }
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
        /// <summary>
        /// 阶段预览时，显示的怪物图标
        /// （怪物ID）
        /// </summary>
        public int[] previewMonsterId { get; set; }
        /// <summary>
        /// 怪物掉落
        /// 每个怪掉落一样
        /// (另外固定掉一个箱子)
        /// 物品Id_数量
        /// </summary>
        public List<int[]> drop { get; set; }
        /// <summary>
        /// 怪物箱子产出
        /// 物品Id_权重值
        /// (物品数量都为1)
        /// (Id为负数表示英雄)
        /// </summary>
        public List<int[]> dropBoxItems { get; set; }
        /// <summary>
        /// 元素宝箱产出
        /// 物品Id_数量_权重值
        /// (Id为负数表示英雄)
        /// </summary>
        public List<int[]> symbolBoxItems { get; set; }
        /// <summary>
        /// 通关奖励
        /// </summary>
        public List<int[]> award { get; set; }
    }
}
