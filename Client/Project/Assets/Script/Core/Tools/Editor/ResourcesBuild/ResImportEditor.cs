using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using CSF;
public class ResImportEditor : AssetPostprocessor
{
    const string UIAtlasSourceDir = "Assets/GameRes/ArtRes/UIAtlas/";
    const string TextrueDir = "Assets/GameRes/BundleRes/Textures";
    const string ArtTextrueDir = "Assets/GameRes/ArtRes/Textures";

    /// <summary>
    /// 自动设置Textures格式
    /// </summary>
    public void OnPreprocessTexture()
    {
        TextureImporter importer = assetImporter as TextureImporter;
        if (importer.assetPath.StartsWith(UIAtlasSourceDir))
            SetUIAtlasSource(importer);
        else if (importer.assetPath.StartsWith(TextrueDir)|| importer.assetPath.StartsWith(ArtTextrueDir))
            SetTexture(importer);
    }

    /// <summary>
    /// 设置UI图集图片属性
    /// </summary>
    /// <param name="importer"></param>
    static void SetUIAtlasSource(TextureImporter importer)
    {
        importer.textureType = TextureImporterType.Sprite;
    }

   
    public static void SetTexture(TextureImporter importer,bool isSave=false)
    {
        if (importer.mipmapEnabled == true)
            importer.mipmapEnabled = false;

        TextureImporterPlatformSettings iosSetting = new TextureImporterPlatformSettings();
        iosSetting.overridden = true;
        iosSetting.name = "iOS"; //Android
        iosSetting.maxTextureSize = Mathf.Min(importer.GetPlatformTextureSettings("iOS").maxTextureSize, 4096);

        TextureImporterPlatformSettings androidSetting = new TextureImporterPlatformSettings();
        androidSetting.overridden = true;
        androidSetting.name = "Android";
        androidSetting.maxTextureSize = Mathf.Min(importer.GetPlatformTextureSettings("Android").maxTextureSize, 4096);

        if (importer.DoesSourceTextureHaveAlpha())
        {
#if UNITY_2018
            iosSetting.format = TextureImporterFormat.ASTC_RGBA_6x6;            
#else
            iosSetting.format = TextureImporterFormat.ASTC_6x6;
#endif
            androidSetting.format = TextureImporterFormat.ETC2_RGBA8;
        }
        else
        {
#if UNITY_2018
            iosSetting.format = TextureImporterFormat.ASTC_RGBA_6x6;
#else
            iosSetting.format = TextureImporterFormat.ASTC_6x6;
#endif
            androidSetting.format = TextureImporterFormat.ETC2_RGB4;
        }
        importer.SetPlatformTextureSettings(iosSetting);
        importer.SetPlatformTextureSettings(androidSetting);
        if (isSave)
            importer.SaveAndReimport();

    }

    static void OnPostprocessAllAssets(
       string[] importedAssets,
       string[] deletedAssets,
       string[] movedAssets,
       string[] movedFromAssetPaths)
    {
        if(movedAssets.Length!=0)
            autoSetAssetBundleName(movedAssets);
        else if(importedAssets.Length != 0)
            autoSetAssetBundleName(importedAssets);
    }

    /// <summary>
    /// 资源导入时自动设置AssetBundle name
    /// 只修改 BundleRes目录下的资源
    /// </summary>
    /// <param name="importedAssets"></param>
    static void autoSetAssetBundleName(string[] importedAssets)
    {
        //return; //不自动改，有点卡
        string baseBunldDir = AppSetting.BundleResDir;      
        foreach (var filepath in importedAssets)
        {
            //文件夹
            if (filepath.EndsWith("/")) continue;
            if (!filepath.StartsWith(AppSetting.BundleResDir))
                continue;
            // 设置新的资源名
            var importer = AssetImporter.GetAtPath(filepath);
            if (importer == null)
            {
                ToolsHelper.Error(string.Format("Not found: {0}", filepath));
                continue;
            }
            string bundleName = filepath.Substring(baseBunldDir.Length);
            bundleName = bundleName.Replace("\\", "/").ToLower();
            if (bundleName.StartsWith(AppSetting.ConfigBundleDir.ToLower()))  //config全部打到一个文件夹中
            {
                bundleName = StringUtil.SubstringIndexOf(bundleName, '/', 1);
            }           
            importer.assetBundleName = bundleName + AppSetting.ExtName;

            //不自动改，有点卡
            //if (filepath.EndsWith(".spriteatlas"))
            //{
            //    EditSpritAtlas.SetSpriteAtlas(filepath);
            //}
        }
        AssetDatabase.Refresh();
    }

}