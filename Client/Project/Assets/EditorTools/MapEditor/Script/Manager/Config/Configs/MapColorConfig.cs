using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace MapEditor
{
    /// <summary>地图配色</summary>
    public class MapColorConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => mapName;
        /// <summary>
        /// 地图名称
        /// </summary>
        public string mapName { get; set; }
        /// <summary>
        /// 底板颜色
        /// </summary>
        public string bgColor { get; set; }
        /// <summary>
        /// 箭头颜色
        /// </summary>
        public string arrowsColor { get; set; }
    }
}
