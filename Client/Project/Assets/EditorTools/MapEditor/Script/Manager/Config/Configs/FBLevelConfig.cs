using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace MapEditor
{
    /// <summary>副本关卡配置</summary>
    public class FBLevelConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 关卡id
        /// 副本章节id*100
        /// 关卡号
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 章节id
        /// </summary>
        public int chId { get; set; }
        /// <summary>
        /// 关卡号
        /// </summary>
        public int levelNo { get; set; }
        /// <summary>
        /// 关卡名称
        /// 语言表id
        /// </summary>
        public Lang name { get; set; }
    }
}
