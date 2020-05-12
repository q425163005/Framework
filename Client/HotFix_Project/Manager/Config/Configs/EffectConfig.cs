using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace HotFix_Project
{
    /// <summary>常规特效</summary>
    public class EffectConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 特效Id
        /// 
        /// 游戏常规特效
        /// ID范围
        /// 0 - 499
        /// </summary>
        public int id;
        /// <summary>
        /// 特效类型
        /// 0:基本特效
        /// 1:飞行特效
        /// 2:连线特效
        /// </summary>
        public int type;
        /// <summary>
        /// 特效预制名
        /// </summary>
        public string res;
        /// <summary>
        /// 声音名
        /// 特效声音统一放在Sound/EffectSound目录下
        /// </summary>
        public string sound;
        /// <summary>
        /// 等待时间
        /// (参考值0.3)
        /// 用于施法特效->过程特效等中间等待
        /// 过程特效->命中特效中间等时间 飞行特效无效
        /// 
        /// </summary>
        public double waitTime;
        /// <summary>
        /// 持续时间（秒）
        /// =0:不自动销毁,自行处理
        /// >0 持续时间到了自动销毁
        /// </summary>
        public double duration;
       
    }        
}
