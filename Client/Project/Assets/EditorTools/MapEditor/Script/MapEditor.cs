using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSF;
using CSF.Tasks;
using Spine.Unity;
using UnityEngine;

namespace MapEditor
{
    public class MapEditor : MonoBehaviour
    {
        // Start is called before the first frame update
        public static MapEditor I;
        public ConfigMgr Config;
        public LangMgr Lang;


        public EMapType MapType = EMapType.FB;
        public int MapId = 0;  //正在编辑的地图Id
        public IWarSceneConf SceneConfig;
        //当前正在编辑的配置
        public MapConfig EditMapConfig;
        // 正在编辑的波数索引
        public int EditWaveIndex = 0;

        //怪物占格数
        public int MonsterGridSize = 2;

        public bool IsEditMonster = true;

        //public string GetSceneZone()
        //{
        //    int type = 0;
        //    if (SceneConfig != null)
        //        type = SceneConfig.sceneType;
        //    return "SceneZone/Zone" + type;
        //}
        public GameObject uiRoot;
        private void Awake()
        {
            I = this;
            Mgr.Initialize();
            //uiRoot.SetActive(false);
           
            
            StartTask().Run();
        }
        async CTask StartTask()
        {
            await Mgr.Assetbundle.Initialize();
            Lang = new LangMgr();
            Config = new ConfigMgr();
            Config.Initialize();
            //CLog.Error(AssetBundles.AssetBundleManager.AssetBundleManifestObject);
            uiRoot.SetActive(true);
        }

        /// <summary>
        /// 设置正在编辑的地图配置
        /// </summary>
        /// <param name="config"></param>
        public void SetMapConfig(int id,MapConfig config, IWarSceneConf sceneConf=null)
        {
            EditMapConfig = config;
            MapId = id;
            if(sceneConf!=null)
                SceneConfig = sceneConf;

            SetMapEditModel(IsEditMonster);
        }
        //设置地图编辑模式
        //isEditMonster 是否为刷怪， fasle 刷初始格子
        public void SetMapEditModel(bool isEditMonster)
        {
            IsEditMonster = isEditMonster;
            if (IsEditMonster)
            {
                UIRoot.I.WaveEdit.Refresh();
                UIRoot.I.MonsterList.Refresh();
                UIRoot.I.Map.Refresh().Run();
            }
            else
            {
                UIRoot.I.MapInitSetting.Refresh();
            }
        }

        /// <summary>
        /// 当前正在编辑的波数
        /// </summary>
        /// <param name="monster"></param>
        public void SetWaveMonster(int index)
        {
            EditWaveIndex = index;
            //Debug.LogError(index);  
            if (EditMapConfig == null)
                UIRoot.I.MonsterGrid.SetData(null);
            else
                UIRoot.I.MonsterGrid.SetData(EditMapConfig.monster[index]);    
        }
                

        //删除一波怪
        public void RemoveWave()
        {
            EditMapConfig.monster.RemoveAt(EditMapConfig.monster.Count - 1);
            UIRoot.I.MapSelect.Refresh();
        }

        /// <summary>
        /// 增加一波怪
        /// </summary>
        public void AddWave()
        {
            //没有地图
            if (EditMapConfig == null)
            {
                MapConfig config;
                config = new MapConfig();
                config.id = MapId;
                config.type = (int)MapType;
                config.monster = new List<List<MapMonster>>();
                config.monster.Add(new List<MapMonster>()); //加一波新怪

                Dictionary<int, MapConfig> list;
                if (!Config.dicMapsList.TryGetValue(config.type, out list))
                {
                    list = new Dictionary<int, MapConfig>();
                    Config.dicMapsList.Add(config.type, list);
                }
                list.Add(config.id, config);
                EditMapConfig = config;
            }
            else
                EditMapConfig.monster.Add(new List<MapMonster>());

            UIRoot.I.MapSelect.Refresh();
            UIRoot.I.WaveEdit.SelectLast();
        }



        public async CTask<SkeletonDataAsset> LoadMonsterModel(string assetBundleName)
        {
            string assetName = assetBundleName.Substring(assetBundleName.LastIndexOf("/") + 1) + AppSetting.SkeletonDataName;
            assetBundleName = AppSetting.SkeletonDataAssetDir + assetBundleName + AppSetting.SkeletonDataName + AppSetting.SkeletonDataExtName;
            return await Mgr.Assetbundle.LoadAsset<SkeletonDataAsset>(assetBundleName, assetName);
        }
    }
}
