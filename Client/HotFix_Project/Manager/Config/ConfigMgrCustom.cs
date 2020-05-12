using CSF.Tasks;
using LitJson;
using System;
using System.Collections.Generic;

namespace HotFix_Project
{
    /// <summary>需要自定义解析方法写在这里面</summary>
    public partial class ConfigMgr
    {
       
       
        public Dictionary<string, EffectConfig> dicEffectByName = new Dictionary<string, EffectConfig>();

      

        /// <summary>任务线[线，任务列表]</summary>
        public Dictionary<int, List<TaskConfig>> dicTaskLine = new Dictionary<int, List<TaskConfig>>();
        /// <summary>任务所在当前任务线的索引</summary>
        public Dictionary<int, int> dicTaskLineIndex = new Dictionary<int, int>();

        public Dictionary<string, HeroModelSetting> dicHeroModelSetting = new Dictionary<string, HeroModelSetting>();

        /// <summary>地图刷怪配置[类型,[id,配置文件]]</summary>
        public Dictionary<int, Dictionary<int, MapConfig>> dicMapsList = new Dictionary<int, Dictionary<int, MapConfig>>();

        /// <summary>建筑多边形点击响应区域数据</summary>
        public Dictionary<string, BuildPolygonSetting> dicBuildPolygonSetting = new Dictionary<string, BuildPolygonSetting>();

        /// <summary>副本位置数据</summary>
        public List<FBMaplPosSetting> dicFBMaplPosSetting = new List<FBMaplPosSetting>();

        private void customRead()
        {
            //readEffect();
            readHeroModelSetting().Run();
            readMapConfig().Run();
            readBuildPolygonSetting().Run();
            readFBMaplPosSetting().Run();
        }
        
        private async CTask readBuildPolygonSetting()
        {
            UnityEngine.Object configObj = await CSF.Mgr.Assetbundle.LoadAsset<UnityEngine.Object>("Data/BuildPolygonSetting.txt", "BuildPolygonSetting");
            if (configObj != null)
            {
                string                 strconfig = configObj.ToString();
                List<BuildPolygonSetting> list      = JsonMapper.ToObject<List<BuildPolygonSetting>>(strconfig);
                for (int i = 0; i < list.Count; i++)
                {
                    dicBuildPolygonSetting.Add(list[i].name, list[i]);
                }
            }
            else
                CLog.Error($"配置文件不存在BuildPolygonSetting");
        }

        private async CTask readHeroModelSetting()
        {
            UnityEngine.Object configObj = await CSF.Mgr.Assetbundle.LoadAsset<UnityEngine.Object>("Data/HeroModelSetting.txt", "HeroModelSetting");
            if (configObj != null)
            {
                string strconfig = configObj.ToString();
                List<HeroModelSetting> list = JsonMapper.ToObject<List<HeroModelSetting>>(strconfig);
                for (int i = 0; i < list.Count; i++)
                {
                    dicHeroModelSetting.Add(list[i].Model, list[i]);
                }
            }
            else
                CLog.Error($"配置文件不存在HeroModelSetting");
        }

        private async CTask readMapConfig()
        {
            UnityEngine.Object configObj = await CSF.Mgr.Assetbundle.LoadAsset<UnityEngine.Object>(configAssetbundle, "MapConfig");
            if (configObj != null)
            {
                string strconfig = configObj.ToString();
                List<MapConfig> maps = JsonMapper.ToObject<List<MapConfig>>(strconfig);

                Dictionary<int, MapConfig> list;

                foreach (var map in maps)
                {
                    if (!dicMapsList.TryGetValue(map.type, out list))
                    {
                        list = new Dictionary<int, MapConfig>();
                        dicMapsList.Add(map.type, list);
                    }
                    list.Add(map.id, map);
                }
            }
            CSF.Mgr.Assetbundle.UnloadAssetBundle(configAssetbundle);
        }

        private async CTask readFBMaplPosSetting()
        {
            UnityEngine.Object configObj = await CSF.Mgr.Assetbundle.LoadAsset<UnityEngine.Object>("Data/FBMaplPosSetting.txt", "FBMaplPosSetting");
            if (configObj != null)
            {
                string                    strconfig = configObj.ToString();
                dicFBMaplPosSetting = JsonMapper.ToObject<List<FBMaplPosSetting>>(strconfig);
            }
            else
                CLog.Error($"配置文件不存在FBMaplPosSetting");
        }
        //=====
    }
}
