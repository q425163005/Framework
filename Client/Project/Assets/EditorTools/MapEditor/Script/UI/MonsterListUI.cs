using System.Collections;
using System.Collections.Generic;
using CSF;
using UnityEngine;
using UnityEngine.UI;
namespace MapEditor
{   
    public class MonsterListUI : MonoBehaviour
    {

        public MonsterListItem MonsterListItemPrefab;

        private List<MonsterListItem> itemList = new List<MonsterListItem>();

        public GameObject SizeGroup;

        protected Toggle[] sizeGroups;


        public InputField inputMosId;

        public Button btnAdd;

        void Awake()
        {
            //默认的一个
            itemList.Add(MonsterListItemPrefab);
            MonsterListItemPrefab.gameObject.SetActive(false);
            //sliderGridSize.onValueChanged.AddListener(sliderGridSize_Change);
            sizeGroups = SizeGroup.GetComponentsInChildren<Toggle>();
            for (int i = 0; i < sizeGroups.Length; i++)
                sizeGroups[i].AddChange(gridSize_Change,i);

            btnAdd.AddClick(btnAdd_Click);
        }
      
        public void Refresh()
        {
            //跟据地图类型，地图Id,找到地图对应的怪
            int mapId = MapEditor.I.MapId;

            if (mapId == 0)
                HideItems();
            else
            {
                List<MonsterConfig> list = null;               
                switch (MapEditor.I.MapType)
                {
                    case EMapType.Guide:
                        list = getMonsterList(0);
                        break;
                    case EMapType.Titan:
                        list = new List<MonsterConfig>();
                        if (MapEditor.I.Config.dicMonster.TryGetValue(mapId, out var config))
                            list.Add(config);
                        break;
                    default:
                        list = getMonsterList(mapId);
                        break;
                }

                
                HideItems(list.Count);
                for(int i=0;i< list.Count;i++)
                    CreateItem(i).SetData(list[i]);
            }
        }
        void gridSize_Change(Toggle toggle,int index)
        {
            if(toggle.isOn)
                MapEditor.I.MonsterGridSize = index+1;
            //txtGridSize.text = (int)val + "格";
        }

        void btnAdd_Click()
        {
            int id = 0;
            int.TryParse(inputMosId.text,out id);

            if (inputMosId.text == string.Empty)
            {
                TipsUI.ShowError("请输入怪物Id");
                return;
            }

            if (!MapEditor.I.Config.dicMonster.ContainsKey(id))
            {
                TipsUI.ShowError("怪物Id不存在:"+id);
                return;
            }
            UIRoot.I.MonsterGrid.AddMonster(id);
        }

        //获取副本地图怪
        private List<MonsterConfig> getMonsterList(int mapId)
        {
            List<MonsterConfig> list = new List<MonsterConfig>();
            MonsterConfig config;
            for (int i = 1; i < 100; i++)
            {
                int monsId = mapId * 100 + i;
                if (MapEditor.I.Config.dicMonster.TryGetValue(monsId, out config))
                    list.Add(config);
            }
            return list;
        }


        private void HideItems(int showNum = 0)
        {
            //隐藏超出的项
            for (int i = showNum; i < itemList.Count; i++)
            {
                itemList[i].gameObject.SetActive(false);
            }
        }

        private MonsterListItem CreateItem(int index)
        {
            MonsterListItem item;
            if (index == itemList.Count) //不够需要加
            {
                item = GameObject.Instantiate<MonsterListItem>(MonsterListItemPrefab);
                item.gameObject.transform.SetParent(MonsterListItemPrefab.transform.parent, false);
                item.gameObject.SetActive(true);
                itemList.Add(item);
            }
            else
            {
                item = itemList[index];
                item.gameObject.SetActive(true);
            }
            return item;
        }
    }
}
