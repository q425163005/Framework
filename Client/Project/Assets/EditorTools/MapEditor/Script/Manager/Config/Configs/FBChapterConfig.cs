using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace MapEditor
{
    /// <summary>副本章节</summary>
    public class FBChapterConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 副本章节id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 副本章节名称
        /// </summary>
        public Lang name { get; set; }
        /// <summary>
        /// 开启条件
        /// 需要玩家等级
        /// </summary>
        public int needLv { get; set; }
        /// <summary>
        /// 副本地图图片
        /// </summary>
        public string imgName { get; set; }
    }
}
