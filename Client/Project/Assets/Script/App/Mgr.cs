using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSF
{
    /// <summary>
    /// 管理器统一入口
    /// </summary>
    public class Mgr
    {
        /// <summary>资源管理器</summary>
        public static AssetbundleMgr Assetbundle;
        /// <summary>网络管理器</summary>
        public static NetMgr Net;
        /// <summary>UI管理器</summary>
        public static UIMgr UI;
        /// <summary>ILRuntime管理器</summary>
        public static ILRMgr ILR;
        /// <summary>版本检测管理器</summary>
        public static VersionCheckMgr VersionCheck;
        public static CTaskMgr Task;
        public static void Initialize()
        {
            Assetbundle = AssetbundleMgr.Create();
            Net = NetMgr.Create();
            UI = UIMgr.Create();
            ILR = ILRMgr.Create();
            VersionCheck = VersionCheckMgr.Create();
            Task = CTaskMgr.Create();
        }

        public static void Dispose()
        {
            Assetbundle?.Dispose();
            Net?.Dispose();
            UI?.Dispose();
            ILR?.Dispose();
            VersionCheck?.Dispose();
            Task?.StopAll();
        }
    }
}