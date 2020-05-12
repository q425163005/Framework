using CSF;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class BuildAPKTools
{
    static string[] SCENES = FindEnabledEditorScenes();

    public static void BulidTarget(bool isRun = false)
    {
        BuildTarget buildTarget = EditorUserBuildSettings.activeBuildTarget;
        if (buildTarget != BuildTarget.Android) return;

        EPlatformType pfType = AppSetting.PlatformType;
        string app_name =
            $"{Application.productName}.{pfType.ToString()}_{Application.version}_{DateTime.Now.ToString("yyyyMMdd_HHmm")}";
        app_name = app_name.Replace(" ", "");
        string      target_dir      = "";
        string      target_name     = "";
       
        string      applicationPath = Application.dataPath.Replace("/Assets", "");
        Debug.Log("Start Bulid:" + app_name);
        target_dir  = applicationPath + "/Builds/Android";
        target_name = app_name        + ".apk";

        Directory.CreateDirectory(target_dir);
        BuildPipeline.BuildPlayer(SCENES, target_dir + "/" + target_name, buildTarget,
                                  isRun ? BuildOptions.AutoRunPlayer : BuildOptions.None);
    }


    private static string[] FindEnabledEditorScenes()
    {
        List<string> EditorScenes = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled) continue;
            EditorScenes.Add(scene.path);
        }

        return EditorScenes.ToArray();
    }

    public static void CopyPartABFile()
    {
        LinkHelper.DeleteLinkStreamingAssets(); //删除
        string streamingPath = Application.streamingAssetsPath + "/" + AppSetting.PlatformName + "/";
        string exportPath    = AppSetting.ExportResBaseDir + AppSetting.PlatformName           + "/";
        ToolsHelper.CreateDir(streamingPath);
        foreach (var dir in AppSetting.CopyAssetBundlesDirs)
            FileUtil.CopyFileOrDirectory(exportPath + dir, streamingPath + dir);


        string abRootPath  = streamingPath;
        string abFilesPath = abRootPath + "/" + AppSetting.ABFiles;
        if (File.Exists(abFilesPath))
            FileUtil.DeleteFileOrDirectory(abFilesPath);

        var abFileList =
            new List<string>(Directory.GetFiles(abRootPath, "*" + AppSetting.ExtName, SearchOption.AllDirectories));
        FileStream   fs = new FileStream(abFilesPath, FileMode.CreateNew);
        StreamWriter sw = new StreamWriter(fs);

        int ver = 0;
        sw.WriteLine(ver + "|" + DateTime.Now.ToString("u"));
        long sizeCount = 0;
        for (int i = 0; i < abFileList.Count; i++)
        {
            string file = abFileList[i];
            long   size = 0;
            string md5  = MD5Utils.MD5File(file, out size);
            sizeCount += size;
            string value = file.Replace(abRootPath, string.Empty).Replace("\\", "/");
            sw.WriteLine(value + "|" + md5 + "|" + size);
        }

        sw.Close();
        fs.Close();
        AssetDatabase.Refresh();
        float s = sizeCount / (1024 * 1024f);
        if (s > 50)
            CLog.Error($"打入资源有{s.ToString("f2")}M,超过50M的资源包体可能会大于100M");

        ToolsHelper.Log($"复制资源完成 {s.ToString("f2")}M!!!!!");
    }
}