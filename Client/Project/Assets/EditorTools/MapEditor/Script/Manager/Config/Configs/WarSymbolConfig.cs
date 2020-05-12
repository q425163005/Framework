using System;
using System.Collections.Generic;
namespace MapEditor
{
    /// <summary>战斗符号</summary>
    public class WarSymbolConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => type;
        /// <summary>
        /// 符号类型
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 元素名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 消除类型
        /// </summary>
        public int cleanType { get; set; }
        /// <summary>
        /// 是否在地图编辑器中显示
        /// </summary>
        public bool isMapEdit { get; set; }
        /// <summary>
        /// 炸弹伤害参数
        /// </summary>
        public int arg { get; set; }
    }
}