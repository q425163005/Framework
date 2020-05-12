using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFix_Project.Module.Login.DataMgr.Data
{
    /// <summary>
    /// 服务器数据
    /// </summary>
    public class ServerItemData
    {
        /// <summary>服务器Id</summary>
        public int ServerId;

        /// <summary>
        /// 服务器状态 -2维护 -1未开放 0流畅 1拥挤 2爆满
        /// </summary>
        public int State;

        /// <summary>
        /// 服务器标记 0无 1新服 2推荐
        /// </summary>
        public int Flag;

        /// <summary>服务器名</summary>
        public string ServerName;

        /// <summary>服务连接地址</summary>
        public string URL;
    }
}