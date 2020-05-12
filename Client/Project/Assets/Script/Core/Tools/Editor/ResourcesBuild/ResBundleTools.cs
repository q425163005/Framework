using AssetBundles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace CSF
{
    public class ResBundleTools
    {
        public static void RemoveAssetBundleNames()
        {
            List<string> filterDir = new List<string>();
            filterDir.Add(AppSetting.BundleResDir);
            foreach (string fold in AppSetting.BundleArtResFolders)
                filterDir.Add(AppSetting.BundleArtResDir + fold);

            bool isClena = false;
            foreach (string assetGuid in AssetDatabase.FindAssets(""))
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
                AssetImporter assetImporter = AssetImporter.GetAtPath(assetPath);
                string bundleName = assetImporter.assetBundleName;
                if (string.IsNullOrEmpty(bundleName))
                    continue;
                isClena = true;
                foreach (string filter in filterDir)
                {
                    if (assetPath.StartsWith(filter)) //清除非打包资源目录下的资源名
                    {
                        isClena = false;
                        break;
                    }
                }
                if (isClena)
                    assetImporter.assetBundleName = null;
            }
            ToolsHelper.Log("全部非打包资源AssetBundle名称已清除!");
        }

        /// <summary>
        /// Unity 5新AssetBundle系统，需要为打包的AssetBundle配置名称
        /// GameRes/BundleRes目录整个自动配置名称，因为这个目录本来就是整个导出
        /// </summary>
        public static void MakeAssetBundleNames()
        {            
            string baseBunldDir = AppSetting.BundleResDir;    
            // 设置新的资源名
            foreach (string filepath in Directory.GetFiles(baseBunldDir, "*.*", SearchOption.AllDirectories))
            {
                if (filepath.EndsWith(".meta")) continue;
                var importer = AssetImporter.GetAtPath(filepath);
                if (importer == null)
                {
                    ToolsHelper.Error(string.Format("Not found: {0}", filepath));
                    continue;
                }
                // var bundleName = filepath.Substring(baseBunldDir.Length, filepath.Length - baseBunldDir.Length);

                string bundleName = filepath.Substring(baseBunldDir.Length);

                bundleName = bundleName.Replace("\\", "/").ToLower();
                if (bundleName.StartsWith(AppSetting.ConfigBundleDir.ToLower()))  //config全部打到一个文件夹中
                {
                    bundleName = StringUtil.SubstringIndexOf(bundleName, '/', 1);
                }
                //bundleName = bundleName.Replace("\\", "/").ToLower();
                //if (bundleName.StartsWith("lua/"))
                //{
                //    bundleName = StringUtil.SubstringIndexOf(bundleName, '/', AppSetting.LuaAssetBundleDepth);
                //}
                importer.assetBundleName = bundleName + AppSetting.ExtName;
            }
            //setAtlasIncludeInBuild(true);
            //setUIAtlasPropert(); //设置UIAtlas
            MakeArtResAssetBundleNames();
            ToolsHelper.Log("设置全部资源AssetBundle名称完成!");
        }


        /// <summary>
        /// </summary>
        public static void MakeArtResAssetBundleNames()
        {
            string baseBunldDir = AppSetting.BundleArtResDir;
            string[] folders = AppSetting.BundleArtResFolders;
            foreach (string fold in folders)
            {
                foreach (string filepath in Directory.GetFiles(baseBunldDir+ fold, "*.*", SearchOption.AllDirectories))
                {
                    if (filepath.EndsWith(".meta")) continue;
                    var importer = AssetImporter.GetAtPath(filepath);
                    if (importer == null)
                    {
                        ToolsHelper.Error(string.Format("Not found: {0}", filepath));
                        continue;
                    }
                    string bundleName = filepath.Substring(baseBunldDir.Length);
                    bundleName = bundleName.Replace("\\", "/").ToLower();
                    importer.assetBundleName = bundleName + AppSetting.ExtName;                   
                }
            }

            //foreach (string assetGuid in AssetDatabase.FindAssets(""))
            //{
            //    string assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
            //    //if (!assetPath.StartsWith(baseBunldDir)) continue;
            //    AssetImporter assetImporter = AssetImporter.GetAtPath(assetPath);
            //    if (assetImporter == null) continue;
            //    string bundleName;               
            //    isCleanName = true;
            //    foreach (string fold in folders)
            //    {                    
            //        if (assetPath.StartsWith(baseBunldDir + fold+"/")) //清除非打包资源目录下的资源名
            //        {
            //            isCleanName = false;
            //            //UnityEngine.Debug.Log(assetPath);
            //            bundleName = assetPath.Substring(baseBunldDir.Length);
            //            bundleName = bundleName.Replace("\\", "/").ToLower();
            //            assetImporter.assetBundleName = bundleName + AppSetting.ExtName;
            //            UnityEngine.Debug.Log(bundleName + AppSetting.ExtName);
            //            UnityEngine.Debug.Log(assetImporter.assetBundleName);
            //            break;
            //        }
            //    }
            //    //if(isCleanName && assetImporter.assetBundleName!=string.Empty)
            //    //    assetImporter.assetBundleName = null;
            //}

        }

        ////设置UIAtlas资源名和属性　作费，暂时无用
        //static void setUIAtlasPropert()
        //{
        //    string uiAtlasDir = AppConfig.BundleResDir + AppConfig.UIAtlasFolder;
        //    DirectoryInfo uiStickers = new DirectoryInfo(uiAtlasDir);
        //    foreach (DirectoryInfo item in uiStickers.GetDirectories())//两级目录
        //    {
        //        var tagName = AppConfig.UIAtlasFolder + "/" + item.Name;
        //        var bundleName = tagName+ AppConfig.ExtName;
        //        foreach (FileInfo pngItem in item.GetFiles("*.png", SearchOption.AllDirectories))
        //        {
        //            string allPath = pngItem.FullName;
        //            string temp_assetPath = allPath.Substring(allPath.IndexOf("Assets"));
        //            TextureImporter imgPNG = AssetImporter.GetAtPath(temp_assetPath) as TextureImporter;
        //            imgPNG.textureType = TextureImporterType.Sprite;
        //            imgPNG.spriteImportMode = SpriteImportMode.Single;
        //            //自动设置打包tag; 
        //            //string dirName = Path.GetDirectoryName(allPath);
        //            //imgPNG.spritePackingTag = Path.GetFileName(dirName);
        //            imgPNG.spritePackingTag = tagName;
        //            imgPNG.sRGBTexture = true;
        //            imgPNG.alphaIsTransparency = true;
        //            imgPNG.mipmapEnabled = false;
        //            imgPNG.wrapMode = TextureWrapMode.Clamp;
        //            //imgPNG.SetPlatformTextureSettings("iPhone", 2048, TextureImporterFormat.RGBA32); 
        //            imgPNG.assetBundleName = bundleName;
        //        }
        //    }
        //} 
        /// <summary>
        /// 设置图集属性
        /// </summary>
        //static void _SetAtlasIncludeInBuild(bool isInBuild)
        //{
        //    string uiAtlasDir = AppSetting.BundleResDir + AppSetting.UIAtlasDir;
        //    DirectoryInfo dir = new DirectoryInfo(uiAtlasDir);
        //    string s = "bindAsDefault: " + (isInBuild ? "0" : "1");
        //    string r = "bindAsDefault: " + (isInBuild ? "1" : "0");
        //    FileInfo[] files = dir.GetFiles("*.spriteatlas", SearchOption.AllDirectories);
        //    int i = 0;
        //    foreach (FileInfo file in files)
        //    {
        //        string str = string.Empty;
        //        using (StreamReader stream = file.OpenText())
        //        {
        //            str = stream.ReadToEnd();
        //            stream.Close();
        //            stream.Dispose();
        //        }
        //        str = str.Replace(s, r);
        //        using (StreamWriter write = file.CreateText())
        //        {
        //            write.Write(str);
        //            write.Close();
        //            write.Dispose();
        //        }                
        //        string allPath = file.FullName;
        //        string temp_assetPath = allPath.Substring(allPath.IndexOf("Assets"));
        //        //AssetDatabase.ImportAsset(temp_assetPath, ImportAssetOptions.Default);
        //        //ToolsHelper.ShowProgress("设置图集属性", files.Length, ++i);
        //    }
        //    AssetDatabase.SaveAssets();
        //    AssetDatabase.Refresh();
        //    AssetDatabase.ImportAsset(uiAtlasDir);
        //}

     

        /// <summary>
        /// 重新打包资源
        /// </summary>
        /// <returns></returns>
        public static void ReBuildAllAssetBundles()
        {
            if (ToolsHelper.IsPlaying()) return;
            ToolsHelper.ClearConsole();
            string outputPath = GetExportPath();
            Directory.Delete(outputPath, true);
            ToolsHelper.Log("删除目录: " + outputPath);
            BuildAllAssetBundles();           
        }

        /// <summary>
        /// 打包所有资源
        /// isBuildLua 是否打包Lua文件
        /// </summary>
        public static void BuildAllAssetBundles()
        {
            if (ToolsHelper.IsPlaying()) return;
            ToolsHelper.ClearConsole();   
            EditorCoroutineRunner.StartEditorCoroutine(_OnBuildAllAssetBundles());
        }

        /// <summary>
        /// 复制热更文件
        /// </summary>
        private static void CopyHotFix()
        {
            string fileDll = AppSetting.ILRCodeDir + AppSetting.HotFixName + ".dll";
            string filePdb = AppSetting.ILRCodeDir + AppSetting.HotFixName + ".pdb";
            FileInfo file = new FileInfo(fileDll);
            if (file.Exists)
            {
                string targetPaht = Path.Combine(AppSetting.BundleResDir, AppSetting.HoxFixBundleDir, AppSetting.HotFixName);
                file.CopyTo(targetPaht+ ".bytes", true);
                new FileInfo(filePdb).CopyTo(targetPaht+ "_pdb.bytes", true);
            }
            AssetDatabase.Refresh();
        }

        static IEnumerator _OnBuildAllAssetBundles()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            yield return new WaitForSeconds(0.3f);
            CopyHotFix();
            yield return null;
            RemoveAssetBundleNames();
            MakeAssetBundleNames();
            yield return null;
            ToolsHelper.Log("资源打包中...");
            yield return null;
            var outputPath = GetExportPath();
            // _SetAtlasIncludeInBuild(false);
            EditSpritAtlas.SetUIAtlas();
            yield return new WaitForSeconds(0.5f);//DessterministicAssetBundle

            BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
            yield return new WaitForSeconds(0.5f);
            //_SetAtlasIncludeInBuild(true);
            yield return null;
            CreateAssetBundleFileInfo();
            yield return null;
            watch.Stop();
            ToolsHelper.Log("资源打包完成!!用时:" + (watch.ElapsedMilliseconds / 1000.0f)+ "秒");
            AssetDatabase.Refresh();
            yield break;
        }
        /// <summary>
        /// 获取导出资源路径目录
        /// </summary>
        /// <returns></returns>
        public static string GetExportPath()
        {
            BuildTarget platfrom = EditorUserBuildSettings.activeBuildTarget;
            string basePath = AppSetting.ExportResBaseDir;

            if (File.Exists(basePath))
            {
                ToolsHelper.ShowDialog("路径配置错误: " + basePath);
                throw new System.Exception("路径配置错误");
            }
            string path = null;
            var platformName = AppSetting.PlatformName;
            path = basePath +platformName+"/";
            ToolsHelper.CreateDir(path);
            return path;
        }

        /// <summary>
        /// 清理冗余，即无此资源，却有AssetBundle的
        /// </summary>
        public static void CleanAssetBundlesRedundancies()
        {
            var platformName = AppSetting.PlatformName;
            var outputPath = GetExportPath();

            int count = 0;
            var toList = new List<string>(Directory.GetFiles(outputPath, "*.*", SearchOption.AllDirectories));
            for (var i = toList.Count - 1; i >= 0; i--)
            {
                var filePath = toList[i];               
                var abName = toList[i].Replace(outputPath, "").Replace('\\', '/');
                var extName = Path.GetExtension(filePath);
                if (abName != platformName && extName!=".manifest")
                {
                    //删除.meta文件
                    if (extName==".meta")
                    {
                        File.Delete(filePath);
                    }
                    else
                    {
                        if (AssetDatabase.GetAssetPathsFromAssetBundle(abName).Length == 0)
                        {
                            var manifestPath = filePath + ".manifest";
                            File.Delete(filePath);
                            ToolsHelper.Log("Delete... " + filePath);
                            count += 1;
                            if (File.Exists(manifestPath))
                            {
                                File.Delete(manifestPath);
                                ToolsHelper.Log("Delete... " + manifestPath);
                                count += 1;
                            }
                        }
                    }
                }
                //删除空文件夹
                DirectoryInfo dir = new DirectoryInfo(outputPath);
                DirectoryInfo[] subdirs = dir.GetDirectories("*.*", SearchOption.AllDirectories);
                foreach (DirectoryInfo subdir in subdirs)
                {
                    FileInfo[] subFiles = subdir.GetFiles();
                    if (subFiles.Length == 0)
                    {
                        subdir.Delete();
                    }
                }                
            }
            ToolsHelper.Log("清理冗余文件完成!! 共" + count + "个文件!");
        }

        /// <summary>
        /// 生成AB资源文件列表
        /// </summary>
        public static void CreateAssetBundleFileInfo()
        {
            string abRootPath = GetExportPath();
            string abFilesPath = abRootPath + "/"+AppSetting.ABFiles;
            if (File.Exists(abFilesPath))
                File.Delete(abFilesPath);

            var abFileList = new List<string>(Directory.GetFiles(abRootPath, "*"+AppSetting.ExtName, SearchOption.AllDirectories));
            abFileList.Add(abRootPath + AppSetting.PlatformName);
            FileStream fs = new FileStream(abFilesPath, FileMode.CreateNew);
            StreamWriter sw = new StreamWriter(fs);

            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(2018, 1, 1));
            int ver =  ((int)((DateTime.Now - startTime).TotalMinutes));
            sw.WriteLine(ver + "|" + DateTime.Now.ToString("u"));
            for (int i = 0; i < abFileList.Count; i++)
            {
                string file = abFileList[i];
                long size = 0;
                string md5 = MD5Utils.MD5File(file,out size);
                string value = file.Replace(abRootPath, string.Empty).Replace("\\","/");
                sw.WriteLine(value + "|" + md5+"|"+ size);
            }
            sw.Close();
            fs.Close();
            ToolsHelper.Log("资源版本Version:"+ver+ "  已复制到剪切板");
            ToolsHelper.Log("ABFiles文件生成完成");
            ToolsHelper.CopyString(ver.ToString());
        }


        //public static void BuildHotFixAssetBundles()
        //{
        //    if (ToolsHelper.IsPlaying()) return;
        //    ToolsHelper.ClearConsole();
        //    Stopwatch watch = new Stopwatch();
        //    watch.Start();
        //    CopyHotFix();
        //    Dictionary<string, List<string>> buildMap = new Dictionary<string, List<string>>();
        //    string baseBunldDir = "Assets/GameRes/BundleRes/Data";
        //    List<string> abNames;
        //    foreach (string filepath in Directory.GetFiles(baseBunldDir, "*.*", SearchOption.AllDirectories))
        //    {
        //        if (filepath.EndsWith(".meta")) continue;
        //        AssetImporter importer = AssetImporter.GetAtPath(filepath);
        //        if (importer == null)
        //            continue;
        //        if (importer.assetBundleName == string.Empty)
        //        {
        //            ToolsHelper.Warning("文件没设置AB名:" + filepath);
        //            continue;
        //        }
        //        if (!buildMap.TryGetValue(importer.assetBundleName, out abNames))
        //        {
        //            abNames = new List<string>();
        //            buildMap.Add(importer.assetBundleName, abNames);
        //        }
        //        abNames.Add(importer.assetPath);
        //    }
        //    // 设置新的资源名
        //    List<AssetBundleBuild> abList = new List<AssetBundleBuild>();
        //    foreach (KeyValuePair<string, List<string>> keyVal in buildMap)
        //    {
        //        AssetBundleBuild ab = new AssetBundleBuild();
        //        ab.assetBundleName = keyVal.Key;
        //        ab.assetNames = keyVal.Value.ToArray();
        //        abList.Add(ab);
        //    }
        //    var outputPath = GetExportPath();
        //    BuildPipeline.BuildAssetBundles(outputPath, abList.ToArray(), BuildAssetBundleOptions.DeterministicAssetBundle, EditorUserBuildSettings.activeBuildTarget);
        //    CreateAssetBundleFileInfo();
        //    watch.Stop();
        //    ToolsHelper.Log("Data文件打包完成!!用时:" + (watch.ElapsedMilliseconds / 1000.0f) + "秒");
        //}
    }
}