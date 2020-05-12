using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace HotFix_Project
{
    /// <summary>物品配置</summary>
    public class ItemConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 物品id
        /// </summary>
        public int id;
        /// <summary>
        /// 物品名称
        /// 语言ID
        /// </summary>
        public Lang name;
        /// <summary>
        /// 物品图标
        /// </summary>
        public string icon;
        /// <summary>
        /// 品质
        /// EQuality
        /// 1 1颗星
        /// 2 2颗星
        /// 3 3颗星
        /// 4 4颗星
        /// 5 5颗星
        /// </summary>
        public int quality;
        /// <summary>
        /// 物品大类
        /// 0虚拟物品
        /// 1道具
        /// 2装备
        /// 3材料
        /// 4头像
        /// 
        /// </summary>
        public int type;
        /// <summary>
        /// 物品子类
        /// </summary>
        public int subType;
        /// <summary>
        /// 物品等级
        /// （强化石，合成石等）
        /// 
        /// 礼包类为使用等级，不填为无等级限制
        /// </summary>
        public int level;
        /// <summary>
        /// 参数1
        /// 使用参照物品类型说明
        /// </summary>
        public int arg1;
        /// <summary>
        /// 参数2
        /// 使用参照物品类型说明
        /// </summary>
        public int arg2;
        /// <summary>
        /// 卖出价格
        /// （金币）
        /// 0 不可出售
        /// </summary>
        public int sell;
        /// <summary>
        /// 点券价格
        /// </summary>
        public int price;
        /// <summary>
        /// 有效时间(秒)
        /// 0永久有效
        /// 
        /// </summary>
        public int validTime;
        /// <summary>
        /// 物品描述
        /// 语言ID
        /// </summary>
        public Lang des;
        /// <summary>
        /// 获取途径
        /// 1 副本战斗
        /// 2 联盟战
        /// </summary>
        public Lang place;
       
    }        
}
