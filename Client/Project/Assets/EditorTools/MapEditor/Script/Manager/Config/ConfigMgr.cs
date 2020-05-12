/// <summary>
/// 工具生成，不要修改
/// </summary>
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MapEditor
{
    public partial class ConfigMgr
    {
        /// <summary> 副本章节</summary>
        public readonly Dictionary<object, FBChapterConfig> dicFBChapter = new Dictionary<object, FBChapterConfig>();
        /// <summary> 副本关卡配置</summary>
        public readonly Dictionary<object, FBLevelConfig> dicFBLevel = new Dictionary<object, FBLevelConfig>();
        /// <summary> 关卡阶段</summary>
        public readonly Dictionary<object, FBLevelStageConfig> dicFBLevelStage = new Dictionary<object, FBLevelStageConfig>();
        /// <summary> 怪物设置</summary>
        public readonly Dictionary<object, MonsterConfig> dicMonster = new Dictionary<object, MonsterConfig>();
        public readonly Dictionary<object, LanguageConfig> dicLanguage = new Dictionary<object, LanguageConfig>();

        /// <summary> 活动副本</summary>
        public readonly Dictionary<object, EventFBConfig> dicEventFB = new Dictionary<object, EventFBConfig>();
        /// <summary> 活动关卡</summary>
        public readonly Dictionary<object, EventFBLevelConfig> dicEventFBLevel = new Dictionary<object, EventFBLevelConfig>();
        /// <summary> 活动关卡战斗</summary>
        public readonly Dictionary<object, EventFBWarConfig> dicEventFBWar = new Dictionary<object, EventFBWarConfig>();

        /// <summary> 指引战斗关卡</summary>
        public readonly Dictionary<object, GuideLevelConfig> dicGuideLevel = new Dictionary<object, GuideLevelConfig>();

        /// <summary> 泰坦战斗关</summary>
        public readonly Dictionary<object, TitanLevelConfig> dicTitanLevel = new Dictionary<object, TitanLevelConfig>();

        /// <summary> 元素符号</summary>
        public readonly Dictionary<object, WarSymbolConfig> dicWarSymbol = new Dictionary<object, WarSymbolConfig>();

        /// <summary> 地图颜色</summary>
        public readonly Dictionary<object, MapColorConfig> dicMapColor = new Dictionary<object, MapColorConfig>();

        public void Initialize()
        {
            readConfig(dicMonster);
            readConfig(dicLanguage);


            readConfig(dicFBChapter);
            readConfig(dicFBLevel);
            readConfig(dicFBLevelStage);

            readConfig(dicEventFB);
            readConfig(dicEventFBLevel);
            readConfig(dicEventFBWar);
            readConfig(dicWarSymbol);

            readConfig(dicGuideLevel);

            readConfig(dicTitanLevel);
            readConfig(dicMapColor);



            ReadMapsConfig();

        }

        /// <summary>地图刷怪配置[类型,[id,配置文件]]</summary>
        public Dictionary<int, Dictionary<int, MapConfig>> dicMapsList = new Dictionary<int, Dictionary<int, MapConfig>>();

        public void ReadMapsConfig()
        {
            dicMapsList.Clear();
            List<MapConfig> maps = LoadMapConfig();            
            if (maps == null) return;
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
    }
}
