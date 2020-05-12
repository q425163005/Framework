using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine.UI;

namespace HotFix_Project.Helper
{
    public class ScrollTeam
    {
        //把ScrollRect划分成5份 
        float currScrolRectValue = 0;

        /// <summary>每个队伍占比多少（相对于滑动窗口）</summary>
        float TeamAreaSize;

        /// <summary>左偏移量 偏移量必须小于TeamAreaSize</summary>
        float leftOffset = 0.1f;

        /// <summary>右偏移量</summary>
        float rightOffset = 0.05f;

        /// <summary>动画过度速度</summary>
        float ScrolRectMoveSpeed = 0.2f;

        /// <summary> 当前队伍 </summary>
        int _currTeam = 1;

        public int GetCurrTeam => _currTeam;

        private int _elmentCount;
        private ScrollRect  Scroll;
        private Action<int> ChangeAction;


        public ScrollTeam(ScrollRect scroll, int count, Action<int> changeAction)
        {
            Scroll       = scroll;
            _elmentCount  = count;
            TeamAreaSize = 1.0f / (count - 1);
            ChangeAction = changeAction;
        }

        public void Show()
        {
            DragEventListener.Get(Scroll.gameObject).onEndDrag = (data) => { DragEnd(); };
        }

        public void SetIndex(int index)
        {
            if (index > _elmentCount || index <= 0)
            {
                CLog.Error("跳转区间错误");
                return;
            }

            _currTeam           = index;
            currScrolRectValue = (_currTeam - 1) * TeamAreaSize;
            Scroll.horizontalNormalizedPosition = currScrolRectValue;
        }

        /// <summary>
        /// 停止拖拽
        /// </summary>
        void DragEnd()
        {
            float currValue = Scroll.horizontalNormalizedPosition;
            if (currValue < 0 || currValue > 1)
                return;
            if (currValue > currScrolRectValue + rightOffset)
                SwitchTeam(2);
            else if (currValue < currScrolRectValue - leftOffset)
                SwitchTeam(1);
            else
                SwitchTeam(0);
        }

        /// <summary>
        /// 切换分组 0-不切换 1上一组 2 下一组
        /// </summary>
        void SwitchTeam(int id)
        {
            switch (id)
            {
                case 1:
                    _currTeam--;
                    ChangeAction?.Invoke(_currTeam);
                    break;
                case 2:
                    _currTeam++;
                    ChangeAction?.Invoke(_currTeam);
                    break;
            }

            currScrolRectValue = (_currTeam - 1) * TeamAreaSize;
            Scroll.DOHorizontalNormalizedPos(currScrolRectValue, ScrolRectMoveSpeed);
        }
    }
}