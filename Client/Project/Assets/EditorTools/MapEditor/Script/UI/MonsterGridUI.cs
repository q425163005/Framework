using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MapEditor
{
    public class MonsterGridUI : MonoBehaviour
    {
        public Image[] GridList;       
        public MonsterItem MonsterItemPrefab;

        //0正常 1有怪 2经过 3存在
        public Color[] GridColor;

        //怪物可拖动区域
        public RectTransform DragArea;

        public bool IsGridAlign = true; //怪物移动是否按格子对齐
        public Text txtGridAlign;

        List<MapMonster> Data;

        //格子显示层级排序,越靠前越显示在后面
        protected int[] GridSort = new int[21] { 20, 14, 19, 15, 18, 16, 17, 13, 7, 12, 8, 11, 9, 10, 6,0,5,1,4,2,3 };

        protected int[] AddMonsterSequence = new int[6] {2,7,12,15,0,5 };


        private MonsterItem[] MonsterItemList = new MonsterItem[21];
        void Awake()
        {
            MonsterItemList[0] = MonsterItemPrefab;
            MonsterItemList[0].gameObject.SetActive(false);
        }

        public void SetData(List<MapMonster> list)
        {
            Data = list;
            Refresh();
        }

        public void Refresh()
        {
            foreach (var item in MonsterItemList)
                item?.gameObject.SetActive(false);

            foreach (var grid in GridList)
                grid.color = GridColor[(int)EMapGridState.None];

            if (Data == null) return;           
            foreach (var mons in Data)
                CreateItem(mons);
            monsterSort();
        }

        private MonsterItem CreateItem(MapMonster mons)
        {
            MonsterItem mItem = MonsterItemList[mons.place];
            if (mItem == null)
            {
                mItem = GameObject.Instantiate<MonsterItem>(MonsterItemPrefab);
                mItem.gameObject.transform.SetParent(DragArea, false);
                MonsterItemList[mons.place] = mItem;
            }
            mItem.gameObject.SetActive(true);
            mItem.SetData(mons);
            //GridList[mons.place].color = GridColor[(int)EMapGridState.Have];
            for(int i=0;i<mons.size;i++)
                GridList[mons.place+i].color = GridColor[(int)EMapGridState.Have];
            return mItem;
        }
        private void monsterSort()
        {
            //跟据格子重新设置加入顺序
            MonsterItem mItem;
            foreach (var index in GridSort)
            {
                mItem = MonsterItemList[index];
                if (mItem != null && mItem.IsHave)
                    mItem.transform.SetAsLastSibling();
            }
        }


        public void SetGridHighlight(int index, int itemIndex,int size)
        {
            //0没怪 1有怪 2经过 3存在
            bool have = false;
            EMapGridState state = EMapGridState.None;
            int haveCount = 0;
            for (int i = 0; i < GridList.Length; i++)
            {
                have = MonsterItemList[i] != null && MonsterItemList[i].gameObject.activeSelf;
                if (have)
                    haveCount = size;
                if (i >= index &&i< index+size)
                {
                    //此格子上已有怪，格子变红
                    if (have && i != itemIndex)
                        state = EMapGridState.DragHave;
                    else
                        state = EMapGridState.Drag;
                }
                else
                {                    
                    if (have || haveCount>0)
                        state = EMapGridState.Have;
                    else
                        state = EMapGridState.None;                 
                }
                haveCount--;
                GridList[i].color = GridColor[(int)state];
            }
        }

        /// <summary>
        /// 判断是否可移动到指定格子
        /// </summary>
        /// <param name="index"></param>
        /// <param name="itemIndex"></param>
        public bool CanMoveGird(int index, int itemIndex)
        {
            bool have = MonsterItemList[index] != null && MonsterItemList[index].gameObject.activeSelf && index!=itemIndex;
            if (have) //存在，还原位置
                return false;
            else //不存在，改变位置
                return true;
        }

        void Update()
        {
            IsGridAlign = !Input.GetKey(KeyCode.LeftControl);
            if (IsGridAlign)
                txtGridAlign.text = "以格子为中心点对齐(Ctrl微调)";
            else
                txtGridAlign.text = "拖放位置";
        }

        //移除怪
        public void RemoveMonster(int index)
        {
            if (MonsterItemList[index] != null)
            {
                Data.Remove(MonsterItemList[index].Data);
                Refresh();
                UIRoot.I.WaveEdit.RefreshMonsterNum();
            }
        }

        public void AddMonster(int templId)
        {

            int place = -1;
            MonsterItem mItem;
            //加入顺序
            foreach (var index in AddMonsterSequence)
            {
                mItem = MonsterItemList[index];
                if (mItem == null || !mItem.IsHave)
                {
                    place = index;
                    break;
                }
            }
            if (place == -1)
            {
                TipsUI.ShowError("怪物站位点已满，不能再增加");
                return;
            }
            if (Data == null)
            {
                TipsUI.ShowError("请先增加一波怪");
                return;
            }

            MapMonster mon = new MapMonster();
            mon.mId = templId;
            mon.place = place;
            mon.size = MapEditor.I.MonsterGridSize;

            //if ((mon.place % 7 + mon.size > 7))
            //    mon.place -= (mon.place % 7 + mon.size - 7);

            Data.Add(mon);

            var item = CreateItem(mon);
            item.ShowNewEffect();
            monsterSort();
            UIRoot.I.WaveEdit.RefreshMonsterNum();
        }
    }
}
