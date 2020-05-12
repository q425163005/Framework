using AssetBundles;
using System;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace CSF
{
    /*快捷键用法
    % = ctrl 
    # = Shift 
    & = Alt
    LEFT/RIGHT/UP/DOWN = 上下左右
    F1…F2 = F...
    HOME, END, PGUP, PGDN = 键盘上的特殊功能键
    特别注意的是，如果是键盘上的普通按键，比如a ~z，则要写成_a ~_z这种带_前缀的。
    */

    /// <summary>
    /// CSF框架工具菜单
    /// </summary>
    public class CSFToolsMenu
    {
        private const string LastScenePrefKey = "CSF.LastSceneOpen";

        #region 资源打包

        /// <summary>
        /// Unity 5新AssetBundle系统，需要为打包的AssetBundle配置名称
        /// GameRes/BundleRes目录整个自动配置名称，因为这个目录本来就是整个导出
        /// </summary>
        [MenuItem("★工具★/资源打包/资源包命名[GameRes\\BundleRes]")]
        public static void MakeAssetBundleNames()
        {
            ResBundleTools.MakeAssetBundleNames();
        }

        /// <summary>
        /// Unity 5新AssetBundle系统，需要为打包的AssetBundle配置名称
        /// GameRes/BundleRes目录整个自动配置名称，因为这个目录本来就是整个导出
        /// </summary>
        [MenuItem("★工具★/资源打包/其它/清除非打包资源资源包命名")]
        public static void RemoveAssetBundleNames()
        {
            ResBundleTools.RemoveAssetBundleNames();
        }

        /// <summary>
        /// Unity 5新AssetBundle系统，需要为打包的AssetBundle配置名称
        /// GameRes/BundleRes目录整个自动配置名称，因为这个目录本来就是整个导出
        /// </summary>
        [MenuItem("★工具★/资源打包/是否使用仿真模式")]
        public static void ToggleSimulationMode()
        {
            AssetBundleManager.SimulateAssetBundleInEditor = !AssetBundleManager.SimulateAssetBundleInEditor;
            ToolsHelper.Log("资源包防真模式:" + (AssetBundleManager.SimulateAssetBundleInEditor ? "开启" : "关闭"));
        }

        [MenuItem("★工具★/资源打包/是否使用仿真模式", true)]
        public static bool ToggleSimulationModeValidate()
        {
            Menu.SetChecked("★工具★/资源打包/是否使用仿真模式", AssetBundleManager.SimulateAssetBundleInEditor);
            return true;
        }
        /// <summary>
        /// SpriteAtlas 
        /// isInBuild/true 编辑器下可显示图片，ab包会重复打包UI里
        /// isInBuild/false 编辑器下不可显示图片，ab包不会打到UI里
        /// </summary>
        [MenuItem("★工具★/资源打包/SpriteAtlas 编辑下可使用(打包时关闭)")]
        public static void ToggleSpriteAtlasInbulidMode()
        {
            EditSpritAtlas.SpritAtlasIsInBuild = !EditSpritAtlas.SpritAtlasIsInBuild;
            EditSpritAtlas.SetUIAtlas(EditSpritAtlas.SpritAtlasIsInBuild);
            ToolsHelper.Log("SpriteAtlas 编辑下可使用:" + (EditSpritAtlas.SpritAtlasIsInBuild ? "true" : "false"));
        }
        [MenuItem("★工具★/资源打包/SpriteAtlas 编辑下可使用(打包时关闭)", true)]
        public static bool ToggleSpriteAtlasInbulidModeValidate()
        {
            Menu.SetChecked("★工具★/资源打包/SpriteAtlas 编辑下可使用(打包时关闭)", EditSpritAtlas.SpritAtlasIsInBuild);
            return true;
        }
        //=====================================================        
        /// <summary>
        /// 重新设置UI图集引用信息
        /// </summary>
        [MenuItem("★工具★/资源打包/其它/重设UI图集引用信息")]
        public static void ResetUISpriteAtlasList()
        {
            string[] assets = AssetDatabase.FindAssets("t:Prefab", new string[] { AppSetting.BundleResDir+"UI" });
            foreach (string path in assets)
            {
                GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(path));
                if (obj != null)
                {
                    AutoSetUISpriteAtlas.SetUIPrefabSpriteAtlas(obj);
                }
            }
            ToolsHelper.Log("重设UI图集引用信息操作完成");
        }

        /// <summary>
        /// 清理冗余，即无此资源，却有AssetBundle的
        /// </summary>
        [MenuItem("★工具★/资源打包/其它/清理冗余")]
        public static void CleanAssetBundlesRedundancies()
        {
            ResBundleTools.CleanAssetBundlesRedundancies();
        }

        /// <summary>
        /// 生成AssetBundle文件信息
        /// </summary>
        [MenuItem("★工具★/资源打包/其它/生成ABFile")]
        public static void CreateAssetBundleFileInfo()
        {
            ResBundleTools.CreateAssetBundleFileInfo();
        }

        ///// <summary>
        ///// 资源缓存清理
        ///// </summary>
        //[MenuItem("★工具★/资源打包/其它/Clear Cache")]
        //public static void ClearCache()
        //{
        //    Caching.ClearCache();
        //    ToolsHelper.Log("缓存清理完成!");
        //}
        /// <summary>
        /// 资源缓存清理
        /// </summary>
        //[MenuItem("★工具★/资源打包/其它/MkLink StreamingAssets")]
        //public static void MkLinkStreamingAssets()
        //{
        //    LinkHelper.MkLinkStreamingAssets();
        //}
        [MenuItem("★工具★/资源打包/其它/Link StreamingAssets")]
        public static void MkLinkStreamingAssets()
        {
            LinkHelper.IsLinkStreamingAssets = !LinkHelper.IsLinkStreamingAssets;
            LinkHelper.MkLinkStreamingAssets();
            ToolsHelper.Log("链接资源到StreamingAssets:" + (LinkHelper.IsLinkStreamingAssets ? "链接" : "关闭"));
        }

        [MenuItem("★工具★/资源打包/其它/Link StreamingAssets", true)]
        public static bool MkLinkStreamingAssetsValidate()
        {
            Menu.SetChecked("★工具★/资源打包/其它/Link StreamingAssets", LinkHelper.IsLinkStreamingAssets);
            return true;
        }
        //=====================================================
        
        /// <summary>
        /// 重新打包，删除原始文件
        /// </summary>
        [MenuItem("★工具★/资源打包/重新生成资源(Delete)")]
        public static void ReBuildAllAssetBundles()
        {
            ResBundleTools.ReBuildAllAssetBundles();
        }

        /// <summary>
        /// 导出资源
        /// </summary>
        [MenuItem("★工具★/资源打包/生成资源")]
        public static void BuildAllAssetBundles()
        {
            ResBundleTools.BuildAllAssetBundles();
        }


        /// <summary>
        /// 打开资源目录
        /// </summary>
        [MenuItem("★工具★/资源打包/Show in Explorer")]
        public static void ShowInExplorer()
        {
            ToolsHelper.ShowExplorer(ResBundleTools.GetExportPath());
        }

        #endregion

        #region 用户数据

        /// <summary>
        /// 清理PC用户资源数据
        /// </summary>
        [MenuItem("★工具★/用户数据/删除PersistentData")]
        public static void ClearPCPersistentData()
        {
            foreach (string dir in Directory.GetDirectories(Application.persistentDataPath))
                Directory.Delete(dir, true);
            foreach (string file in Directory.GetFiles(Application.persistentDataPath))
                File.Delete(file);
            ToolsHelper.Log("删除PersistentData完成！");
        }

        /// <summary>
        /// 清理PlayerPrefs
        /// </summary>
        [MenuItem("★工具★/用户数据/删除缓存(PlayerPrefs)")]
        public static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            ToolsHelper.Log("删除缓存(PlayerPrefs)完成！");
        }

        /// <summary>
        /// 打开户资源数据
        /// </summary>
        [MenuItem("★工具★/用户数据/Show in Explorer")]
        public static void ShowInExplorerPersistentData()
        {
            ToolsHelper.ShowExplorer(Application.persistentDataPath);
        }

        #endregion

        #region 工具

        [MenuItem("★工具★/指引&&功能开放 调试")]
        public static void OpenGuideDebugWindow()
        {
            GuideDebugWindow win = EditorWindow.GetWindow<GuideDebugWindow>(false, "指引&功能开放", true);
            win.autoRepaintOnSceneChange = true;
            win.Show(true);
        }

        [MenuItem("★工具★/打开游戏工具")]
        public static void OpenTools()
        {
            string path = Path.Combine(System.Environment.CurrentDirectory, "../../工具/Tools.exe");
            ToolsHelper.OpenEXE(path);
        }

        [MenuItem("★工具★/打开热更项目文件夹")]
        public static void OpenHotProject()
        {
            string path = Path.Combine(System.Environment.CurrentDirectory, "../" + AppSetting.HotFixName);
            ToolsHelper.ShowExplorer(path);
        }

        #endregion

        #region 多语言设置

        [MenuItem("★工具★/多语言[Editor]/重新加载多语言配置", false, 0)]
        public static void ReLoadLangConfig()
        {
            LangService.Instance.LoadConfig();
            LangService.Instance.RefAllText();
        }

        [MenuItem("★工具★/多语言[Editor]/简体中文")]
        public static void LangSetZH_CN()
        {
            LangService.Instance.LangType = ELangType.ZH_CN;
        }

        [MenuItem("★工具★/多语言[Editor]/简体中文", true)]
        public static bool LangSetZH_CN_Valide()
        {
            Menu.SetChecked("★工具★/多语言[Editor]/简体中文", LangService.Instance.LangType == ELangType.ZH_CN);
            return true;
        }

        [MenuItem("★工具★/多语言[Editor]/繁体中文")]
        public static void LangSetZH_TW()
        {
            LangService.Instance.LangType = ELangType.ZH_TW;
        }

        [MenuItem("★工具★/多语言[Editor]/繁体中文", true)]
        public static bool LangSetZH_TW_Valide()
        {
            Menu.SetChecked("★工具★/多语言[Editor]/繁体中文", LangService.Instance.LangType == ELangType.ZH_TW);
            return true;
        }

        [MenuItem("★工具★/多语言[Editor]/英文")]
        public static void LangSetZH_EN()
        {
            LangService.Instance.LangType = ELangType.EN;
        }

        [MenuItem("★工具★/多语言[Editor]/英文", true)]
        public static bool LangSetZH_EN_Valide()
        {
            Menu.SetChecked("★工具★/多语言[Editor]/英文", LangService.Instance.LangType == ELangType.EN);
            return true;
        }

        [MenuItem("★工具★/多语言[Editor]/日语")]
        public static void LangSetZH_JA()
        {
            LangService.Instance.LangType = ELangType.JA;
        }

        [MenuItem("★工具★/多语言[Editor]/日语", true)]
        public static bool LangSetZH_JA_Valide()
        {
            Menu.SetChecked("★工具★/多语言[Editor]/日语", LangService.Instance.LangType == ELangType.JA);
            return true;
        }

        [MenuItem("★工具★/多语言[Editor]/韩语")]
        public static void LangSetZH_KO()
        {
            LangService.Instance.LangType = ELangType.KO;
        }

        [MenuItem("★工具★/多语言[Editor]/韩语", true)]
        public static bool LangSetZH_KO_Valide()
        {
            Menu.SetChecked("★工具★/多语言[Editor]/韩语", LangService.Instance.LangType == ELangType.KO);
            return true;
        }

        #endregion

        #region 场景切换       

        /// <summary>
        /// 打开主场景之前的一个场景
        /// </summary>
        [MenuItem("★工具★/打开上个场景 _F4")]
        public static void OpenLastScene()
        {
            var lastScene = EditorPrefs.GetString(LastScenePrefKey);
            if (!string.IsNullOrEmpty(lastScene))
                ToolsHelper.OpenScene(lastScene);
            else
                ToolsHelper.Error("Not found last scene!");
        }

        /// <summary>
        /// 打开主场景
        /// </summary>
        [MenuItem("★工具★/开始游戏 _F5")]
        public static void OpenMainScene()
        {
            var currentScene = EditorSceneManager.GetActiveScene().path;
            var mainScene = "Assets/Main.unity";
            if (mainScene != currentScene)
                EditorPrefs.SetString(LastScenePrefKey, currentScene);

            ToolsHelper.OpenScene(mainScene);

            if (!EditorApplication.isPlaying)
                EditorApplication.isPlaying = true;
        }

        [MenuItem("★工具★/关卡编辑器")]
        public static void OpenEdtiroScene()
        {
            var currentScene = EditorSceneManager.GetActiveScene().path;
            var mainScene = "Assets/EditorTools/MapEditor/MapEditor.unity";
            if (mainScene != currentScene)
                EditorPrefs.SetString(LastScenePrefKey, currentScene);

            ToolsHelper.OpenScene(mainScene);
            if (!EditorApplication.isPlaying)
                EditorApplication.isPlaying = true;

            //设置关卡编辑分辨率
            var type   = typeof(Editor).Assembly.GetType("UnityEditor.GameView");
            var window = EditorWindow.GetWindow(type);
            var SizeSelectionCallback = type.GetMethod("SizeSelectionCallback",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.NonPublic);
            SizeSelectionCallback.Invoke(window, new object[] {0, null});
        }


        [MenuItem("★工具★/英雄模型位置编辑器")]
        public static void OpenHeroModelEdtiroScene()
        {
            var currentScene = EditorSceneManager.GetActiveScene().path;
            var mainScene = "Assets/EditorTools/HeroModelEditor/WarHeroEdit.unity";
            if (mainScene != currentScene)
                EditorPrefs.SetString(LastScenePrefKey, currentScene);

            ToolsHelper.OpenScene(mainScene);
            if (!EditorApplication.isPlaying)
                EditorApplication.isPlaying = true;

            //设置关卡编辑分辨率
            var type = typeof(Editor).Assembly.GetType("UnityEditor.GameView");
            var window = EditorWindow.GetWindow(type);
            var SizeSelectionCallback = type.GetMethod("SizeSelectionCallback",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.NonPublic);
            SizeSelectionCallback.Invoke(window, new object[] { 0, null });
        }

        #endregion

        #region 一键打包
        
        ///////////////打包//////////////////////////
#if UNITY_ANDROID
        [MenuItem("★工具★/一键打包/BuildAndRun")]
        public static void CreateApp_Run()
        {
            BuildAPKTools.BulidTarget(true);
        }

        [MenuItem("★工具★/一键打包/100M包/复制资源")]
        public static void CreateCopyPart()
        {
            BuildAPKTools.CopyPartABFile();
        }
        [MenuItem("★工具★/一键打包/100M包/复制 && 打包")]
        public static void CreateAppPart()
        {
            BuildAPKTools.CopyPartABFile();
            BuildAPKTools.BulidTarget();
        }
           [MenuItem("★工具★/一键打包/打包")]
        public static void CreateApp()
        {
            BuildAPKTools.BulidTarget();
        }
#endif



        #endregion
    }
}