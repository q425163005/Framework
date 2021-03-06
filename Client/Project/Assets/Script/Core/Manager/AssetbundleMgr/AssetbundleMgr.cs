﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UObject = UnityEngine.Object;
using UnityEngine.U2D;
using AssetBundles;
using System.Threading.Tasks;
using CSF.Tasks;

namespace CSF
{
    /// <summary>
    /// 资源包管理器
    /// 全部资源包加载都使用异步加载
    /// </summary>
    public class AssetbundleMgr : BaseMgr<AssetbundleMgr>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="initOK"></param>
        public async CTask Initialize()
        {
            AssetBundleManager.logMode = AssetBundleManager.LogMode.JustErrors;
            AssetBundleManager.BaseDownloadingURL = ResUtil.GetRelativePath();
            AssetBundleLoadManifestOperation request = AssetBundleManager.Initialize();
            if (request == null) return;
            await request;
            //await CTask.WaitUntil(() => { return AssetBundles.AssetBundleManager.AssetBundleManifestObject!=null; });
        }
        
        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <param name="assetBundleName">完整资源包名,注：带文件扩展名</param>
        /// <param name="assetName">资源名</param>
        public async CTask<T> LoadAsset<T>(string assetBundleName, string assetName) where T : UObject
        {
            assetBundleName = assetBundleName.ToLower();
            assetBundleName += AppSetting.ExtName;
            return await _LoadAsset<T>(assetBundleName, assetName);
        }

        /// <summary>
        /// 异步加载预制
        /// </summary>
        /// <param name="assetBundleName">资源包名</param>
        /// <param name="assetName">资源名,不填自动获取资源包最后的名字</param>
        public async CTask<GameObject> LoadPrefab(string assetBundleName, string assetName=null)
        {
            if (string.IsNullOrEmpty(assetName))
                assetName = assetBundleName.Substring(assetBundleName.LastIndexOf("/")+1);
            assetBundleName += ".prefab";
            assetBundleName = assetBundleName.ToLower();
            assetBundleName += AppSetting.ExtName;
            GameObject obj = await _LoadAsset<GameObject>(assetBundleName, assetName, true);
            GameObject rtn  = Instantiate(obj) as GameObject;
            Utils.ResetShader(rtn);
            return rtn;
        }
        /// <summary>
        /// 异步加载图集
        /// </summary>
        /// <param name="assetBundleName"></param>
        public async CTask<SpriteAtlas> LoadSpriteAtlas(string assetBundleName)
        {
            string assetName = assetBundleName.Substring(assetBundleName.LastIndexOf("/") + 1);
            assetBundleName = AppSetting.UIAtlasDir + assetBundleName + ".spriteatlas";            
            return await LoadAsset<SpriteAtlas>(assetBundleName, assetName);
        }
        /// <summary>
        /// 异步加载材质球
        /// </summary>
        /// <param name="assetBundleName"></param>
        public async CTask<Material> LoadMaterial(string assetBundleName)
        {
            string assetName = assetBundleName.Substring(assetBundleName.LastIndexOf("/") + 1);
            assetBundleName = "Materials/" + assetBundleName + ".mat";
            return await LoadAsset<Material>(assetBundleName, assetName);
        }

        /// <summary>
        /// 加载Texture
        /// </summary>
        /// <param name="assetBundleName"></param>
        /// <returns></returns>
        public async CTask<Texture> LoadTexture(string assetBundleName)
        {
            string assetName = assetBundleName.Substring(assetBundleName.LastIndexOf("/") + 1);
            assetBundleName = "Textures/" + assetBundleName + ".png";
            return await LoadAsset<Texture>(assetBundleName, assetName);
        }


        /// <summary>
        /// 异步加载场景
        /// </summary>
        /// <param name="sceneName">场景名</param>
        /// <param name="isAdditive">是否叠加场景</param>
        /// <param name="cbProgress">进度回调</param>
        public async CTask LoadScene(string sceneName, bool isAdditive = false, Action<float> cbProgress = null)
        {
            string sceen = sceneName.Substring(sceneName.LastIndexOf("/") + 1);
            string assetBundleName = "scene/" + sceneName.ToLower() + ".unity";          
            if (!assetBundleName.EndsWith(AppSetting.ExtName))
                assetBundleName += AppSetting.ExtName;
            await _LoadScene(assetBundleName, sceen, isAdditive, cbProgress);
        }

        public void UnloadAssetBundle(string assetBundleName)
        {
             if (!assetBundleName.EndsWith(AppSetting.ExtName))
                assetBundleName += AppSetting.ExtName;
            AssetBundleManager.UnloadAssetBundle(assetBundleName.ToLower(),true);
        }

        #region 私有协同方法
        /// <summary>
        /// 异步加载资源
        /// </summary>
        private async CTask<T> _LoadAsset<T>(string assetBundleName, string assetName, bool isWait = false) where T : UObject
        {
            // Load asset from assetBundle.
            AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(assetBundleName, assetName, typeof(T));
            if (request == null) return default(T);
            await CTask.WaitUntil(() => { return request.IsDone(); });
#if UNITY_EDITOR
            //if (AssetBundleManager.SimulateAssetBundleInEditor)
            //    await CTask.WaitForNextFrame();
#endif

            T obj = request.GetAsset<T>();
            if (obj == null)
                CLog.Error($"加载资源失败:{assetBundleName}  AssName:{assetName}");
            return obj;
        }

        //private WaitForEndOfFrame waitFrame =  new WaitForEndOfFrame();
        /// <summary>
        /// 异步加载场景
        /// </summary>
        private async CTask _LoadScene(string sceneAssetBundle, string levelName, bool isAdditive, Action<float> cbProgress)
        {
            //float startTime = Time.realtimeSinceStartup;
            AssetBundleLoadOperation request = AssetBundleManager.LoadLevelAsync(sceneAssetBundle, levelName, isAdditive);
            if (request != null && cbProgress!=null) 
            {
                while (request.Progress() < 1f)
                {
                    //await waitFrame;
                    await CTask.WaitForNextFrame();
                    cbProgress(request.Progress());                    
                    if (request.IsDone())
                        break;
                }
                cbProgress(1f);
            }
            //float elapsedTime = Time.realtimeSinceStartup - startTime;
            Utils.ResetShader(null);
            //Debug.Log("Finished loading scene " + levelName + " in " + elapsedTime + " seconds");
        }
        #endregion
    }
}
