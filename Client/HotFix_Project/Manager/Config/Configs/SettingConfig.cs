using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace HotFix_Project
{
    /// <summary>系统设置</summary>
    public class SettingConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => NameMaxLen;
        /// <summary>
        /// 名字最大长度,一个汉字占二个长度 (玩家名，马名，公会名公用)
        /// </summary>
        public int NameMaxLen;
        /// <summary>
        /// 清除1分钟CD花费点券数 没特殊指定统一使用这个
        /// </summary>
        public int ClearCDTicket;
       
    }        
}
