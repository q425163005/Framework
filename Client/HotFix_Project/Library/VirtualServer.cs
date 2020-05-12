namespace HotFix_Project
{
    /// <summary>
    /// 虚拟服务器-测试模块
    /// </summary>
    public class VirtualServer
    {
        protected static VirtualServer instance;

        /// <summary>
        /// 实例
        /// </summary>
        public static VirtualServer I{get{if (instance == null)instance = new VirtualServer();return instance; } }

        /// <summary>
        /// 初始化测试数据
        /// </summary>
        public void InitData()
        {
           
        }
        

    }
}
