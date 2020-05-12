using UnityEngine;
using System.Collections;
using System.IO;
using AssetBundles;

namespace CSF
{
    public class ResUtil
    {
        /// <summary>
        /// 获取资源实际目录
        /// </summary>
        public static string GetRelativePath()
        {
            if(Application.isEditor)
                return "file://" + AppSetting.ExportResBaseDir + AppSetting.PlatformName + "/";
            if (AppSetting.PlatformType== EPlatformType.WebGL)
            {
                return Application.streamingAssetsPath + "/" + AppSetting.PlatformType+ "/";
            }
            return string.Empty;
        }      
    }
}