using System.Collections;
using System.Collections.Generic;
using CSF;
using UnityEngine;
using UnityEngine.UI;

namespace MapEditor
{
    public class MapSelectItem : MonoBehaviour
    {
        // Start is called before the first frame update
        public Toggle toggSelect;
        public Text txtName;
        public Text txtState;

        private int Id;
        private string MapScene;
        private MapConfig mapConfig;
        private IWarSceneConf sceneConf;
        void Awake()
        {
            toggSelect.AddChange(toggSelect_Change);
        }
        //选择地图进行编辑
        void toggSelect_Change(bool isOn)
        {
            if(isOn)
                MapEditor.I.SetMapConfig(Id,mapConfig, sceneConf);
        }
        public void SetFBData(FBLevelStageConfig config)
        {
            txtName.text = config.name.Value;
            Id = config.id;
            MapScene = config.mapScene;
            sceneConf = config;
            setState();
        }

        public void SetEventFBData(string acName,EventFBLevelConfig config)
        {
            string[] complexity = new string[] {"VeryEasy" ,"Easy", "Normal", "Challenging","Hard", "VeryHard" };
            string comName = MapEditor.I.Lang.Get("EDifficulty." + complexity[config.complexity ]);
            txtName.text = acName+"("+ comName + ")";
            Id = config.id;
            if (MapEditor.I.Config.dicEventFBWar.TryGetValue(Id, out var cof))
            {
                sceneConf = cof;
            }
            else
            {
                CLog.Error("未找到EventFBWar配置 Id:"+Id);
            }
            setState();
        }

        public void SetWarData(string levelName, IWarSceneConf conf)
        {
            txtName.text = levelName;
            Id = conf.id;
            MapScene = conf.mapScene;
            sceneConf = conf;
            setState();
        }

        

        public void Refresh()
        {
            setState();
        }

        void setState()
        {
            int type = (int)MapEditor.I.MapType;
            mapConfig = null;
            if (MapEditor.I.Config.dicMapsList.TryGetValue(type, out var dic))
            {
                dic.TryGetValue(Id, out mapConfig);
            }
            if (mapConfig != null)
            {
                txtState.text = mapConfig.monster.Count + "波怪";
                txtState.color = Color.blue;
            }
            else
            {
                txtState.text = "未配置";
                txtState.color = Color.red;
            }            
        }

        public void SetSelect(bool isOn)
        {
            if (toggSelect.isOn == isOn)
                toggSelect_Change(isOn);
            else
                toggSelect.isOn = isOn;
           
        }

        public void Dispose()
        {
            mapConfig = null;
        }
    }
}
