using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace HotFix_Project
{
    /// <summary>常规任务_类型名</summary>
    public class LanguageConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// id
        /// 例UI:UILogin.btnLoing
        /// 例表:Test/id或Test/字段/id
        /// </summary>
        public string id;
        /// <summary>
        /// 中文
        /// </summary>
        public string zh_cn;
        /// <summary>
        /// 繁体
        /// </summary>
        public string zh_tw;
        /// <summary>
        /// 英语
        /// </summary>
        public string en;
        /// <summary>
        /// 日语
        /// </summary>
        public string ja;
        /// <summary>
        /// 韩语
        /// </summary>
        public string ko;
       
    }        
}
