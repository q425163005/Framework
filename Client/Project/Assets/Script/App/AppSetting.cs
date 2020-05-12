using AssetBundles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CSF
{
    /// <summary>
    /// APP配置
    /// </summary>
    public class AppSetting
    {
        /// <summary>
        /// 是否为发布版
        /// 发布版模式(true): 执行正常更新流程     
        /// 更新下载完成开始游戏
        /// 开发版模式(false): 不进行版本更新检测  
        /// 可以使用仿真资源加载模式
        /// </summary>
        public static bool IsVersionCheck = true;                     //是否启用版本检测 正式包改为true

        public static bool IsLocalResServer = false; //是否为本地资源测试
        public static bool LoaddelayTest    = false; //延时加载UI测试(只有编辑器下有效)
        public static bool ILRNotABTest     = false; //不使用AB资源加载ILR(只有编辑器下有效)  
        public static bool EditorVerCheckt  = false; //编辑器下是否启用版本检测(只有编辑器下有效)  

        //public const int GameFrameRate = 60;                          //(60)游戏帧频
        public const string ExtName              = ".unity3d"; //(.unity3d)素材扩展名
        public const string UIAtlasDir           = "UIAtlas/";
        public const string SkeletonDataAssetDir = "SkeletonAnimation/";
        public const string SkeletonDataExtName  = ".asset";
        public const string SkeletonDataName     = "_SkeletonData";
        public const string HotFixName           = "HotFix_Project"; //热更工程名
        public const string ProjectName          = "Project"; //项目名
        public const bool   HotFixUnbound        = true;                 //(true)是否可使用未绑定的方法,禁用后没有CLR的方法会抛异常
        public const string ConfigBundleDir      = "Data/Config/";       //配置文件目录(相对于BundleResDir)
        public const string HoxFixBundleDir      = "Data/HotFix/";       //配置文件目录(相对于BundleResDir)
        public const string ABFiles              = "ABFiles.txt";        //AB资源文件信息  资源路径|MD5值|大小
        public const string VersionFile          = "Version.txt";        //版本信息文件
        public const string MapDataDir           = "Data/MapData/";

        public static string[] CopyAssetBundlesDirs = new string[] { "ui", "textures", "font", "effect" , "sound" };  //打部分资源包需要复制的文件夹

        public static bool IsForcedUpdate = false; //是否强制更新
        
        //HTTP Server地址 (用于请求版本信息，服务器列表)
        public static string ServerURL => "http://192.168.31.211:9821/";

        public static string VersionURL =>$"{ServerURL}v{Application.version}/";

        public static EPlatformType PlatformType
        {
            get
            {
#if UNITY_ANDROID
                return EPlatformType.Android;
#elif UNITY_IOS
                return EPlatformType.IOS;
#elif UNITY_WEBGL
                return EPlatformType.WebGL;
#else
                  return EPlatformType.PC;
#endif
            }
        }

        /// <summary>获取平台名称,后面可能会跟据不同渠道进行特殊处理</summary>
        public static string PlatformName
        {
            get { return Utility.GetPlatformName(); }
        }

        //ILR逻辑代码目录,只用于编辑环境
        public static string ILRCodeDir
        {
            get { return Path.GetFullPath("../Product/ILR/").Replace("\\", "/"); }
        }

        /// <summary>
        /// 导出资源根目录
        /// </summary>
        public static string ExportResBaseDir
        {
            get { return Path.GetFullPath("../Product/AssetBundles/").Replace("\\", "/"); }
        }

        /// <summary>
        /// 需要打包的资源目录
        /// </summary>
        public const string BundleResDir = "Assets/GameRes/BundleRes/";

        public const string BundleArtResDir = "Assets/GameRes/ArtRes/";

        public static string[] BundleArtResFolders = new string[] {"Textures", "Prefabs", "Materials"};

        /// <summary>
        /// persistentDataPath
        /// </summary>
        public static string PersistentDataPath
        {
            get {
                switch (PlatformType)
                {
                    case EPlatformType.WebGL:
                    case EPlatformType.PC:
                        return Application.streamingAssetsPath + "/" + PlatformName + "/";
                    default:
                        return Application.persistentDataPath + "/" + PlatformName + "/";
                }
            }
        }

        /// <summary>
        /// persistentDataPath
        /// </summary>
        public static string PersistentDataURL
        {
            get { return "file:///" + PersistentDataPath; }
        }

        /// <summary>
        /// StreamingAssetsUR腾讯手机助手管家L
        /// </summary>
        public static string StreamingAssetsURL
        {
#if UNITY_ANDROID
            get { return StreamingAssetsPath; }
#else
            get { return "file:///" + StreamingAssetsPath; }
#endif
        }

        /// <summary>
        /// streamingAssetsPath
        /// </summary>
        public static string StreamingAssetsPath
        {
            get { return Application.streamingAssetsPath + "/" + PlatformName + "/"; }
        }
    }
}