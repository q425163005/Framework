using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TreeMenuSpace
{
    public class TreeMenu : MonoBehaviour
    {
        public NodeUI NodeUIItem;
        NodeData rootNode;
        public float NodeWidth;
        int levelY=0;
        public Transform TreeMenuParent;
        public List<NodeUI> nodeUIs = new List<NodeUI>();

        private NodeUI currNodeUI;
        public System.Action<int> SelectNode;
        void Start()
        {
           
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init(NodeData RootNode,int currNodeId)
        {
            DestroyItems();
            rootNode = RootNode;
            CreateTree(rootNode);
            RefreshPos();
            if (currNodeId > 0)
            {
                foreach (var item in nodeUIs)
                {
                    if (item.Data.id == currNodeId)
                        item.OnClickSelf();
                }
            }
        }
        void CreateTree(NodeData data)
        {
            GameObject obj = Instantiate(NodeUIItem.gameObject, TreeMenuParent, false);
            obj.SetActive(true);
            NodeUI UI = obj.GetComponent<NodeUI>();
            UI.SetData(data);
            UI.BtnOnClick = ClickTreeItem;
            UI.ClickSelf = OnClickNode;
            nodeUIs.Add(UI);
            foreach (var item in data.NodeDatas)
            {
                CreateTree(item);
            }
        }

        void ClickTreeItem(NodeUI nodeUI)
        {
            nodeUI.Data.NodeIsOpen = !nodeUI.Data.NodeIsOpen;
            RefreshPos();
        }
        /// <summary>
        /// 刷新节点位置
        /// </summary>
         void RefreshPos() {
            levelY = 0;
            InitNodeY(rootNode);
            SetNodeY(rootNode);
            foreach (var item in nodeUIs)
            {
                item.SetPos();
            }
        }
        void DestroyItems()
        {
            foreach (var item in nodeUIs)
            {
                item.DestroyItem();
            }
            nodeUIs.Clear();
            currNodeUI = null;
        }
        /// <summary>
        /// 还原Y轴索引
        /// </summary>
        /// <param name="data"></param>
        void InitNodeY(NodeData data)
        {
            data.LevelY = -100;
            foreach (var item in data.NodeDatas)
            {
                InitNodeY(item);
            }
        }

        void SetNodeY(NodeData data)
        {
            levelY += 1;
            data.LevelY = levelY;
            if (data.NodeIsOpen)
            {
                foreach (var item in data.NodeDatas)
                {
                    SetNodeY(item);
                }
            }
        }


        void OnClickNode(NodeUI nodeUI)
        {
            if (currNodeUI == nodeUI)
                return;
            currNodeUI?.Select(false);
            currNodeUI = nodeUI;
            currNodeUI.Select(true);
            //加载地图文件
            SelectNode?.Invoke(nodeUI.Data.id);
        }
    }
}
   
