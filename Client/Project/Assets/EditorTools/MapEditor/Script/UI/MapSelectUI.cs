using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Dropdown;

namespace MapEditor
{    
    public class MapSelectUI : MonoBehaviour
    {
        public Dropdown ddlMapType;
        public Text txtChapterTitle;
        public Dropdown ddlChapter;

        public Text txtLevelTitle;
        public Dropdown ddlLevel;


        public MapSelectItem MapSelectItemPrefab;

        private List<MapSelectItem> mapItemList = new List<MapSelectItem>();

        // Start is called before the first frame update
        void Start()
        {
            ddlMapType.options.Clear();
            ddlMapType.options.Add(new OptionData("副本地图" + ID_SPLIT + (int)EMapType.FB));
            ddlMapType.options.Add(new OptionData("活动地图" + ID_SPLIT + (int)EMapType.EventFB));
            ddlMapType.options.Add(new OptionData("泰坦地图" + ID_SPLIT + (int)EMapType.Titan));
            ddlMapType.options.Add(new OptionData("指引地图" + ID_SPLIT + (int)EMapType.Guide));
            

            mapItemList.Add(MapSelectItemPrefab);
            ddlMapType.AddChange(ddlMapType_Change);
            ddlChapter.AddChange(ddlChapter_Change);
            ddlLevel.AddChange(ddlLevel_Change);
            SetDropDownDefaultValue(ddlMapType);
        }

        public void Refresh()
        {
            foreach (var item in mapItemList)
            {
                if (item.gameObject.activeSelf)
                    item.Refresh();
            }
        }

        /// <summary>副本类型选择</summary>
        void ddlMapType_Change(int index)
        {
            int type = 0;
            int.TryParse(ddlMapType.options[index].text.Split(ID_SPLIT)[1], out type);
            MapEditor.I.MapType = (EMapType)type;
            switch (MapEditor.I.MapType)
            {
                case EMapType.FB:
                    txtChapterTitle.gameObject.SetActive(true);
                    txtLevelTitle.gameObject.SetActive(true);
                    txtChapterTitle.text = "副本章节:";
                    txtLevelTitle.text = "副本关卡:";
                    SetFBChapter();
                    break;
                case EMapType.EventFB:
                    txtChapterTitle.gameObject.SetActive(false);
                    txtLevelTitle.gameObject.SetActive(true);
                    txtLevelTitle.text = "活动关卡:";
                    SetEventFB();
                    break;
                case EMapType.Guide:
                    txtChapterTitle.gameObject.SetActive(false);
                    txtLevelTitle.gameObject.SetActive(false);
                    SetGuideWar();
                    break;
                case EMapType.Titan:
                    txtChapterTitle.gameObject.SetActive(false);
                    txtLevelTitle.gameObject.SetActive(false);
                    SetTitanWar();
                    break;
                default:
                    txtChapterTitle.gameObject.SetActive(false);
                    txtLevelTitle.gameObject.SetActive(false);
                    HideMapSelectItem(0);
                    break;
            }
        }

        char ID_SPLIT = '_';
        /// <summary>章节选择</summary>
        void ddlChapter_Change(int index)
        {
            int id = 0;
            int.TryParse(ddlChapter.options[index].text.Split(ID_SPLIT)[1], out id);
            switch (MapEditor.I.MapType)
            {
                case EMapType.FB:
                    SetFBLevel(id);
                    break;
            }
        }
        /// <summary>关卡选择</summary>
        void ddlLevel_Change(int index)
        {            
            int id = 0;
            int.TryParse(ddlLevel.options[index].text.Split(ID_SPLIT)[1], out id);
            switch (MapEditor.I.MapType)
            {
                case EMapType.FB:
                    SetLevelStage(id);
                    break;
                case EMapType.EventFB:
                    SetEventFBLevel(id);
                    break;
            }            
        }

        #region 副本地图
        /// <summary>设置副本章节</summary>
        private void SetFBChapter()
        {
            ddlChapter.ClearOptions();
            foreach (var item in MapEditor.I.Config.dicFBChapter.Values)
            {
                ddlChapter.options.Add(new OptionData(item.name.Value+ ID_SPLIT + item.id));
            }
            SetDropDownDefaultValue(ddlChapter);
        }
        /// <summary>
        /// 设置副本关卡
        /// </summary>
        private void SetFBLevel(int chapterId)
        {
            ddlLevel.ClearOptions();
            foreach (var item in MapEditor.I.Config.dicFBLevel.Values)
            {
                if(item.chId == chapterId)
                    ddlLevel.options.Add(new OptionData(item.name.Value + ID_SPLIT + item.id));
            }
            SetDropDownDefaultValue(ddlLevel);
        }
        /// <summary>
        /// 设置关卡阶段
        /// </summary>
        private void SetLevelStage(int levelId)
        {
            List<FBLevelStageConfig> stageList = new List<FBLevelStageConfig>();
            FBLevelStageConfig config;
            for (int i = 1; i < 100; i++)
            {
                int stageId = levelId * 100 + i;
                if (MapEditor.I.Config.dicFBLevelStage.TryGetValue(stageId, out config))
                    stageList.Add(config);
            }
            HideMapSelectItem(stageList.Count);
            for (int i = 0; i < stageList.Count; i++)
                CreateMapSelectItem(i).SetFBData(stageList[i]);
            mapItemList[0].SetSelect(true);
        }
        #endregion

        #region 活动副本
        /// <summary>
        /// 设置活动副本关卡
        /// </summary>
        private void SetEventFB()
        {
            ddlLevel.ClearOptions();
            foreach (var item in MapEditor.I.Config.dicEventFB.Values)
            {
                ddlLevel.options.Add(new OptionData(item.name.Value + ID_SPLIT + item.id));
            }
            SetDropDownDefaultValue(ddlLevel);
        }
        /// <summary>
        /// 设置活动副本关卡
        /// </summary>
        private void SetEventFBLevel(int fbId)
        {
            List<EventFBLevelConfig> stageList = new List<EventFBLevelConfig>();
            EventFBLevelConfig config;
            for (int i = 1; i < 1000; i++)
            {
                int levelId = fbId * 1000 + i;
                if (MapEditor.I.Config.dicEventFBLevel.TryGetValue(levelId, out config))
                    stageList.Add(config);
            }            
            string acNeme = MapEditor.I.Config.dicEventFB[fbId].name.Value;
            HideMapSelectItem(stageList.Count);
            for (int i = 0; i < stageList.Count; i++)
                CreateMapSelectItem(i).SetEventFBData(acNeme, stageList[i]);
            mapItemList[0].SetSelect(true);
        }
        #endregion

        #region 指引战斗
        /// <summary>
        /// 设置指引战斗关卡
        /// </summary>
        private void SetGuideWar()
        {
            
            HideMapSelectItem(MapEditor.I.Config.dicGuideLevel.Count);
            int i =0;
            foreach (var config in MapEditor.I.Config.dicGuideLevel.Values)
            {
                CreateMapSelectItem(i++).SetWarData("指引战斗"+ ID_SPLIT+config.id, config);
            }
            mapItemList[0].SetSelect(true);
        }
        #endregion

        #region 泰坦战斗
        private void SetTitanWar()
        {

            HideMapSelectItem(MapEditor.I.Config.dicTitanLevel.Count);
            int i = 0;
            foreach (var config in MapEditor.I.Config.dicTitanLevel.Values)
            {
                CreateMapSelectItem(i++).SetWarData("泰坦战斗" + ID_SPLIT + config.id, config);
            }
            mapItemList[0].SetSelect(true);
        }
        #endregion


        private void HideMapSelectItem(int showNum)
        {
            //隐藏超出的项
            for (int i = showNum; i < mapItemList.Count; i++)
                mapItemList[i].gameObject.SetActive(false);
        }
        private MapSelectItem CreateMapSelectItem(int index)
        {
            MapSelectItem item;
            if (index == mapItemList.Count) //不够需要加
            {
                item = GameObject.Instantiate<MapSelectItem>(MapSelectItemPrefab);
                item.gameObject.transform.SetParent(MapSelectItemPrefab.transform.parent, false);
                item.gameObject.SetActive(true);
                item.SetSelect(false);
                mapItemList.Add(item);
            }
            else
            {
                item = mapItemList[index];
                item.gameObject.SetActive(true);
            }
            return item;
        }

        private void SetDropDownDefaultValue(Dropdown ddl)
        {
            if (ddl.value == 0) //不会触发事件，强制触发一下
            {
                ddl.captionText.text = ddl.options[0].text;
                ddl.onValueChanged.Invoke(0);
            }
            else
                ddl.value = 0;
        }
    }
}
