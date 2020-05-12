using System.Collections;
using System.Collections.Generic;
using CSF;
using UnityEngine;
using UnityEngine.UI;
namespace MapEditor
{   
    public class WaveEditUI : MonoBehaviour
    {
        public Button btnAdd;
        public Button btnDelete;

        public WaveItem WaveItemPrefab;

        private List<WaveItem> warItemList = new List<WaveItem>();

        public int WaveCount = 0;
        void Awake()
        {
            btnAdd.AddClick(btnAdd_Click);
            btnDelete.AddClick(btnDelete_Click);
            //默认的一个
            warItemList.Add(WaveItemPrefab);            
            WaveItemPrefab.gameObject.SetActive(false);
            //WaveItemPrefab.SetData();
        }
        //增加一波怪
        void btnAdd_Click()
        {                      
            WaveItem item = CreateItem(WaveCount);
            item.SetData(WaveCount);
            WaveCount += 1;
            MapEditor.I.AddWave();
        }
        void btnDelete_Click()
        {
            if (WaveCount <= 1)
            {
                TipsUI.ShowError("至少保留一波怪!!!");
                return;
            }
            var lastItem = warItemList[WaveCount - 1];
            lastItem.gameObject.SetActive(false);
            if (lastItem.IsSelect)
            {
                warItemList[WaveCount - 2].SetSelect(true);
                lastItem.SetSelect(false);
            }

            WaveCount -= 1;
            //删除一波怪
            MapEditor.I.RemoveWave();
        }
        public void Refresh()
        {
            if (MapEditor.I.EditMapConfig == null)
                HideItems();
            else
            {
                var waves = MapEditor.I.EditMapConfig.monster;
                HideItems(waves.Count);

                for(int i=0;i< waves.Count;i++)
                    CreateItem(i).SetData(i,waves[i].Count);
            }
            warItemList[0].SetSelect(true);
        }

        public void SelectLast()
        {
            for (var i = warItemList.Count; --i >= 0;)
            {
                warItemList[i].SetSelect(WaveCount-1==i);
            }
        }

        //刷新怪数量显示
        public void RefreshMonsterNum()
        {
            var waves = MapEditor.I.EditMapConfig.monster;
            for (int i = 0; i < waves.Count; i++)
            {
                warItemList[i].SetNum(waves[i].Count);
            }
        }


        private void HideItems(int showNum = 0)
        {
            //隐藏超出的项
            for (int i = showNum; i < warItemList.Count; i++)
            {
                warItemList[i].gameObject.SetActive(false);
                warItemList[i].SetSelect(false);
            }
            WaveCount = showNum;           
        }

        private WaveItem CreateItem(int index)
        {
            WaveItem item;
            if (index == warItemList.Count) //不够需要加
            {
                item = GameObject.Instantiate<WaveItem>(WaveItemPrefab);
                item.gameObject.transform.SetParent(WaveItemPrefab.transform.parent, false);
                item.gameObject.SetActive(true);
                item.SetSelect(false);
                warItemList.Add(item);
            }
            else
            {
                item = warItemList[index];
                item.gameObject.SetActive(true);
            }
            return item;
        }
    }
}
