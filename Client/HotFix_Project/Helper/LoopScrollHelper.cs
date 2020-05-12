using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace HotFix_Project.Helper
{
    public class LoopScrollHelper
    {
        private List<GameObject>            goList;      //当前显示的go列表
        private Queue<GameObject>           freeGoQueue; //空闲的go队列，存放未显示的go
        private Dictionary<GameObject, int> goIndexDic;  //key:所有的go value:真实索引
        private ScrollRect                  scrollRect;
        private RectTransform               contentRectTra;
        private Vector2                     scrollRectSize;
        private Vector2                     cellSize;
        private int                         startIndex;  //起始索引
        private int                         maxCount;    //创建的最大数量
        private int                         createCount; //当前显示的数量

        private       int cacheCount        = 3;  //缓存数目
        private const int invalidStartIndex = -1; //非法的起始索引

        private int                     dataCount;
        private GameObject              prefabGo;
        private Action<GameObject, int> updateCellCB;
        private float                   cellPadding;

        public LoopScrollHelper(ScrollRect scroll, GameObject prefabGo,
            Action<GameObject, int>        updateCellCB, int     cacheCount = 3)
        {
            //数据和组件初始化
            scrollRect          = scroll;
            this.prefabGo       = prefabGo;
            this.updateCellCB   = updateCellCB;
            this.scrollRectSize = scroll.viewport.rect.size;
            this.cacheCount     = cacheCount;
            goList              = new List<GameObject>();
            freeGoQueue         = new Queue<GameObject>();
            goIndexDic          = new Dictionary<GameObject, int>();
            contentRectTra      = scrollRect.content;
            if (scrollRect.horizontal)
            {
                contentRectTra.anchorMin = new Vector2(0, 0);
                contentRectTra.anchorMax = new Vector2(0, 1);
                cellPadding = scrollRect.content.GetComponent<HorizontalLayoutGroup>().spacing;
            }
            else
            {
                contentRectTra.anchorMin = new Vector2(0, 1);
                contentRectTra.anchorMax = new Vector2(1, 1);
                cellPadding = scrollRect.content.GetComponent<VerticalLayoutGroup>().spacing;
            }

            cellSize    = prefabGo.GetComponent<RectTransform>().sizeDelta;
            startIndex  = 0;
            maxCount    = GetMaxCount();
            createCount = 0;
           
        }

        //初始化SV并刷新
        public void Show(int dataCount)
        {
            this.dataCount = dataCount;
            scrollRect.onValueChanged.RemoveAllListeners();
            scrollRect.onValueChanged.AddListener(OnValueChanged);
            ResetSize(dataCount);
        }

        //重置数量
        public void ResetSize(int dataCount)
        {
            this.dataCount           = dataCount;
            contentRectTra.sizeDelta = GetContentSize();
            //回收显示的go
            for (int i = goList.Count - 1; i >= 0; i--)
            {
                GameObject go = goList[i];
                RecoverItem(go);
            }

            //创建或显示需要的go
            createCount = Mathf.Min(dataCount, maxCount);
            for (int i = 0; i < createCount; i++)
            {
                CreateItem(i);
            }

            //刷新数据
            startIndex                      = -1;
            contentRectTra.anchoredPosition = Vector3.zero;
            OnValueChanged(Vector2.zero);
        }

        //更新当前显示的列表
        public void UpdateList()
        {
            for (int i = 0; i < goList.Count; i++)
            {
                GameObject go    = goList[i];
                int        index = goIndexDic[go];
                updateCellCB(go, index);
            }
        }

        //创建或显示一个item
        private void CreateItem(int index)
        {
            if (index < 0)
            {
                return;
            }

            GameObject go;
            if (freeGoQueue.Count > 0) //使用原来的
            {
                go             = freeGoQueue.Dequeue();
                goIndexDic[go] = index;
                go.SetActive(true);
            }
            else //创建新的
            {
                go = Object.Instantiate(prefabGo, contentRectTra);
                goIndexDic.Add(go, index);
                //go.transform.SetParent(contentRectTra.transform);
                go.SetActive(true);
                RectTransform rect = go.GetComponent<RectTransform>();
                rect.pivot     = new Vector2(0, 1);
                rect.anchorMin = new Vector2(0, 1);
                rect.anchorMax = new Vector2(0, 1);
            }

            goList.Add(go);
            go.transform.localPosition = GetPosition(index);
            updateCellCB(go, index);
        }

        //回收一个item
        private void RecoverItem(GameObject go)
        {
            go.SetActive(false);
            goList.Remove(go);
            freeGoQueue.Enqueue(go);
            goIndexDic[go] = invalidStartIndex;
        }

        //滑动回调
        private void OnValueChanged(Vector2 vec)
        {
            int curStartIndex = GetStartIndex();
            //CLog.Log($"{curStartIndex}~~~~{startIndex}~~~~{goList.Count}~~~~{createCount}");
            if (curStartIndex < 0)
            {
                startIndex = 0;
            }

            if ((startIndex != curStartIndex))
            {
                startIndex = curStartIndex;
                if (curStartIndex < 0)
                {
                    startIndex = 0;
                }

                //收集被移出去的go
                //索引的范围:[startIndex, startIndex + createCount - 1]
                for (int i = goList.Count - 1; i >= 0; i--)
                {
                    GameObject go    = goList[i];
                    int        index = goIndexDic[go];
                    if (index < startIndex || index > (startIndex + createCount - 1))
                    {
                        //CLog.Log($"--------||{index}~~~~{startIndex}");
                        RecoverItem(go);
                    }
                }

                //对移除出的go进行重新排列
                for (int i = startIndex; i < startIndex + createCount; i++)
                {
                    if (i >= dataCount)
                    {
                        break;
                    }

                    bool isExist = false;
                    for (int j = 0; j < goList.Count; j++)
                    {
                        GameObject go    = goList[j];
                        int        index = goIndexDic[go];
                        if (index == i)
                        {
                            isExist = true;
                            break;
                        }
                    }

                    if (isExist)
                    {
                        continue;
                    }

                    CreateItem(i);
                }
            }
        }

        //获取需要创建的最大prefab数目
        private int GetMaxCount()
        {
            if (scrollRect.horizontal)
            {
                return Mathf.CeilToInt(scrollRectSize.x / (cellSize.x + cellPadding)) + cacheCount;
            }
            else
            {
                return Mathf.CeilToInt(scrollRectSize.y / (cellSize.y + cellPadding)) + cacheCount;
            }
        }

        //获取起始索引
        private int GetStartIndex()
        {
            if (scrollRect.horizontal)
            {
                return Mathf.FloorToInt(-contentRectTra.anchoredPosition.x / (cellSize.x + cellPadding));
            }
            else
            {
                return Mathf.FloorToInt(contentRectTra.anchoredPosition.y / (cellSize.y + cellPadding));
            }
        }

        //获取索引所在位置
        private Vector3 GetPosition(int index)
        {
            if (scrollRect.horizontal)
            {
                return new Vector3(index * (cellSize.x + cellPadding), 0, 0);
            }
            else
            {
                return new Vector3(scrollRect.content.GetComponent<VerticalLayoutGroup>().padding.left, 
                    index * -(cellSize.y + cellPadding)- scrollRect.content.GetComponent<VerticalLayoutGroup>().padding.top, 0);
            }
        }

        //获取内容长宽
        private Vector2 GetContentSize()
        {
            if (scrollRect.horizontal)
            {
                return new Vector2(cellSize.x * dataCount + cellPadding * (dataCount - 1), contentRectTra.sizeDelta.y);
            }
            else
            {
                return new Vector2(contentRectTra.sizeDelta.x, cellSize.y * dataCount + cellPadding * (dataCount - 1)+
                    scrollRect.content.GetComponent<VerticalLayoutGroup>().padding.bottom);
            }
        }
    }
}