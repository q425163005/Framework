using System.Collections;
using System.Collections.Generic;
using CSF;
using UnityEngine;
using UnityEngine.UI;

namespace MapEditor
{
    public class MapInitSettingUI : MonoBehaviour
    {
        public GameObject SymbolList;
        public Button btnCleanAll;
        public Text txtEditState;

        public ToggleGroup StateType;
        public ToggleGroup StackNum;

        public GameObject AllSymbolList;
        public GameObject goSelectSymbol;

        public SymbolItem SymbolItemPrefab;

        public RawImage GameZone;
        public RawImage GameZoneArrow;

        public Queue<SymbolItem> symbolItemCache = new Queue<SymbolItem>();
        public List<SymbolItem> mapSymbolItemList = new List<SymbolItem>();
        private Button[] SymbolGrid;
        List<MapSymbol> listConfig;

        public int SelectState = 0; //当前选中的状态
        public int SelectStack = 5; //当前选中的层数
        public int SelectSymbolType = 0;
        private bool IsDeleteState = false;
        public void Awake()
        {
            SymbolItemPrefab.SetActive(false);
            SymbolGrid = SymbolList.GetComponentsInChildren<Button>();
            for (int i = 0; i < SymbolGrid.Length; i++)
                SymbolGrid[i].AddClick(mapSymbolGrid_Click, i);
            var stateTypeList = StateType.GetComponentsInChildren<Toggle>();
            for (int i = 0; i < stateTypeList.Length; i++)
                stateTypeList[i].AddChange(StateType_Change,i);

            var stackNumList = StackNum.GetComponentsInChildren<Toggle>();
            for (int i = 0; i < stackNumList.Length; i++)
                stackNumList[i].AddChange(StackNum_Change, i);

            btnCleanAll.AddClick(btnCleanAll_Click);
        }
        private void Start()
        {
            SymbolItem item;
            foreach (var config in MapEditor.I.Config.dicWarSymbol.Values)
            {
                if (config.isMapEdit)
                {
                    item = GameObject.Instantiate<SymbolItem>(SymbolItemPrefab);
                    item.gameObject.transform.SetParent(AllSymbolList.transform, false);
                    item.SetData(config);
                    item.SetActive(true);
                    item.onClick = setSelectSymbolItem;
                }
            }
        }
        public void Refresh()
        {
            if (MapEditor.I.EditMapConfig == null)
                return;
            listConfig = MapEditor.I.EditMapConfig.symbols;
            if (listConfig == null)
            {
                listConfig = new List<MapSymbol>();
                MapEditor.I.EditMapConfig.symbols = listConfig;
            }
            //GameZone.SetTextures(MapEditor.I.GetSceneZone());
            if (MapEditor.I.SceneConfig != null)
            {
                if (MapEditor.I.Config.dicMapColor.TryGetValue(MapEditor.I.SceneConfig.mapScene, out var colorCnf))
                {
                    GameZone.color = GetColor(colorCnf.bgColor);
                    GameZoneArrow.color = GetColor(colorCnf.arrowsColor);
                }
            }           

            foreach (var item in mapSymbolItemList)
            {
                item.SetActive(false);
                symbolItemCache.Enqueue(item);
            }
            mapSymbolItemList.Clear();

            //设置已刷的格子

            for (int i=0;i< listConfig.Count;i++)
            {
                CreateSymbolItem(listConfig[i]);
            }
        }

        Color GetColor(string colorStr)
        {
            Color color = Color.white;
            ColorUtility.TryParseHtmlString("#" + colorStr, out color);
            return color;
        }
        private SymbolItem CreateSymbolItem(MapSymbol config)
        {
            SymbolItem item;
            if (symbolItemCache.Count > 0)
                item = symbolItemCache.Dequeue();
            else
            {
                item = GameObject.Instantiate<SymbolItem>(SymbolItemPrefab);              
                item.onClick = mapSymbolItem_Click;
            }
            mapSymbolItemList.Add(item);
            item.SetActive(true);
            item.SetData(config);
            item.gameObject.transform.SetParent(SymbolGrid[item.Index].transform, false);
            return item;

        }

        private void setSelectSymbolItem(SymbolItem item)
        {
            goSelectSymbol.transform.position = item.PostionCenter;
            SelectSymbolType = item.Config.type;
        }
        //点击元素，修改元素属性或删除元素
        private void mapSymbolItem_Click(SymbolItem item)
        {
            if (IsDeleteState)
            {
                listConfig.Remove(item.MapSymbol);
                item.SetActive(false);
                mapSymbolItemList.Remove(item);
                symbolItemCache.Enqueue(item);
            }
            else
            {
                item.MapSymbol.state = SelectState;
                if (SelectState == 0)
                    item.MapSymbol.stack = 0;
                else
                    item.MapSymbol.stack = SelectStack;
                item.SetData(item.MapSymbol);//重新设置一下并刷新
            }
        }

        void Update()
        {
            IsDeleteState = Input.GetKey(KeyCode.LeftControl);
            if (IsDeleteState)
                txtEditState.text = "点击元素移除";
            else
                txtEditState.text = "刷图状态(按住Ctrl删除)";

            
        }
        //点击空位，增加一个新的元素
        private void mapSymbolGrid_Click(int index)
        {
            if (listConfig == null) return;
            MapSymbol symbol = new MapSymbol();
            symbol.index = index;
            symbol.type = SelectSymbolType;
            symbol.state = SelectState; 
            if (SelectState == 0)
                symbol.stack = 0;
            else
                symbol.stack = SelectStack;
            listConfig.Add(symbol);
            CreateSymbolItem(symbol);
        }

        private void StateType_Change(int index)
        {
            SelectState = index;
        }
        private void StackNum_Change(int index)
        {
            SelectStack = index+1;
        }
        private void btnCleanAll_Click()
        {
            if (listConfig != null)
                listConfig.Clear();
            Refresh();
        }


    }
}
