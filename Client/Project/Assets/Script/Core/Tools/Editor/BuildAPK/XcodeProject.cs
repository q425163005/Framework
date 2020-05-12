#if UNITY_IOS
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.Collections;
using System.IO;

public class XcodeProject : MonoBehaviour
{
    //复制AppIcon文件
    private static void copyAppIcon(string targetPath)
    {
        string appIconRoot = Path.Combine(Application.dataPath ,"GameRes/AppIcon/ios/AppIcon.appiconset");
        string targetRoot = Path.Combine(targetPath, "Unity-iPhone/Images.xcassets/AppIcon.appiconset");
        copyAppFile(appIconRoot, targetRoot);
    }
    internal static void copyAppFile(string sourceRoot, string targetRoot)
    {
        Directory.Delete(targetRoot, true);
        Directory.CreateDirectory(targetRoot);
        string[] files = Directory.GetFiles(sourceRoot);
        foreach (string file in files)
        {
            if (file.EndsWith(".json") || file.EndsWith(".png"))
            {
                string tagFile = Path.Combine(targetRoot, Path.GetFileName(file));
                File.Copy(file, tagFile, true);
            }
        }
    }


    [PostProcessBuild]
    public static void OnPostprocessBuild(BuildTarget buildTarget, string path)
    {
        if (buildTarget == BuildTarget.iOS)
        {
            Debug.Log(path);
            EditProj(path);   //暂时不需要修改
            EditInfoPlist(path); //暂时不需要修改
            copyAppIcon(path);
        }
    }
    static void EditProj(string path)
    {
        string projPath = PBXProject.GetPBXProjectPath(path);
        PBXProject proj = new PBXProject();
        proj.ReadFromString(File.ReadAllText(projPath));
        string target = proj.TargetGuidByName("Unity-iPhone");
        // 添加framwrok True if the framework is optional (i.e. weakly linked), false if the framework is required.
        proj.AddFrameworkToProject(target, "AuthenticationServices.framework", true);
        //Sing in With Apple
        //proj.AddCapability(target, PBXCapabilityType.
        //proj.AddCapability(target, PBXCapabilityType.GameCenter);
        //proj.AddCapability(target, PBXCapabilityType.InAppPurchase);
        File.WriteAllText(projPath, proj.WriteToString());


        //string projPath = PBXProject.GetPBXProjectPath(path);
        //PBXProject proj = new PBXProject();        
        //proj.ReadFromString(File.ReadAllText(projPath));
        //string target = proj.TargetGuidByName("Unity-iPhone");
        //proj.AddBuildProperty(target, "OTHER_LDFLAGS", "-ObjC");
        //proj.SetBuildProperty(target, "ENABLE_BITCODE", "NO");

        //// 添加framwrok
        //proj.AddFrameworkToProject(target, "Accelerate.framework", false);
        //proj.AddFrameworkToProject(target, "CoreSpotlight.framework", false);
        //proj.AddFrameworkToProject(target, "CoreLocation.framework", false);        
        //proj.AddFrameworkToProject(target, "MobileCoreServices.framework", false);
        //proj.AddFrameworkToProject(target, "QuickLook.framework", false);
        //proj.AddFrameworkToProject(target, "Photos.framework", false);
        //proj.AddFrameworkToProject(target, "Social.framework", false);
        //proj.AddFrameworkToProject(target, "SafariServices.framework", false);
        //proj.AddFrameworkToProject(target, "Accounts.framework", false);
        //proj.AddFrameworkToProject(target, "WebKit.framework", false);
        //proj.AddFrameworkToProject(target, "AdSupport.framework", false);
        //proj.AddFrameworkToProject(target, "CoreTelephony.framework", false);
        //proj.AddFrameworkToProject(target, "LocalAuthentication.framework", false);
        //proj.AddFrameworkToProject(target, "GameKit.framework", false);
        ////添加lib
        //AddLibToProject(proj, target, "libiconv.tbd");
        //AddLibToProject(proj, target, "libicucore.tbd");
        //AddLibToProject(proj, target, "libxml2.tbd");
        //AddLibToProject(proj, target, "libsqlite3.tbd");
        //AddLibToProject(proj, target, "libxml2.tbd");
        //AddLibToProject(proj, target, "libc++.tbd");

        //proj.AddCapability(target, PBXCapabilityType.GameCenter);
        //proj.AddCapability(target, PBXCapabilityType.InAppPurchase);

        ////添加自定义库
        ////CopyAndReplaceDirectory("Assets/Lib/mylib.framework", Path.Combine(path, "Frameworks/mylib.framework"));
        ////proj.AddFileToBuild(target, proj.AddFile("Frameworks/mylib.framework", "Frameworks/mylib.framework", PBXSourceTree.Source));

        //////添加文件
        ////var fileName = "my_file.xml";
        ////var filePath = Path.Combine("Assets/Lib", fileName);
        ////File.Copy(filePath, Path.Combine(path, fileName));
        ////proj.AddFileToBuild(target, proj.AddFile(fileName, fileName, PBXSourceTree.Source));


        //// Yosemiteでipaが書き出せないエラーに対応するための設定
        ////proj.SetBuildProperty(target, "CODE_SIGN_RESOURCE_RULES_PATH", "$(SDKROOT)/ResourceRules.plist");
        ////设置/添加框架搜索路径
        ////proj.SetBuildProperty(target, "FRAMEWORK_SEARCH_PATHS", "$(inherited)");
        ////proj.AddBuildProperty(target, "FRAMEWORK_SEARCH_PATHS", "$(PROJECT_DIR)/Frameworks");


        ////project.overwriteBuildSetting ("CODE_SIGN_IDENTITY", "dev.boom", "Release");
        ////project.overwriteBuildSetting ("CODE_SIGN_IDENTITY", "dev.boom", "Debug");

        ////proj.SetBuildProperty(target,"CODE_SIGN_IDENTITY", "saima-dev");

        ////var sign = EditorPrefs.GetString("JenkinsBuilder.sign", "iPhone Distribution: BABYBUS CO., LIMITED (AMSTN4PRF9)");
        ////pbxProj.SetBuildProperty(targetGuid, "CODE_SIGN_IDENTITY", sign);

        ////var provision = EditorPrefs.GetString("JenkinsBuilder.provision", "f4f9a377-57f0-4e0d-81c0-00904ac17378");
        ////pbxProj.SetBuildProperty(targetGuid, "PROVISIONING_PROFILE", provision);

        ////pbxProj.SetBuildProperty(targetGuid, "CODE_SIGN_ENTITLEMENTS", "Libraries/BabyFrameWork/Babybus.entitlements");

        ////pbxProj.SetBuildProperty(targetGuid, "GCC_C_LANGUAGE_STANDARD", "gnu99");

        //File.WriteAllText(projPath, proj.WriteToString());

    }
    //添加lib方法
    static void AddLibToProject(PBXProject inst, string targetGuid, string lib)
    {
        string fileGuid = inst.AddFile("usr/lib/" + lib, "Frameworks/" + lib, PBXSourceTree.Sdk);
        inst.AddFileToBuild(targetGuid, fileGuid);
    }

    static void EditInfoPlist(string filePath)
    {
        var path = filePath + "/Info.plist";

        var plistDocument = new PlistDocument();
        plistDocument.ReadFromFile(path);

        var dictRoot = plistDocument.root.AsDict();

        dictRoot.SetBoolean("ITSAppUsesNonExemptEncryption", false);  //缺少出口合规证明
        plistDocument.WriteToFile(path);
    }

    static void EditInfoPlist_Saima(string filePath)
    {
        var path = filePath + "/Info.plist";

        var plistDocument = new PlistDocument();
        plistDocument.ReadFromFile(path);

        var dictRoot = plistDocument.root.AsDict();

        dictRoot.SetString("CFBundleDisplayName","香港育馬王");
        dictRoot.SetString("NSPhotoLibraryUsageDescription", "是否允許訪問相冊");
        dictRoot.SetString("NSPhotoLibraryAddUsageDescription", "是否允許保存圖片到相冊");
        dictRoot.SetString("NSCameraUsageDescription", "是否允許使用相機");
        dictRoot.SetString("NSMicrophoneUsageDescription", "是否允許使用麥克風");

        //dictRoot.SetString("NSLocationUsageDescription", "是否允許獲取位置");
        //dictRoot.SetString("NSLocationWhenInUseUsageDescription", "是否允許在使用期間獲取位置");
        //dictRoot.SetString("NSLocationAlwaysUsageDescription", "是否允許始終獲取位置");
        //dictRoot.SetString("NSCalendarsUsageDescription", "是否允許訪問日曆");
        //dictRoot.SetString("NSRemindersUsageDescription", "是否允許使用提醒事項");
        //dictRoot.SetString("NSMotionUsageDescription", "是否允許使用運動與健身");
        //dictRoot.SetString("NSHealthUpdateUsageDescription", "是否允許健康更新");
        //dictRoot.SetString("NSHealthShareUsageDescription", "是否允許健康分享");
        //dictRoot.SetString("NSBluetoothPeripheralUsageDescription", "是否允許使用藍牙");
        //dictRoot.SetString("NSAppleMusicUsageDescription", "是否允許媒體資料庫");


        var bundleURLTypes = dictRoot.CreateArray("CFBundleURLTypes");
        var dict = bundleURLTypes.AddDict();
        dict.SetString("CFBundleTypeRole", "Editor");
        var bundleURLSchemes = dict.CreateArray("CFBundleURLSchemes");
        bundleURLSchemes.AddString("scheme.com.saima.chuanqi");


        dict = bundleURLTypes.AddDict();
        dict.SetString("CFBundleTypeRole", "Editor");
        bundleURLSchemes = dict.CreateArray("CFBundleURLSchemes");
        bundleURLSchemes.AddString("fb339219903555107");

        dict = bundleURLTypes.AddDict();
        dict.SetString("CFBundleTypeRole", "Editor");
        bundleURLSchemes = dict.CreateArray("CFBundleURLSchemes");
        bundleURLSchemes.AddString("twitterkit-2HsWLr1uV6flDeQr1jc9ZNYaU");

        dictRoot.SetString("FacebookAppID", "339219903555107");

        var appQueriesSchemes = dictRoot.CreateArray("LSApplicationQueriesSchemes");
        appQueriesSchemes.AddString("mqq");
        appQueriesSchemes.AddString("mqqwpa");
        appQueriesSchemes.AddString("fb-messenger-api");
        appQueriesSchemes.AddString("fb-messenger-share-api");
        appQueriesSchemes.AddString("fbauth2");
        appQueriesSchemes.AddString("fbshareextension");
        appQueriesSchemes.AddString("fbapi");
        appQueriesSchemes.AddString("twitter");
        appQueriesSchemes.AddString("twitterauth");


        //var array2 = dict2.CreateArray("CFBundleURLSchemes");
        //array2.AddString(PlayerSettings.bundleIdentifier);

        //var wxappid = BuildProductKeys.GetValue("ios_um_wxappid");
        //if (!string.IsNullOrEmpty(wxappid))
        //{
        //    dict2 = array.AddDict();
        //    dict2.SetString("CFBundleURLName", "weixin");
        //    array2 = dict2.CreateArray("CFBundleURLSchemes");
        //    array2.AddString(wxappid);
        //}

        //var qqappid = BuildProductKeys.GetValue("ios_um_qqappid");
        //if (!string.IsNullOrEmpty(qqappid))
        //{
        //    dict2 = array.AddDict();
        //    dict2.SetString("CFBundleURLName", "");
        //    array2 = dict2.CreateArray("CFBundleURLSchemes");
        //    array2.AddString("tencent" + qqappid);

        //    dict2 = array.AddDict();
        //    dict2.SetString("CFBundleURLName", "");
        //    array2 = dict2.CreateArray("CFBundleURLSchemes");
        //    array2.AddString("QQ" + int.Parse(qqappid).ToString("X"));
        //}

        //var sinakey = BuildProductKeys.GetValue("ios_um_sinakey");
        //if (!string.IsNullOrEmpty(sinakey))
        //{
        //    dict2 = array.AddDict();
        //    dict2.SetString("CFBundleURLName", "");
        //    array2 = dict2.CreateArray("CFBundleURLSchemes");
        //    array2.AddString("wb" + sinakey);
        //}

        ////[[UIApplication sharedApplication] openURL:[NSURL URLWithString:@"prefs:root=WIFI"]];
        //dict2 = array.AddDict();
        //dict2.SetString("CFBundleURLName", "");
        //array2 = dict2.CreateArray("CFBundleURLSchemes");
        //array2.AddString("prefs");

        //dict.SetString("CFBundleIdentifier", Application.bundleIdentifier);

        //var lines = File.ReadAllText("iOS产品白名单").Split('\n');

        //array = dict.CreateArray("LSApplicationQueriesSchemes");

        //foreach (var line in lines)
        //{
        //    array.AddString(line.Replace("\r", "").Replace("\n", ""));
        //}

        //dict.SetString("View controller-based status bar appearance", "NO");

        plistDocument.WriteToFile(path);
    }
}
#endif