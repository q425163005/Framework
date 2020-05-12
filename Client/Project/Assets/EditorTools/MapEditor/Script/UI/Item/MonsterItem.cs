using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using CSF.Tasks;

namespace MapEditor
{
    public class MonsterItem : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public SkeletonGraphic goModel;
        public Image imgEffect;
        public Image imgShadow;
        public RectTransform FootParent;
        public Image imgHP;
        public Image imgMP;
        public Text txtRound;

        private MonsterConfig Config;
        public MapMonster Data;

        private RectTransform rectTransform;

        Vector2 offSet = Vector2.zero;

        Vector2Int GridSize = new Vector2Int(148, 148); //偏移量限制
        Vector2Int AreaSize;
        private RectTransform dragArea;
        private int Index; //位置索引
        void Awake()
        {
            AreaSize = new Vector2Int(GridSize.x * 7, GridSize.y * 3);
            rectTransform = transform as RectTransform;
        }

        public bool IsHave => gameObject.activeSelf;

        public void SetData(MapMonster monster)
        {
            Data = monster;            
            Index = monster.place;           
            SetPostion();
            MonsterConfig config = null;
            MapEditor.I.Config.dicMonster.TryGetValue(monster.mId, out config);

            if (Config == config) return;           
            Config = config;
            LoadMode().Run();

            //double scale = Config.scale;
            //if (scale == 0)
            //    scale = Config.isBoss ? 1 : 0.5f;
            //goModel.transform.localScale = Vector3.one * (float)scale;

            double scale = Config.scale;
            if (scale == 0)
                scale = Config.type == 0 ? 0.5f : 1f;
            goModel.transform.localScale = Vector3.one * (float)scale;
            imgMP.gameObject.SetActive(config.maxMP > 0);

            //光圈和影子
            imgEffect.SetSprite("effect_" + config.elemType, "WarUI").Run();
            imgEffect.transform.localScale = Vector3.one;
            if (Data.haloSize == 0)
                Data.haloSize = Data.size;
            int width = 148 * Data.haloSize;
            imgEffect.rectTransform.sizeDelta = new Vector2(width, width / 2);
            imgShadow.rectTransform.sizeDelta = new Vector2(width, width / 2);

            switch (Config.type)
            {
                case 0:
                    FootParent.sizeDelta = Vector2.right * 150;
                    imgHP.rectTransform.sizeDelta = new Vector2(0, 15);
                    break;
                case 1:
                    FootParent.sizeDelta = Vector2.right * 300;
                    imgHP.rectTransform.sizeDelta = new Vector2(0, 15);
                    break;
                case 2:
                    FootParent.sizeDelta = Vector2.right * 750;
                    imgHP.rectTransform.sizeDelta = new Vector2(0, 37);
                    break;
            }
            txtRound.text = config.attackInterval.ToString();
        }
        //设置光环值，加-值
        public void SetHaloSize(int val)
        {
            Data.haloSize += val;
            if (Data.haloSize < 2)
                Data.haloSize = 2;
            if (Data.haloSize > Data.size)
                Data.haloSize = Data.size;

            int width = 148 * Data.haloSize;
            imgEffect.rectTransform.sizeDelta = new Vector2(width, width / 2);
            imgShadow.rectTransform.sizeDelta = new Vector2(width, width / 2);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            UIRoot.I.MonsterTips.Show(Config,this);
            UIRoot.I.MonsterTips.RefPos(Data.place, Data.offX, Data.offY);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            UIRoot.I.MonsterTips.Close();
        }



        //重新设置位置
        private void SetPostion()
        {
            offSet.x = Mathf.Clamp(Data.offX, -GridSize.x/2, GridSize.x/2);
            offSet.y = Mathf.Clamp(Data.offY, -GridSize.y/2, GridSize.y/2);

            offSet.x += ((Data.size-1) * GridSize.x) / 2f;
            //父对像有高度,算高时减1 ((int)3/2)，没有不减
            rectTransform.anchoredPosition = new Vector2((Data.place % 7 - 3) * GridSize.x, (Data.place / 7 - 1) * GridSize.y) + offSet;
            dragArea = UIRoot.I.MonsterGrid.DragArea;
        }

        //显示新加入特效
        public async void ShowNewEffect()
        {
            await new WaitForEndOfFrame();
            transform.localScale = Vector3.zero;
            transform.DOScale(1, 0.5f).SetEase(Ease.OutBounce);
        }

        async CTask LoadMode()
        {
            if (Config == null) return;
            goModel.skeletonDataAsset = await MapEditor.I.LoadMonsterModel("Enemy/" + Config.model);
            goModel.Initialize(true);
            string animName = "Idle01"; //goModel.skeletonDataAsset.GetSkeletonData(true).Animations.Items[0].Name;
            goModel.AnimationState.SetAnimation(0, animName, true);
            goModel.rectTransform.sizeDelta = new Vector2(goModel.SkeletonData.Width, goModel.SkeletonData.Height);
        }

     
        //当鼠标按下时调用 接口对应  IPointerDownHandler
        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        //当鼠标拖动时调用   对应接口 IDragHandler
        public void OnDrag(PointerEventData eventData)
        {
            Vector2 mouseDrag = eventData.position;   //当鼠标拖动时的屏幕坐标
            Vector2 uguiPos = new Vector2();   //用来接收转换后的拖动坐标
                                               //和上面类似
            bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(dragArea, mouseDrag, eventData.enterEventCamera, out uguiPos);
            uguiPos.x-= ((Data.size - 1) * GridSize.x) / 2f;
            rectTransform.anchoredPosition = uguiPos;
            int tagIndex = GetItemIndex();
            //设置格子高亮
            UIRoot.I.MonsterGrid.SetGridHighlight(tagIndex, Index,Data.size);
            //重新计算所在格子


            //实时刷新格子位置信息
            Vector2Int off = Vector2Int.zero;
            if (!UIRoot.I.MonsterGrid.IsGridAlign)
                off = GetItemOffset();
            UIRoot.I.MonsterTips.RefPos(tagIndex, off.x, off.y);
        }

        //当鼠标抬起时调用  对应接口  IPointerUpHandler
        public void OnPointerUp(PointerEventData eventData)
        {          
            int tagIndex = GetItemIndex();
            if (tagIndex < 0 || tagIndex > 20)
            {
                //Debug.LogError("超出范围");
                UIRoot.I.MonsterGrid.RemoveMonster(Index);
            }
            else
            {
                if (!UIRoot.I.MonsterGrid.CanMoveGird(tagIndex, Index))
                {
                    //不可移动，还原位置
                    SetPostion();
                }
                else //可以移动,改变位置
                {
                    Data.place = tagIndex;
                    Vector2Int off = Vector2Int.zero;
                    if (!UIRoot.I.MonsterGrid.IsGridAlign)
                        off = GetItemOffset();
                    Data.offX = off.x;
                    Data.offY = off.y;
                    UIRoot.I.MonsterGrid.Refresh();
                }
            }
        }
        //跟据坐标获得格子位置
        private int GetItemIndex()
        {
            int x = (int)rectTransform.anchoredPosition.x;
            int y = (int)rectTransform.anchoredPosition.y;

            int gridX = (x + AreaSize.x / 2) / GridSize.x;
            int gridY = (y + AreaSize.y / 2) / GridSize.y;

            return gridX + gridY * 7;
        }
        //获取位置偏移
        private Vector2Int GetItemOffset()
        {
            int x = (int)rectTransform.anchoredPosition.x;
            int y = (int)rectTransform.anchoredPosition.y;

            int offX = (x + AreaSize.x / 2) % GridSize.x - GridSize.x / 2;
            int offY = (y + AreaSize.y / 2) % GridSize.y - GridSize.y / 2;
            return new Vector2Int(offX, offY);
        }

    }
}
