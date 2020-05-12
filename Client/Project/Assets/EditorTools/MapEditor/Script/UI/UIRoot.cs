using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MapEditor
{
    public class UIRoot : MonoBehaviour
    {
        public GameObject WarPanel;
        public GameObject EditTypeTab;

        public MapUI Map;
        /// <summary>怪物格子UI</summary>
        public MonsterGridUI MonsterGrid;
        /// <summary>波数编辑UI</summary>
        public WaveEditUI WaveEdit;
        /// <summary>怪物列表UI</summary>
        public MonsterListUI MonsterList;
        

        /// <summary>副本选择UI</summary>
        public MapSelectUI MapSelect;

        /// <summary>副本选择UI</summary>
        public MonsterTipsUI MonsterTips;
        /// <summary>TipsUI</summary>
        public TipsUI Tips;

        public MapInitSettingUI MapInitSetting;

      
        public static UIRoot I;
        

        private void Awake()
        {
            I = this;
            var editTypeTabs = EditTypeTab.GetComponentsInChildren<Toggle>();
            for (int i = 0; i < editTypeTabs.Length; i++)
                editTypeTabs[i].AddChange(editTypeTab_Change,i);

            WarPanel.SetActive(MapEditor.I.IsEditMonster);
            MapInitSetting.gameObject.SetActive(!MapEditor.I.IsEditMonster);
        }
        private void editTypeTab_Change(Toggle toogle,int index)
        {
            MapEditor.I.SetMapEditModel(index == 0);
            WarPanel.SetActive(MapEditor.I.IsEditMonster);
            MapInitSetting.gameObject.SetActive(!MapEditor.I.IsEditMonster);
        }
    }
}

