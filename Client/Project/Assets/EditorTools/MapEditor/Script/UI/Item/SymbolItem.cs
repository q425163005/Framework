using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;
using CSF.Tasks;

namespace MapEditor
{
    public class SymbolItem : MonoBehaviour
    {
        public Image imgIcon;
        public Image imgState;
        public Text txtStack;

        public Action<SymbolItem> onClick;

        public WarSymbolConfig Config { get; private set; }

        public MapSymbol MapSymbol { get; private set; }
        public int State;
        public int StackNum;
        public int Index;

        const int SymbolSize = 148;
        void Awake()
        {
            //在滚动区内Item加点击事件，只能在Button上加
            gameObject.AddClick(self_Click);
            imgState.gameObject.SetVisible(false);
            txtStack.gameObject.SetVisible(false);
        }
        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        /// <summary>当前对象点击事件</summary>
        void self_Click()
        {
            onClick?.Invoke(this);
        }

        public Vector3 PostionCenter => imgState.transform.position;

        public void SetData()
        {
        }

        public void SetData(WarSymbolConfig config)
        {
            Config = config;
            Refresh();
        }

        public void SetData(MapSymbol symbolConfig)
        {
            MapSymbol = symbolConfig;            
            Config = MapEditor.I.Config.dicWarSymbol[symbolConfig.type];
            if (Config.cleanType == -1) //不可消除的没有状态和层数
            {
                symbolConfig.state = 0;
                symbolConfig.stack = 0;
            }
            State = symbolConfig.state;
            StackNum = symbolConfig.stack;
            Index = symbolConfig.index;
            Refresh();
        }

        public void Refresh()
        {
            imgState.gameObject.SetVisible(State != 0);
            txtStack.gameObject.SetVisible(StackNum != 0);
            txtStack.text = StackNum.ToString();
            imgIcon.SetSprite(Config.type.ToString(), "WarUI").Run();
            transform.localPosition = Vector3.zero;
            switch (State)
            {
                case 1:  //冻结
                    imgState.SetSprite("State_Frozen", "WarUI").Run();
                    break;
                case 2: //封印
                    imgState.SetSprite("State_Seal", "WarUI").Run();
                    imgState.SetAlpha(0.5f);
                    break;
                case 3: //炸弹
                    imgState.SetSprite("State_Bomb", "WarUI").Run();
                    break;
            }
            // (transform as RectTransform).anchoredPosition = getAnchoredPosition();
        }

        protected Vector2 getAnchoredPosition()
        {
            int x = Index % 7;
            int y = Index / 7;
            return new Vector2(x * SymbolSize, -y * SymbolSize );
        }
    }
}
