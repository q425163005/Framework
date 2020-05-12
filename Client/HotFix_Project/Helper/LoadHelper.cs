using CSF;
using CSF.Tasks;
using Spine.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotFix_Project
{
    public class LoadHelper
    {
        /// <summary>
        /// 加载Texture
        /// </summary>
        /// <param name="assetBundleName"></param>
        /// <returns></returns>
        public static async CTask<Texture> LoadTexture(string assetBundleName)
        {
            string assetName = assetBundleName.Substring(assetBundleName.LastIndexOf("/") + 1);
            assetBundleName = "Textures/" + assetBundleName + ".png";
            return await CSF.Mgr.Assetbundle.LoadAsset<Texture>(assetBundleName, assetName);
        }

        public static async CTask LoadScene(string sceneName, bool isAdditive = false, Action<float> cbProgress = null)
        {
            await CSF.Mgr.Assetbundle.LoadScene(sceneName, isAdditive, cbProgress);
            //GuideTrigger.LoadScene(sceneName);
        }

        /// <summary>
        /// 加载声音文件
        /// </summary>
        /// <param name="assetBundleName"></param>
        /// <returns></returns>
        public static async CTask<AudioClip> LoadSound(string assetBundleName)
        {
            string assetName = assetBundleName.Substring(assetBundleName.LastIndexOf("/") + 1);
            assetBundleName = "Sound/" + assetBundleName + ".mp3";
            return await CSF.Mgr.Assetbundle.LoadAsset<AudioClip>(assetBundleName, assetName);
        }
        /// <summary>
        /// 异步加载UI骨骼动画 实际需要传入/英雄敌人/动画名称
        /// </summary>
        /// <param name="assetBundleName">Enemy/silvia_1</param>
        public static async CTask<SkeletonDataAsset> LoadSkeletonData(string assetBundleName)
        {
            string assetName = assetBundleName.Substring(assetBundleName.LastIndexOf("/") + 1)+ AppSetting.SkeletonDataName;
            assetBundleName = AppSetting.SkeletonDataAssetDir + assetBundleName+ AppSetting.SkeletonDataName + AppSetting.SkeletonDataExtName;
            return await CSF.Mgr.Assetbundle.LoadAsset<SkeletonDataAsset>(assetBundleName, assetName);
        }


        //预加载的资源
        public static async CTask Initialize()
        {
            //await CSF.Mgr.Assetbundle.LoadSpriteAtlas(UIAtlas.PublicBKNew);
            //await CSF.Mgr.Assetbundle.LoadSpriteAtlas("EditTeamNew");
            //await CSF.Mgr.Assetbundle.LoadSpriteAtlas("CardInfo");
            
        }
    }
}
